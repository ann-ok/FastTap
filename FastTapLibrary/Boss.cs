using System;
using System.Collections.Generic;
using System.Text;

namespace FastTapLibrary
{
    public class Boss : Monster
    {
        const int AwardMultiplier = 2;
        const double criticalChance = 0.2;

        double criticalDamage;

        public Boss(int level) : base(level)
        {
            Name = "Не пусто";
            Damage *= 1.2;
            Health *= 1.2;
            criticalDamage = 1.1 * Damage;
        }

        public override double Attack()
        {
            return 0.0;
        }
    }
}
