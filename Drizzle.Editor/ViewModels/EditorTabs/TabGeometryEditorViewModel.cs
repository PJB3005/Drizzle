using ReactiveUI.Fody.Helpers;

namespace Drizzle.Editor.ViewModels.EditorTabs;

public sealed class TabGeometryEditorViewModel : EditorTabViewModelBase
{
    public override string Title => "Geometry";

    public EditorContentViewModel ParentVm { get; }

    [Reactive] public GeometryPlacementTool PlacingTool { get; set; } = GeometryPlacementTool.Wall;

    [Reactive] public bool Layer1Visible { get; set; } = true;
    [Reactive] public bool Layer2Visible { get; set; } = true;
    [Reactive] public bool Layer3Visible { get; set; } = true;

    public TabGeometryEditorViewModel(EditorContentViewModel parentVm)
    {
        ParentVm = parentVm;
    }
}

public enum GeometryPlacementTool : byte
{
    // Tile geometry
    Wall,
    Slope,
    Floor,
    Glass,

    // Tile features
    BeamHorizontal,
    BeamVertical,
    Hive,
    ShortcutEntrance,
    Shortcut,
    Entrance,
    DragonDen,
    Rock,
    Spear,
    Crack,
    ForbidFlyChains,
    GarbageWormHole,
    Waterfall,
    WhackAMoleHole,
    WormGrass,
    ScavengerHole,
}