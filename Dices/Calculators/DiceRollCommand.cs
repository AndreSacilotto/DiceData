using System;
using System.Text.RegularExpressions;

namespace Dices
{
    public static class DiceRollCommand
    {
        public static DiceData Run(string rollString)
        {
            string[] rollsArr = Separate(rollString);
            if (rollsArr != null)
                return ReadRoll(rollsArr);
            return DiceData.Zero;
        }

        public static void ReadConsole(bool setClipboard = false)
        {
            var str = Console.ReadLine();
            if (string.IsNullOrEmpty(str))
                return;
            var data = Run(str);
            Console.WriteLine(data.ToTextAv());
            if (setClipboard)
                Clipboard.SetClipboard(data.ToExcelAv());
            Console.WriteLine();
        }

        public static int RollDieFromString(in string rollStr, int modifier = 0)
        {
            var sep = Regex.Split(rollStr, @"\d+", RegexOptions.IgnoreCase);
            var times = int.Parse(sep[0]);
            var max = int.Parse(sep[1]) + 1;
            var rng = new Random();
            int result = 0;
            for (int t = 0; t < times; t++)
                result += rng.Next(1, max);
            return result + modifier;
        }
        public static bool IsValidRoll(in string rollStr) => Regex.IsMatch(rollStr, @"^[+-]?(\d*?[dD]\d+|\d+)$");

        public static string[] Separate(string rollStr)
        {
            if (string.IsNullOrWhiteSpace(rollStr))
                return null;

            rollStr = Regex.Replace(rollStr.Replace('d', 'D'), @"[^-+0123456789D]", "", RegexOptions.Compiled);
            var split = Regex.Split(rollStr, @"(?=[-+])", RegexOptions.Compiled);

            for (int i = 0; i < split.Length; i++)
            {
                if (string.IsNullOrWhiteSpace(split[i]))
                    return null;
                if (split[i][0] != '-' && split[i][0] != '+')
                    split[i] = '+' + split[i];
            }
            return split;
        }

        public static DiceData DieFromString(in string rollStr)
        {
            bool isNeg = rollStr[0] == '-';

            var sep = Regex.Split(rollStr, @"d", RegexOptions.IgnoreCase);
            if (!int.TryParse(sep[0], out var times))
                times = isNeg ? -1 : 1;

            var max = int.Parse(sep[1]);
            return new DiceData
            {
                name = rollStr,
                count = Math.Abs(times),
                min = times,
                max = max * times,
            };
        }
        private static bool IsDiceRoll(in string rollStr) => rollStr.Contains("d", StringComparison.OrdinalIgnoreCase);
        public static DiceData ReadRoll(string[] rolls)
        {
            DiceData data = DiceData.Zero;
            if (rolls == null || rolls.Length < 1)
                return data;

            for (int i = 0; i < rolls.Length; i++)
            {
                var roll = rolls[i];
                if (IsDiceRoll(roll))
                {
                    data.count++;
                    data += DieFromString(roll);
                }
                else
                    data += int.Parse(roll);
            }

            if (data.name[0] == '+')
                data.name = data.name[1..];
            return data;
        }


    }



}
