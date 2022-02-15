using System;
using System.Text;
using static Dices.Constants;

namespace Dices
{
    public struct RollData : ICloneable
    {
        public const uint DEFAULT_TIMES = 1;
        public const int DEFAULT_MOD = 0;

        public uint dice;
        public uint times;
        public int mod;

        public RollData(uint dice, uint times = DEFAULT_TIMES, int mod = DEFAULT_MOD)
        {
            this.mod = mod;
            this.times = times;
            this.dice = dice;
        }

        public override string ToString()
        {
            string t = times == DEFAULT_TIMES ? "" : times.ToString();
            string m = mod == DEFAULT_MOD ? "" : mod.ToString(PLUS_MINUS_SPACE);
            return $"{t}D{dice}{m}";
        }

        public long Calculate() => times * dice + mod;
        public long CalculateNoMod() => times * dice;

        public object Clone() => new RollData(dice, times, mod);

        public static StringBuilder Stringify(params RollData[] datas)
        {
            var sb = new StringBuilder(datas.Length * PLUS_SEPARATOR.Length * 3);
            foreach (var el in datas)
                sb.Append(el.ToString() + PLUS_SEPARATOR);
            sb.Length -= PLUS_SEPARATOR.Length;
            return sb;
        }

        public static RollData operator +(RollData lh, int rh)
        {
            lh.times += (uint)rh;
            return lh;
        }
        public static RollData operator -(RollData lh, int rh)
        {
            lh.times -= (uint)rh;
            return lh;
        }
    }
}
