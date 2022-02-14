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
        //DiceConverter.Print(() => DiceConverter.AnyLess(2, 3));
        //DiceTable.Print();

        //NumberCombinator.Print(15);

        //int min = 1;
        //int max = 12;
        //for (int i = 2; i <= 4; i++)
        //    WriteLine($"({i}) " + NumberCombinator.DiceComb(max, i));

    }
}
