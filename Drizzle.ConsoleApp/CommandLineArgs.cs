using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using C = System.Console;

namespace Drizzle.ConsoleApp
{
    public sealed record CommandLineArgs(CommandLineArgs.BaseVerb Verb)
    {
        public abstract record BaseVerb;

        public sealed record VerbRender(int MaxParallelism, List<string> Levels) : BaseVerb
        {
            public static VerbRender? ContinueParse(IEnumerator<string> enumerator)
            {
                var levels = new List<string>();
                var parallelism = 0;

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

                return new VerbRender(parallelism, levels);
            }

            private static void PrintVerbHelp()
            {
                C.WriteLine(@"usage: Drizzle.ConsoleApp render [options] level [level ...]
Arguments:
  level                       Level to render

Options:
  --parallelism PARALLELISM   Maximum amount of threads to use. Leave out or 0 to select automatically.
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
}
