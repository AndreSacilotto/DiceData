using Dices;
using System;
using System.Linq;
using System.Text;
using static System.Console;

class Program
{
    public struct Test<T> where T : IComparable<T>
    {
        public T value;
        public T expected;
        public Test(T value, T expected)
        {
            this.value = value;
            this.expected = expected;
        }
        public bool Same(T other) => expected.CompareTo(other) == 0;
    }

    [STAThread]
    public static void Main()
    {
        //DiceConverter.Run(() => DiceConverter.DoubleLess(), false);
        //DiceTable.Run();

        //while (true)
        //{
        //    var str = ReadLine();
        //    if (string.IsNullOrEmpty(str))
        //        break;
        //    var data = DiceString.Run(str);
        //    WriteLine(data.ToTextAv());
        //    Clipboard.SetClipboard(data.ToExcelAv());
        //    WriteLine();
        //}

        WriteLine(NumberCombinator.DiceComb(3, 12, 3));
    }
}
