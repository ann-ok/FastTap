using System;

namespace FastTapLibrary
{
    public class Pet : StrongCreature
    {
        public static int Price = 500;
        public static TimeSpan AttackSpeed = TimeSpan.FromSeconds(3);
        public static double DamageMultiplier = 1.3;
        public static double BaseDamage = 50;

        public double Damage { private get; set; }

        public override string ImagePath { get; set; }

        protected override double CriticalDamage => 1.1 * Damage;

        public override string Name { get; protected set; }

        public override Statuses Status { get; set; } = Statuses.Active;

        public Pet(string name)
        {
            Name = name;
            Damage = BaseDamage;
        }

        /// <summary>
        /// The method allows to get information about the pet.
        /// </summary>
        /// <returns>The string containing information about the pet.</returns>
        public override string GetInformation() => $"Имя питомца: {Name}\n" +
                $"Cкорость атаки: раз в {AttackSpeed.Seconds.ToString()} секунды\n";

        /// <summary>
        /// The method allows to find out the pet's damage.
        /// </summary>
        /// <returns>Damage size.</returns>
        public override double Attack() => new Random().NextDouble() <= CriticalChance ? CriticalDamage : Damage;
    }
}
