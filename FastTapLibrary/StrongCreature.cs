namespace FastTapLibrary
{
    /// <summary>
    /// The abstract class describing the StrongCreature.
    /// </summary>
    public abstract class StrongCreature : Creature, IInformative
    {

        protected const double CriticalChance = 0.05;

        protected abstract double CriticalDamage { get; }

        public abstract string ImagePath { get; set; }

        /// <summary>
        /// The method allows to obtain information about the object.
        /// </summary>
        /// <returns>Returns a string containing information.</returns>
        public abstract string GetInformation();
    }
}
