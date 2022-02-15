using Dices;
using System;

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
        //DiceConverter.Print(() => DiceConverter.AnyLess(2, 5));
        //DiceTable.Print(2);

        //NumberFinder.PrintWithNeg(15);

        int min = 1;
        int max = 18;
        RollFinder.Print(min, max, 2);
        //for (int i = 2; i <= 3; i++)
        //    DiceFinder.Print(min, max, i);

    }
}
