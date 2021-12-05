using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace Drizzle.Editor.Views.Render;

public partial class RenderStageLightView : UserControl
{
    public RenderStageLightView()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}