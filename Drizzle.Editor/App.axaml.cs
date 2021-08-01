using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Drizzle.Editor.ViewModels;
using Drizzle.Editor.Views;

namespace Drizzle.Editor
{
    public class App : Application
    {
        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public override void OnFrameworkInitializationCompleted()
        {
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                var viewModel = new MainWindowViewModel();
                desktop.MainWindow = new MainWindow
                {
                    DataContext = viewModel,
                };

                viewModel.LingoVM.Init();
            }

            base.OnFrameworkInitializationCompleted();
        }
    }
}
