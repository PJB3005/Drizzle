using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Drizzle.ConsoleApp;
using Drizzle.Lingo.Runtime;
using Drizzle.Logic;
using Drizzle.Logic.Rendering;
using Drizzle.Ported;
using Serilog;
using Serilog.Sinks.SystemConsole.Themes;

if (!CommandLineArgs.TryParse(args, out var parsedArgs))
    return 1;

var isCi = Environment.GetEnvironmentVariable("CI") == "true";

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Error()
    .WriteTo.Console(theme: AnsiConsoleTheme.Literate)
    .CreateLogger();

return parsedArgs.Verb switch
{
    CommandLineArgs.VerbRender render => DoCmdRender(render),
    _ => throw new ArgumentOutOfRangeException()
};

int DoCmdRender(CommandLineArgs.VerbRender options)
{
    Console.WriteLine("Initializing Zygote runtime");

    var zygote = MakeZygoteRuntime();

    Console.WriteLine($"Starting render of {options.Levels.Count} levels");
    var sw = Stopwatch.StartNew();

    var errors = 0;
    var success = 0;

    var parallelOptions = new ParallelOptions
    {
        MaxDegreeOfParallelism = options.MaxParallelism == 0 ? -1 : options.MaxParallelism
    };

    Parallel.ForEach(options.Levels, parallelOptions, s =>
    {
        var renderRuntime = zygote.Clone();

        var levelName = Path.GetFileNameWithoutExtension(s);

        try
        {
            EditorRuntimeHelpers.RunLoadLevel(renderRuntime, s);

            var renderer = new LevelRenderer(renderRuntime, null);

            renderer.DoRender();
        }
        catch (Exception e)
        {
            // Fancy error output for Actions CI.
            if (isCi)
                Console.WriteLine($"::error::{levelName}: Rendering failed");

            Console.WriteLine($"{levelName}: Exception while rendering!");
            Console.WriteLine(e);
            Interlocked.Increment(ref errors);
            return;
        }

        Console.WriteLine($"{levelName}: Render succeeded");
        Interlocked.Increment(ref success);
    });

    Console.WriteLine($"Finished rendering in {sw.Elapsed}. {errors} errored, {success} succeeded");
    return errors != 0 ? 1 : 0;
}

static LingoRuntime MakeZygoteRuntime()
{
    var runtime = new LingoRuntime(typeof(MovieScript).Assembly);
    runtime.Init();

    EditorRuntimeHelpers.RunStartup(runtime);

    return runtime;
}
