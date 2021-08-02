using Drizzle.Lingo.Runtime;
using Drizzle.Ported;

namespace Drizzle.Logic
{
    public sealed class MapEditorRuntime
    {
        public LingoRuntime LingoRuntime { get; }

        public MapEditorRuntime(LingoRuntime runtime)
        {
            LingoRuntime = runtime;
        }
    }
}
