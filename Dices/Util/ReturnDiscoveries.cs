using System.Collections.Generic;

namespace Dices
{
    public class ReturnFinds<T>
    {
        public readonly List<T[]> list = new();
        public void AddComb(params T[] data) => list.Add(data);
        public T[][] ToArray() => list.ToArray();
    }

    public class ReturnDiscoveries : ReturnFinds<SimpleRollData> { }
}
