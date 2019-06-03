using System;
using System.Collections.Generic;
using System.Text;

namespace FastTapLibrary
{
    class Pet : StrongCreature
    {
        protected const int Price = 2000;

        protected TimeSpan AttackSpeed;

        protected double Damage;

        public override string ImagePath { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public override double CriticalDamage { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        protected override string Name { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        protected override Statuses Status { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public Pet(string name)
        {
            Name = name;
            Status = Statuses.Inactive;
        }

        public void ChangeStatus() => Status = Status == Statuses.Inactive ? Statuses.Active : Statuses.Inactive;

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
