using System;
using System.Diagnostics.CodeAnalysis;

namespace Drizzle.Lingo.Runtime
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public sealed class LingoGlobal
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

        public static int abs(int value) => Math.Abs(value);
        public static LingoDecimal abs(LingoDecimal value) => LingoDecimal.Abs(value);

        public static int sqrt(int value) => (int) Math.Sqrt(value);
        public static LingoDecimal sqrt(LingoDecimal value) => LingoDecimal.Sqrt(value);

        public static int contains(string container, string value) => container.Contains(value, StringComparison.InvariantCultureIgnoreCase) ? 1 : 0;
        public static int starts(string container, string value) => container.StartsWith(value, StringComparison.InvariantCultureIgnoreCase) ? 1 : 0;

        public static string concat(object left, object right) => $"{left}{right}";
        public static string concat_space(object left, object right) => $"{left} {right}";

        public static dynamic slice_helper(dynamic obj, int start, int end)
        {
            throw new NotImplementedException();
        }

        public static dynamic new_castlib(dynamic type, dynamic lib)
        {
            throw new NotImplementedException();
        }

        public static dynamic new_script(dynamic type, LingoList list)
        {
            throw new NotImplementedException();
        }
    }
}
