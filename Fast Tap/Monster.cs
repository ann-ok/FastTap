using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fast_Tap
{
    public class Monster : IAction
    {

        public int Health
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

        public int Level
        {
            get => default;
            set
            {
            }
        }

        public int Drop
        {
            get => default;
            set
            {
            }
        }

        public int Speed
        {
            get => default;
            set
            {
            }
        }

        public void Attack()
        {
            throw new NotImplementedException();
        }

        public void Die()
        {
            throw new NotImplementedException();
        }
    }
}