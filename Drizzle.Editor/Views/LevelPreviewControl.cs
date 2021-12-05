using Avalonia.Media;
using Avalonia.ReactiveUI;
using Drizzle.Editor.ViewModels;
using Drizzle.Lingo.Runtime;
using Drizzle.Ported;
using ReactiveUI;

namespace Drizzle.Editor.Views;

public sealed class LevelPreviewControl : ReactiveUserControl<EditorContentViewModel>
{
    private const float TileSize = 5;

    private Brush _brushLayer3 = new SolidColorBrush(Colors.DarkGray);
    private Brush _brushLayer2 = new SolidColorBrush(Colors.Gray);
    private Brush _brushLayer1 = new SolidColorBrush(Colors.Black);
    private Pen _penNone = new Pen(Brushes.Transparent, 0f);

    public LevelPreviewControl()
    {
        this.WhenActivated(disposables =>
        {
            var mv = ViewModel!.Runtime.MovieScript();
            Width = (int)mv.gLOprops.size.loch * TileSize;
            Height = (int)mv.gLOprops.size.locv * TileSize;
        });

        InvalidateVisual();
    }

    public override void Render(DrawingContext context)
    {
        base.Render(context);

        var mv = ViewModel!.Runtime.MovieScript();

        var sizeMaxX = (int)mv.gLOprops.size.loch;
        var sizeMaxY = (int)mv.gLOprops.size.locv;

        for (var layer = 3; layer > 0; layer--)
        {
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

                    var features = (LingoList) dat[2];
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

            context.DrawGeometry(brush, _penNone, geometry);
        }
    }
}