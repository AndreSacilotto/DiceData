using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Dices
{
    public static class DiceString
    {
        public const string PLUS_SEPARATOR = " + ";
        public const string LESS_SEPARATOR = " - ";

        public const string PLUS_MINUS = "+0;-0";
        public const string PLUS_MINUS_SPACE = " + 0; - 0";
        public const string PLUS_MINUSF = "+0.0#;-0.0#";


        public static bool IsValidRoll(in string rollStr) => Regex.IsMatch(rollStr, @"^[+-]?(\d*?[dD]\d+|\d+)$", RegexOptions.Compiled);

        public static string[] SeparateRoll(string rollStr)
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

        public static bool IsDiceRoll(string roll) => roll.Contains("d", StringComparison.OrdinalIgnoreCase);

    }
}
