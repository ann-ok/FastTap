using System;
using System.Collections.Generic;
using System.Text;

namespace FastTapLibrary
{
    public abstract class StrongCreature : Creature, IInformative
    {

        public const double CriticalChance = 0.05;

        public abstract string ImagePath { get; set; }

        public abstract double CriticalDamage { get; set; }

        public abstract void GetInformation();
    }
}
