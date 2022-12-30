using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Drizzle.ConsoleApp;
using Drizzle.Lingo.Runtime;
using Drizzle.Lingo.Runtime.Utils;
using Drizzle.Logic;
using Drizzle.Logic.Rendering;
using Drizzle.Ported;
using Meow;
using Serilog;
using Serilog.Sinks.SystemConsole.Themes;
using SixLabors.ImageSharp;

CultureFix.FixCulture();

if (!CommandLineArgs.TryParse(args, out var parsedArgs))
    return 1;

var isCi = Environment.GetEnvironmentVariable("CI") == "true";
var checksumErrors = 0;

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
    Configuration.Default.PreferContiguousImageBuffers = true;

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

    var doChecksums = options.Checksums;
    Dictionary<string, Dictionary<string, string>>? checksums = null;
    if (options.CompareChecksums is { } chkFileName)
    {
        doChecksums = true;
        using var chkFile = File.OpenRead(chkFileName);
        checksums = JsonSerializer.Deserialize<Dictionary<string, Dictionary<string, string>>>(chkFile);
    }

    Shuffle(options.Levels, new Random());

    Parallel.ForEach(options.Levels, parallelOptions, s =>
    {
        var renderRuntime = zygote.Clone();

        var levelName = Path.GetFileNameWithoutExtension(s);

        var levelSw = Stopwatch.StartNew();
        try
        {
            EditorRuntimeHelpers.RunLoadLevel(renderRuntime, s);

            var renderer = new LevelRenderer(renderRuntime, null);
            if (doChecksums)
                renderer.OnScreenRenderCompleted += (cam, img) => HandleChecksum(levelName, cam, img, checksums);

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
    if (checksums != null)
        Console.WriteLine($"{checksumErrors} checksum failures.");

    return errors != 0 || checksumErrors != 0 ? 1 : 0;
}

void HandleChecksum(string name, int cameraIndex, LingoImage finalImg,
    Dictionary<string, Dictionary<string, string>>? checksums)
{
    Span<byte> hash = stackalloc byte[16];
    CalcChecksum(finalImg, hash);
    var hashHex = Convert.ToHexString(hash);

    Console.WriteLine($"checksum {name} cam {cameraIndex}: {hashHex}");
    if (checksums == null)
        return;

    if (!checksums.TryGetValue(name, out var cameras) ||
        !cameras.TryGetValue(cameraIndex.ToString(), out var checksum))
    {
        Console.WriteLine(
            $"{(isCi ? "::notice::" : "")}{name}#{cameraIndex} not found in checksum manifest.");
        return;
    }

    if (checksum != hashHex)
    {
        Console.WriteLine(
            $"{(isCi ? "::error::" : "")}{name}#{cameraIndex} mismatches checksum! New: {hashHex} old: {checksum}.");

        Interlocked.Increment(ref checksumErrors);
    }
    else
    {
        Console.WriteLine($"{name}#{cameraIndex}: Checksums passed");
    }
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

static void Shuffle<T>(List<T> array, System.Random random)
{
    var n = array.Count;
    while (n > 1)
    {
        n--;
        var k = random.Next(n + 1);
        (array[k], array[n]) =
            (array[n], array[k]);
    }
}
