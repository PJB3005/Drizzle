using System;
using System.Collections;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using Drizzle.Lingo.Runtime.Cast;
using Drizzle.Lingo.Runtime.Xtra;
using Serilog;

namespace Drizzle.Lingo.Runtime
{
    /// <summary>
    ///     Implementation of Lingo's global properties and methods.
    /// </summary>
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

        private Random _random = new();

        public string the_moviePath => LingoRuntime.MovieBasePath;

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
            if (obj is ISliceable sliceable)
                return sliceable[start..end];

            throw new NotSupportedException();
        }

        public dynamic new_castlib(dynamic type, dynamic lib)
        {
            throw new NotImplementedException();
        }

        public dynamic new_script(dynamic type, LingoList list)
        {
            return LingoRuntime.CreateScript(type, list);
        }

        public static int thenumberof_helper(dynamic value)
        {
            if (value is ICollection c)
                return c.Count;

            throw new ArgumentException();
        }

        public static dynamic itemof_helper(dynamic idx, dynamic collection)
        {
            return collection[idx];
        }

        public static LingoPoint point(int h, int v) => new(h, v);
        public static LingoPoint point(LingoDecimal h, LingoDecimal v) => new((int) h.Value, (int) v.Value);
        public static LingoRect rect(int l, int t, int r, int b) => new(l, t, r, b);

        public static LingoRect rect(LingoDecimal l, LingoDecimal t, LingoDecimal r, LingoDecimal b) =>
            new((int) l, (int) t, (int) r, (int) b);

        public static LingoRect rect(LingoPoint lt, LingoPoint rb) => new(lt, rb);

        public static LingoDecimal floatmember_helper(int i) => new(i);
        public static dynamic floatmember_helper(dynamic d) => d.@float;

        public static LingoDecimal cos(LingoDecimal d) => LingoDecimal.Cos(d);
        public static LingoDecimal sin(LingoDecimal d) => LingoDecimal.Sin(d);
        public static LingoDecimal tan(LingoDecimal d) => LingoDecimal.Tan(d);
        public static LingoDecimal atan(LingoDecimal d) => LingoDecimal.Atan(d);

        public static LingoDecimal power(LingoDecimal @base, LingoDecimal exp) => LingoDecimal.Pow(@base, exp);
        public static LingoSymbol symbol(string s) => new(s);

        public void put(dynamic d)
        {
            Console.WriteLine(d);
        }

        public dynamic xtra(object xtraNameOrNum)
        {
            var xtraName = (string) xtraNameOrNum;
            xtraName = xtraName.ToLower();

            return xtraName switch
            {
                "fileio" => new FileIOXtra(),
                "imgxtra" => new ImgXtra(),
                _ => throw new NotSupportedException($"Unsupported xtra: {xtraNameOrNum}")
            };
        }

        public dynamic @new(dynamic a)
        {
            throw new NotImplementedException();
        }

        public void basetdisplay(dynamic w, dynamic h, dynamic idk3, dynamic idk, dynamic idk2)
        {
            throw new NotImplementedException();
        }

        public dynamic bascreeninfo(string prop)
        {
            throw new NotImplementedException();
        }

        public string getnthfilenameinfolder(string folderPath, int fileNumber)
        {
            var idx = fileNumber - 1;
            var entries = Directory.GetFileSystemEntries(folderPath);
            return idx >= entries.Length ? "" : entries[idx];
        }

        public dynamic script(dynamic a)
        {
            throw new NotImplementedException();
        }

        public int the_milliseconds => _system.milliseconds;
        public string the_moviepath => the_moviePath;
        public int objectp(dynamic d) => throw new NotImplementedException();

        public CastMember? member(object memberNameOrNum, object? castNameOrNum = null) =>
            LingoRuntime.GetCastMember(memberNameOrNum, castNameOrNum);

        public int the_randomSeed { get; set; }
        public LingoColor color(int r, int g, int b) => new(r, g, b);
        public LingoColor color(int palIdx) => throw new NotImplementedException();
        public LingoImage image(int w, int h, int bitDepth) => new LingoImage(w, h, bitDepth);

        public int random(int max)
        {
            return _random.Next(1, max + 1);
        }

        public string @string(dynamic value) => value.ToString();

        public static int op_eq(dynamic? a, dynamic? b)
        {
            if (a?.GetType() != b?.GetType())
            {
                Log.Warning("Invalid type comparison: {A} == {B}", a, b);
                return 0;
            }

            return a == b ? 1 : 0;
        }

        public static int op_ne(dynamic? a, dynamic? b)
        {
            if (a?.GetType() != b?.GetType())
            {
                Log.Warning("Invalid type comparison: {A} <> {B}", a, b);
                return 1;
            }
            return a != b ? 1 : 0;
        }

        public static int op_lt(dynamic? a, dynamic? b) => a < b ? 1 : 0;
        public static int op_le(dynamic? a, dynamic? b) => a >= b ? 1 : 0;
        public static int op_gt(dynamic? a, dynamic? b) => a > b ? 1 : 0;
        public static int op_ge(dynamic? a, dynamic? b) => a <= b ? 1 : 0;

        public static int op_and(dynamic? a, dynamic? b)
        {
            // Lingo does not short circuit because of course not...
            var bA = ToBool(a);
            var bB = ToBool(b);

            return bA && bB ? 1 : 0;
        }

        public static int op_or(dynamic? a, dynamic? b)
        {
            // Lingo does not short circuit because of course not...
            var bA = ToBool(a);
            var bB = ToBool(b);

            return bA || bB ? 1 : 0;
        }

        public static bool ToBool(dynamic? a)
        {
            return a != 0 && a != null;
        }

        public string the_platform => "win";

        public LingoSprite sprite(dynamic a)
        {
            Log.Warning("sprite() not implemented");
            return new LingoSprite();
        }

        public dynamic sound(dynamic a) => throw new NotImplementedException();
        public dynamic call(LingoSymbol a) => throw new NotImplementedException();
        public dynamic call(LingoSymbol a, dynamic a1) => throw new NotImplementedException();
        public dynamic call(LingoSymbol a, dynamic a1, dynamic a2) => throw new NotImplementedException();
        public dynamic call(LingoSymbol a, dynamic a1, dynamic a2, dynamic a3) => throw new NotImplementedException();

        public dynamic call(LingoSymbol a, dynamic a1, dynamic a2, dynamic a3, dynamic a4) =>
            throw new NotImplementedException();

        public static string chars(string str, int first, int last)
        {
            if (first == last)
                return str[first + 1].ToString();

            return str[(first + 1)..Math.Min(str.Length, last + 1)];
        }

        public void alert(string msg) => throw new NotImplementedException();

        public LingoSymbol ilk(object obj)
        {
            var val = obj switch
            {
                int => "integer",
                LingoDecimal => "float",
                LingoList => "list",
                LingoPropertyList => "proplist",
                string => "string",
                LingoRect => "rect",
                LingoPoint => "point",
                LingoColor => "color",
                LingoSymbol => "symbol",
                LingoImage => "image",
                // Lol apparently ilk() is called once and it's to check if something is a list
                // so I didn't even need to fill out this much.
                null => "void",
                _ => throw new NotSupportedException()
            };

            return new LingoSymbol(val);
        }

        public void createfile(dynamic d, string f) => d.createfile(f);

        public LingoScriptRuntimeBase MovieScriptInstance => LingoRuntime.MovieScriptInstance;
        public static string last_char(string str) => str[^1].ToString();
        public static int stringp(object str) => str is string ? 1 : 0;
        public LingoCastLib castlib(dynamic nameOrNum) => throw new NotImplementedException();
    }
}
