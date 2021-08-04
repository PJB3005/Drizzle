using System;

namespace Drizzle.Lingo.Runtime
{
    // Lingo numbers are *not* IEEE-754.
    // Well shit.
    public struct LingoDecimal : IEquatable<LingoDecimal>, IComparable<LingoDecimal>
    {
        // TODO: Implement Lingo arithmetic accurately. If we care.
        public double Value;

        public int integer => (int) Math.Round(Value, MidpointRounding.AwayFromZero);

        public LingoDecimal(double value)
        {
            Value = value;
        }

        public static LingoDecimal Parse(string value) => new LingoDecimal(double.Parse(value));
        public static LingoDecimal Abs(LingoDecimal dec) => new(Math.Abs(dec.Value));
        public static LingoDecimal Sqrt(LingoDecimal dec) => new(Math.Sqrt(dec.Value));
        public static LingoDecimal Cos(LingoDecimal dec) => new(Math.Cos(dec.Value));
        public static LingoDecimal Sin(LingoDecimal dec) => new(Math.Sin(dec.Value));
        public static LingoDecimal Tan(LingoDecimal dec) => new(Math.Tan(dec.Value));
        public static LingoDecimal Atan(LingoDecimal dec) => new(Math.Atan(dec.Value));
        public static LingoDecimal Pow(LingoDecimal @base, LingoDecimal exp) => new(Math.Pow(@base.Value, exp.Value));

        public override string ToString()
        {
            return Value.ToString();
        }

        public static LingoDecimal operator -(LingoDecimal dec) => new(-dec.Value);
        public static LingoDecimal operator +(LingoDecimal dec) => new(+dec.Value);

        public static LingoDecimal operator +(LingoDecimal a, LingoDecimal b) => new(a.Value + b.Value);
        public static LingoDecimal operator -(LingoDecimal a, LingoDecimal b) => new(a.Value - b.Value);
        public static LingoDecimal operator *(LingoDecimal a, LingoDecimal b) => new(a.Value * b.Value);
        public static LingoDecimal operator /(LingoDecimal a, LingoDecimal b) => new(a.Value / b.Value);
        public static LingoDecimal operator %(LingoDecimal a, LingoDecimal b) => new(a.Value % b.Value);

        public static implicit operator LingoDecimal(int x) => new(x);
        public static implicit operator LingoDecimal(float x) => new(x);
        public static explicit operator int(LingoDecimal x) => (int) x.Value;
        public static explicit operator float(LingoDecimal x) => (float) x.Value;

        public bool Equals(LingoDecimal other)
        {
            return Value.Equals(other.Value);
        }

        public override bool Equals(object? obj)
        {
            return obj is LingoDecimal other && Equals(other);
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        public static bool operator ==(LingoDecimal left, LingoDecimal right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(LingoDecimal left, LingoDecimal right)
        {
            return !left.Equals(right);
        }

        public int CompareTo(LingoDecimal other)
        {
            return Value.CompareTo(other.Value);
        }

        public static bool operator <(LingoDecimal left, LingoDecimal right)
        {
            return left.CompareTo(right) < 0;
        }

        public static bool operator >(LingoDecimal left, LingoDecimal right)
        {
            return left.CompareTo(right) > 0;
        }

        public static bool operator <=(LingoDecimal left, LingoDecimal right)
        {
            return left.CompareTo(right) <= 0;
        }

        public static bool operator >=(LingoDecimal left, LingoDecimal right)
        {
            return left.CompareTo(right) >= 0;
        }

        public static bool TryAs(object? obj, out LingoDecimal dec)
        {
            if (obj is LingoDecimal decC)
            {
                dec = decC;
                return true;
            }

            if (obj is int i)
            {
                dec = i;
                return true;
            }

            dec = default;
            return false;
        }
    }
}
