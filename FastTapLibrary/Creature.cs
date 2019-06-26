namespace FastTapLibrary
{
    /// <summary>
    /// The abstract class describing the creature.
    /// </summary>
    public abstract class Creature
    {
        /// <summary>
        /// Name property.
        /// </summary>
        public abstract string Name { get; protected set; }

        /// <summary>
        /// Status property.
        /// </summary>
        public abstract Statuses Status { get; set; }

        /// <summary>
        /// Attack method.
        /// </summary>
        public abstract double Attack();
    }

    /// <summary>
    /// Enumeration of statuses (ative/inactive).
    /// </summary>
    public enum Statuses
    {
        Inactive, 
        Active
    }
}
