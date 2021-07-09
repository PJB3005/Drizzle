using System;

namespace Drizzle.Lingo.Runtime
{
    public sealed partial class LingoGlobal
    {
        public Key _key { get; private set; }

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
        }
    }
}
