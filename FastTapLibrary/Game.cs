using System;

namespace FastTapLibrary
{
    [Serializable]
    public class Game : IInformative
    {
        private readonly DateTime startDateTime;
        
        public bool nextStage, prevStage;
        private int maxStage;
        private int currentStage;

        public Hero GHero { get; private set; }

        public Monster GMonster { get; private set; }

        public Pet GPet { get; private set; }

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

        public Hero Hero { get; set; }

        public Pet Pet { get; set; }

        public Monster Monster { get; set; }

        public Game(Hero hero, Pet pet = null)
        {
            startDateTime = DateTime.Now;

            GHero = hero;
            GPet = pet;

            GoToTheFirstStage();
        }

        /// <summary>
        /// The method allows to get information about the game.
        /// </summary>
        /// <returns>The string containing information about the game.</returns>
        public string GetInformation()
        {
            string infoPet = GPet == null ? "" : "\tИнформация о питомце\n" + GPet.GetInformation();
            DateTime gameTime = new DateTime(2019,06,27,(DateTime.Now - startDateTime).Hours, (DateTime.Now - startDateTime).Minutes, (DateTime.Now - startDateTime).Seconds);

            return $"\tИнформация об игре\n" +
                $"Текущий этап: {currentStage}\n" +
                $"Максимальный этап: {maxStage}\n" +
                $"Время в игре: {gameTime.ToLongTimeString()}\n" +
                $"\tИнформация о герое\n{GHero.GetInformation()}\n" +
                $"{infoPet}\n";
        }

        /// <summary>
        /// The method updates the game parameters depending on the current stage.
        /// </summary>
        public void UpdateStage()
        {
            if (GHero.Status == Statuses.Inactive)
            {
                if (prevStage == true && CurrentStage % 10 != 1)
                    CurrentStage--;

                GHero.Status = Statuses.Active;
            }

            if (GMonster.Status == Statuses.Inactive)
            {
                GHero.GetReward(CurrentStage, GMonster.AwardMultiplier);

                if (prevStage != false)
                    CurrentStage++;

                GMonster.Status = Statuses.Active;
            }

            GHero.HealthIndicator = GHero.Skills.SHealth;
            GMonster = CreateMonster();
        }

        /// <summary>
        /// The method changes the current stage to the first.
        /// </summary>
        public void GoToTheFirstStage()
        {
            currentStage = 1;

            nextStage = false;
            prevStage = true;

            GHero.HealthIndicator = GHero.Skills.SHealth;
            GMonster = CreateMonster();
        }

        /// <summary>
        /// The method creates a Monster class object.
        /// </summary>
        /// <returns>Monster class object.</returns>
        private Monster CreateMonster()
        {
            if (CurrentStage % 10 == 0)
                return new Boss(CurrentStage);
            else if (new Random().NextDouble() <= Monster.BonusChance)
                return new BonusBoss(CurrentStage);
            else
                return new Monster(CurrentStage);
        }

        /// <summary>
        /// The method creates a Pet class object.
        /// </summary>
        /// <param name="petName">Pet class object.</param>
        public void CreatePet(string petName)
        {
            GHero.Balance -= Pet.Price;
            GPet = new Pet(petName);
        }

        /// <summary>
        /// The method simulates a monster attack.
        /// </summary>
        public void MonsterAttack() => GHero.HealthIndicator -= GMonster.Attack() * (1 - GHero.Skills.SProtection);

        /// <summary>
        /// The method simulates a hero attack.
        /// </summary>
        public void HeroAttack() => GMonster.HealthIndicator -= GHero.Attack();

        /// <summary>
        /// The method simulates a pet attack.
        /// </summary>
        public void PetAttack() => GMonster.HealthIndicator -= GPet.Attack();

        //public static bool Load(string fileName, ref Game g)
        //{
        //    try
        //    {
        //        BinaryFormatter formatter = new BinaryFormatter();
        //        using (FileStream fs = new FileStream(fileName, FileMode.Open))
        //            g = (Game)formatter.Deserialize(fs);

        //        return true;
        //    }
        //    catch
        //    {
        //        return false;
        //    }
        //}

        //public bool Save(string fileName)
        //{
        //    try
        //    {
        //        BinaryFormatter formatter = new BinaryFormatter();
        //        using (FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate))
        //            formatter.Serialize(fs, this);

        //        return true;
        //    }
        //    catch
        //    {
        //        return false;
        //    }
        //}
    }
}
