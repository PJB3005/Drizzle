using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Drizzle.Logic;
using Drizzle.Logic.Rendering;
using C = System.Console;

namespace Drizzle.Editor
{
    public sealed record CommandLineArgs(bool Render, string? Project, bool AutoPause, RenderStage? RenderStage)
    {
        public static bool TryParse(IReadOnlyList<string> args, [NotNullWhen(true)] out CommandLineArgs? parsed)
        {
            parsed = null;
            var render = false;
            var autoPause = false;
            string? project = null;
            RenderStage? renderStage = null;

            using var enumerator = args.GetEnumerator();

            while (enumerator.MoveNext())
            {
                var arg = enumerator.Current;
                if (arg == "--render")
                {
                    render = true;
                }
                else if (arg == "--pause")
                {
                    autoPause = true;
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
                else if (arg == "--render-stage")
                {
                    if (!enumerator.MoveNext())
                    {
                        C.WriteLine("Missing render stage.");
                        return false;
                    }

                    renderStage = Enum.Parse<RenderStage>(enumerator.Current);
                }
                else if (arg == "--help")
                {
                    PrintHelp();
                    return false;
                }
            }

            parsed = new CommandLineArgs(render, project, autoPause, renderStage);

            return true;
        }

        private static void PrintHelp()
        {
            C.WriteLine(@"
Options:
  --project <project> Which project to load.
  --render            Immediately render the loaded project.
  --render-stage      Render stage to run to before pausing.
  --pause             Automatically pause the lingo runtime on load to allow stepping.
");
        }
    }
}
