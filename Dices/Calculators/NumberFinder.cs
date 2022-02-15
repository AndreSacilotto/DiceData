using System;
using System.Linq;
using System.Text;
using static Dices.Constants;

namespace Dices
{
    public static class NumberFinder
    {
        public static void Print(int value)
        {
            var sb = new StringBuilder();
            for (int i = 2; i <= 4; i++)
            {
                Console.WriteLine($"({i}) {value}:");
                var v = CombPlus(value, i, value).ToArray();
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
        public static int[][] CombPlus(int targetValue, int searchCount, int end, int start = 1, int step = 1, int mod = 0)
        {
            var rc = new ReturnFinds<int>();
            int[] buffer = new int[searchCount];
            CombPlusRecursive(rc, buffer, targetValue, 0, start, end, step, mod);
            return rc.ToArray();
        }
        private static void CombPlusRecursive(ReturnFinds<int> rc, int[] buffer, int target, int index, int start, int end, int step, int mod)
        {
            if (index >= buffer.Length)
                return;

            bool isLast = index + 1 >= buffer.Length;
            for (int i = start; i <= end; i += step)
            {
                buffer[index] = i;
                if (isLast)
                {
                    int sum = buffer.Sum() + mod;
                    if (sum == target)
                        rc.AddComb((int[])buffer.Clone());
                }

                CombPlusRecursive(rc, buffer, target, index + 1, i, end, step, mod);
            }

        }

        //To be done
        public static void PrintWithNeg(int value)
        {
            var sb = new StringBuilder();
            var sb2 = new StringBuilder();
            for (int i = 2; i <= 4; i++)
            {
                sb2.AppendLine($"({i}) {value}:");
                var v = Comb(value, i, value).ToArray();
                foreach (var item in v)
                {
                    sb.Clear();
                    foreach (var el in item)
                        sb.Append(el + PLUS_SEPARATOR);
                    sb2.AppendLine(sb.ToString()[..^PLUS_SEPARATOR.Length]);
                }
                sb2.AppendLine();
            }
            Console.WriteLine(sb2);
        }
        public static int[][] Comb(int targetValue, int searchDepth, int end, int step = 1, int mod = 0)
        {
            var rc = new ReturnFinds<int>();
            int[] buffer = new int[searchDepth * 2 - 1];
            CombRecursive(rc, buffer, targetValue, 0, searchDepth, -end, end, step, mod);
            return rc.ToArray();
        }
        private static void CombRecursive(ReturnFinds<int> rc, int[] buffer, int target, int index, int depth, int start, int end, int step, int mod)
        {
            if (index >= buffer.Length)
                return;

            bool isLast = index + 1 >= buffer.Length;
            for (int i = start; i <= end; i += step)
            {
                if (i == 0)
                    continue;

                buffer[index] = i;
                if (isLast)
                {
                    int sum = buffer.Sum() + mod;
                    if (sum == target)
                        rc.AddComb((int[])buffer.Clone());
                }

                CombRecursive(rc, buffer, target, index + 1, depth, i, end, step, mod);
            }

        }

    }
}
