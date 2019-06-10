using System;
using System.Collections.Generic;
using System.Text;

namespace FastTapLibrary
{
    public class Hero : StrongCreature
    {
        public int coinsSpent = 0;
        private int maxNumberOfCoins = 0;
        public SkillPanel skills;
        private int opportunityToGetAnAward = 0;

        private int Level
        {
            get
            {
                int level = skills.GetSumOfSkillLevels();

                if (level % 10 == 0)
                    opportunityToGetAnAward++;

                return level;
            }
        }
        private int balance;
        private double Balance
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
        protected double HealthIndicator
        {
            get { return healthIndicator; }
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
        public override double CriticalDamage { get; set; }

        private string name;
        protected override string Name
        {
            get => name;
            set
            {
                if (value == "")
                    throw new Exception("Имя не может быть пустым");

                name = value;
            }
        }

        public override Statuses Status { protected get; set; }

        public Hero()
        {
            Name = "Герой";
            Balance = 0;
            skills = new SkillPanel();
            ImagePath = "";
            HealthIndicator = skills.Health;
            CriticalDamage = 1.05 * skills.Damage;
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
           return (new Random()).NextDouble() <= CriticalChance ? CriticalDamage : skills.Damage;
        }

        public void LevelUp(string skillName)
        {
            Skill skill;

            switch (skillName)
            {
                case "Health": skill = skills.Health; break;
                case "Damage": skill = skills.Damage; break;
                case "Protection": skill = skills.Protection; break;
                case "Pet": skill = skills.Pet; break;
                default:
                    throw new Exception("Запрос на повышение несуществующего навыка.");
            }

            Balance -= skill.Cost;
            skills.LevelUp(skill);
        }
    }
}
