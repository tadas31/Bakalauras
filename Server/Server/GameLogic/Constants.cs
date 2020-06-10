using System;
using System.Collections.Generic;
using System.Text;

namespace Server
{
    class Constants
    {
        public const int TICKS_PER_SEC = 30;
        public const int MS_PER_TICK = 1000 / TICKS_PER_SEC;

        public const int START_CARD_COUNT = 5;
        public const int TURN_TIME_SECONDS = 60;
        public const int TURN_TIME_MILISECONDS = TURN_TIME_SECONDS * 1000;

        public const int START_LIFE = 5;
        public const int MAX_MANA = 10;
    }
}
