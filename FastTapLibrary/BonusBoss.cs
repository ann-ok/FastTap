using System;

namespace FastTapLibrary
{
    /// <summary>
    /// The class describing the bonus boss.
    /// </summary>
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
