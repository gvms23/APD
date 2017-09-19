using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;

namespace APD.Util
{
    /// <summary>
    /// Static class that contains timing and numerical utilities
    /// </summary>
    public static class Utilitarios
    {
        #region Timing utilities

        /// <summary>
        /// Format and print elapsed time returned by Stopwatch
        /// </summary>
        public static string ImprimirTempo(TimeSpan ts)
        {
            return TempoFormatado(ts);
        }

        /// <summary>
        /// TimeSpan pretty printer
        /// </summary>
        /// <param name="ts">The TimeSpan to format</param>
        /// <returns>A formatted string</returns>
        public static string TempoFormatado(TimeSpan ts)
        {
            return String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                ts.Hours, ts.Minutes, ts.Seconds,
                ts.Milliseconds / 10);
        }

        #endregion

        #region File utilities


        /// <summary>
        /// Repeatedly loop through all of the files in the source directory. This
        /// enumerable has an infinite number of values.
        /// </summary>
        /// <param name="dirOrigem"></param>
        /// <param name="numMaxImagens"></param>
        /// <returns></returns>
        public static IEnumerable<string> RetornarListaNomeDeImagens(string dirOrigem, int numMaxImagens)
        {
            var nomes = RetornarNomeDeImagens(dirOrigem, numMaxImagens);
            while (true)
            {
                foreach (var nome in nomes)
                    yield return nome;
            }
        }

        /// <summary>
        /// Get names of image files in directory
        /// </summary>
        /// <param name="dirOrigem">Name of directory</param>
        /// <param name="numMaxImagens">Maximum number of image file names to return</param>
        /// <returns>List of image file names in directory (basenames not including directory path)</returns>
        static IEnumerable<string> RetornarNomeDeImagens(string dirOrigem, int numMaxImagens)
        {
            List<string> nomesArquivos = new List<string>();
            var dirInfo = new DirectoryInfo(dirOrigem);

            foreach (var file in dirInfo.GetFiles())
            {
                if (file.Extension.ToUpper(CultureInfo.InvariantCulture) == ".JPG") // LIMITATION - só JPG
                {
                    nomesArquivos.Add(file.Name);
                }
            }
            return nomesArquivos.Take(Math.Min(numMaxImagens, nomesArquivos.Count)).OrderBy(f => f).ToList();
        }

        #endregion

        #region Numerical Routines

        /// <summary>
        /// Return array of floats for indices 0 .. count-1
        /// </summary>
        public static double[] Alcance(int count)
        {
            if (count < 0)
                throw new ArgumentOutOfRangeException("count");

            double[] x = new double[count];
            for (int i = 0; i < count; i++)
            {
                x[i] = i;
            }
            return x;
        }

        /// <summary>
        /// Calculates an approximation of the inverse of the cumulative normal distribution.
        /// </summary>
        /// <param name="distribuicaoCumulativa">The percentile as a fraction (.50 is the fiftieth percentile). 
        /// Must be greater than 0 and less than 1.</param>
        /// <param name="media">The underlying distribution's average (i.e., the value at the 50th percentile) (</param>
        /// <param name="desvioPadrao">The distribution's standard deviation</param>
        /// <returns>The value whose cumulative normal distribution (given media and stddev) is the percentile given as an argument.</returns>
        public static double GaussianaInversa(double distribuicaoCumulativa, double media, double desvioPadrao)
        {
            if (!(0.0 < distribuicaoCumulativa && distribuicaoCumulativa < 1.0))
                throw new ArgumentOutOfRangeException("cumulativeDistribution");

            double result = GaussianaInversa(distribuicaoCumulativa);
            return media + result * desvioPadrao;
        }

        // Adaptation of Peter J. Acklam's Perl implementation. See http://home.online.no/~pjacklam/notes/invnorm/
        // This approximation has a relative error of 1.15 × 10−9 or less. 
        static double GaussianaInversa(double value)
        {
            // Lower and upper breakpoints
            const double plow = 0.02425;
            const double phigh = 1.0 - plow;

            double p = (phigh < value) ? 1.0 - value : value;
            double sign = (phigh < value) ? -1.0 : 1.0;
            double q;

            if (p < plow)
            {
                // Rational approximation for tail
                var c = new double[]{-7.784894002430293e-03, -3.223964580411365e-01,
                                         -2.400758277161838e+00, -2.549732539343734e+00,
                                         4.374664141464968e+00, 2.938163982698783e+00};

                var d = new double[]{7.784695709041462e-03, 3.224671290700398e-01,
                                       2.445134137142996e+00, 3.754408661907416e+00};
                q = Math.Sqrt(-2 * Math.Log(p));
                return sign * (((((c[0] * q + c[1]) * q + c[2]) * q + c[3]) * q + c[4]) * q + c[5]) /
                                                ((((d[0] * q + d[1]) * q + d[2]) * q + d[3]) * q + 1);
            }
            else
            {
                // Rational approximation for central region
                var a = new double[]{-3.969683028665376e+01, 2.209460984245205e+02,
                                         -2.759285104469687e+02, 1.383577518672690e+02,
                                         -3.066479806614716e+01, 2.506628277459239e+00};

                var b = new double[]{-5.447609879822406e+01, 1.615858368580409e+02,
                                         -1.556989798598866e+02, 6.680131188771972e+01,
                                         -1.328068155288572e+01};
                q = p - 0.5;
                var r = q * q;
                return (((((a[0] * r + a[1]) * r + a[2]) * r + a[3]) * r + a[4]) * r + a[5]) * q /
                                         (((((b[0] * r + b[1]) * r + b[2]) * r + b[3]) * r + b[4]) * r + 1);
            }
        }

        #endregion
        
        #region Other Utilities
       
        /// <summary>
        /// Creates a seed that does not depend on the system clock. A unique value will be created with each invocation.
        /// </summary>
        /// <returns>An integer that can be used to seed a aleatorio generator</returns>
        /// <remarks>This method is thread safe.</remarks>
        public static int SementesAleatorias()
        {
            return Guid.NewGuid().ToString().GetHashCode();
        } 
        #endregion
    }
}
