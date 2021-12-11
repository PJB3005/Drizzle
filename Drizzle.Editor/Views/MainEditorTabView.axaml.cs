using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using JetBrains.Annotations;

namespace Drizzle.Editor.Views;

[UsedImplicitly]
public sealed partial class MainEditorTabView : UserControl
{
    public MainEditorTabView()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}
