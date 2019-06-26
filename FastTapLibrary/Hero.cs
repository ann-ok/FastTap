using System;

namespace FastTapLibrary
{
    public class Hero : StrongCreature
    {
        public int coinsSpent = 0;
        private int maxNumberOfCoins = 0;
        private int numberOfAwardsReceived;
        private int balance;
        private double healthIndicator;
        private string name;

        protected override double CriticalDamage => 1.5 * Skills.SDamage;

        public SkillPanel Skills { get; private set; }

        public int OpportunityToGetAnAward => (Level - 10 * numberOfAwardsReceived) / 10;

        public int Level => Skills.GetSumOfSkillLevels();

        public double Balance
        {
            get { return balance; }
            set
            {
                balance = (int)value;

                if (balance > maxNumberOfCoins)
                    maxNumberOfCoins = balance;
            }
        }
        public double HealthIndicator
        {
            get { return (int)healthIndicator; }
            set
            {
                if (value < 0)
                {
                    Status = Statuses.Inactive;
                    healthIndicator = 0;
                }
                else
                    healthIndicator = value;
            }
        }

        public override string Name
        {
            get => name;
            protected set
            {
                if (value == "")
                    throw new Exception("Имя не может быть пустым");

                name = value;
            }
        }

        public override Statuses Status { get; set; }

        public override string ImagePath { get; set; }

        public SkillPanel SkillPanel
        {
            get => default;
            set
            {
            }
        }

        public Hero(string name, string imagePath)
        {
            Name = name;
            Balance = 0;
            Skills = new SkillPanel();
            ImagePath = imagePath;
            HealthIndicator = Skills.SHealth;
            Status = Statuses.Active;
            numberOfAwardsReceived = 0;
        }

        /// <summary>
        /// The method adds value to balance.
        /// </summary>
        /// <param name="startBonus">Allows you to determine whether the bonus is a starting reward.</param>
        public void GetReward(bool startBonus = false)
        {
            Balance += Reward.GetСoins(multiplier: Level);

            if (!startBonus)
                numberOfAwardsReceived++;
        }

        /// <summary>
        /// The method adds value to balance.
        /// </summary>
        /// <param name="level">Reward level.</param>
        /// <param name="multiplier">Reward multiplier.</param>
        public void GetReward(int level, double multiplier = 1) => Balance += Reward.GetСoins(level, multiplier);

        /// <summary>
        /// The method allows to get information about the hero.
        /// </summary>
        /// <returns>The string containing information about the hero.</returns>
        public override string GetInformation()
        {
            return $"Имя героя: {Name}\n" +
                $"Уровень: {Level}\n" +
                $"Максимальное количество монет: {maxNumberOfCoins}\n" +
                $"Монет потрачено: {coinsSpent}\n";
        }

        /// <summary>
        /// The method allows to find out the hero's damage.
        /// </summary>
        /// <returns>Damage size.</returns>
        public override double Attack() => new Random().NextDouble() <= CriticalChance ? CriticalDamage : Skills.SDamage;

        /// <summary>
        /// The method increases skill.
        /// </summary>
        /// <param name="skillName">The skill name.</param>
        public void LevelUp(string skillName) => LevelUp(Skills.GetValue(skillName));

        /// <summary>
        /// The method increases skill.
        /// </summary>
        /// <param name="skill">The skill.</param>
        public void LevelUp(Skill skill)
        {
            Balance -= skill.Cost;
            Skills.LevelUp(skill);
        }
    }
}
