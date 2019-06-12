using System;
using System.Collections.Generic;
using System.Text;

namespace FastTapLibrary
{
    public class Game : IInformative
    {
        private int maxStage;
        private readonly DateTime startDateTime;
        public bool nextStage = false, prevStage = true;

        private int currentStage;

        public int CurrentStage
        {
            get { return currentStage; }
            set
            {
                if (value < currentStage)
                {
                    prevStage = false;
                    nextStage = true;
                }
                else
                {
                    prevStage = true;
                    nextStage = false;
                }

                if (value < 1)
                    currentStage = 1;
                else
                    currentStage = value;

                if (currentStage > maxStage)
                    maxStage = currentStage;
            }
        }

        public Hero GHero { get; private set; }

        public Monster GMonster { get; private set; }

        public Game(Hero hero)
        {
            currentStage = 1;
            startDateTime = DateTime.Now;

            GHero = hero;
            GMonster = CreateMonster();
        }

        public string GetInformation()
        {
            return $"Текущий этап: {currentStage}\n" +
                $"Максимальный этап: {maxStage}\n" +
                $"Время в игре: {DateTime.Now - startDateTime}\n";
        }

        public void UpdateStage()
        {
            GHero.HealthIndicator = GHero.Skills.Health;
            GHero.Status = Statuses.Active;
            GMonster = CreateMonster();
            GMonster.Status = Statuses.Active;
        }

        private Monster CreateMonster()
        {
            if (CurrentStage % 10 == 0)
                return new Boss(CurrentStage);
            else if (new Random().NextDouble() <= Monster.BonusChance)
                return new BonusBoss(CurrentStage);
            else
                return new Monster(CurrentStage);
        }

        public void MonsterAttack()
        {
            GHero.HealthIndicator -= GMonster.Attack()*(1 - GHero.Skills.Protection);

            if (GHero.Status == Statuses.Inactive)
            {
                if (prevStage == true)
                    CurrentStage--;

                UpdateStage();
            }
        }

        public void HeroAttack()
        {
            GMonster.HealthIndicator -= GHero.Attack();

            if (GMonster.Status == Statuses.Inactive)
            {
                GHero.GetReward(CurrentStage, GMonster.AwardMultiplier);

                if (prevStage != false)
                    CurrentStage++;

                UpdateStage();
            }
        }
    }
}
