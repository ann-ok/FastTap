using System;

namespace FastTapLibrary
{
    public abstract class Creature
    {
        /// <summary>
        /// Name property.
        /// </summary>
        protected abstract string Name { get; set; }

        /// <summary>
        /// Status property.
        /// </summary>
        protected abstract Statuses Status { get; set; }

        /// <summary>
        /// Damage to the object.
        /// </summary>
        protected abstract void Attack();
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
