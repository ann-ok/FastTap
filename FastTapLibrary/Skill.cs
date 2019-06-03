using System;
using System.Collections.Generic;
using System.Text;

namespace FastTapLibrary
{
    class Skill
    {
        private const double CostMultiplier = 1.1, BaseCost = 50;
        private double valueMultiplier;

        public int level;
        private double Value
        {
            get { return Value; }
            set
            {
                Value = value;
                Inc = Math.Floor(value * (valueMultiplier - 1));
            }
        }
        private double Inc
        {
            get { return (int)Inc; }
            set { Inc = value; }
        }
        private double cost;

        public Skill(double value, double valueMultiplier)
        {
            level = 1;
            Value = value;
            this.valueMultiplier = valueMultiplier;
            cost = BaseCost;
        }

        public void LevelUp()
        {
            level++;
            Value *= valueMultiplier;
            cost *= CostMultiplier;
        }
    }
}
