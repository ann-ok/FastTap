using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FastTapLibrary
{
    public class SkillPanel
    {
        private readonly Dictionary<string ,Skill> skills;

        public Skill Health
        {
            get { return skills["Health"]; }
        }

        public Skill Damage
        {
            get { return skills["Damage"]; }
        }

        public Skill Protection
        {
            get { return skills["Protection"]; }
        }

        public Skill Pet
        {
            get { return skills["Pet"]; }
        }

        /// <summary>
        /// Initializes a SkillPanel class object with health, damage, defense, and pet skills.
        /// </summary>
        public SkillPanel()
        {
            skills = new Dictionary<string, Skill>
            {
                { "Health", new Skill(100, 1.2) },
                { "Damage", new Skill(16, 1.3) },
                { "Protection", new Skill(0.1, 1.1) },
                { "Pet", new Skill(50, 1.1) }
            };
        }

        /// <summary>
        /// The method returns the sum of all skill levels.
        /// </summary>
        /// <returns>The sum of skill levels.</returns>
        public int GetSumOfSkillLevels() => skills.Sum(v => v.Value.Level);

        /// <summary>
        /// The method allows to increase the level of skill.
        /// </summary>
        /// <param name="skill">Skill whose level you need to increase.</param>
        public void LevelUp(Skill skill) => skill.LevelUp();
    }
}
