using Dices;
using System;
using System.Globalization;
using System.Linq;
using System.Threading;
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
        Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo("en-US");

#if false
        int min = 5;
        int max = 30;
        int depth = 3;

        int end = max + 6;
        for (; max <= end; max += 2)
        {
            Console.WriteLine("################ " + ((min + max) / 2f).ToString("0.0") + " ################");
            for (int d = 1; d <= depth; d++)
                RollFinder.Print(min, max, d, 5, 0);
        }
#elif false
        while (true)
            RollCommand.ReadConsole(true);
#elif true
        var roll = Statistics.RollFromString("D6 + D10-2");
        var seq = Statistics.RollMany(roll, 10000000);
        var items = Statistics.Apparitions(seq);

        foreach (var (key, count, percent) in items)
            WriteLine(key + "\t" + count + "\t" + percent.ToString("0.0%"));

#endif

    }
}
