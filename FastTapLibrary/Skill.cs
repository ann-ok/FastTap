using System;

namespace FastTapLibrary
{
    /// <summary>
    /// Determines the skill that has the level, value and price.
    /// </summary>
    public class Skill
    {
        private const double CostMultiplier = 1.1, BaseCost = 50;

        private double ValueMultiplier { get; }

        public int Level { get; private set; }

        public double Value { get; private set; }

        public double Cost { get; private set; }

        public double GetInc() => Math.Round(Value * (ValueMultiplier - 1), 3);

        /// <summary>
        /// Initializes a new instance of the class Skill with 1 level, the base price, the specified value and the multiplier.
        /// </summary>
        /// <param name="value">Initial value.</param>
        /// <param name="valueMultiplier">Multiplier.</param>
        public Skill(double value, double valueMultiplier)
        {
            Level = 1;
            Value = value;
            ValueMultiplier = valueMultiplier;
            Cost = BaseCost;
        }

        /// <summary>
        /// The method increases skill level, value and cost.
        /// </summary>
        public void LevelUp()
        {
            Level++;
            Value *= ValueMultiplier;
            Cost *= CostMultiplier;
        }

        public static implicit operator int(Skill skill) => (int)skill.Value;
    }
}
