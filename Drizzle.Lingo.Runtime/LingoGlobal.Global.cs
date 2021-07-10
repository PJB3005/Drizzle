using System;

namespace Drizzle.Lingo.Runtime
{
    public sealed partial class LingoGlobal
    {
        public Global _global { get; private set; }

        public sealed class Global
        {
            private readonly LingoGlobal _global;

            public Global(LingoGlobal global)
            {
                _global = global;
            }

            public void clearglobals()
            {
                throw new NotImplementedException();
            }

        }
    }
}
