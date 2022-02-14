using System.Collections.Generic;

namespace Dices
{
    public static class Constants
    {
        public static readonly int[] CommomDices = new int[] { 4, 6, 8, 10, 12, 20 };

        public static readonly HashSet<int> ValidDice = new(
            new int[] {
            2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 24, 30, 48, 50, 60, 100, 120
            }
        );

    }
}
