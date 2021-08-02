using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using C = System.Console;

namespace Drizzle.Editor
{
    public sealed record CommandLineArgs(bool Render, string? Project)
    {
        public static bool TryParse(IReadOnlyList<string> args, [NotNullWhen(true)] out CommandLineArgs? parsed)
        {
            parsed = null;
            var render = false;
            string? project = null;

            using var enumerator = args.GetEnumerator();

            while (enumerator.MoveNext())
            {
                var arg = enumerator.Current;
                if (arg == "--render")
                {
                    render = true;
                }
                else if (arg == "--project")
                {
                    if (!enumerator.MoveNext())
                    {
                        C.WriteLine("Missing project name.");
                        return false;
                    }

                    project = enumerator.Current;
                }
                else if (arg == "--help")
                {
                    PrintHelp();
                    return false;
                }
            }

            parsed = new CommandLineArgs(render, project);

            return true;
        }

        private static void PrintHelp()
        {
            C.WriteLine(@"
Options:
  --project <project> Which project to load.
  --render            Immediately render the loaded project.
");
        }

    }
}
