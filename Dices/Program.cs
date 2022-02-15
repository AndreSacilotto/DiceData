using Dices;
using System;
using System.Globalization;
using System.Threading;

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
        Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo("en-US");

        //int min = 5;
        //int max = 32;
        //RollFinder.Print(min, max, 3, 5);


        while (true)
            DiceRollCommand.ReadConsole(true);
    }
}
