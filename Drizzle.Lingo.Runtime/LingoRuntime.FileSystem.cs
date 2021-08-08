using System.IO;
using System.Reflection;

namespace Drizzle.Lingo.Runtime
{
    public partial class LingoRuntime
    {
        public static readonly string MovieBasePath;

        static LingoRuntime()
        {
            var path = Path.Combine(Assembly.GetEntryAssembly()!.Location, "..", "..", "..", "..", "..", "Data");
            MovieBasePath = Path.GetFullPath(path) + Path.DirectorySeparatorChar;
            CastPath = Path.Combine(MovieBasePath, "Cast");
        }

        public string GetFilePath(string relPath) => Path.Combine(MovieBasePath, relPath);
    }
}
