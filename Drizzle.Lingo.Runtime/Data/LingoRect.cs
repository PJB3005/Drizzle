using System;

namespace Drizzle.Lingo.Runtime
{
    public struct LingoRect : IEquatable<LingoRect>
    {
        public LingoNumber left;
        public LingoNumber top;
        public LingoNumber right;
        public LingoNumber bottom;

        public LingoNumber width => right - left;
        public LingoNumber height => bottom - top;

        public LingoRect(LingoNumber left, LingoNumber top, LingoNumber right, LingoNumber bottom)
        {
            this.left = left;
            this.top = top;
            this.right = right;
            this.bottom = bottom;
        }

        // Yes, constructing a rect out of points rounds them.
        public LingoRect(LingoPoint lt, LingoPoint rb)
            : this(lt.loch.integer, lt.locv.integer, rb.loch.integer, rb.locv.integer)
        {
        }

        public static LingoRect operator +(LingoRect a, LingoRect b)
        {
            return new(
                a.left + b.left,
                a.top + b.top,
                a.right + b.right,
                a.bottom + b.bottom);
        }

        public static LingoRect operator -(LingoRect a, LingoRect b)
        {
            return new(
                a.left - b.left,
                a.top - b.top,
                a.right - b.right,
                a.bottom - b.bottom);
        }

        public static LingoRect operator *(LingoRect a, LingoNumber b)
        {
            return new(
                a.left * b,
                a.top * b,
                a.right * b,
                a.bottom * b);
        }

        public static LingoRect operator /(LingoRect a, LingoRect b)
        {
            return new(
                a.left / b.left,
                a.top / b.top,
                a.right / b.right,
                a.bottom / b.bottom);
        }

        public bool Equals(LingoRect other)
        {
            return left == other.left && top == other.top && right == other.right && bottom == other.bottom;
        }

        public override bool Equals(object? obj)
        {
            return obj is LingoRect other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(left, top, right, bottom);
        }

        public static bool operator ==(LingoRect left, LingoRect right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(LingoRect left, LingoRect right)
        {
            return !left.Equals(right);
        }

        public override string ToString() => $"rect({left}, {top}, {right}, {bottom})";
    }
}
