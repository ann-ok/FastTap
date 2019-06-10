namespace FastTapLibrary
{
    public abstract class Creature
    {
        /// <summary>
        /// Name property.
        /// </summary>
        public abstract string Name { get; protected set; }

        /// <summary>
        /// Status property.
        /// </summary>
        public abstract Statuses Status { get; protected set; }

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
