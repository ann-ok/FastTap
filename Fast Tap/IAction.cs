using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fast_Tap
{
    public interface IAction
    {
        void Attack();
        void Die();
    }
}