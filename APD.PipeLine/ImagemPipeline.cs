//===============================================================================
// Microsoft patterns & practices
// Parallel Programming Guide
//===============================================================================
// Copyright © Microsoft Corporation.  All rights reserved.
// This code released under the terms of the 
// Microsoft patterns & practices license (http://parallelpatterns.codeplex.com/license).
//===============================================================================

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using APD.Util;

namespace APD.PipeLine
{
    static class ImagemPipeline
    {
        const int CapacidadeLimitadaDaFila = 4;
        const int LoadBalancingDegreeOfConcurrency = 2;
        const int MaxNumberOfImages = 500;
        const double GaussianNoiseAmount = 50.0;

        #region Image Pipeline Top Level Loop

        /// <summary>
        /// Runs the image pipeline example. The program goes through the jpg images located in the SourceDir
        /// directory and performs a series of steps: it resizes each image and adds a black border and then applies
        /// a Gaussian noise filter operation to give the image a grainy effect. Finally, the program invokes 
        /// a user-provided delegate to the image (for example, to display the image on the user interface).
        /// 
        /// Images are processed in sequential order. That is, the display delegate will be invoked in exactly the same
        /// order as the images appear in the file system.
        /// </summary>
        /// <param name="visualizarImagem">A delegate that is invoked for each image at the end of the pipeline, for example, to 
        /// display the image in the user interface.</param>
        /// <param name="tokenCancelamentoThread">A token that can signal an external cancellation request.</param>
        /// <param name="escolhaAlgoritmo">The method of calculation. 0=sequential, 1=pipeline, 2=load balanced pipeline</param>
        /// <param name="delegateErro">A delegate that will be invoked if this method or any of its parallel subtasks observe an exception during their execution.</param>
        [SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")]
        public static void LoopPrincipalPipeline(Action<ImagemControle> visualizarImagem, CancellationToken tokenCancelamentoThread,
            int escolhaAlgoritmo, Action<Exception> delegateErro)
        {
            try
            {
                string diretorioOrigem = Directory.GetCurrentDirectory();

                // Ensure that frames are presented in sequence before invoking the user-provided display function.
                int imagensAteAgora = 0;
                Action<ImagemControle> safeDisplayFn = info =>
                {
                    if (info.NumeroSequencial != imagensAteAgora)
                        throw new InvalidOperationException("Imagens processadas fora de ordem. Visto " +
                            info.NumeroSequencial.ToString() + " , esperado " +
                            imagensAteAgora);

                    visualizarImagem(info);
                    imagensAteAgora += 1;
                   
                };

                // Create a cancellation handle for inter-task signaling of exceptions. This cancellation
                // handle is also triggered by the incoming token that indicates user-requested
                // cancellation.
                using (CancellationTokenSource cts = CancellationTokenSource.CreateLinkedTokenSource(tokenCancelamentoThread))
                {
                    IEnumerable<string> nomeDosArquivos = Utilitarios.RetornarListaNomeDeImagens(diretorioOrigem, MaxNumberOfImages);
                    switch (escolhaAlgoritmo)
                    {
                        case 0:
                            RodarSequencia(nomeDosArquivos, diretorioOrigem, safeDisplayFn, cts);
                            break;
                        case 1:
                            RodarPipeline(nomeDosArquivos, diretorioOrigem, CapacidadeLimitadaDaFila, safeDisplayFn, cts);
                            break;
                        default:
                            throw new InvalidOperationException("Escolha inválida.");
                    }
                }
            }
            catch (AggregateException ae)
            {
                delegateErro((ae.InnerExceptions.Count == 1) ? ae.InnerExceptions[0] : ae);
            }
            catch (Exception e)
            {
                delegateErro(e);
            }
        }

        #endregion

        #region Variations (Sequential and Pipelined)

        /// <summary>
        /// Run the image processing pipeline.
        /// </summary>
        /// <param name="nomeDosArquivos">List of image file names in source directory</param>
        /// <param name="diretorioOrigem">Name of directory of source images</param>
        /// <param name="mostrarAcao">Display action</param>
        /// <param name="cts">Cancellation token</param>
        static void RodarSequencia(IEnumerable<string> nomeDosArquivos, string diretorioOrigem,
                                  Action<ImagemControle> mostrarAcao, CancellationTokenSource cts)
        {
            int contagem = 0;
            int clockOffset = Environment.TickCount;
            int duracao = 0;
            var token = cts.Token;
            ImagemControle info = null;
            try
            {
                foreach (var fileName in nomeDosArquivos)
                {
                    if (token.IsCancellationRequested)
                        break;

                    info = CarregarImagem(fileName, diretorioOrigem, contagem, clockOffset);
                    EscalarImagem(info);
                    FiltrarImagem(info);
                    int displayStart = Environment.TickCount;
                    Visualizar(info, contagem + 1, mostrarAcao, duracao);
                    duracao = Environment.TickCount - displayStart;

                    contagem += 1;
                    info = null;
                }
            }
            finally
            {
                if (info != null) info.Dispose();
            }
        }

        /// <summary>
        /// Run the image processing pipeline.
        /// </summary>
        /// <param name="nomeDosArquivos">List of image file names in source directory</param>
        /// <param name="diretorioOrigem">Name of directory of source images</param>
        /// <param name="tamanhoFila">Length of image fila</param>
        /// <param name="acaoVisualizar">Display action</param>
        /// <param name="cts">Cancellation token</param>
        static void RodarPipeline(IEnumerable<string> nomeDosArquivos, string diretorioOrigem, int tamanhoFila, Action<ImagemControle> acaoVisualizar,
            CancellationTokenSource cts)
        {
            // Data pipes 
            var imagensOriginais = new BlockingCollection<ImagemControle>(tamanhoFila);
            var miniaturaImagens = new BlockingCollection<ImagemControle>(tamanhoFila);
            var imagensFiltradas = new BlockingCollection<ImagemControle>(tamanhoFila);
            try
            {
                var f = new TaskFactory(TaskCreationOptions.LongRunning, TaskContinuationOptions.None);
                Action<ImagemControle> atualizarEstatisticas = info =>
                {
                    info.ContagemFila1 = imagensOriginais.Count();
                    info.ContagemFila2 = miniaturaImagens.Count();
                    info.ContagemFila3 = imagensFiltradas.Count();
                };

                //Iniciando tarefas em pipeline
                var tarefaCarregar = f.StartNew(() =>
                      CarregarImagensEmPipeline(nomeDosArquivos, diretorioOrigem, imagensOriginais, cts));

                var tarefaEscala = f.StartNew(() =>
                      EscalarImagensEmPipeline(imagensOriginais, miniaturaImagens, cts));

                var tarefaFiltro = f.StartNew(() =>
                      FiltrarImagensEmPipeline(miniaturaImagens, imagensFiltradas, cts));

                var tarefaVisualizar = f.StartNew(() =>
                      VisualizarImagensEmPipeline(imagensFiltradas.GetConsumingEnumerable(),
                           acaoVisualizar, atualizarEstatisticas, cts));

                Task.WaitAll(tarefaCarregar, tarefaEscala, tarefaFiltro, tarefaVisualizar);
            }
            finally
            {
                // in case of exception or cancellation, there might be bitmaps
                // that need to be disposed.
                DisposeImagensNaFila(imagensOriginais);
                DisposeImagensNaFila(miniaturaImagens);
                DisposeImagensNaFila(imagensFiltradas);
            }
        }

        #region Fases do Pipeline

        /// <summary>
        /// Fase 1: carregue imagens do disco e coloque-as em uma fila.
        /// </summary>
        static void CarregarImagensEmPipeline(IEnumerable<string> nomeDosArquivos, string diretorioOrigem,
            BlockingCollection<ImagemControle> original, CancellationTokenSource cts)
        {
            int contagem = 0;
            int clockOffset = Environment.TickCount;
            var token = cts.Token;
            ImagemControle info = null;
            try
            {
                foreach (var nome in nomeDosArquivos)
                {
                    if (token.IsCancellationRequested)
                        break;
                    info = CarregarImagem(nome, diretorioOrigem, contagem, clockOffset);
                    original.Add(info, token);
                    contagem += 1;
                    info = null;
                    Thread.Sleep(1000 * 1);
                }
            }
            catch (Exception e)
            {
                // in case of exception, signal shutdown to other pipeline tasks
                cts.Cancel();
                if (!(e is OperationCanceledException))
                    throw;
            }
            finally
            {
                original.CompleteAdding();
                if (info != null) info.Dispose();
            }
        }

        /// <summary>
        /// Fase: Escalar para o tamanho da miniatura e renderizar a moldura.
        /// </summary>
        static void EscalarImagensEmPipeline(
            BlockingCollection<ImagemControle> imagensOriginais,
            BlockingCollection<ImagemControle> miniaturaDeImagens,
            CancellationTokenSource cts)
        {
            var token = cts.Token;
            ImagemControle info = null;
            try
            {
                foreach (var infoTmp in imagensOriginais.GetConsumingEnumerable())
                {
                    info = infoTmp;
                    if (token.IsCancellationRequested)
                        break;
                    EscalarImagem(info);
                    miniaturaDeImagens.Add(info, token);
                    info = null;
                }
            }
            catch (Exception e)
            {
                cts.Cancel();
                if (!(e is OperationCanceledException))
                    throw;
            }
            finally
            {
                miniaturaDeImagens.CompleteAdding();
                if (info != null) info.Dispose();
            }
        }

        /// <summary>
        /// Fase 3: Filtro nas imagens (dê uma aparência salpicada ao adicionar o ruído gaussiano)
        /// </summary>
        static void FiltrarImagensEmPipeline(
            BlockingCollection<ImagemControle> miniaturaDeImagens,
            BlockingCollection<ImagemControle> imagensFiltradas,
            CancellationTokenSource cts)
        {
            ImagemControle info = null;
            try
            {
                var token = cts.Token;
                foreach (ImagemControle infoTmp in
                    miniaturaDeImagens.GetConsumingEnumerable())
                {
                    info = infoTmp;
                    if (token.IsCancellationRequested)
                        break;
                    FiltrarImagem(info);
                    imagensFiltradas.Add(info, token);
                    info = null;
                }
            }
            catch (Exception e)
            {
                cts.Cancel();
                if (!(e is OperationCanceledException))
                    throw;
            }
            finally
            {
                imagensFiltradas.CompleteAdding();
                if (info != null) info.Dispose();
            }
        }

        /// <summary>
        /// Fase 4 do encanamento da imagem: invoque o delegate chamado pelo usuário (por exemplo, para exibir o resultado em uma UI)
        /// </summary>
        static void VisualizarImagensEmPipeline(IEnumerable<ImagemControle> imagensFiltradas,
                                           Action<ImagemControle> acaoVisualizar,
                                           Action<ImagemControle> atualizarEstatisticas,
                                           CancellationTokenSource cts)
        {
            int contagem = 1;
            int duracao = 0;
            var token = cts.Token;
            ImagemControle info = null;
            try
            {
                foreach (ImagemControle infoTmp in imagensFiltradas)
                {
                    info = infoTmp;
                    if (token.IsCancellationRequested)
                        break;
                    int exibicaoInicial = Environment.TickCount;
                    atualizarEstatisticas(info);
                    Visualizar(info, contagem, acaoVisualizar, duracao);
                    duracao = Environment.TickCount - exibicaoInicial;

                    contagem = contagem + 1;
                    info = null;
                }
            }
            catch (Exception e)
            {
                cts.Cancel();
                if (!(e is OperationCanceledException))
                    throw;
            }
            finally
            {
                if (info != null) info.Dispose();
            }
        }

        #endregion

        #region Operations for Individual Images

        [SuppressMessage("Microsoft.Reliability", "CA2000:DisposeObjectsBeforeLosingScope")]
        static ImagemControle CarregarImagem(string nomeArquivo, string diretorioOrigem, int contagem, int clockOffset)
        {
            int momentoInicial = Environment.TickCount;
            ImagemControle info = null;
            Bitmap bitmap = new Bitmap(Path.Combine(diretorioOrigem, nomeArquivo));
            try
            {
                bitmap.Tag = nomeArquivo;

                info = new ImagemControle(contagem, nomeArquivo, bitmap, clockOffset);
                info.MomentoInicioDaFase[0] = momentoInicial - clockOffset;
                bitmap = null;
            }
            finally
            {
                if (bitmap != null) bitmap.Dispose();
            }

            if (info != null) info.MomentoFimDaFase[0] = Environment.TickCount - clockOffset;
            return info;
        }

        static void EscalarImagem(ImagemControle info)
        {
            int momentoInicial = Environment.TickCount;
            var orig = info.ImagemOriginal;
            info.ImagemOriginal = null;
            const int escala = 200;

            var imagemEstaGrande = (orig.Width > orig.Height);
            var novaLargura = imagemEstaGrande ? escala : escala * orig.Width / orig.Height;
            var novaAltura = !imagemEstaGrande ? escala : escala * orig.Height / orig.Width;
            Bitmap bitmap = new Bitmap(orig, novaLargura, novaAltura);
            try
            {
                Bitmap bitmap2 = bitmap.AdicionarBorda(15);
                try
                {
                    bitmap2.Tag = orig.Tag;
                    info.ImagemMiniatura = bitmap2;
                    info.MomentoInicioDaFase[1] = momentoInicial - info.ClockOffset;
                    bitmap2 = null;
                }
                finally
                {
                    if (bitmap2 != null) bitmap2.Dispose();
                }
            }
            finally
            {
                bitmap.Dispose();
                orig.Dispose();
            }
            info.MomentoFimDaFase[1] = Environment.TickCount - info.ClockOffset;
        }

        static void FiltrarImagem(ImagemControle info)
        {
            int momentoInicial = Environment.TickCount;
            var sc = info.ImagemMiniatura;
            info.ImagemMiniatura = null;
            Bitmap bitmap = sc.AdicionarDesfoque(GaussianNoiseAmount);

            try
            {
                bitmap.Tag = sc.Tag;
                info.FilteredImage = bitmap;
                info.MomentoInicioDaFase[2] = momentoInicial - info.ClockOffset;

                bitmap = null;
            }
            finally
            {
                if (bitmap != null) bitmap.Dispose();
                sc.Dispose();
            }
            info.MomentoFimDaFase[2] = Environment.TickCount - info.ClockOffset;
        }

        static void Visualizar(ImagemControle info, int contagem, Action<ImagemControle> acaoVisualizar, int duracao)
        {
            int momentoInicial = Environment.TickCount;
            info.ImageCount = contagem;
            info.MomentoInicioDaFase[3] = momentoInicial - info.ClockOffset;
            info.MomentoFimDaFase[3] = (duracao > 0) ? momentoInicial - info.ClockOffset + duracao :
                                                     Environment.TickCount - info.ClockOffset;
            acaoVisualizar(info);
        }

        #endregion

        #region Cleanup methods used by error handling

        // Ensure that the fila contents is disposed. You could also implement this by 
        // subclassing BlockingCollection<> and providing an IDisposable implmentation.
        static void DisposeImagensNaFila(BlockingCollection<ImagemControle> fila)
        {
            if (fila != null)
            {
                fila.CompleteAdding();
                foreach (var info in fila)
                {
                    info.Dispose();
                }
            }
        }

        #endregion
    }
}
#endregion
