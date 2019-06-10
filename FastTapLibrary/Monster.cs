using System;
using System.Collections.Generic;
using System.Text;

namespace FastTapLibrary
{
    public class Monster : Creature
    {
        private const double Multiplier = 1.07;
        private const int BaseHealth = 200, BaseDamage = 25;

        protected const double BonusChance = 0.05;

        private double health;
        private double healthIndicator;

        protected Shape appearance;

        protected double Damage { get; set; }

        protected TimeSpan AttackSpeed { get; private set; }

        public override string Name { get; protected set; }

        public override Statuses Status { get; protected set; }

        protected double Health
        {
            get { return health; }
            set
            {
                health = value;
                HealthIndicator = health;
            }
        }

        public double HealthIndicator
        {
            get { return healthIndicator; }
            set
            {
                healthIndicator = value;
                Status = healthIndicator < 0 ? Statuses.Inactive : Statuses.Active;
            }
        }

        public Monster(int level = 1)
        {
            //appearance
            Name = "";
            Status = Statuses.Active;
            Health = BaseHealth * Math.Pow(Multiplier, level);
            Damage = BaseDamage * Math.Pow(Multiplier, level);
            // бьет каждую секунду
            AttackSpeed = new TimeSpan(0, 0, 1); 
        }

        public override double Attack()
        {
            throw new NotImplementedException();
        }
    }
}
