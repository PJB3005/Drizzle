using System;
using System.IO;

namespace Drizzle.Lingo.Runtime.Xtra
{
    public sealed class FileIOXtra : BaseXtra
    {
        private FileStream? _file;
        private StreamReader? _reader;

        public void openfile(string filePath, int mode = 0)
        {
            var access = mode switch
            {
                0 => FileAccess.ReadWrite,
                1 => FileAccess.Read,
                2 => FileAccess.Write,
                _ => throw new ArgumentException("Invalid file mode!")
            };

            _file = File.Open(filePath, FileMode.Open, access);
            _reader = new StreamReader(_file, leaveOpen: true);
        }

        public void closefile()
        {
            _file?.Dispose();
            _file = null;
            _reader = null;
        }

        public string readfile()
        {
            return _reader!.ReadToEnd();
        }

        public override BaseXtra Duplicate()
        {
            return new FileIOXtra();
        }
    }
}
