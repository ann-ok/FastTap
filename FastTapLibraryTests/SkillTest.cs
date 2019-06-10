using System;
using FastTapLibrary;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FastTapLibraryTests
{
    [TestClass]
    public class SkillTest
    {
        [TestMethod]
        public void ImplicitOperatorDoubleTestMethod()
        {
            var skill = new Skill(10, 1);

            Assert.AreEqual(skill, skill.Value);
        }

        [TestMethod]
        public void LevelUpTestMethod()
        {
            var skill = new Skill(10, 1);

            skill.LevelUp();

            Assert.AreEqual(2, skill.Level);
            Assert.AreEqual(10, skill.Value);
        }
    }
}
