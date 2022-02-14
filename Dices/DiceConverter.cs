using System;
using System.Collections.Generic;
using static Dices.Constants;

namespace Dices
{
    public static class DiceConverter
    {
        public struct DiceResult
        {
            public readonly int dice;
            public readonly string result;
            public readonly string formula;
            public DiceResult(int dice, string result, string formula)
            {
                this.dice = dice;
                this.result = result;
                this.formula = formula;
            }
        }

        const int MAX_DICE = 10;

        private static bool IsValid(int value) => ValidDice.Contains(value);

        private static void DictAdd(Dictionary<int, List<DiceResult>> dict, int result, int dice, string resultStr, string formula)
        {
            if (IsValid(result))
                dict[result].Add(new DiceResult(dice, resultStr, formula));
        }

        private static Dictionary<int, List<T>> NewResultDict<T>()
        {
            var dict = new Dictionary<int, List<T>>(ValidDice.Count);
            foreach (var item in ValidDice)
                dict.Add(item, new List<T>());
            return dict;
        }

        #region MAIN

        public static void Run(Func<Dictionary<int, List<DiceResult>>> act, bool mode)
        {
            var dict = act();
            if (mode)
            {
                var diceDict = new Dictionary<int, List<string>>(CommomDices.Length);
                foreach (var el in CommomDices)
                    diceDict.Add(el, new List<string>());
                foreach (var item in dict.Values)
                    foreach (var el in item)
                        diceDict[el.dice].Add(el.formula);
                foreach (var item in diceDict)
                {
                    Console.WriteLine($"\n--{item.Key}--\n");
                    foreach (var el in item.Value)
                        Console.WriteLine(el);
                }
            }
            else
            {
                foreach (var item in dict)
                    if (item.Value.Count > 0)
                    {
                        Console.WriteLine($"\n---D{item.Key}---\n");
                        foreach (var el in item.Value)
                            Console.WriteLine(el.formula);
                    }
            }

        }

        #endregion

        public static Dictionary<int, List<DiceResult>> Division()
        {
            var dict = NewResultDict<DiceResult>();
            for (int i = 0; i < CommomDices.Length; i++)
            {
                float d = CommomDices[i];
                for (int t = 2; t < d; t++)
                {
                    var result = d / t;
                    var ceil = (int)Math.Ceiling(result);
                    if (d % t == 0)
                        DictAdd(dict, ceil, (int)d, $"D{result}", $"D{d} / {t}");
                }
            }
            return dict;
        }

        public static Dictionary<int, List<DiceResult>> Less()
        {
            var dict = NewResultDict<DiceResult>();
            for (int i = 0; i < CommomDices.Length; i++)
            {
                int d = CommomDices[i];
                for (int t = 2; t <= MAX_DICE; t++)
                {
                    int result = d * t - (t - 1);
                    DictAdd(dict, result, d, $"D{result}", $"{t}D{d} - {t - 1}");
                }
            }
            return dict;
        }

        public static Dictionary<int, List<DiceResult>> DoubleLess()
        {
            int len = CommomDices.Length;
            var dict = NewResultDict<DiceResult>();
            for (int i = 0; i < len; i++)
            {
                for (int j = i + 1; j < len; j++)
                {
                    if (i == j)
                        continue;
                    int a, b;
                    for (b = 1; b <= MAX_DICE / 2; b++)
                    {
                        for (a = 1; a + b <= MAX_DICE / 2; a++)
                        {
                            var result = a * CommomDices[i] + b * CommomDices[j] - (a + b - 1);
                            var str = $"{(a == 1 ? "" : a)}D{CommomDices[i]} + {(b == 1 ? "" : b)}D{CommomDices[j]} - {a + b - 1}";
                            DictAdd(dict, result, CommomDices[i], $"D{result}", str);
                        }
                    }

                }
            }
            return dict;
        }

        public static Dictionary<int, List<DiceResult>> SimpleTripleLess()
        {
            int len = CommomDices.Length;
            var dict = NewResultDict<DiceResult>();
            for (int i = 0; i < len; i++)
                for (int j = i + 1; j < len; j++)
                    for (int k = j + 1; k < len; k++)
                    {
                        var result = CommomDices[i] + CommomDices[j] + CommomDices[k] - 2;
                        var str = $"D{CommomDices[i]} + D{CommomDices[j]} + D{CommomDices[k]} - 2";
                        DictAdd(dict, result, CommomDices[i], $"D{result}", str);
                    }
            return dict;
        }

    }
}