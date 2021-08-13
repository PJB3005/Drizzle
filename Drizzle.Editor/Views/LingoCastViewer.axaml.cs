using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Drizzle.Editor.ViewModels;

namespace Drizzle.Editor.Views
{
    public sealed class LingoCastViewer : Window
    {
        public LingoCastViewer()
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

        private void OnClosed(object? sender, EventArgs e)
        {
            (DataContext as LingoCastViewerViewModel)?.Closed();
        }

        private void OpOpened(object? sender, EventArgs e)
        {
            (DataContext as LingoCastViewerViewModel)?.Opened();
        }
    }
}
