using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace APD.PipeLine
{
    public partial class FrmPrincipalPipeLine : Form
    {
        enum ModoAlgoritmo { Sequencia, Pipeline }
        enum Estado { Parado, Rodando, Parando }

        delegate void CallbackUsuario(object o);
        readonly CallbackUsuario delegateAtualizarBitmap;
        readonly CallbackUsuario cancelarDelegateFinalizado;
        readonly CallbackUsuario mostrarErroDelegate;

        Task taskPrincipal = null;
        CancellationTokenSource cts = null;
        ModoAlgoritmo modoImg = ModoAlgoritmo.Pipeline;
        Estado estado = Estado.Parado;
        readonly Stopwatch sw = new Stopwatch();

        int imagensAteAgora = 0;
        readonly int[] tempoTotal = { 0, 0, 0, 0, 0, 0, 0, 0 };

        public FrmPrincipalPipeLine()
        {
            InitializeComponent();
            cancelarDelegateFinalizado = new CallbackUsuario(o =>
            {
                this.cts = null;
                this.taskPrincipal = null;
                this.estado = Estado.Parado;
                AtualizarStatusEnabled();
            });

            delegateAtualizarBitmap = new CallbackUsuario(DefinirImagem);

            mostrarErroDelegate = new CallbackUsuario(obj =>
            {
                Exception e = (Exception)obj;
                MessageBox.Show(e.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                this.cts = null;
                this.taskPrincipal = null;
                this.estado = Estado.Parado;
                AtualizarStatusEnabled();
            });

            AtualizarStatusEnabled();
        }

        private void AtualizarStatusEnabled()
        {
            radSequencial.Enabled = (estado == Estado.Parado);
            radPipeline.Enabled = (estado == Estado.Parado);
            btnIniciar.Enabled = (estado == Estado.Parado);
            btnParar.Enabled = (estado == Estado.Rodando);
            btnSair.Enabled = (estado == Estado.Parado);
            radSequencial.Checked = (modoImg == ModoAlgoritmo.Sequencia);
            radPipeline.Checked = (modoImg == ModoAlgoritmo.Pipeline);
        }

        private void DefinirImagem(object info)
        {
            var imagemPrioritaria = this.pcbImg1.Image;
            var imageInfo = (ImagemControle)info;
            this.pcbImg1.Image = imageInfo.FilteredImage;
            imageInfo.FilteredImage = null;
            this.imagensAteAgora += 1;

            //cálculo da duração de cada fase
            for (int i = 0; i < 4; i++)
            {
                this.tempoTotal[i] += imageInfo.MomentoFimDaFase[i] - imageInfo.MomentoInicioDaFase[i];
            }
            
            this.tempoTotal[4] += imageInfo.MomentoInicioDaFase[1] - imageInfo.MomentoFimDaFase[0];
            this.tempoTotal[5] += imageInfo.MomentoInicioDaFase[2] - imageInfo.MomentoFimDaFase[1];
            this.tempoTotal[6] += imageInfo.MomentoInicioDaFase[3] - imageInfo.MomentoFimDaFase[2];

            this.txt1CarregadasTempoCrescimento.Text = (this.tempoTotal[0] / this.imagensAteAgora).ToString();
            this.txt2EscalaAlteradaTempoCrescimento.Text = (this.tempoTotal[1] / this.imagensAteAgora).ToString();
            this.txt3FiltroTonsCinzaTempoCrescimento.Text = (this.tempoTotal[2] / this.imagensAteAgora).ToString();
            this.txt4VisualizadasTempoCrescimento.Text = (this.tempoTotal[3] / this.imagensAteAgora).ToString();

            this.txtFila1TempoEspera.Text = (this.tempoTotal[4] / this.imagensAteAgora).ToString();
            this.txtFila2TempoEspera.Text = (this.tempoTotal[5] / this.imagensAteAgora).ToString();
            this.txtFila3TempoEspera.Text = (this.tempoTotal[6] / this.imagensAteAgora).ToString();

            this.txtFila1Contagem.Text = imageInfo.ContagemFila1.ToString();
            this.txtFila2Contagem.Text = imageInfo.ContagemFila2.ToString();
            this.txtFila3Contagem.Text = imageInfo.ContagemFila3.ToString();

            this.txtNomeArquivo.Text = imageInfo.NomeArquivo;
            this.txtContadorImagens.Text = imageInfo.ImageCount.ToString();

            long tempoGasto = this.sw.ElapsedMilliseconds;
            this.txtTempoPorImagem.Text =
                string.Format("{0: 0}", tempoGasto / imageInfo.ImageCount);

            if (imagemPrioritaria != null) imagemPrioritaria.Dispose();

            if (imageInfo.NumeroSequencial != imagensAteAgora - 1)
            {
                var msg = string.Format("Erro: Imagens fora de ordem. Esperado. Esperado: {0} \nRecebido: {1}",
                     imagensAteAgora - 1, imageInfo.NumeroSequencial);
                MessageBox.Show(msg, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                Application.Exit();
            }
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnIniciar_Click(object sender, EventArgs e)
        {
            if (taskPrincipal == null)
            {
                estado = Estado.Rodando;
                AtualizarStatusEnabled();
                Action<ImagemControle> updateFn = (ImagemControle bm) => this.Invoke(this.delegateAtualizarBitmap, (object)bm);
                Action<Exception> errorFn = (Exception ex) => this.Invoke(this.mostrarErroDelegate, (object)ex);
                cts = new CancellationTokenSource();
                int enumVal = (int)modoImg;
                this.sw.Restart();
                imagensAteAgora = 0;
                for (int i = 0; i < tempoTotal.Length; i++)
                {
                    tempoTotal[i] = 0;
                }
                taskPrincipal = Task.Factory.StartNew(() => ImagemPipeline.LoopPrincipalPipeline(updateFn, cts.Token, enumVal, errorFn),
                    cts.Token,
                    TaskCreationOptions.LongRunning,
                    TaskScheduler.Default);
            }
        }

        private void radSequencia_CheckedChanged(object sender, EventArgs e)
        {
            if (radSequencial.Checked)
                modoImg = ModoAlgoritmo.Sequencia;
        }

        private void radPipeline_CheckedChanged(object sender, EventArgs e)
        {
            if (radPipeline.Checked)
                modoImg = ModoAlgoritmo.Pipeline;
        }

        private void btnParar_Click(object sender, EventArgs e)
        {
            if (cts != null)
            {
                estado = Estado.Parando;
                AtualizarStatusEnabled();
                cts.Cancel();

                Task.Factory.StartNew(() =>
                {
                    taskPrincipal.Wait();
                    this.Invoke(cancelarDelegateFinalizado, this);
                });
            }
        }

        private void FrmPrincipalPipeLine_Load(object sender, EventArgs e)
        {

        }

        private void FrmPrincipalPipeLine_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();     
        }

        private void FrmPrincipalPipeLine_FormClosed(object sender, FormClosedEventArgs e)
        {

        }

        private void lnkGabrielVicente_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://gabrielvicente.ch");
        }
    }
}
