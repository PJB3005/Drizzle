using Avalonia.Controls;
using Avalonia.Controls.Templates;
using Avalonia.Layout;
using Avalonia.Markup.Xaml;
using Avalonia.Markup.Xaml.Templates;
using Drizzle.Editor.ViewModels.Render;

namespace Drizzle.Editor.Views.Render
{
    public partial class RenderStageEffectsView : UserControl
    {
        public RenderStageEffectsView()
        {
            InitializeComponent();

            this.FindControl<ItemsControl>("EffectsList").ItemTemplate = new FuncDataTemplate(
                typeof(RenderSingleEffectViewModel),
                (o, _) =>
                {
                    var effect = (RenderSingleEffectViewModel)o;
                    var stackPanel = new StackPanel { Orientation = Orientation.Horizontal };

                    if (effect.Current)
                        stackPanel.Classes.Add("Current");

                    stackPanel.Children.Add(new TextBlock { Text = effect.Name });

                    return stackPanel;
                });
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
