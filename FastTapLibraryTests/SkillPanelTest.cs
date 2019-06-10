using System;
using System.Collections.Generic;
using FastTapLibrary;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FastTapLibraryTests
{
    [TestClass]
    public class SkillPanelTest
    {
        [TestMethod]
        public void SkillLevelIncreaseTestMethod()
        {
            var skillPanelSum3 = new SkillPanel();
            var skillPanelSum4 = new SkillPanel();

            skillPanelSum4.LevelUp(skillPanelSum4.Damage);

            Assert.AreEqual(3, skillPanelSum3.GetSumOfSkillLevels());
            Assert.AreEqual(4, skillPanelSum4.GetSumOfSkillLevels());
        }

        [TestMethod]
        public void GetSumOfSkillLevelsTestMethod()
        {
            var skillPanel = new SkillPanel();
            var li = new List<int>();
            Assert.AreEqual(3, skillPanel.GetSumOfSkillLevels());
        }

    }
}
