using System.Collections.Generic;
using System.Linq;

namespace FastTapLibrary
{
    public class SkillPanel
    {
        private readonly Dictionary<string ,Skill> skills;

        public Skill SHealth => skills["Health"];

        public Skill SDamage => skills["Damage"];

        public Skill SProtection => skills["Protection"];

        public Skill SPet => skills["Pet"];

        public Skill Skill { get; set; }

        /// <summary>
        /// Initializes a SkillPanel class object with health, damage, defense, and pet skills.
        /// </summary>
        public SkillPanel()
        {
            skills = new Dictionary<string, Skill>
            {
                { "Health", new Skill(170, 1.2) },
                { "Damage", new Skill(20, 1.3) },
                { "Protection", new Skill(0.01, 1.05) },
                { "Pet", new Skill(Pet.BaseDamage, Pet.DamageMultiplier) }
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

        /// <summary>
        /// The method allows you to get the skill by its name.
        /// </summary>
        /// <param name="skillName">The skill name.</param>
        /// <returns></returns>
        public Skill GetValue(string skillName) => skills.Where(x => x.Key == skillName).First().Value;
    }
}
