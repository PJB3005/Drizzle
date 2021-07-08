using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Drizzle.Lingo;
using Drizzle.Lingo.Ast;
using Drizzle.Lingo.Data;
using Pidgin;

namespace Drizzle.ConsoleApp
{
    class Program
    {
        private static int Diag(LingoPoint a, LingoPoint b)
        {
            var w = Math.Abs(a.LocH - b.LocH);
            var h = Math.Abs(a.LocV - b.LocV);

            return (int) Math.Sqrt(w * w + h * h);
        }

        static void Main(string[] args)
        {
            var runtime = new LingoRuntime();

            var scriptContents = File.ReadAllText(Path.Combine("..", "..", "..", "..", "LingoSource", "fiffigt.ls"));
            var script = LingoParser.Script.ParseOrThrow(scriptContents);
            var handlerDiag = script.Nodes.Single(n => n is AstNode.Handler {Name: "diag"});
            var compiled = ScriptCompiler.CompileHandler((AstNode.Handler) handlerDiag, runtime);

            const int count = 1024;

            var inputA = new LingoPoint[count];
            var inputB = new LingoPoint[count];
            var output = new int[count];

            var r = new Random();

            for (var i = 0; i < 256; i++)
            {
                inputA[i] = new LingoPoint(r.Next(-50, 50), r.Next(-50, 50));
                inputB[i] = new LingoPoint(r.Next(-50, 50), r.Next(-50, 50));
            }

            var diag = (Func<object?, object?, object?>) compiled;

            // Shitty warmup.
            diag(default(LingoPoint), default(LingoPoint));
            diag(default(LingoPoint), default(LingoPoint));
            diag(default(LingoPoint), default(LingoPoint));
            diag(default(LingoPoint), default(LingoPoint));
            diag(default(LingoPoint), default(LingoPoint));

            Diag(default, default);
            Diag(default, default);
            Diag(default, default);
            Diag(default, default);
            Diag(default, default);

            var sw = new Stopwatch();
            for (var iter = 0; iter < 20; iter++)
            {
                sw.Restart();
                for (var i = 0; i < count; i++)
                {
                    output[i] = Diag(inputA[i], inputB[i]);
                }
                sw.Stop();
                var ns = sw.Elapsed.TotalMilliseconds / count * 1_000_000;
                Console.WriteLine($"[{iter}] C# -> {sw.Elapsed.TotalMilliseconds * 1000:F2} ({ns:F} ns / iter)");

                sw.Restart();
                for (var i = 0; i < count; i++)
                {
                    output[i] = (int) diag(inputA[i], inputB[i])!;
                }
                sw.Stop();
                ns = sw.Elapsed.TotalMilliseconds / count * 1_000_000;
                Console.WriteLine($"[{iter}] Lingo -> {sw.Elapsed.TotalMilliseconds * 1000:F2} us ({ns:F} ns / iter)");
            }


            //Console.WriteLine(diag(new LingoPoint(0, 0), new LingoPoint(20, 20)));

            /*
            var param = Expression.Parameter(typeof(int), "a");
            var set = Expression.Assign(param, Expression.Constant(5));
            var add = Expression.AddAssign(param, Expression.Constant(10));
            var print = Expression.Call(null,
                typeof(Console).GetMethod("WriteLine", new Type[] {typeof(int)}), param);
            var block = Expression.Block(new List<ParameterExpression>() {param}, set, add, print);

            var del = Expression.Lambda<Action>(block).Compile();
            del();
            del();
            del();
            del();
            del();*/

            /*
            if (true)
            {
                var text = File.ReadAllText(
                    @"C:\Users\Pieter-Jan Briers\Projects\DrizzleEdit\LingoSource\levelRendering.ls");
                var sw = Stopwatch.StartNew();
                var parsed = LingoParser.Script.ParseOrThrow(text);
                Console.WriteLine(DebugPrint.PrintAstNode(parsed));
                Console.WriteLine($"Parsed in {sw.Elapsed}");
            }
            else
            {
                var text = @"

on renderLevel()
  tm = _system.milliseconds

  gSkyColor = color(0,0,0)
  gTinySignsDrawn = 0

  gRenderTrashProps = []

  RENDER = 0

  -- member(""shortCutSymbolsImg"").image = image(1040, 800, 1)
                    -- saveLvl()
                cols = 100--gLOprops.size.loch
                    rows = 60--gLOprops.size.locv

                    -- member(""bkgBkgImage"").image = image(cols*20, rows*20, 16)
                member(""finalImage"").image = image(cols*20, rows*20, 32)

                the randomSeed = gLOprops.tileSeed

                setUpLayer(3)
                setUpLayer(2)
                setUpLayer(1)

                gLastImported = ""

                global gLoadedName

                put gLoadedName && ""rendered in"" && (_system.milliseconds-tm)
                end


";
                var parsed = LingoParser.Script.ParseOrThrow(text);
                Console.WriteLine("FINAL: {0}", DebugPrint.PrintAstNode(parsed));
            }
        */
        }
    }
}
