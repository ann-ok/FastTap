using System;
using System.Collections.Generic;
using System.Text;

namespace FastTapLibrary
{
    //размер от 150 до 300 width
    public class Monster : Creature
    {
        private const double Multiplier = 1.07;
        private const int BaseHealth = 200, BaseDamage = 25;
        private List<Uri> monsterImgs = new List<Uri>()
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

        public double AwardMultiplier { get; protected set; } = 1;

        public static double BonusChance = 0.05;
        public static TimeSpan AttackSpeed = new TimeSpan(0, 0, 1);

        private double health;
        private double healthIndicator;

        public Uri Appearance { get; protected set; }

        protected double Damage { get; set; }

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
            get { return (int)healthIndicator; }
            set
            {
                healthIndicator = value;
                Status = healthIndicator < 0 ? Statuses.Inactive : Statuses.Active;
            }
        }

        public Monster(int level = 1)
        {
            Name = "";
            Status = Statuses.Active;
            Health = BaseHealth * Math.Pow(Multiplier, level);
            Damage = BaseDamage * Math.Pow(Multiplier, level);
            Appearance = monsterImgs[(new Random()).Next(0, monsterImgs.Count)];
        }

        public override double Attack() => Damage;
    }
}
