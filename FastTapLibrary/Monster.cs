using System;
using System.Collections.Generic;
using System.Text;

namespace FastTapLibrary
{
    public class Monster : Creature
    {
        const double Multiplier = 1.07;
        const int BaseHealth = 200, BaseDamage = 25;
        protected const double BonusChance = 0.05;

        protected Shape appearance;

        protected override string Name { get; set; }

        protected override Statuses Status { get; set; }

        protected double Health
        {
            get { return Health; }
            set
            {
                Health = value;
                HealthIndicator = Health;
            }
        }

        //нужен тест
        protected double HealthIndicator
        {
            get { return HealthIndicator; }
            set
            {
                if (value < 0)
                {
                    Status = Statuses.Inactive;
                    HealthIndicator = 0;
                }
                else
                    HealthIndicator = value;
            }
        }

        protected double Damage { get; set; }

        protected TimeSpan AttackSpeed { get; private set; }

        public Monster(int level = 1)
        {
            //appearance
            Name = "";
            Status = Statuses.Active;
            Health = (int)(BaseHealth * Math.Pow(Multiplier, level));
            Damage = (int)(BaseDamage * Math.Pow(Multiplier, level));
            AttackSpeed = new TimeSpan(0, 0, 1); // бьет каждую секунду
        }

        protected override void Attack()
        {
            throw new NotImplementedException();
        }
    }
}
