using System;
using System.Collections.Generic;

namespace FastTapLibrary
{
    public class Monster : Creature
    {
        private const double Multiplier = 1.07;
        private const int BaseHealth = 200, BaseDamage = 23;

        public static double BonusChance = 0.01;
        public static TimeSpan AttackSpeed = TimeSpan.FromSeconds(0.5);

        private readonly List<Uri> monsterImgs = new List<Uri>()
        {
            new Uri("image/monster1.png", UriKind.Relative),
            new Uri("image/monster2.png", UriKind.Relative),
            new Uri("image/monster3.png", UriKind.Relative),
            new Uri("image/monster4.png", UriKind.Relative),
            new Uri("image/monster5.png", UriKind.Relative),
            new Uri("image/monster6.png", UriKind.Relative),
            new Uri("image/monster7.png", UriKind.Relative),
            new Uri("image/monster8.png", UriKind.Relative)
        };

        private double health;
        private double healthIndicator;

        public double AwardMultiplier { get; protected set; } = 1;

        public Uri Appearance { get; protected set; }

        public override string Name { get; protected set; }

        public override Statuses Status { get; set; }

        public double Health
        {
            get => (int)health;
            protected set
            {
                health = value;
                HealthIndicator = health;
            }
        }

        public double HealthIndicator
        {
            get => (int)healthIndicator;
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

        protected double Damage { get; set; }

        public Monster(int level = 1)
        {
            Name = "";
            Status = Statuses.Active;
            Health = BaseHealth * Math.Pow(Multiplier, level);
            Damage = BaseDamage * Math.Pow(Multiplier, level);
            Appearance = monsterImgs[(new Random()).Next(0, monsterImgs.Count)];
        }

        /// <summary>
        /// The method allows to find out the monster's damage.
        /// </summary>
        /// <returns>Damage size.</returns>
        public override double Attack() => Damage;
    }
}
