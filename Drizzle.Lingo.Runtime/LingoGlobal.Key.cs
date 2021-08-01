using System;

namespace Drizzle.Lingo.Runtime
{
    public sealed partial class LingoGlobal
    {
        public Key _key { get; private set; } = default!;

        public sealed class Key
        {
            private readonly LingoGlobal _global;

            public Key(LingoGlobal global)
            {
                _global = global;
            }

            public int keypressed(dynamic a)
            {
                return 0;
            }

            public int keypressed(int a)
            {
                return 0;
            }
        }
    }
}
