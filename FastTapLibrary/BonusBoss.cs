using System;
using System.Collections.Generic;
using System.Text;

namespace FastTapLibrary
{
    public class BonusBoss : Boss
    {
        public BonusBoss(int level): base(level)
        {
            Damage *= 1.2;
            Health *= 1.2;
            Appearance = new Uri("image/bonus.png", UriKind.Relative);
            AwardMultiplier = 3;
        }
    }
}
