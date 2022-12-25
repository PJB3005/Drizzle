using Avalonia;
using Avalonia.ReactiveUI;
using Drizzle.Lingo.Runtime.Utils;
using Serilog;
using Serilog.Sinks.SystemConsole.Themes;
using SixLabors.ImageSharp;

namespace Drizzle.Editor;

class Program
{
    // Initialization code. Don't use any Avalonia, third-party APIs or any
    // SynchronizationContext-reliant code before AppMain is called: things aren't initialized
    // yet and stuff might break.
    public static void Main(string[] args)
    {
        CultureFix.FixCulture();

        Configuration.Default.PreferContiguousImageBuffers = true;

        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .WriteTo.Console(theme: AnsiConsoleTheme.Literate)
            .CreateLogger();

        BuildAvaloniaApp()
            .StartWithClassicDesktopLifetime(args);
    }

    // Avalonia configuration, don't remove; also used by visual designer.
    public static AppBuilder BuildAvaloniaApp()
        => AppBuilder.Configure<App>()
            .UsePlatformDetect()
            .LogToTrace()
            .UseReactiveUI();
}
