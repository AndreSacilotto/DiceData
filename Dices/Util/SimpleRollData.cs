using System;
using System.Text;
using static Dices.DiceString;

namespace Dices
{
    public class Roller 
    {
        public readonly SimpleRollData[] rolls;
        public Roller(SimpleRollData[] rolls) => this.rolls = rolls;
        public int Roll(Random rng)
        {
            int result = 0;
            foreach (var el in rolls)
                result += el.Roll(rng);
            return result;
        }
    }


    public struct SimpleRollData : ICloneable
    {
        public static SimpleRollData Zero => new();

        public const uint DEFAULT_DICE = 0;
        public const uint DEFAULT_TIMES = 1;
        public const int DEFAULT_MOD = 0;

        public uint dice;
        public uint times;
        public int mod;

        public SimpleRollData(uint dice, uint times = DEFAULT_TIMES, int mod = DEFAULT_MOD)
        {
            this.mod = mod;
            this.times = times;
            this.dice = dice;
        }

        public override string ToString()
        {
            string t = times == DEFAULT_TIMES ? "" : times.ToString();
            string droll = dice == DEFAULT_DICE ? "" : t + "D" + dice;
            string m = mod == DEFAULT_MOD ? "" : mod.ToString(PLUS_MINUS_SPACE);

            return droll + m;
        }

        public long Calculate() => times * dice + mod;
        public long CalculateNoMod() => times * dice;

        public double Probability() => 1d / (dice * times);

        public int Roll(Random rng)
        {
            if (dice == DEFAULT_DICE)
                return mod;

            int result = 0;
            int max = (int)dice+1;
            for (int i = 0; i < times; i++)
                result += rng.Next(1, max);
            return result + mod;
        }

        public object Clone() => new SimpleRollData(dice, times, mod);

        public static StringBuilder Stringify(params SimpleRollData[] datas)
        {
            var sb = new StringBuilder(datas.Length * PLUS_SEPARATOR.Length * 3);
            foreach (var el in datas)
                sb.Append(el.ToString() + PLUS_SEPARATOR);
            sb.Length -= PLUS_SEPARATOR.Length;
            return sb;
        }

        public static SimpleRollData operator +(SimpleRollData lh, int rh)
        {
            lh.times += (uint)rh;
            return lh;
        }
        public static SimpleRollData operator -(SimpleRollData lh, int rh)
        {
            lh.times -= (uint)rh;
            return lh;
        }
    }
}
