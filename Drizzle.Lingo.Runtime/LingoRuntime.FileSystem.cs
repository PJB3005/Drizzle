using System.IO;

namespace Drizzle.Lingo.Runtime
{
    public partial class LingoRuntime
    {
        // TODO unhardcode this or ship the files with Drizzle.
        public const string MovieBasePath =
            @"C:\Users\Pieter-Jan Briers\Applications\Rain World Level Editor\RWEditor160131\RWEditor2";

        public string GetFilePath(string relPath) => Path.Combine(MovieBasePath, relPath);
    }
}
