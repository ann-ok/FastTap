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
                if (currentStage < 1)
                    throw new Exception("Этап не может быть меньше 1.");

                currentStage = value;

                if (currentStage > maxStage)
                    maxStage = currentStage;
            }
        }

        public Game()
        {
            currentStage = 1;
            startDateTime = DateTime.Now;
        }

        public string GetInformation()
        {
            return $"Текущий этап: {currentStage}\n" +
                $"Максимальный этап: {maxStage}\n" +
                $"Время в игре: {DateTime.Now - startDateTime}\n";
        }

        public void ReturnToPrevStage()
        {
            if (CurrentStage != 1)
                CurrentStage--;

            //герой восстанавливает здоровье
        }
    }
}
