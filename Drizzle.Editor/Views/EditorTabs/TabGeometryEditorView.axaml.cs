using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace Drizzle.Editor.Views.EditorTabs
{
    public partial class TabGeometryEditorView : UserControl
    {
        public TabGeometryEditorView()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
