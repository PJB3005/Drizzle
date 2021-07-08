using System;

namespace Drizzle.Lingo
{
    public static class LingoGlobals
    {
        public static object Abs(object value)
        {
            return value switch
            {
                int i => Math.Abs(i),
                LingoDecimal d => LingoDecimal.Abs(d),
                _ => throw new ArgumentException(nameof(value))
            };
        }

        public static object Sqrt(object value)
        {
            return value switch
            {
                int i => (int) Math.Sqrt(i),
                LingoDecimal d => LingoDecimal.Sqrt(d),
                _ => throw new ArgumentException(nameof(value))
            };
        }
    }
}
