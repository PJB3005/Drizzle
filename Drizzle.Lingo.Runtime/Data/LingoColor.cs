using System;
using System.Collections.Generic;
using Serilog;

namespace Drizzle.Lingo.Runtime
{
    public struct LingoColor : IEquatable<LingoColor>
    {
        private static readonly Dictionary<int, (int r, int g, int b)> Palette = new()
        {
            [0] = (255, 255, 255),
            [255] = (0, 0, 0)
        };

        public int red;
        public int green;
        public int blue;

        public LingoColor(int r, int g, int b)
        {
            red = r;
            green = g;
            blue = b;
        }

        public static implicit operator LingoColor(int paletteIndex)
        {
            if (!Palette.TryGetValue(paletteIndex, out var color))
            {
                Log.Warning("Unknown palette color: {PaletteIndex}", paletteIndex);
                color = (255, 255, 255);
            }

            return new LingoColor(color.r, color.g, color.b);
        }

        public bool Equals(LingoColor other)
        {
            return red == other.red && green == other.green && blue == other.blue;
        }

        public override bool Equals(object? obj)
        {
            return obj is LingoColor other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(red, green, blue);
        }

        public static bool operator ==(LingoColor left, LingoColor right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(LingoColor left, LingoColor right)
        {
            return !left.Equals(right);
        }
    }
}
