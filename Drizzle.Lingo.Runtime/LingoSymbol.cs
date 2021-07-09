namespace Drizzle.Lingo.Runtime
{
    public readonly struct LingoSymbol
    {
        public string Value { get; }

        public LingoSymbol(string value)
        {
            Value = value;
        }
    }
}
