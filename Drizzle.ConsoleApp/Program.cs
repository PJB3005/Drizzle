using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics;
using System.Threading;
using System.Threading.Tasks;
using Drizzle.ConsoleApp;
using Drizzle.Lingo.Runtime;
using Drizzle.Logic;
using Drizzle.Logic.Rendering;
using Drizzle.Ported;
using Meow;
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

        var levelSw = Stopwatch.StartNew();
        try
        {
            EditorRuntimeHelpers.RunLoadLevel(renderRuntime, s);

            var renderer = new LevelRenderer(renderRuntime, null);
            if (options.checksums)
                renderer.OnScreenRenderCompleted += (cam, img) => PrintChecksum(levelName, cam, img);

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

        Console.WriteLine($"{levelName}: Render succeeded in {levelSw.Elapsed}");
        Interlocked.Increment(ref success);
    });

    Console.WriteLine($"Finished rendering in {sw.Elapsed}. {errors} errored, {success} succeeded");
    return errors != 0 ? 1 : 0;
}

static void PrintChecksum(string name, int cameraIndex, LingoImage finalImg)
{
    Span<byte> hash = stackalloc byte[16];
    CalcChecksum(finalImg, hash);
    var hashHex = Convert.ToHexString(hash);

    Console.WriteLine($"checksum {name} cam {cameraIndex}: {hashHex}");
}

static void CalcChecksum(LingoImage img, Span<byte> outData)
{
    unsafe
    {
        Debug.Assert(sizeof(Vector128<byte>) == outData.Length);
    }

    if (img.depth.IntValue == 1)
    {
        // 1-bit images have padding bytes to make certain ops easier.
        // These bytes can contain undefined garbage,
        // and I can't be bothered to clear them to make sure the hash is consistent.
        // Just make it not supported for now, it's fine for the final images (those are 32bpp).
        throw new NotSupportedException();
    }

    var hash = MeowHash.Hash(MeowHash.MeowDefaultSeed, img.ImageBuffer);
    Unsafe.WriteUnaligned(ref outData[0], hash);
}

static LingoRuntime MakeZygoteRuntime()
{
    var runtime = new LingoRuntime(typeof(MovieScript).Assembly);
    runtime.Init();

    EditorRuntimeHelpers.RunStartup(runtime);

    return runtime;
}
