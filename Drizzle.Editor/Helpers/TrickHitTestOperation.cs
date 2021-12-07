using Avalonia;
using Avalonia.Media;
using Avalonia.Media.Immutable;
using Avalonia.Platform;
using Avalonia.Rendering.SceneGraph;

namespace Drizzle.Editor.Helpers;

public sealed class TrickHitTestOperation : ICustomDrawOperation
{
    /*
    private readonly ImmutablePen _penRed = new ImmutablePen(Brushes.Red);
    private readonly ImmutableSolidColorBrush _brushBlue = new ImmutableSolidColorBrush(new Color(0x80, 0x80, 0x80, 0xff));
    */

    public TrickHitTestOperation(Rect bounds)
    {
        Bounds = bounds;
    }

    public void Dispose()
    {
    }

    public bool HitTest(Point p)
    {
        return Bounds.Contains(p);
    }

    public void Render(IDrawingContextImpl context)
    {
        // context.DrawRectangle(_brushBlue, _penRed, new RoundedRect(Bounds));
    }

    public Rect Bounds { get; }

    public bool Equals(ICustomDrawOperation? other)
    {
        return other is TrickHitTestOperation { Bounds: var b } && b == Bounds;
    }
}
