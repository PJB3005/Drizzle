using System;
using System.Diagnostics;
using System.IO;
using Drizzle.Lingo.Ast;
using Pidgin;

namespace Drizzle.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            if (true)
            {
                var text = File.ReadAllText(
                    @"C:\Users\Pieter-Jan Briers\Projects\DrizzleEdit\LingoSource\spelrelarat.ls");
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
        }
    }
}
