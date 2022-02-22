using System;
using System.Text.RegularExpressions;

namespace Dices
{
    public static class RollCommand
    {
        public static void ReadConsole(bool setClipboard = false)
        {
            var str = Console.ReadLine();
            if (!string.IsNullOrEmpty(str))
            {
                var data = DiceDataFromString(str);
                Console.WriteLine(data.ToTextAv());
                if (setClipboard)
                    Clipboard.PushString(data.ToExcelAv());
                Console.WriteLine();
            }
        }

        public static DiceData DiceDataFromString(string rollString)
        {
            string[] rollsArr = DiceString.SeparateRoll(rollString);
            if (rollsArr != null)
                return ReadRoll(rollsArr);
            return DiceData.Zero;
        }

        private static DiceData DieFromString(in string rollStr)
        {
            var sep = Regex.Split(rollStr, @"d", RegexOptions.IgnoreCase);
            if (!int.TryParse(sep[0], out var times))
                times = rollStr[0] == '-' ? -1 : 1;

            var max = int.Parse(sep[1]);
            return new DiceData
            {
                formula = rollStr,
                count = Math.Abs(times),
                min = times,
                max = max * times,
            };
        }

        public static DiceData ReadRoll(string[] rolls)
        {
            DiceData data = DiceData.Zero;
            if (rolls == null || rolls.Length < 1)
                return data;

            for (int i = 0; i < rolls.Length; i++)
            {
                var roll = rolls[i];
                if (DiceString.IsDiceRoll(roll))
                {
                    data.count++;
                    data += DieFromString(roll);
                }
                else
                    data += int.Parse(roll);
            }

            if (data.formula[0] == '+')
                data.formula = data.formula[1..];
            return data;
        }


    }



}
