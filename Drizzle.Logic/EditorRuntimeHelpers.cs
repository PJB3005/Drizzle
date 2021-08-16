using System.IO;
using Drizzle.Lingo.Runtime;
using Drizzle.Ported;

namespace Drizzle.Logic
{
    public static class EditorRuntimeHelpers
    {
        public static void RunStartup(LingoRuntime runtime)
        {
            var startUp = runtime.CreateScript<startUp>();

            startUp.exitframe();
        }

        public static void RunLoadLevel(LingoRuntime runtime, string filePath)
        {
            var abs = Path.GetFullPath(filePath);

            var withoutExt = Path.Combine(
                Path.GetDirectoryName(abs)!,
                Path.GetFileNameWithoutExtension(abs));

            runtime.CreateScript<loadLevel>().loadlevel(withoutExt, new LingoNumber(1));
        }
    }
}
