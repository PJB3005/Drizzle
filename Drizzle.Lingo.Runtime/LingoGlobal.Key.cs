using System.Collections.Generic;
using Serilog;

namespace Drizzle.Lingo.Runtime
{
    public sealed partial class LingoGlobal
    {
        public Key _key { get; private set; } = default!;

        public sealed class Key
        {
            private static readonly Dictionary<string, int> KeyNameMap = new()
            {
                {"A", 0},
                {"B", 11},
                {"C", 8},
                {"D", 2},
                {"E", 14},
                {"F", 3},
                {"G", 5},
                {"H", 4},
                {"I", 34},
                {"J", 38},
                {"K", 40},
                {"L", 37},
                {"M", 46},
                {"N", 45},
                {"O", 31},
                {"P", 35},
                {"Q", 12},
                {"R", 15},
                {"S", 1},
                {"T", 17},
                {"U", 32},
                {"V", 9},
                {"W", 13},
                {"X", 7},
                {"Y", 16},
                {"Z", 6},
                {"1", 18},
                {"2", 19},
                {"3", 20},
                {"4", 21},
                {"5", 23},
                {"6", 22},
                {"7", 26},
                {"8", 28},
                {"9", 25},
                {"0", 29},
            };

            private readonly LingoGlobal _global;

            public Key(LingoGlobal global)
            {
                _global = global;
            }

            public LingoNumber keypressed(dynamic keyName)
            {
                switch (keyName)
                {
                    case string str:
                        return keypressed(str);
                    case int i:
                        return keypressed(i);
                    default:
                        Log.Warning("Unknown keyPressed() ??? {Value}", keyName);
                        return 0;
                }
            }

            public LingoNumber keypressed(string keyName)
            {
                if (!KeyNameMap.TryGetValue(keyName, out var keyCode))
                {
                    Log.Warning("keyPressed(): Unknown key name {KeyName}", keyName);
                    return 0;
                }

                return keypressed(keyCode);
            }

            public LingoNumber keypressed(int keyCode)
            {
                return _global.LingoRuntime.KeysDown.Contains(keyCode) ? 1 : 0;
            }
        }
    }
}
