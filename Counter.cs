using System;

namespace Pizdyk
{
    class Counter
    {
        int reduplicateCounter;
        int reduplicateInterval = 5;

        int nickCounter;
        int nickInterval = 10;

        public string name;

        public Counter(string name)
        {
            this.name = name;
        }

        public bool reduplicationIsNeeded()
        {
            reduplicateCounter++;
            if (reduplicateCounter == reduplicateInterval)
            {
                reduplicateCounter = 0;
                reduplicateInterval = new Random().Next(7, 12);
                return true;
            }
            return false;
        }
        public bool NickIsNeeded()
        {
            nickCounter++;
            if (nickCounter == nickInterval)
            {
                nickCounter = 0;
                nickInterval = new Random().Next(15, 25);
                return true;
            }
            return false;
        }
    }
}
