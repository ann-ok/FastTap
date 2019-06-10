using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FastTapLibrary;

namespace FastTapLibraryTests
{
    [TestClass]
    public class RewardTest
    {
        [TestMethod]
        public void TrueValueOfCoinsInRewardByMultiplierTestMethod()
        {
            int baseCoins = 10;
            double multiplier = 2.1;
            int expected = (int)(baseCoins * multiplier);

            Assert.AreEqual(expected, Reward.GetСoins(multiplier));
        }

        [TestMethod]
        public void TrueValueOfCoinsInRewardByLevelTestMethod()
        {
            int baseCoins = 10;
            double multiplier = 1.1;
            int level = 2;
            int expected = (int)(baseCoins * Math.Pow(multiplier, level));

            Assert.AreEqual(expected, Reward.GetСoins(level));
        }

        [TestMethod]
        public void TrueValueOfCoinsInRewardByLevelAndMultiplierTestMethod()
        {
            int baseCoins = 10;
            double multiplier = 1.1;
            int level = 2;
            double additionalMult = 2;
            int expected = (int)(baseCoins * Math.Pow(multiplier, level) * additionalMult);

            Assert.AreEqual(expected, Reward.GetСoins(level, additionalMult));
        }
    }
}
