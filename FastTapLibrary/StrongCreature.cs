using System;
using System.Collections.Generic;
using System.Text;

namespace FastTapLibrary
{
    public abstract class StrongCreature : Creature, IInformative
    {

        protected const double CriticalChance = 0.05;

        protected abstract double CriticalDamage { get; set; }

        public abstract string ImagePath { get; set; }

        /// <summary>
        /// The method allows to obtain information about the object.
        /// </summary>
        /// <returns>Returns a string containing information.</returns>
        public abstract string GetInformation();
    }
}
