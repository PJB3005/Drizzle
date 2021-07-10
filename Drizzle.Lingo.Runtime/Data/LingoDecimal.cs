using System;

namespace Drizzle.Lingo.Runtime
{
    // Lingo numbers are *not* IEEE-754.
    // Well shit.
    public struct LingoDecimal
    {
        // TODO: Implement Lingo arithmetic accurately. If we care.
        public double Value;

        public int integer => (int) Value;

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

        public static implicit operator LingoDecimal(int x) => new(x);
        public static explicit operator int(LingoDecimal x) => (int) x.Value;
    }
}
