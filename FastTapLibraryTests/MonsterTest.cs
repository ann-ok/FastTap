using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FastTapLibrary;

namespace FastTapLibraryTests
{
    [TestClass]
    public class MonsterTest
    {
        [TestMethod]
        public void ConstructorTestMethod()
        {
            Monster monster = new Monster();

            Assert.AreEqual(200 * Math.Pow(1.07, 1), monster.HealthIndicator);
        }
    }
}
