using System;
using FastTapLibrary;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FastTapLibraryTests
{
    [TestClass]
    public class HeroTest
    {
        [TestMethod]
        public void AttackTestMethod()
        {
            Hero hero = new Hero();

            Assert.AreEqual(hero.skills.Damage, hero.Attack());
        }
    }
}
