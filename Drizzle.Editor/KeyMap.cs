using System.Collections.Generic;
using Avalonia.Input;

namespace Drizzle.Editor;

/// <summary>
///     Avalonia -> Lingo key map.
/// </summary>
public static class KeyMap
{
    public static readonly Dictionary<Key, int> Map = new()
    {
        { Key.Return, 36 },
        { Key.Up, 126 },
        { Key.Down, 125 },
        { Key.Left, 123 },
        { Key.Right, 124 },
    };
}