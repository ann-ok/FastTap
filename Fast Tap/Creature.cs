using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fast_Tap
{
    public abstract class Creature : IAction
    {
        public int Name
        {
            get => default;
            set
            {
            }
        }

        public int Damage
        {
            get => default;
            set
            {
            }
        }

        public abstract void Attack();
        public abstract void Die();
    }
}