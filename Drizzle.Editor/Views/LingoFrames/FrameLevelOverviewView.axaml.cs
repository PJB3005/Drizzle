using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace Drizzle.Editor.Views.LingoFrames;

public sealed partial class FrameLevelOverviewView : UserControl
{
    public FrameLevelOverviewView()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}