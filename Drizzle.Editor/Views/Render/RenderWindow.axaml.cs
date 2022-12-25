using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Threading;
using Drizzle.Editor.ViewModels.Render;

namespace Drizzle.Editor.Views.Render;

public partial class RenderWindow : Window
{
    private IDisposable? _renderTimer;

    public RenderWindow()
    {
        InitializeComponent();
#if DEBUG
        this.AttachDevTools();
#endif
    }

    private void TopLevel_OnClosed(object? sender, EventArgs e)
    {
        (DataContext as RenderViewModel)?.StopRender();

        _renderTimer?.Dispose();
    }

    private void TopLevel_OnOpened(object? sender, EventArgs e)
    {
        _renderTimer = DispatcherTimer.Run(() =>
        {
            UpdateElapsedText();
            return true;
        }, TimeSpan.FromMilliseconds(50), DispatcherPriority.Background);
    }

    private void UpdateElapsedText()
    {
        if (DataContext is not RenderViewModel vm)
            return;

        this.FindControl<TextBlock>("ElapsedText").Text = vm.RenderTimeElapsed.ToString(@"mm\:ss\.f");
    }
}
