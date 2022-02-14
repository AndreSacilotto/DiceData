using Dices;
using System;
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
        //DiceConverter.Run(() => DiceConverter.AnyLess(2, 3));
        //DiceTable.Run();

        //NumberCombinator.Run(15);

        //int min = 1;
        //int max = 12;
        //for (int i = 2; i <= 4; i++)
        //    WriteLine($"({i}) " + NumberCombinator.DiceComb(max, i));

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
    }
}
