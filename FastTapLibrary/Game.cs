using System;
using System.Collections.Generic;
using System.Text;

namespace FastTapLibrary
{
    class Game : IInformative
    {
        //private const string heroImage;
        private int maxStage;
        private TimeSpan totalTime;
        //music
        //sounds
        private int currentStage
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
            totalTime = new TimeSpan(0, 0, 1);
        }

        public string GetInformation()
        {
            return $"Текущий этап: {currentStage}\n" +
                $"Максимальный этап: {maxStage}\n" +
                $"Время в игре: {totalTime}\n";
        }
    }
}
