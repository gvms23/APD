using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Diagnostics.CodeAnalysis;

namespace APD.PipeLine
{
    public class ImagemControle : IDisposable
    {
        // Image data

        public int NumeroSequencial { get; private set; }
        public string NomeArquivo { get; private set; }
        public Bitmap ImagemOriginal { get; set; }
        public Bitmap ImagemMiniatura { get; set; }
        public Bitmap FilteredImage { get; set; }

        // Image pipeline performance data

        public int ClockOffset { get; private set; }
        [SuppressMessage("Microsoft.Performance", "CA1819:PropertiesShouldNotReturnArrays")]
        public int[] MomentoInicioDaFase { get; private set; }
        [SuppressMessage("Microsoft.Performance", "CA1819:PropertiesShouldNotReturnArrays")]
        public int[] MomentoFimDaFase { get; private set; }

        public int ContagemFila1 { get; set; }
        public int ContagemFila2 { get; set; }
        public int ContagemFila3 { get; set; }

        public int ImageCount { get; set; }
        public double FramesPerSecond { get; set; }

        public ImagemControle(int numeroSequencial, string nomeArquivo, Bitmap imgOriginal, int clockOffset)
        {
            NumeroSequencial = numeroSequencial;
            NomeArquivo = nomeArquivo;
            ImagemOriginal = imgOriginal;
            ClockOffset = clockOffset;

            MomentoInicioDaFase = (int[])Array.CreateInstance(typeof(int), 4);
            MomentoFimDaFase = (int[])Array.CreateInstance(typeof(int), 4);
        }

        #region IDisposable Members

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (ImagemOriginal != null)
                {
                    ImagemOriginal.Dispose();
                    ImagemOriginal = null;
                }
                if (ImagemMiniatura != null)
                {
                    ImagemMiniatura.Dispose();
                    ImagemMiniatura = null;
                }
                if (FilteredImage != null)
                {
                    FilteredImage.Dispose();
                    FilteredImage = null;
                }
            }
        }

        #endregion
    }
}
