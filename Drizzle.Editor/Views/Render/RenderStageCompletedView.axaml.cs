using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace Drizzle.Editor.Views.Render;

public partial class RenderStageCompletedView : UserControl
{
    public RenderStageCompletedView()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}