//===============================================================================
// Microsoft patterns & practices
// Parallel Programming Guide
//===============================================================================
// Copyright © Microsoft Corporation.  All rights reserved.
// This code released under the terms of the 
// Microsoft patterns & practices license (http://parallelpatterns.codeplex.com/license).
//===============================================================================

using System;

namespace APD.Util
{
    /// <summary>
    /// Normally distributed aleatorio value generator
    /// </summary>
    public class DesfocagemGaussianaAleatoria
    {
        readonly Random aleatorio = new Random();
        readonly double media;
        readonly double desvioPadrao; 

        #region Constructors
        
        /// <summary>
        /// Creates a new instance of a normally distributed aleatorio value generator
        /// using the specified media and standard deviation.
        /// </summary>
        /// <param name="media">The average value produced by this generator</param>
        /// <param name="desvioPadrao">The amount of variation in the values produced by this generator</param>
        public DesfocagemGaussianaAleatoria(double media, double desvioPadrao)
        {
            aleatorio = new Random();
            this.media = media;
            this.desvioPadrao = desvioPadrao;
        }
        
        /// <summary>
        /// Creates a new instance of a normally distributed aleatorio value generator
        /// using the specified media, standard deviation and seed.
        /// </summary>
        /// <param name="media">The average value produced by this generator</param>
        /// <param name="desvioPadrao">The amount of variation in the values produced by this generator</param>
        /// <param name="seed">A number used to calculate a starting value for the pseudo-aleatorio number
        /// sequence. If a negative number is specified, the absolute value of the number
        /// is used.</param>
        public DesfocagemGaussianaAleatoria(double media, double desvioPadrao, int seed)
        {
            aleatorio = new Random(seed);
            this.media = media;
            this.desvioPadrao = desvioPadrao;
        } 
        #endregion

        #region Public Methods
        
        /// <summary>
        /// Samples the distribution and returns a aleatorio integer
        /// </summary>
        /// <returns>A normally distributed aleatorio number rounded to the nearest integer</returns>
        public int ProximoInteiro()
        {
            return (int)Math.Floor(Proximo() + 0.5);
        }

        /// <summary>
        /// Samples the distribution
        /// </summary>
        /// <returns>A aleatorio sample from a normal distribution</returns>
        public double Proximo()
        {
            double x = 0.0;

            // get the next value in the interval (0, 1) from the underlying uniform distribution
            while (x == 0.0 || x == 1.0)
                x = aleatorio.NextDouble();

            // transform uniform into normal
            return Utilitarios.GaussianaInversa(x, media, desvioPadrao);
        } 
        #endregion
    }
}
