using System;
using System.Collections.Generic;
using System.Text;

namespace FastTapLibrary
{
    class SkillPanel
    {
        private Dictionary<string ,Skill> skills;

        public SkillPanel()
        {
            skills = new Dictionary<string, Skill>();
        }

        public void Add(string title, double value, double multiplier)
        {
            try
            {
                skills.Add(title, new Skill(value, multiplier));
            }
            catch
            {
                throw new Exception("Ошибка при добавлении навыка.");
            }
        }

        //переписать с linq
        public int GetSumOfSkillLevels()
        {
            int sum = 0;
            foreach (var skill in skills)
                sum += skill.Value.level;
            return sum;
        }
    }
}
