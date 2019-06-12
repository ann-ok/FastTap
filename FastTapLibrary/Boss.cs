using System;
using System.Collections.Generic;
using System.Text;

namespace FastTapLibrary
{
    public class Boss : Monster
    {
        private const double СriticalChance = 0.2;
        private List<Uri> bossImgs = new List<Uri>()
        {
            new Uri("image/boss1.png", UriKind.Relative),
            new Uri("image/boss2.png", UriKind.Relative),
            new Uri("image/boss3.png", UriKind.Relative),
            new Uri("image/boss4.png", UriKind.Relative),
            new Uri("image/boss5.png", UriKind.Relative),
            new Uri("image/boss6.png", UriKind.Relative),
        };

        protected double CriticalDamage => 2 * Damage;

        public Boss(int level) : base(level)
        {
            Name = GenerateName(new Random().Next(3, 10));
            Damage *= 1.2;
            Health *= 1.2;
            Appearance = bossImgs[(new Random()).Next(0, bossImgs.Count)];
            AwardMultiplier = 1.5;
        }

        public override double Attack()
        {
            return new Random().NextDouble() <= СriticalChance ? CriticalDamage : Damage;
        }

        private static string GenerateName(int len)
        {
            Random r = new Random();
            string[] consonants = { "b", "c", "d", "f", "g", "h", "j", "k", "l", "m", "l", "n", "p", "q", "r", "s", "sh", "zh", "t", "v", "w", "x" };
            string[] vowels = { "a", "e", "i", "o", "u", "ae", "y" };

            string Name = consonants[r.Next(consonants.Length)].ToUpper() + vowels[r.Next(vowels.Length)];
            int b = 2;

            while (b < len)
            {
                Name += consonants[r.Next(consonants.Length)] + vowels[r.Next(vowels.Length)];
                b += 2;
            }

            return Name;
        }
    }
}
