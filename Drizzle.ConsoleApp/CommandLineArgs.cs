using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using C = System.Console;

namespace Drizzle.ConsoleApp;

public sealed record CommandLineArgs(CommandLineArgs.BaseVerb Verb)
{
    public abstract record BaseVerb;

    public sealed record VerbRender(int MaxParallelism, List<string> Levels, bool Checksums, string? CompareChecksums) : BaseVerb
    {
        public static VerbRender? ContinueParse(IEnumerator<string> enumerator)
        {
            var levels = new List<string>();
            var parallelism = 0;
            var genChecksums = false;
            string? compareChecksums = null;

            while (enumerator.MoveNext())
            {
                var arg = enumerator.Current;
                if (arg == "--parallelism")
                {
                    if (!enumerator.MoveNext())
                    {
                        C.WriteLine("Expected max parallelism");
                        return null;
                    }

                    parallelism = int.Parse(enumerator.Current);
                }
                else if (arg == "--gen-checksums")
                {
                    genChecksums = true;
                }
                else if (arg == "--compare-checksums")
                {
                    genChecksums = true;

                    if (!enumerator.MoveNext())
                    {
                        C.WriteLine("Expected checksum comparison file");
                        return null;
                    }

                    compareChecksums = enumerator.Current;
                }
                else if (arg == "--help")
                {
                    PrintVerbHelp();
                    return null;
                }
                else
                {
                    levels.Add(arg);
                }
            }

            if (levels.Count == 0)
            {
                C.WriteLine("No levels specified!");
                return null;
            }

            return new VerbRender(parallelism, levels, genChecksums, compareChecksums);
        }

        private static void PrintVerbHelp()
        {
            C.WriteLine(@"usage: Drizzle.ConsoleApp render [options] level [level ...]
Arguments:
  level                       Level to render

Options:
  --parallelism PARALLELISM   Maximum amount of threads to use. Leave out or 0 to select automatically.
  --gen-checksums             Generate checksums of generated level images.
  --compare-checksums <file>  Checksums file to compare against.
                              The checksum of the generated image will be looked up and compared,
                              and an error will be raised if it does not match.
  --help                      Print help then exit.
");
        }
    }

    public static bool TryParse(IReadOnlyList<string> args, [NotNullWhen(true)] out CommandLineArgs? parsed)
    {
        parsed = null;
        BaseVerb? verb = null;

        using var enumerator = args.GetEnumerator();

        while (enumerator.MoveNext())
        {
            var arg = enumerator.Current;
            if (arg == "--help")
            {
                PrintHelp();
                return false;
            }
            else
            {
                switch (arg)
                {
                    case "render":
                        verb = VerbRender.ContinueParse(enumerator);
                        break;

                    default:
                        C.WriteLine($"Unknown command {arg}");
                        return false;
                }

                if (verb == null)
                    return false;

                break;
            }
        }

        if (verb == null)
        {
            C.WriteLine("No command specified!");
            return false;
        }

        parsed = new CommandLineArgs(verb);
        return true;
    }

    private static void PrintHelp()
    {
        C.WriteLine(@"usage: Drizzle.ConsoleApp [options] <command>
Commands:
  render              Render Rain World levels then exit.

Options:
  --help              Print help then exit.
");
    }
}
