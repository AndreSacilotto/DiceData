using System;
using System.Text;
using static Dices.DiceConstants;
using static Dices.DiceString;

namespace Dices
{
    public static class RollFinder
    {
        private static string Print(ReturnDiscoveries rf, StringBuilder sb)
        {
            var tempSb = new StringBuilder();
            foreach (var arr in rf.ToArray())
            {
                long sum = 0;
                for (int i = 0; i < arr.Length; i++)
                {
                    sum += arr[i].Calculate();
                    tempSb.Append(arr[i] + PLUS_SEPARATOR);
                }
                tempSb.Length -= PLUS_SEPARATOR.Length;
                sb.AppendLine(tempSb + "");// + " = " + sum);
                tempSb.Clear();
            }

            Console.WriteLine(sb.ToString());
            return sb.ToString();
        }

        public static void Print(int min, int max, int diceDepth = 1, int maxDices = 3, int maxMod = 5)
        {
            var mm = min + max;
            var sb = new StringBuilder($"[{diceDepth}] | {min}..{max} | {mm} {mm / 2f:0.#}:\r\n");
            var rf = CombPlus(min, max, diceDepth, maxDices, maxMod);
            Print(rf, sb);
        }

        public static ReturnDiscoveries CombPlus(int min, int max, int diceQuant, int diceTimes = HALF_TIMES, int maxMod = HALF_TIMES)
        {
            var rc = new ReturnDiscoveries();
            var buffer = new SimpleRollData[diceQuant];
            CombPlusRecursive(rc, buffer, diceTimes, maxMod, min, max, 0, 0);
            return rc;
        }

        private static void CombPlusRecursive(ReturnDiscoveries rc, SimpleRollData[] buffer, int maxTimes, int maxMod, int min, int max, int index, int start)
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
                        var diceCount = 0;
                        long sum = 0;
                        foreach (var el in buffer)
                        {
                            sum += el.CalculateNoMod();
                            diceCount += (int)el.times;
                        }
                        for (int m = -maxMod; m <= maxMod; m++)
                        {
                            if (diceCount + m == min && sum + m == max)
                            {
                                buffer[index].mod = m;
                                rc.AddComb((SimpleRollData[])buffer.Clone());
                            }
                        }
                    }
                    CombPlusRecursive(rc, buffer, maxTimes, maxMod, min, max, index + 1, i + 1);
                }
            }

        }


    }
}
