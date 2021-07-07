namespace Drizzle.Lingo
{
    // Lingo numbers are *not* IEEE-754.
    // Well shit.
    public struct LingoDecimal
    {
        // TODO: Implement Lingo arithmetic accurately. If we care.
        public double Value;

        public LingoDecimal(double value)
        {
            Value = value;
        }

        public static LingoDecimal Parse(string value)
        {
            return new LingoDecimal(double.Parse(value));
        }

        public override string ToString()
        {
            return Value.ToString();
        }
    }
}
