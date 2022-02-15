using System.Collections.Generic;

namespace Dices
{
    public static class Constants
    {
        public const string PLUS_SEPARATOR = " + ";
        public const string LESS_SEPARATOR = " - ";

        public const string PLUS_MINUS = "+0;-0";
        public const string PLUS_MINUS_SPACE = " + 0; - 0";
        public const string PLUS_MINUSF = "+0.0#;-0.0#";

        public const int MAX_TIMES = 10;
        public const int HALF_TIMES = 5;
        //public const int DICE_STEP = 2;

        public static readonly uint[] CommomDices = new uint[] { 4, 6, 8, 10, 12, 20 };
        public static readonly uint[] RareDices = new uint[] { 2, 3, 14, 16, 24, 50, 60, 100, 120 };

        public static readonly HashSet<uint> ValidDice = new(
            new uint[] {
            2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 24, 30, 48, 50, 60, 100, 120
            }
        );

    }
}
