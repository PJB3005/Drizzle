namespace Drizzle.Lingo.Runtime
{
    public sealed partial class LingoGlobal
    {
        public Mouse _mouse { get; private set; } = default!;

        public sealed class Mouse
        {
            private readonly LingoGlobal _global;

            public Mouse(LingoGlobal global)
            {
                _global = global;
            }

            public LingoPoint mouseloc => default;
            public LingoNumber mousedown => 0;
            public LingoNumber rightmousedown => 0;
        }

    }
}
