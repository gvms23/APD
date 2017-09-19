using System;
using System.Drawing;
using APD.Util;

namespace APD.PipeLine
{
    /// <summary>
    /// Extension methods for System.Drawing.Bitmap
    /// </summary>
    public static class ExtensoesBitmap
    {
        /// <summary>
        /// Creates an image with a border from this image.
        /// </summary>
        /// <param name="origem">Color image (Bitmap)</param>
        /// <param name="larguraBorda">Width of border</param>
        /// <returns>Image with border</returns>
        /// <remarks>
        /// This code uses Bitmap.GetPixel and SetPixel methods for clarity. An implementation using Bitmap.LockBits
        /// and then directly modifying the image data may be faster, espectially for large images.
        /// </remarks>
        public static Bitmap AdicionarBorda(this Bitmap origem, int larguraBorda)
        {
            if (origem == null)
                throw new ArgumentNullException("origem vazia");
            Bitmap bitmap = null;
            Bitmap tempBitmap = null;
            try
            {
                int largura = origem.Width;
                int altura = origem.Height;
                tempBitmap = new Bitmap(largura, altura);
                for (int y = 0; y < altura; y++)
                {
                    bool yFlag = (y < larguraBorda || (altura - y) < larguraBorda);
                    for (int x = 0; x < largura; x++)
                    {
                        bool xFlag = (x < larguraBorda || (largura - x) < larguraBorda);
                        if (xFlag || yFlag)
                        {
                            var distance = Math.Min(y, Math.Min(altura - y, Math.Min(x, largura - x)));
                            var percent = distance / (double)larguraBorda;
                            var percent2 = percent * percent;
                            var pixel = origem.GetPixel(x, y);
                            var color = Color.FromArgb((int)(pixel.R * percent2), (int)(pixel.G * percent2), (int)(pixel.B * percent2));
                            tempBitmap.SetPixel(x, y, color);
                        }
                        else
                        {
                            tempBitmap.SetPixel(x, y, origem.GetPixel(x, y));
                        }
                    }
                }
                bitmap = tempBitmap;
                tempBitmap = null;
            }
            finally
            {
                if (tempBitmap != null) tempBitmap.Dispose();
            }
            return bitmap;
        }

        /// <summary>
        /// Inserts Gaussian noise into a bitmap.
        /// </summary>
        /// <param name="origem">Bitmap to be processed</param>
        /// <param name="montante">Standard deviation of perturbation for each color channel.</param>
        /// <returns>New, speckled bitmap</returns>
        /// <remarks>
        /// This code uses Bitmap.GetPixel and SetPixel methods for clarity. An implementation using Bitmap.LockBits
        /// and then directly modifying the image data may be faster, espectially for large images.
        /// </remarks>
        public static Bitmap AdicionarDesfoque(this Bitmap origem, double montante)
        {
            if (origem == null)
                throw new ArgumentNullException("source");
            Bitmap bitmap = null;
            Bitmap tempBitmap = null;
            try
            {
                var gerar = new DesfocagemGaussianaAleatoria(0.0, montante, Utilitarios.SementesAleatorias());
                tempBitmap = new Bitmap(origem.Width, origem.Height);
                for (int y = 0; y < tempBitmap.Height; y++)
                {
                    for (int x = 0; x < tempBitmap.Width; x++)
                    {
                        var pixel = origem.GetPixel(x, y);
                        Color newPixel = AdicionarPixelsDesfoque(pixel, gerar);
                        tempBitmap.SetPixel(x, y, newPixel);
                    }
                }
                bitmap = tempBitmap;
                tempBitmap = null;
            }
            finally
            {
                if (tempBitmap != null) tempBitmap.Dispose();
            }
            return bitmap;
        }

        static Color AdicionarPixelsDesfoque(Color pixel, DesfocagemGaussianaAleatoria generator)
        {
            int newR = (int)pixel.R + generator.ProximoInteiro();
            int newG = (int)pixel.G + generator.ProximoInteiro();
            int newB = (int)pixel.B + generator.ProximoInteiro();
            int r = Math.Max(0, Math.Min(newR, 255));
            int g = Math.Max(0, Math.Min(newG, 255));
            int b = Math.Max(0, Math.Min(newB, 255));
            return Color.FromArgb(r, g, b);
        }
    }
}

