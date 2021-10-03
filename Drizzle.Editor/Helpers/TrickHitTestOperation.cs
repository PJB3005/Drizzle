using Avalonia;
using Avalonia.Media;
using Avalonia.Media.Immutable;
using Avalonia.Platform;
using Avalonia.Rendering.SceneGraph;

namespace Drizzle.Editor.Helpers
{
    public sealed class TrickHitTestOperation : ICustomDrawOperation
    {
        private readonly Matrix _transformInvert;
        /*
        private readonly ImmutablePen _penRed = new ImmutablePen(Brushes.Red);
        private readonly ImmutableSolidColorBrush _brushBlue = new ImmutableSolidColorBrush(new Color(0x80, 0x80, 0x80, 0xff));
        */

        public TrickHitTestOperation(Rect bounds, Matrix transformInvert)
        {
            _transformInvert = transformInvert;
            Bounds = bounds;
        }

        public void Dispose()
        {
        }

        public bool HitTest(Point p)
        {
            // Avalonia currently has a bug where HitTest() gets bogus coords (matrix applied the wrong way).
            // This fixes that.
            // https://github.com/AvaloniaUI/Avalonia/pull/6657
            return Bounds.Contains(p * _transformInvert * _transformInvert);
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
}
