using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Serilog;

namespace Drizzle.Lingo.Runtime
{
    public class LingoList : IEnumerable<object>, ILingoListDuplicate
    {
        public List<object?> List { get; }

        public dynamic? this[int index]
        {
            get => List[index - 1];
            set => List[index - 1] = value;
        }

        public dynamic? this[LingoDecimal index]
        {
            get => this[(int) index];
            set => this[(int) index] = value;
        }

        public int count => List.Count;

        public LingoList()
        {
            List = new List<object?>();
        }

        public LingoList(int capacity)
        {
            List = new List<object?>(capacity);
        }

        public LingoList(IEnumerable<object?> items)
        {
            List = new List<object?>(items);
        }

        public int getpos(object? value)
        {
            return List.IndexOf(value) + 1;
        }

        public void add(object? value)
        {
            Add(value);
        }

        public void Add(object? value)
        {
            List.Add(value);
        }

        public void append(object? value)
        {
            List.Add(value);
        }

        public void deleteat(int number)
        {
            if (number > List.Count || number <= 0)
            {
                Log.Warning("Invalid deleteAt() index");
                return;
            }

            List.RemoveAt(number - 1);
        }

        public void addat(int number, object? value)
        {
            List.Insert(number - 1, value);
        }

        public LingoList duplicate()
        {
            return new LingoList(List.Select(i => DuplicateIfList(i)));
        }

        public void sort()
        {
            List.Sort(LingoComparer.Instance);
        }

        ILingoListDuplicate ILingoListDuplicate.duplicate()
        {
            return duplicate();
        }

        public IEnumerator<object> GetEnumerator()
        {
            return List.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public static LingoList operator +(LingoList a, dynamic b)
        {
            return new(a.Select(e => (dynamic)e + b));
        }

        public static LingoList operator -(LingoList a, dynamic b)
        {
            return new(a.Select(e => (dynamic)e - b));
        }

        public static LingoList operator *(LingoList a, dynamic b)
        {
            return new(a.Select(e => (dynamic)e * b));
        }

        public static LingoList operator /(LingoList a, dynamic b)
        {
            return new(a.Select(e => (dynamic)e / b));
        }

        public static bool operator ==(LingoList a, LingoList b)
        {
            if (a.count != b.count)
                return false;

            for (var i = 0; i < a.count; i++)
            {
                var itemA = a.List[i];
                var itemB = b.List[i];

                if (itemA != itemB)
                    return false;
            }

            return true;
        }


        public static bool operator !=(LingoList a, LingoList b)
        {
            return !(a == b);
        }

        public static object? DuplicateIfList(object? obj)
        {
            return obj is ILingoListDuplicate dup ? dup.duplicate() : obj;
        }

        private sealed class LingoComparer : IComparer<object?>
        {
            public static readonly LingoComparer Instance = new ();

            public int Compare(object? x, object? y)
            {
                if (x == y)
                    return 0;

                if (x == null)
                    return -1;

                if (y == null)
                    return 1;

                // Try a numeric compare if they're both numbers.
                if (TryDecimal(x, out var decX) && TryDecimal(y, out var decY))
                    return decX.CompareTo(decY);

                // Compare by string.
                var strX = x.ToString();
                var strY = y.ToString();

                return string.Compare(strX, strY, StringComparison.Ordinal);

                bool TryDecimal(object? val, out LingoDecimal dec)
                {
                    if (val is LingoDecimal decC)
                    {
                        dec = decC;
                        return true;
                    }

                    if (val is int i)
                    {
                        dec = i;
                        return true;
                    }

                    dec = default;
                    return false;
                }
            }
        }
    }
}
