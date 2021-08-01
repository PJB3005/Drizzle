using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Drizzle.Lingo.Runtime
{
    public class LingoList : IEnumerable<object>
    {
        public List<dynamic> List { get; }

        public dynamic this[int index]
        {
            get => List[index - 1];
            set => List[index - 1] = value;
        }

        public int count => List.Count;

        public LingoList()
        {
            List = new List<dynamic>();
        }

        public LingoList(IEnumerable<dynamic> items)
        {
            List = new List<dynamic>(items);
        }

        public int getpos(dynamic value)
        {
            return List.IndexOf(value) + 1;
        }

        public void add(dynamic value)
        {
            List.Add(value);
        }

        public void append(dynamic value)
        {
            List.Add(value);
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
    }
}
