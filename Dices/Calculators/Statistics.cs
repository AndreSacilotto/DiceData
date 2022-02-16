using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Dices
{
    public class Statistics
    {
        public const int DEFAULT_MANY = 100;

        private static Random rng = new();
        public static void SetSeed(int seed = 0)
        {
            rng = seed != 0 ? new Random(seed) : new Random(Guid.NewGuid().GetHashCode());
        }

        #region Dice Roll

        public static Roller RollFromString(string rollStr)
        {
            var split = DiceString.SeparateRoll(rollStr);

            var arr = new SimpleRollData[split.Length];

            for (int i = 0; i < split.Length; i++)
            {
                var str = split[i];
                if (DiceString.IsDiceRoll(str))
                {
                    var n = Regex.Split(str, @"d", RegexOptions.IgnoreCase);
                    if(!uint.TryParse(n[0], out var times))
                        times = SimpleRollData.DEFAULT_TIMES;
                    var dice = uint.Parse(n[1]);
                    arr[i] = new SimpleRollData(dice, times, 0);
                }
                else
                {
                    var mod = int.Parse(str);
                    arr[i] = new SimpleRollData(0, 1, mod);
                }
            }
            return new Roller(arr);
        }

        public static int Roll(Roller roller) => roller.Roll(rng);
        public static int Roll(string rollStr) => Roll(RollFromString(rollStr));

        public static int[] RollMany(Roller roller, int many = DEFAULT_MANY)
        {
            var arr = new int[many];
            for (int i = 0; i < many; i++)
                arr[i] = roller.Roll(rng);
            return arr;
        }

        #endregion

        #region Theoretical Dice

        public static int TheoreticalRoll(int min, int max, int mod = 0) => rng.Next(min, max) + mod;

        public static int[] TheoreticalRollMany(int min, int max, int mod = 0, int many = DEFAULT_MANY)
        {
            var arr = new int[many];
            max++;
            for (int i = 0; i < many; i++)
                arr[i] = TheoreticalRoll(min, max, mod);
            return arr;
        }

        public static double TheoreticalAverage(int min, int max, int mod = 0) => (min + max) / 2d + mod;

        #endregion
        public static List<(int key, int count, double percent)> Apparitions(IEnumerable<int> sequence)
        {
            var dict = new Dictionary<int, int>();
            int count = 0;
            foreach (var item in sequence)
            {
                dict[item] = dict.ContainsKey(item) ? dict[item] + 1 : 1;
                count++;
            }

            var list = new List<(int key, int count, double percent)>(count);
            foreach (var pair in dict)
                list.Add((pair.Key, pair.Value, pair.Value / (double)count));
            list.Sort((x, y) => x.key.CompareTo(y.key));
            return list;
        }
        
        public static double Average(IEnumerable<int> sequence) => sequence.Average();
        public static double Variance(IEnumerable<int> sequence, double? average = null)
        {
            if (!average.HasValue)
                average = Average(sequence);
            int end = sequence.Count();
            double variance = 0;
            foreach (var item in sequence)
                variance += Math.Pow(item - average.Value, 2);
            return variance / end;
        }
        public static double StandardDeviation(IEnumerable<int> sequence, bool isPopulation = false)
        {
            double variance = Variance(sequence);
            return Math.Sqrt(variance / (sequence.Count() - (isPopulation ? 0 : 1)));
        }

    }
}