using System;
using System.Collections.Generic;
using System.Text;

namespace FastTapLibrary
{
    public class Hero : StrongCreature
    {
        public int coinsSpent = 0;
        private int maxNumberOfCoins = 0;
        public SkillPanel Skills { get; private set; }
        private int opportunityToGetAnAward = 0;

        public int Level
        {
            get
            {
                int level = Skills.GetSumOfSkillLevels();

                if (level % 10 == 0)
                    opportunityToGetAnAward++;

                return level;
            }
        }
        private int balance;
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
        private double healthIndicator;
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

        public override string ImagePath { get; set; }
        protected override double CriticalDamage => 1.5 * Skills.Damage;

        private string name;
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

        public override Statuses Status { get;  protected set; }

        public Hero(string name, string imagePath)
        {
            Name = name;
            Balance = 0;
            Skills = new SkillPanel();
            ImagePath = imagePath;
            HealthIndicator = Skills.Health;
            Status = Statuses.Active;
        }

        public void GetReward(double multiplier)
        {
            Balance += Reward.GetСoins(multiplier);
        }

        public void GetReward(int level, double multiplier = 1)
        {
            Balance += Reward.GetСoins(level, multiplier);
        }

        public override string GetInformation()
        {
            return $"Имя героя: {Name}\n" +
                $"Уровень: {Level}\n" +
                $"Максимальное количество монет: {maxNumberOfCoins}\n" +
                $"Монет потрачено: {coinsSpent}\n";
        }

        public override double Attack()
        {
           return new Random().NextDouble() <= CriticalChance ? CriticalDamage : Skills.Damage;
        }

        public void LevelUp(string skillName)
        {
            Skill skill;

            switch (skillName)
            {
                case "Health": skill = Skills.Health; break;
                case "Damage": skill = Skills.Damage; break;
                case "Protection": skill = Skills.Protection; break;
                case "Pet": skill = Skills.Pet; break;
                default:
                    throw new Exception("Запрос на повышение несуществующего навыка.");
            }

            Balance -= skill.Cost;
            Skills.LevelUp(skill);
        }
    }
}
