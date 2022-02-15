using System;
using System.Collections.Generic;
using static Dices.Constants;

namespace Dices
{
    public static class DiceConverter
    {

        #region DICT

        public struct DiceResult
        {
            public readonly string result;
            public readonly string formula;
            public DiceResult(string result, string formula)
            {
                this.result = result;
                this.formula = formula;
            }
        }

        const int MAX_DICE = 10;

        private static void DictAdd(Dictionary<uint, List<DiceResult>> dict, uint result, string formula)
        {
            if (ValidDice.Contains(result))
                dict[result].Add(new DiceResult("D" + result, formula));
        }

        private static Dictionary<uint, List<T>> NewResultDict<T>()
        {
            var dict = new Dictionary<uint, List<T>>(ValidDice.Count);
            foreach (var item in ValidDice)
                dict.Add(item, new List<T>());
            return dict;
        }

        #endregion

        public static void Print(Func<Dictionary<uint, List<DiceResult>>> act)
        {
            var dict = act();
            foreach (var item in dict)
                if (item.Value.Count > 0)
                {
                    Console.WriteLine($"\n-----D{item.Key}-----\n");
                    foreach (var el in item.Value)
                        Console.WriteLine(el.formula);
                }
        }

        public static Dictionary<uint, List<DiceResult>> Division()
        {
            var dict = NewResultDict<DiceResult>();
            for (int i = 0; i < CommomDices.Length; i++)
            {
                float d = CommomDices[i];
                for (int t = 2; t < d; t++)
                {
                    var result = d / t;
                    var ceil = (uint)Math.Ceiling(result);
                    if (d % t == 0)
                        DictAdd(dict, ceil, $"D{d} / {t}");
                }
            }
            return dict;
        }

        public static Dictionary<uint, List<DiceResult>> OneLess(int maxDices = MAX_DICE)
        {
            var dict = NewResultDict<DiceResult>();
            for (int i = 0; i < CommomDices.Length; i++)
            {
                var d = CommomDices[i];
                for (int t = 2; t <= maxDices; t++)
                {
                    var diceCount = t - 1;
                    var result = d * t - diceCount;
                    DictAdd(dict, result, $"{t}D{d} - {diceCount}");
                }
            }
            return dict;
        }

        public static Dictionary<uint, List<DiceResult>> AnyLess(int depth = 1, int maxDices = MAX_DICE)
        {
            var dict = NewResultDict<DiceResult>();
            AnyLessRecursive(dict, new int[depth], new int[depth], maxDices, 0, 0);
            return dict;
        }
        private static void AnyLessRecursive(Dictionary<uint, List<DiceResult>> dict, int[] bufferDice, int[] bufferTimes, int maxDices, int index, int start = 0)
        {
            if (index >= bufferDice.Length)
                return;
            bool isLast = index + 1 >= bufferDice.Length;

            for (int i = start; i < CommomDices.Length; i++)
            {
                bufferDice[index] = CommomDices[i];
                for (int k = 1; k <= maxDices; k++)
                {
                    bufferTimes[index] = k;
                    if (isLast)
                    {
                        uint sum = 0;
                        uint diceCount = -1;
                        string str = "";
                        for (int b = 0; b < bufferTimes.Length; b++)
                        {
                            sum += bufferDice[b] * bufferTimes[b];
                            diceCount += bufferTimes[b];
                            str += (bufferTimes[b] == 1 ? "" : bufferTimes[b].ToString()) + 'D' + bufferDice[b] + PLUS_SEPARATOR;
                        }
                        sum -= diceCount;
                        str = str[..^PLUS_SEPARATOR.Length] + LESS_SEPARATOR + diceCount;
                        DictAdd(dict, sum, str);
                    }
                    AnyLessRecursive(dict, bufferDice, bufferTimes, maxDices, index + 1, i+1);
                }
            }


        }

        public static Dictionary<uint, List<DiceResult>> SimpleAnyLess(int depth = 3)
        {
            var dict = NewResultDict<DiceResult>();
            SimpleAnyLessRecursive(dict, new int[depth], 0, 0);
            return dict;
        }
        private static void SimpleAnyLessRecursive(Dictionary<uint, List<DiceResult>> dict, int[] buffer, int index, int start = 0)
        {
            if (index >= buffer.Length)
                return;

            bool isLast = index + 1 >= buffer.Length;
            for (int i = start; i < CommomDices.Length; i++)
            {
                buffer[index] = CommomDices[i];
                if (isLast)
                {
                    int diceCount = buffer.Length - 1;
                    int sum = 0;
                    string str = "";
                    foreach (var el in buffer)
                    {
                        sum += el;
                        str += "D" + el + PLUS_SEPARATOR;
                    }
                    sum -= diceCount;
                    str = str[..^PLUS_SEPARATOR.Length] + LESS_SEPARATOR + diceCount;
                    DictAdd(dict, sum, str);
                }

                SimpleAnyLessRecursive(dict, buffer, index + 1, i);
            }
        }

    }
}