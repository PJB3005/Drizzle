using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Logging;
using Avalonia.Markup.Xaml;
using Drizzle.Editor.ViewModels;
using Drizzle.Editor.Views;
using Serilog;
using Serilog.Sinks.SystemConsole.Themes;
using SS14.Launcher;
using LogEventLevel = Serilog.Events.LogEventLevel;

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
#if DEBUG
            Logger.Sink = new AvaloniaSeriLogger(new LoggerConfiguration()
                .MinimumLevel.Is(LogEventLevel.Warning)
                .Enrich.FromLogContext()
                .WriteTo.Console(
                    outputTemplate: "[{Area}] {Message} ({SourceType} #{SourceHash})\n",
                    theme: AnsiConsoleTheme.Literate)
                .CreateLogger());
#endif

            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                if (!CommandLineArgs.TryParse(desktop.Args, out var parsed))
                    Environment.Exit(1);

                var viewModel = new MainWindowViewModel();
                desktop.MainWindow = new MainWindow
                {
                    DataContext = viewModel,
                };
                desktop.ShutdownMode = ShutdownMode.OnMainWindowClose;

                viewModel.Init(parsed);
            }

            base.OnFrameworkInitializationCompleted();
        }
    }
}
