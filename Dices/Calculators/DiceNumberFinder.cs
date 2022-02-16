using System;
using System.Text;
using static Dices.DiceConstants;
using static Dices.DiceString;

namespace Dices
{
    public static class DiceNumberFinder
    {
        private static void Print(ReturnDiscoveries rf, StringBuilder sb)
        {
            var tempSb = new StringBuilder();
            foreach (var arr in rf.ToArray())
            {
                long sum = 0;
                for (int i = 0; i < arr.Length; i++)
                {
                    var el = arr[i];
                    sum += el.Calculate();
                    tempSb.Append(el + PLUS_SEPARATOR);
                }
                tempSb.Length -= PLUS_SEPARATOR.Length;
                sb.AppendLine(tempSb + "");// + " = " + sum);
                tempSb.Clear();
            }

            Console.WriteLine(sb.ToString());
        }

        public static void Print(int target, int diceDepth = 1, int maxT = 3)
        {
            var sb = new StringBuilder($"[{diceDepth}] | {target} | {target / 2f:0.#}:\r\n");
            var rf = CombPlus(target, diceDepth, maxT, maxT);
            Print(rf, sb);
        }

        public static ReturnDiscoveries CombPlus(int target, int diceQuant, int diceTimes = HALF_TIMES, int maxMod = HALF_TIMES)
        {
            var rc = new ReturnDiscoveries();
            var buffer = new SimpleRollData[diceQuant];
            CombPlusRecursive(rc, buffer, diceTimes, maxMod, target, 0, 0);
            return rc;
        }

        private static void CombPlusRecursive(ReturnDiscoveries rc, SimpleRollData[] buffer, int maxTimes, int maxMod, int target, int index, int start)
        {
            if (index >= buffer.Length)
                return;

            bool isLast = index + 1 >= buffer.Length;
            for (int i = start; i < CommomDices.Length; i++)
            {
                buffer[index].dice = CommomDices[i];
                for (uint t = 1; t <= maxTimes; t++)
                {
                    buffer[index].times = t;
                    if (isLast)
                    {
                        long sum = 0;
                        foreach (var el in buffer)
                            sum += el.CalculateNoMod();
                        for (int m = -maxMod; m <= maxMod; m++)
                        {
                            buffer[index].mod = m;
                            if (sum + m == target)
                                rc.AddComb((SimpleRollData[])buffer.Clone());
                        }
                    }
                    CombPlusRecursive(rc, buffer, maxTimes, maxMod, target, index + 1, i + 1);
                }
            }

        }


    }
}
