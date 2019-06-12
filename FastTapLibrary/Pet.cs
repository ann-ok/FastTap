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
        protected override double CriticalDamage { get => throw new NotImplementedException(); }
        public override string Name { get => throw new NotImplementedException(); protected set => throw new NotImplementedException(); }
        public override Statuses Status { get => throw new NotImplementedException(); protected set => throw new NotImplementedException(); }

        public Pet(string name)
        {
            Name = name;
            Status = Statuses.Inactive;
        }

        public void ChangeStatus() => Status = Status == Statuses.Inactive ? Statuses.Active : Statuses.Inactive;

        public override string GetInformation()
        {
            throw new NotImplementedException();
        }

        public override double Attack()
        {
            throw new NotImplementedException();
        }
    }
}
