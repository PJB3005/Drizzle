using System;
using System.Collections;
using System.Diagnostics.CodeAnalysis;

namespace Drizzle.Lingo.Runtime
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public sealed partial class LingoGlobal
    {
        public const int BACKSPACE = 51;
        public const string EMPTY = "";
        public const int ENTER = 3;
        public const int FALSE = 0;
        public static readonly LingoDecimal PI = new LingoDecimal(Math.PI);
        public const string QUOTE = "\"";
        public const int RETURN = 36;
        public const int SPACE = 49;
        public const int TRUE = 1;
        public const object VOID = null;

        public string the_moviePath => throw new NotImplementedException();

        public static int abs(int value) => Math.Abs(value);
        public static LingoDecimal abs(LingoDecimal value) => LingoDecimal.Abs(value);

        public static int sqrt(int value) => (int) Math.Sqrt(value);
        public static LingoDecimal sqrt(LingoDecimal value) => LingoDecimal.Sqrt(value);

        public static int contains(string container, string value) =>
            container.Contains(value, StringComparison.InvariantCultureIgnoreCase) ? 1 : 0;

        public static int starts(string container, string value) =>
            container.StartsWith(value, StringComparison.InvariantCultureIgnoreCase) ? 1 : 0;

        public static string concat(object left, object right) => $"{left}{right}";
        public static string concat_space(object left, object right) => $"{left} {right}";

        public dynamic slice_helper(dynamic obj, int start, int end)
        {
            throw new NotImplementedException();
        }

        public dynamic new_castlib(dynamic type, dynamic lib)
        {
            throw new NotImplementedException();
        }

        public dynamic new_script(dynamic type, LingoList list)
        {
            throw new NotImplementedException();
        }

        public static int thenumberof_helper(dynamic value)
        {
            if (value is ICollection c)
                return c.Count;

            throw new ArgumentException();
        }

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

        public static dynamic itemof_helper(dynamic idx, dynamic collection)
        {
            return collection[idx];
        }

        public static string lineof_helper(int idx, string collection)
        {
            throw new NotImplementedException();
        }

        public static char charof_helper(int idx, string str)
        {
            return str[idx];
        }

        public static LingoPoint point(int h, int v) => new(h, v);
        public static LingoPoint point(LingoDecimal h, LingoDecimal v) => new((int) h.Value, (int) v.Value);
        public static LingoRect rect(int l, int t, int r, int b) => new(l, t, r, b);
        public static LingoRect rect(LingoPoint lt, LingoPoint rb) => new(lt, rb);

        public static LingoDecimal floatmember_helper(int i) => new(i);
        public static dynamic floatmember_helper(dynamic d) => d.@float;
        public static dynamic charmember_helper(string d) => throw new NotImplementedException();

        public static LingoDecimal cos(LingoDecimal d) => LingoDecimal.Cos(d);
        public static LingoDecimal sin(LingoDecimal d) => LingoDecimal.Sin(d);
        public static LingoDecimal tan(LingoDecimal d) => LingoDecimal.Tan(d);
        public static LingoDecimal atan(LingoDecimal d) => LingoDecimal.Atan(d);

        public static LingoDecimal power(LingoDecimal @base, LingoDecimal exp) => LingoDecimal.Pow(@base, exp);

        public void put(dynamic d)
        {
            Console.WriteLine(d);
        }

        public dynamic xtra(dynamic xtraNameOrNum)
        {
            throw new NotImplementedException();
        }

        public dynamic @new(dynamic a)
        {
            throw new NotImplementedException();
        }

        public void basetdisplay(dynamic w, dynamic h, dynamic idk3, dynamic idk, dynamic idk2)
        {
            throw new NotImplementedException();
        }

        public string getnthfilenameinfolder(dynamic a, dynamic b)
        {
            throw new NotImplementedException();
        }

        public dynamic script(dynamic a)
        {
            throw new NotImplementedException();
        }

        public int the_milliseconds => _system.milliseconds;
        public string the_moviepath => throw new NotImplementedException();
        public int objectp(dynamic d) => throw new NotImplementedException();

        public dynamic member(dynamic a) => throw new NotImplementedException();
        public int the_randomSeed { get; set; }
        public LingoColor color(int r, int g, int b) => new(r, g, b);
        public LingoImage image(int w, int h, int bitDepth) => throw new NotImplementedException();
        public int random(int max) => throw new NotImplementedException();
        public string @string(dynamic value) => value.ToString();

        public static int op_eq(dynamic a, dynamic b) => a == b ? 1 : 0;
        public static int op_ne(dynamic a, dynamic b) => a != b ? 1 : 0;
        public static int op_lt(dynamic a, dynamic b) => a < b ? 1 : 0;
        public static int op_le(dynamic a, dynamic b) => a >= b ? 1 : 0;
        public static int op_gt(dynamic a, dynamic b) => a > b ? 1 : 0;
        public static int op_ge(dynamic a, dynamic b) => a <= b ? 1 : 0;

        public static int op_and(dynamic a, dynamic b)
        {
            // Lingo does not short circuit because of course not...
            var bA = ToBool(a);
            var bB = ToBool(b);

            return bA && bB ? 1 : 0;
        }

        public static int op_or(dynamic a, dynamic b)
        {
            // Lingo does not short circuit because of course not...
            var bA = ToBool(a);
            var bB = ToBool(b);

            return bA || bB ? 1 : 0;
        }

        public static bool ToBool(dynamic a)
        {
            return a != 0 && a != null;
        }

        public string the_platform => "win";

        public dynamic sprite(dynamic a) => throw new NotImplementedException();
    }
}
