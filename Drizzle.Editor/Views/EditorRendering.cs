using Avalonia;
using Avalonia.Media;
using Drizzle.Ported;

namespace Drizzle.Editor.Views;

public static class EditorRendering
{
    public static void DrawTileGeometry(
        float offsetX,
        float offsetY,
        float tileSize,
        StreamGeometryContext ctx,
        TileGeometry geometry)
    {
        switch (geometry)
        {
            case TileGeometry.Air:
                break;
            case TileGeometry.SolidWall:
                ctx.BeginFigure(new Point(offsetX, offsetY), true);
                ctx.LineTo(new Point(offsetX + tileSize, offsetY));
                ctx.LineTo(new Point(offsetX + tileSize, offsetY + tileSize));
                ctx.LineTo(new Point(offsetX, offsetY + tileSize));
                ctx.EndFigure(true);
                break;
            case TileGeometry.SlopeBL:
                ctx.BeginFigure(new Point(offsetX, offsetY), true);
                ctx.LineTo(new Point(offsetX + tileSize, offsetY + tileSize));
                ctx.LineTo(new Point(offsetX, offsetY + tileSize));
                ctx.EndFigure(true);
                break;
            case TileGeometry.SlopeBR:
                ctx.BeginFigure(new Point(offsetX + tileSize, offsetY), true);
                ctx.LineTo(new Point(offsetX + tileSize, offsetY + tileSize));
                ctx.LineTo(new Point(offsetX, offsetY + tileSize));
                ctx.EndFigure(true);
                break;
            case TileGeometry.SlopeTL:
                ctx.BeginFigure(new Point(offsetX, offsetY), true);
                ctx.LineTo(new Point(offsetX + tileSize, offsetY));
                ctx.LineTo(new Point(offsetX, offsetY + tileSize));
                ctx.EndFigure(true);
                break;
            case TileGeometry.SlopeTR:
                ctx.BeginFigure(new Point(offsetX, offsetY), true);
                ctx.LineTo(new Point(offsetX + tileSize, offsetY));
                ctx.LineTo(new Point(offsetX + tileSize, offsetY + tileSize));
                ctx.EndFigure(true);
                break;
            case TileGeometry.Floor:
                ctx.BeginFigure(new Point(offsetX, offsetY), true);
                ctx.LineTo(new Point(offsetX + tileSize, offsetY));
                ctx.LineTo(new Point(offsetX + tileSize, offsetY + tileSize / 2));
                ctx.LineTo(new Point(offsetX, offsetY + tileSize / 2));
                ctx.EndFigure(true);
                break;
            case TileGeometry.Glass:
                break;
        }
    }

    public static void DrawBeamVertical(
        float offsetX,
        float offsetY,
        float tileSize,
        StreamGeometryContext ctx)
    {
        ctx.BeginFigure(new Point(offsetX + tileSize * 2f / 5f, offsetY), true);
        ctx.LineTo(new Point(offsetX + tileSize * 3f / 5f, offsetY));
        ctx.LineTo(new Point(offsetX + tileSize * 3f / 5f, offsetY + tileSize));
        ctx.LineTo(new Point(offsetX + tileSize * 2f / 5f, offsetY + tileSize));
        ctx.EndFigure(true);
    }


    public static void DrawBeamHorizontal(
        float offsetX,
        float offsetY,
        float tileSize,
        StreamGeometryContext ctx)
    {
        ctx.BeginFigure(new Point(offsetX, offsetY + tileSize * 2f / 5f), true);
        ctx.LineTo(new Point(offsetX, offsetY + tileSize * 3f / 5f));
        ctx.LineTo(new Point(offsetX + tileSize, offsetY + tileSize * 3f / 5f));
        ctx.LineTo(new Point(offsetX + tileSize, offsetY + tileSize * 2f / 5f));
        ctx.EndFigure(true);
    }
}