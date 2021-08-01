using System;
using System.Dynamic;
using System.IO;

namespace Drizzle.Lingo.Runtime
{
    public sealed partial class LingoGlobal
    {
        // TODO: Basically everything here requires caching.
        public static int thenumberoflines_helper(string value)
        {
            // TODO: Requires caching
            var count = 0;
            foreach (var chr in value)
            {
                if (chr == '\n')
                    count += 1;
            }

            return count;
        }

        public static string lineof_helper(int idx, string collection)
        {
            return linemember_helper(collection)[idx];
        }

        public static char charof_helper(int idx, string str)
        {
            return str[idx - 1];
        }

        public static dynamic charmember_helper(string d)
        {
            return new StringCharIndex(d);
        }

        public static dynamic linemember_helper(string d)
        {
            return new StringLineIndex(d);
        }

        public static dynamic lengthmember_helper(dynamic d)
        {
            if (d is string str)
            {
                return str.Length;
            }

            return d.length;
        }

        private sealed class StringCharIndex : DynamicObject, ISliceable
        {
            public StringCharIndex(string s)
            {
                String = s;
            }

            public string String { get; }

            public string this[int idx] => String[idx - 1].ToString();

            // I have no idea why this is necessary.
            public override bool TryGetIndex(GetIndexBinder binder, object[] indexes, out object? result)
            {
                result = this[(int) indexes[0]];
                return true;
            }

            public object this[Range idx]
            {
                get
                {
                    if (idx.Start.IsFromEnd || idx.End.IsFromEnd)
                        throw new ArgumentException();

                    return String[(idx.Start.Value - 1)..(idx.End.Value)];
                }
            }
        }

        private sealed class StringLineIndex : DynamicObject, ISliceable
        {
            public StringLineIndex(string s)
            {
                String = s;
            }

            public string String { get; }

            public string this[int value]
            {
                get
                {
                    var curLine = 1;
                    var sr = new StringReader(String);

                    while (sr.ReadLine() is { } line)
                    {
                        if (curLine++ == value)
                            return line;
                    }

                    throw new IndexOutOfRangeException();
                }
            }

            public override bool TryGetIndex(GetIndexBinder binder, object[] indexes, out object? result)
            {
                result = this[(int) indexes[0]];
                return true;
            }

            public object this[Range idx] => throw new NotImplementedException();
        }

        private object DoSliceString(string str, int start, int end)
        {
            throw new NotImplementedException();
        }
    }
}
