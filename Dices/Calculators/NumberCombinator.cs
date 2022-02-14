using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static Dices.Constants;

namespace Dices
{
    public static class NumberCombinator
    {
        private class ReturnComb
        {
            public List<int[]> arrList = new();
            public void AddComb(params int[] items) => arrList.Add(items);
            public int[][] ToArray() => arrList.ToArray();
        }

        public static void Print(int value)
        {
            var sb = new StringBuilder();
            for (int i = 2; i <= 4; i++)
            {
                Console.WriteLine($"({i}) {value}:");
                var v = CombPlus(value, i, value, 1).ToArray();
                foreach (var item in v)
                {
                    sb.Clear();
                    foreach (var el in item)
                        sb.Append(el + PLUS_SEPARATOR);
                    Console.WriteLine(sb.ToString()[..^PLUS_SEPARATOR.Length]);
                }
                Console.WriteLine();
            }
        }

        public static int[][] CombPlus(int targetValue, int searchCount, int end, int start = 0, int step = 1, int plus = 0)
        {
            var rc = new ReturnComb();
            int[] buffer = new int[searchCount];
            CombPlusRecursive(rc, buffer, targetValue, 0, searchCount, start, end, step, plus);
            return rc.ToArray();
        }

        private static void CombPlusRecursive(ReturnComb rc, int[] buffer, int target, int index, int depth, int start, int end, int step, int plus)
        {
            if (index >= depth)
                return;

            bool isLast = index + 1 >= depth;
            for (int i = start; i <= end; i += step)
            {
                buffer[index] = i;
                if (isLast)
                {
                    int sum = buffer.Sum() + plus;
                    if (sum == target)
                        rc.AddComb((int[])buffer.Clone());
                }

                CombPlusRecursive(rc, buffer, target, index + 1, depth, i, end, step, plus);
            }

        }


    }
}
