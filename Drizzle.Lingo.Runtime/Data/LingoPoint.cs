using System;
using System.Numerics;

namespace Drizzle.Lingo.Runtime
{
    public struct LingoPoint : IEquatable<LingoPoint>
    {
        // Yes, of course
        // Despite what the documentation clearly states
        // These contain float coordinates, not int.

        public LingoDecimal loch;
        public LingoDecimal locv;

        public Vector2 AsVector2 => new((float)loch.Value, (float)locv.Value);

        public LingoPoint(int loch, int locv)
        {
            this.loch = loch;
            this.locv = locv;
        }

        public LingoPoint(LingoDecimal loch, LingoDecimal locv)
        {
            this.loch = loch;
            this.locv = locv;
        }

        public static LingoPoint operator +(LingoPoint a, LingoPoint b)
        {
            return new(a.loch + b.loch, a.locv + b.locv);
        }

        public static LingoPoint operator -(LingoPoint a, LingoPoint b)
        {
            return new(a.loch - b.loch, a.locv - b.locv);
        }

        public static LingoPoint operator *(LingoPoint a, LingoPoint b)
        {
            return new(a.loch * b.loch, a.locv * b.locv);
        }

        public static LingoPoint operator /(LingoPoint a, LingoPoint b)
        {
            return new(a.loch / b.loch, a.locv / b.locv);
        }

        public static LingoPoint operator +(LingoPoint a, LingoDecimal b)
        {
            return new(a.loch + b, a.locv + b);
        }

        public static LingoPoint operator -(LingoPoint a, LingoDecimal b)
        {
            return new(a.loch - b, a.locv - b);
        }

        public static LingoPoint operator *(LingoPoint a, LingoDecimal b)
        {
            return new(a.loch * b, a.locv * b);
        }

        public static LingoPoint operator /(LingoPoint a, LingoDecimal b)
        {
            return new(a.loch / b, a.locv / b);
        }

        public static LingoPoint operator -(LingoPoint a)
        {
            return new LingoPoint(-a.loch, -a.locv);
        }

        public int inside(LingoRect rect)
        {
            var b = rect.left <= loch && rect.top <= locv &&
                    rect.right > loch && rect.bottom > locv;

            return b ? 1 : 0;
        }

        public bool Equals(LingoPoint other)
        {
            return loch.Equals(other.loch) && locv.Equals(other.locv);
        }

        public override bool Equals(object? obj)
        {
            return obj is LingoPoint other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(loch, locv);
        }

        public static bool operator ==(LingoPoint left, LingoPoint right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(LingoPoint left, LingoPoint right)
        {
            return !left.Equals(right);
        }

        public override string ToString() => $"point({loch}, {locv})";
    }
}
