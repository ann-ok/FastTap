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
        /// Field responsible for the initial bid.
        /// </summary>
        private const int Coins = 10;

        private const double Multiplier = 1.1;

        /// <summary>
        /// The method calculates the number of coins by the current multiplier.
        /// </summary>
        /// <param name="coefficient">Multiplier.</param>
        /// <returns>The number of coins in reward</returns>
        public static int GetСoins(double multiplier) => (int)(Coins * multiplier);

        public static int GetСoins(int level, double multiplier = 1) => (int)(Coins * Math.Pow(Multiplier, level) * multiplier);
    }
}
