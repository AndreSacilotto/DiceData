using System;
using System.Text;
using static Dices.Constants;

namespace Dices
{
    public static class DiceTable
    {
        public static void Run(int any = 3)
        {
            var result = SimpleAny(any);
            Console.WriteLine(result.ToString());
        }

        public static StringBuilder SimpleAny(int any)
        {
            var sb = new StringBuilder(CommomDices.Length * 3 * any);
            SimpleAnyRecursion(sb, new int[any], 0, -1);
            return sb;
        }

        private static void SimpleAnyRecursion(StringBuilder sb, int[] anyArr, int anyIndex, int diceIndex)
        {
            for (int i = diceIndex + 1; i < CommomDices.Length; i++)
            {
                anyArr[anyIndex] = CommomDices[i];

                if (anyArr.Length - 1 == anyIndex)
                {
                    var data = new DiceData { count = anyIndex + 1 };
                    data.min = data.count;
                    for (int any = 0; any < anyArr.Length; any++)
                    {
                        var d = anyArr[any];
                        data.name += "D" + d + "+";
                        data.max += d;
                    }
                    data.name = data.name.Remove(data.name.Length - 1);
                    sb.AppendLine(data.ToText());
                }

                if (anyArr.Length > anyIndex + 1)
                    SimpleAnyRecursion(sb, anyArr, anyIndex + 1, i);
            }
        }


    }
}
