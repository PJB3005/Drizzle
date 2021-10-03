using System.Collections.Generic;
using Drizzle.Logic;

namespace Drizzle.Editor.Helpers
{
    public static class Bresenham
    {
        public static IEnumerable<Vector2i> PlotLine(Vector2i from, Vector2i to)
        {
            // From https://circuitcellar.com/resources/bresenhams-algorithm/
            var (x0, y0) = from;
            var (x1, y1) = to;

            var dx = x1 >= x0 ? x1 - x0 : x0 - x1;
            var dy = y1 >= y0 ? y0 - y1 : y1 - y0;
            var sx = x0 < x1 ? 1 : -1;
            var sy = y0 < y1 ? 1 : -1;
            var err = dx + dy;
            var x = x0;
            var y = y0;

            while (true)
            {
                yield return (x, y);
                if (x == x1 && y == y1)
                    break;

                var e2 = 2 * err;
                if (e2 >= dy)
                {
                    // step x
                    err += dy;
                    x += sx;
                }

                if (e2 <= dx)
                {
                    // step y
                    err += dx;
                    y += sy;
                }
            }
        }
    }
}
