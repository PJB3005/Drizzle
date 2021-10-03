using System;

namespace Drizzle.Editor.Helpers
{
    public static class Swap
    {
        /// <summary>
        /// Swap <paramref name="a"/> and <paramref name="b"/> if necessary, so that <paramref name="a"/> &lt;= <paramref name="b"/>.
        /// </summary>
        public static void Asc<T>(ref T a, ref T b) where T : IComparable<T>
        {
            if (a.CompareTo(b) > 0)
                (a, b) = (b, a);
        }
    }
}
