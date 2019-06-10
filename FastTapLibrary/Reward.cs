using System;
using System.Collections.Generic;
using System.Text;

namespace FastTapLibrary
{
    /// <summary>
    /// Provides the ability to create and pick up a reward.
    /// </summary>
    public static class Reward
    {
        /// <summary>
        /// The field responsible for the initial bid.
        /// </summary>
        private const int Coins = 10;

        /// <summary>
        /// The field is responsible for the initial multiplier.
        /// </summary>
        private const double Multiplier = 1.1;

        /// <summary>
        /// The method calculates the number of coins by the current multiplier.
        /// </summary>
        /// <param name="multiplier">Multiplier.</param>
        /// <returns>The number of coins in reward.</returns>
        public static int GetСoins(double multiplier) => (int)(Coins * multiplier);

        /// <summary>
        /// The method calculates the number of coins by the current level and multiplier.
        /// </summary>
        /// <param name="level">Level.</param>
        /// <param name="multiplier">Multiplier.</param>
        /// <returns>The number of coins in reward.</returns>
        public static int GetСoins(int level, double multiplier = 1) => (int)(Coins * Math.Pow(Multiplier, level) * multiplier);
    }
}
