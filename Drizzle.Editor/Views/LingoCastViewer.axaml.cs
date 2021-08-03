using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

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

    }
}
