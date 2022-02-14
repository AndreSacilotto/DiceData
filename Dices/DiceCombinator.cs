using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dices
{
    public static class DiceCombinator
    {

        public class ReturnComb
        {
            public List<int[]> arrList = new();
            public void AddComb(params int[] items) => arrList.Add(items);
            public int[][] ToArray() => arrList.ToArray();
        }

        public static string Run(int max, int diceCount)
        {
            var target = max;
            //int plus = min - diceCount;

            const string separator = " + ";
            var sb = new StringBuilder(target + ":" + "\r\n");
            var comb = CombPlus(target, diceCount, 12, 4, 2, 0).ToArray();
            foreach (var arr in comb)
            {
                string str = "";
                int sum = 0;
                foreach (var el in arr)
                {
                    sum += el;
                    str += el + separator;
                }
                str = $"{str[..^separator.Length]} + '{arr.Length}' = {sum + arr.Length} = {(sum + arr.Length) / 2f:0.0}";
                sb.AppendLine($"{str}");
            }
            return sb.ToString();
        }

        public static ReturnComb CombPlus(int targetValue, int searchCount, int end, int start = 0, int step = 1, int plus = 0)
        {
            var rc = new ReturnComb();
            int[] buffer = new int[searchCount];
            CombPlusRecursive(rc, buffer, targetValue, 0, searchCount, start, end, step, plus);
            return rc;
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
