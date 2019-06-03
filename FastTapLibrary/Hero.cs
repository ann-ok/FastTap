using System;
using System.Collections.Generic;
using System.Text;

namespace FastTapLibrary
{
    class Hero : StrongCreature
    {
        public int coinsSpent = 0;
        private int maxNumberOfCoins = 0;
        private SkillPanel skills;

        private int Balance
        {
            get { return Balance; }
            set
            {
                Balance = value;

                if (Balance > maxNumberOfCoins)
                    maxNumberOfCoins = Balance;
            }
        }
        public override string ImagePath { get; set; }
        public override double CriticalDamage { get; set; }
        protected override string Name { get; set; }
        protected override Statuses Status { get; set; }

        public Hero()
        {
            Balance = 0;
            skills = new SkillPanel();
            skills.Add("Health", 100, 1.2);
            skills.Add("Attack", 16, 1.3);
            skills.Add("Protection", 0.05, 1.01);
            ImagePath = "";

        }

        public override void GetInformation()
        {
            throw new NotImplementedException();
        }

        protected override void Attack()
        {
            throw new NotImplementedException();
        }
    }
}
