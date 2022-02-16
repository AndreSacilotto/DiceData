
using System.Text;

namespace Dices
{
    public struct DiceData
    {
        public static DiceData Zero => new();

        public string formula;
        public int count;
        public int min;
        public int max;

        public float Average() => (max + min) / 2f;
        public string ToText() => $"{formula,-12} {count,-4} {min,-4} {max,-4}";
        public string ToTextAv() => $"{formula,-12} {min,-4} {max,-4} {Average(),-4:0.0}";

        public string ToExcel() => $"{formula}\t{count}\t{min}\t{max}";
        public string ToExcelAv() => $"{formula}\t{min}\t{max}\t{Average():0.0}";

        public static DiceData operator -(DiceData other)
        {
            //lh.count -= rh.count;
            other.min -= other.min;
            other.max -= other.max;

            var sb = new StringBuilder(other.formula);

            if (sb[0] != '-' && sb[0] != '+')
                sb.Insert(0, '+');

            for (int i = 0; i < other.formula.Length; i++)
            {
                if (sb[i] == '+')
                {
                    sb.Remove(i, 1);
                    sb.Insert(i, '-');
                }
                if (sb[i] == '-')
                {
                    sb.Remove(i, 1);
                    sb.Insert(i, '+');
                }
            }
            return other;
        }

        public static DiceData operator +(DiceData lh, DiceData rh)
        {
            //lh.count += rh.count;
            lh.min += rh.min;
            lh.max += rh.max;
            lh.formula += rh.formula;
            return lh;
        }

        public static DiceData operator -(DiceData lh, DiceData rh)
        {
            //lh.count -= rh.count;
            lh.min -= rh.min;
            lh.max -= rh.max;
            lh.formula += rh.formula;
            return lh;
        }

        public static DiceData operator +(DiceData lh, int rh)
        {
            lh.min += rh;
            lh.max += rh;
            lh.formula += rh.ToString("+0;-#");
            return lh;
        }

        public static DiceData operator -(DiceData lh, int rh)
        {
            lh.min -= rh;
            lh.max -= rh;
            lh.formula += rh.ToString("+0;-#");
            return lh;
        }




    }
}