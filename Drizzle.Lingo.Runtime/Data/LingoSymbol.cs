using System;

namespace Drizzle.Lingo.Runtime
{
    public readonly struct LingoSymbol
    {
        public string Value { get; }

        public LingoSymbol(string value)
        {
            Value = value;
        }

        public static bool operator ==(LingoSymbol a, LingoSymbol b)
        {
            return a.Value.Equals(b.Value, StringComparison.OrdinalIgnoreCase);
        }

        public static bool operator !=(LingoSymbol a, LingoSymbol b)
        {
            return !(a == b);
        }
    }
}
