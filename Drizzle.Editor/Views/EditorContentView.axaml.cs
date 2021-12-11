using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace Drizzle.Editor.Views;

public sealed partial class EditorContentView : UserControl
{
    public EditorContentView()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}
