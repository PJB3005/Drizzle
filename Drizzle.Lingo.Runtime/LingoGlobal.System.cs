using System;

namespace Drizzle.Lingo.Runtime
{
    public sealed partial class LingoGlobal
    {
        public System _system { get; private set; } = default!;

        public sealed class System
        {
            private readonly LingoGlobal _global;

            public System(LingoGlobal global)
            {
                _global = global;
            }

            public int milliseconds => throw new NotImplementedException();
        }
    }
}
