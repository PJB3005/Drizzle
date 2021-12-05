using System;

namespace Drizzle.Lingo.Runtime;

public struct LingoRect : IEquatable<LingoRect>, ILingoVector
{
    public LingoNumber left;
    public LingoNumber top;
    public LingoNumber right;
    public LingoNumber bottom;

    public LingoNumber width => right - left;
    public LingoNumber height => bottom - top;

    int ILingoVector.CountElems => 4;

    object ILingoVector.this[int index] => index switch
    {
        0 => left,
        1 => top,
        2 => right,
        3 => bottom,
        _ => throw new ArgumentOutOfRangeException()
    };

    private LingoRect(LingoNumber all) : this(all, all, all, all)
    {
    }

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

    public static LingoRect operator *(LingoRect a, LingoRect b)
    {
        return new(
            a.left * b.left,
            a.top * b.top,
            a.right * b.right,
            a.bottom * b.bottom);
    }

    public static LingoRect operator /(LingoRect a, LingoRect b)
    {
        return new(
            a.left / b.left,
            a.top / b.top,
            a.right / b.right,
            a.bottom / b.bottom);
    }

    public static LingoRect operator %(LingoRect a, LingoRect b)
    {
        return new(
            a.left % b.left,
            a.top % b.top,
            a.right % b.right,
            a.bottom % b.bottom);
    }

    public static LingoRect operator +(LingoRect a, LingoNumber b) => a + new LingoRect(b);
    public static LingoRect operator +(LingoNumber a, LingoRect b) => new LingoRect(a) + b;
    public static LingoRect operator -(LingoRect a, LingoNumber b) => a - new LingoRect(b);
    public static LingoRect operator -(LingoNumber a, LingoRect b) => new LingoRect(a) - b;
    public static LingoRect operator *(LingoRect a, LingoNumber b) => a * new LingoRect(b);
    public static LingoRect operator *(LingoNumber a, LingoRect b) => new LingoRect(a) * b;
    public static LingoRect operator /(LingoRect a, LingoNumber b) => a / new LingoRect(b);
    public static LingoRect operator /(LingoNumber a, LingoRect b) => new LingoRect(a) / b;
    public static LingoRect operator %(LingoRect a, LingoNumber b) => a % new LingoRect(b);
    public static LingoRect operator %(LingoNumber a, LingoRect b) => new LingoRect(a) % b;

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