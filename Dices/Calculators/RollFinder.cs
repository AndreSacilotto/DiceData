using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static Dices.Constants;

namespace Dices
{
    public static class RollFinder
    {
        public class ReturnDiscoveries
        {
            public readonly List<RollData[]> list = new();
            public void AddComb(RollData[] data) => list.Add(data);
            public RollData[][] ToArray() => list.ToArray();
        }

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
                //tempSb.Length -= tempSb[^1] == '0' ? PLUS_SEPARATOR.Length + 1 : 0;
                sb.AppendLine(tempSb + " = " + sum);
                tempSb.Clear();
            }

            Console.WriteLine(sb.ToString());
        }

        public static void Print(int sides, int diceDepth = 1) => Print(1, sides, diceDepth);
        public static void Print(int min, int max, int diceDepth = 1)
        {
            var mm = min + max;
            var sb = new StringBuilder($"[{diceDepth}] | {min}..{max} = {mm} | {mm / 2f:0.#}:\r\n");
            var rf = CombPlus(min, max, diceDepth);
            Print(rf, sb);
        }

        public static ReturnDiscoveries CombPlus(int min, int max, int diceQuant, int diceTimes = HALF_TIMES, int maxMod = HALF_TIMES)
        {
            var target = min + max;

            var rc = new ReturnDiscoveries();
            var buffer = new RollData[diceQuant];
            CombPlusRecursive(rc, buffer, diceTimes, maxMod, target, 0, 0);
            return rc;
        }

        private static void CombPlusRecursive(ReturnDiscoveries rc, RollData[] buffer, int maxTimes, int maxMod, int target, int index, int start)
        {
            if (index >= bt.Length)
                return;

            bool isLast = index + 1 >= bt.Length;
            for (int i = start; i < CommomDices.Length; i++)
            {
                buffer[index].dice = (uint)CommomDices[i];
                for (int t = 1; t <= maxTimes; t++)
                {
                    buffer[index].times = t;
                    if (isLast)
                    {
                        int sum = 0;
                        for (int s = 0; s < bt.Length; s++)
                            sum += bt[s] * buffer[s];

                        for (int m = -maxMod; m < maxMod; m++)
                        {
                            if (sum + m == target)
                            {
                                var arr = new int[buffer.Length];
                                for (int s = 0; s < bt.Length; s++)
                                    arr[s] = buffer[s] * bt[s];
                                arr[^1] = m;
                                rc.AddComb(arr);
                            }
                        }
                    }
                    CombPlusRecursive(rc, buffer, bt, times, maxMod, target, index + 1, i + 1);
                }
            }

        }


    }
}
