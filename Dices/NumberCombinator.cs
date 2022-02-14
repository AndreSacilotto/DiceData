using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dices
{
    public static class NumberCombinator
    {

        public class ReturnComb
        {
            public List<int[]> arrList = new();
            public void AddComb(params int[] items) => arrList.Add(items);
            public int[][] ToArray() => arrList.ToArray(); 
        }

        public static void Run()
        {
            var t = 15;
            Console.WriteLine(t + ":");
            const string separator = " + ";
            var sb = new StringBuilder();
            for (int i = 2; i <= 3; i++)
            {
                var v = CombPlus(t, i, 1, 3).ToArray();
                foreach (var item in v)
                {
                    sb.Clear();
                    foreach (var el in item)
                        sb.Append(el + separator);
                    Console.WriteLine(sb.ToString()[..^(separator.Length)]);
                }
            }
        }

        public static string DiceComb(int min, int max, int diceCount)
        {
            var t = min + max;

            const string separator = " + ";
            var sb = new StringBuilder(t + ":" + "\r\n");
            var v = CombPlus(t, diceCount, 2, 2, diceCount).ToArray();
            foreach (var item in v)
            {
                string str = "";
                foreach (var el in item)
                    str += el + separator;
                sb.AppendLine($"{str}'{diceCount}'");
            }
            return sb.ToString();
        }

        public static ReturnComb CombPlus(int targetValue, int searchCount = 2, int start = 1, int step = 1, int plus = 0)
        {
            var rc = new ReturnComb();
            int[] buffer = new int[searchCount];
            CombPlusRecursive(rc, buffer, targetValue, 0, searchCount, start, step, plus);
            return rc;
        }

        private static ReturnComb CombPlusRecursive(ReturnComb rc, int[] buffer, int target, int index, int depth, int start, int step, int plus)
        {
            bool isNotLast = index + 1 < depth;
            bool isLast = !isNotLast;
            for (int i = start; i < target; i += step)
            {
                buffer[index] = i;
                if (isLast)
                {
                    int sum = buffer.Sum() + plus;
                    if (sum == target)
                        rc.AddComb((int[])buffer.Clone());
                }

                if(isNotLast)
                    CombPlusRecursive(rc, buffer, target, index + 1, depth, start, step, plus);

            }
            return rc;
        }


    }
}
