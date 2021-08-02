using System;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Drizzle.Editor.ViewModels;
using Drizzle.Editor.Views;
using Splat;

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
                if (!CommandLineArgs.TryParse(desktop.Args, out var parsed))
                    Environment.Exit(1);

                Locator.CurrentMutable.RegisterConstant(parsed);
                var viewModel = new MainWindowViewModel();
                desktop.MainWindow = new MainWindow
                {
                    DataContext = viewModel,
                };

                viewModel.Init();
            }

            base.OnFrameworkInitializationCompleted();
        }
    }
}
