using System;
using System.Linq;
using System.Reactive.Disposables;
using Avalonia;
using Avalonia.Input;
using Avalonia.Media;
using Avalonia.ReactiveUI;
using Drizzle.Editor.Helpers;
using Drizzle.Editor.ViewModels.EditorTabs;
using Drizzle.Lingo.Runtime;
using Drizzle.Logic;
using Drizzle.Ported;
using ReactiveUI;
using Serilog;
using PlaceTool = Drizzle.Editor.ViewModels.EditorTabs.GeometryPlacementTool;
using TGeom = Drizzle.Ported.TileGeometry;

namespace Drizzle.Editor.Views.EditorTabs;

public sealed class GeometryEditorControl : ReactiveUserControl<TabGeometryEditorViewModel>
{
    public const float TileSize = 20f;

    private Brush _brushLayer3 = new SolidColorBrush(new Color(96, 192, 64, 64));
    private Brush _brushLayer2 = new SolidColorBrush(new Color(96, 32, 128, 32));
    private Brush _brushLayer1 = new SolidColorBrush(Colors.Black);
    private Pen _penGridThin = new Pen(new SolidColorBrush(Color.Parse("#B5D28866")), 0.5f);
    private Pen _penGridThick = new Pen(new SolidColorBrush(Color.Parse("#B5D28866")), 1f);

    private bool _currentlyDragging;
    private bool _erasing;
    private Vector2i? _rectDragStart;
    private Vector2i _lastPos;

    private LingoRuntime Runtime => ViewModel!.ParentVm.Runtime;
    private MovieScript MovieScript => Runtime.MovieScript();

    public PlaceTool PlacingTool { get; set; }

    public GeometryEditorControl()
    {
        this.WhenActivated(disposables =>
        {
            var mv = ViewModel!.ParentVm.Runtime.MovieScript();
            Width = (int)mv.gLOprops.size.loch * TileSize;
            Height = (int)mv.gLOprops.size.locv * TileSize;

            this.WhenAnyValue(
                    x => x.ViewModel!.Layer1Visible,
                    x => x.ViewModel!.Layer2Visible,
                    x => x.ViewModel!.Layer3Visible)
                .Subscribe(_ =>
                {
                    Log.Debug("Updating visibility: invalidating visual");
                    InvalidateVisual();
                })
                .DisposeWith(disposables);

            this.WhenAnyValue(x => x.ViewModel!.PlacingTool)
                .Subscribe(t => PlacingTool = t)
                .DisposeWith(disposables);
        });

        InvalidateVisual();
    }

    protected override void OnPointerPressed(PointerPressedEventArgs e)
    {
        var position = e.GetCurrentPoint(this);
        if (position.Properties.PointerUpdateKind is not
            (PointerUpdateKind.LeftButtonPressed or PointerUpdateKind.RightButtonPressed))
        {
            base.OnPointerPressed(e);
            return;
        }

        _erasing = position.Properties.PointerUpdateKind is PointerUpdateKind.RightButtonPressed;
        _currentlyDragging = true;
        var pos = PosToGridPos(position.Position);

        _lastPos = pos;
        if ((e.KeyModifiers & KeyModifiers.Control) != 0)
            _rectDragStart = pos;
        else
            TryPlace(pos, MovieScript);

        e.Handled = true;
        // Log.Debug("Down: {Pos} {Erasing} {IsRect}", pos, _erasing, _rectDragStart.HasValue);
    }

    protected override void OnPointerMoved(PointerEventArgs e)
    {
        if (!_currentlyDragging)
        {
            base.OnPointerMoved(e);
            return;
        }

        var position = e.GetCurrentPoint(this);
        var pos = PosToGridPos(position.Position);
        if (pos == _lastPos)
            return;

        var last = _lastPos;
        _lastPos = pos;

        if (_rectDragStart.HasValue)
        {
            InvalidateVisual();
            return;
        }

        // Draw line between last and new to make sure there's no gaps if the mouse moves too fast.
        foreach (var placePos in Bresenham.PlotLine(last, pos))
        {
            TryPlace(placePos, MovieScript);
        }

        e.Handled = true;
    }

    protected override void OnPointerReleased(PointerReleasedEventArgs e)
    {
        var position = e.GetCurrentPoint(this);
        if (position.Properties.PointerUpdateKind is not
            (PointerUpdateKind.LeftButtonReleased or PointerUpdateKind.RightButtonReleased))
        {
            base.OnPointerReleased(e);
            return;
        }

        _currentlyDragging = false;

        if (_rectDragStart is var (x0, y0))
        {
            var (x1, y1) = PosToGridPos(position.Position);

            Swap.Asc(ref x0, ref x1);
            Swap.Asc(ref y0, ref y1);

            var sizeMaxX = (int)MovieScript.gLOprops.size.loch;
            var sizeMaxY = (int)MovieScript.gLOprops.size.locv;

            for (var x = x0; x <= x1; x++)
            for (var y = y0; y <= y1; y++)
            {
                if (x < 1 || x > sizeMaxX || y < 1 || y > sizeMaxY)
                    continue;

                TryPlace((x, y), MovieScript);
            }

            _rectDragStart = null;
        }

        InvalidateVisual();
        _erasing = false;
        e.Handled = true;
    }

    private static Vector2i PosToGridPos(Point point)
    {
        var gridPos = point / TileSize;
        var x = (int)gridPos.X + 1;
        var y = (int)gridPos.Y + 1;
        return (x, y);
    }

    private void TryPlace(Vector2i pos, MovieScript mv)
    {
        var (x, y) = pos;
        var dat = mv.gLEProps.matrix[x][y][1];

        switch (PlacingTool)
        {
            case GeometryPlacementTool.Wall:
                ChangeGeometry(TileGeometry.SolidWall);
                break;
            case GeometryPlacementTool.Floor:
                ChangeGeometry(TileGeometry.Floor);
                break;
            case GeometryPlacementTool.Glass:
                ChangeGeometry(TileGeometry.Glass);
                break;
            case GeometryPlacementTool.Slope:
                if (_erasing)
                    // Just fast path this if clearing no need to do in depth stuff for slope placement.
                    ChangeGeometry(TileGeometry.Air);

                // C# 11 array pattern matching when
                var combination = string.Join("", new Vector2i[]
                    {
                        new(-1, 0),
                        new(0, -1),
                        new(1, 0),
                        new(0, 1)
                    }
                    .Select(o => EditorRuntimeHelpers.GetTileGeomBordered(Runtime, o + pos, 1))
                    .Select(o => ((int)o).ToString())
                    .ToArray());

                var result = combination switch
                {
                    "1001" => TGeom.SlopeBL,
                    "0011" => TGeom.SlopeBR,
                    "1100" => TGeom.SlopeTL,
                    "0110" => TGeom.SlopeTR,
                    _ => TGeom.Air,
                };

                if (result == TileGeometry.Air)
                    return;

                ChangeGeometry(result);
                break;
            case GeometryPlacementTool.BeamVertical:
                ChangeFeature(TileFeature.BeamVertical);
                break;
            case GeometryPlacementTool.BeamHorizontal:
                ChangeFeature(TileFeature.BeamHorizontal);
                break;
        }

        InvalidateVisual();

        void ChangeGeometry(TileGeometry to)
        {
            if (_erasing)
                to = TileGeometry.Air;

            dat[1] = new LingoNumber((int)to);
        }

        void ChangeFeature(TileFeature feature)
        {
            var featureList = (LingoList)dat[2];
            var featureNum = (LingoNumber)(int)feature;

            if (_erasing)
                featureList.List.Remove(featureNum);
            else if (!featureList.List.Contains(featureNum))
                featureList.List.Add(featureNum);
        }
    }

    public override void Render(DrawingContext context)
    {
        base.Render(context);

        context.Custom(
            new TrickHitTestOperation(
                new Rect(0, 0, Bounds.Width, Bounds.Height),
#pragma warning disable 618
                context.CurrentContainerTransform.Invert()));
#pragma warning restore 618

        var mv = MovieScript;

        var sizeMaxX = (int)mv.gLOprops.size.loch;
        var sizeMaxY = (int)mv.gLOprops.size.locv;

        for (var layer = 1; layer <= 3; layer++)
        {
            switch (layer)
            {
                case 1 when !ViewModel!.Layer1Visible:
                case 2 when !ViewModel!.Layer2Visible:
                case 3 when !ViewModel!.Layer3Visible:
                    continue;
            }

            var brush = layer switch
            {
                1 => _brushLayer1,
                2 => _brushLayer2,
                _ => _brushLayer3,
            };

            var geometry = new StreamGeometry();
            var gCtx = geometry.Open();

            for (var x = 1; x <= sizeMaxX; x++)
            {
                var column = mv.gLEProps.matrix[x];
                for (var y = 1; y <= sizeMaxY; y++)
                {
                    var offsetX = (x - 1) * TileSize;
                    var offsetY = (y - 1) * TileSize;

                    var dat = column[y][layer];
                    EditorRendering.DrawTileGeometry(offsetX, offsetY, TileSize, gCtx, (TileGeometry)(int)dat[1]);

                    var features = (LingoList)dat[2];
                    foreach (var feature in features.List)
                    {
                        switch ((TileFeature)(int)(LingoNumber)feature!)
                        {
                            case TileFeature.BeamHorizontal:
                                EditorRendering.DrawBeamHorizontal(offsetX, offsetY, TileSize, gCtx);
                                break;
                            case TileFeature.BeamVertical:
                                EditorRendering.DrawBeamVertical(offsetX, offsetY, TileSize, gCtx);
                                break;
                        }
                    }
                }
            }

            context.DrawGeometry(brush, null, geometry);
        }

        var gridThinGeometry = new StreamGeometry();
        var gridThickGeometry = new StreamGeometry();
        var gtnCtx = gridThinGeometry.Open();
        var gtkCtx = gridThickGeometry.Open();

        for (var x = 0; x < sizeMaxX; x++)
        {
            var ctx = x % 2 == 0 ? gtnCtx : gtkCtx;
            var xPos = x * TileSize;
            ctx.BeginFigure(new Point(xPos, 0), false);
            ctx.LineTo(new Point(xPos, sizeMaxY * TileSize));
            ctx.EndFigure(false);
        }

        for (var y = 0; y < sizeMaxY; y++)
        {
            var ctx = y % 2 == 0 ? gtnCtx : gtkCtx;
            var yPos = y * TileSize;
            ctx.BeginFigure(new Point(0, yPos), false);
            ctx.LineTo(new Point(sizeMaxX * TileSize, yPos));
            ctx.EndFigure(false);
        }

        gtnCtx.Dispose();
        gtkCtx.Dispose();

        context.DrawGeometry(null, _penGridThin, gridThinGeometry);
        context.DrawGeometry(null, _penGridThick, gridThickGeometry);

        if (_rectDragStart is var (rectStartX, rectStartY))
        {
            var (rectEndX, rectEndY) = _lastPos;

            Swap.Asc(ref rectStartX, ref rectEndX);
            Swap.Asc(ref rectStartY, ref rectEndY);

            var rect = new Rect(
                (rectStartX - 1) * TileSize,
                (rectStartY - 1) * TileSize,
                (rectEndX - rectStartX + 1) * TileSize,
                (rectEndY - rectStartY + 1) * TileSize);

            context.DrawRectangle(null, new Pen(Brushes.Red, 2), rect);
        }
    }
}