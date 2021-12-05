using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Drizzle.Editor.ViewModels.Render;

namespace Drizzle.Editor.Views.Render;

public partial class RenderWindow : Window
{
    public RenderWindow()
    {
        InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }

    private void TopLevel_OnClosed(object? sender, EventArgs e)
    {
        (DataContext as RenderViewModel)?.StopRender();
    }
}