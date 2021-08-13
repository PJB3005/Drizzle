using System.Diagnostics;
using System.IO;
using System.Linq;
using Drizzle.Lingo.Runtime;
using Drizzle.Ported;
using Serilog;

namespace Drizzle.Logic.Rendering
{
    public sealed partial class LevelRenderer
    {
        // This partial contains core rendering logic.
        private int _cameraIndex;
        private int _countCamerasDone;

        public void DoRender()
        {
            RenderStart();

            // Set up camera order.
            var camOrder = Enumerable.Range(1, (int)Movie.global_gcameraprops.cameras.count).ToList();

            if (Movie.global_gpriocam is not null and not 0)
            {
                camOrder.Remove(Movie.global_gpriocam);
                camOrder.Insert(0, Movie.global_gpriocam);
            }

            foreach (var camIndex in camOrder)
            {
                RenderSetupCamera(camIndex);

                RenderLayers();
                RenderPropsPreEffects();
                RenderEffects();
                RenderPropsPostEffects();
                RenderLight();
                RenderFinalize();
                RenderColors();
                RenderFinished();

                RenderStartFrame(RenderStage.SaveFile);
                // Save image.
                var fileName = Path.Combine(
                    LingoRuntime.MovieBasePath,
                    "Levels",
                    $"{Movie.global_gloadedname}_{camIndex}.png");

                var file = File.OpenWrite(fileName);
                _runtime.GetCastMember("finalImage")!.image!.SaveAsPng(file);

                _countCamerasDone += 1;
            }

            // Output level data.
            Movie.newmakelevel(Movie.global_gloadedname);
        }

        private void RenderStart()
        {
            RenderStartFrame(RenderStage.Start);
            _runtime.CreateScript<renderStart>().exitframe();
        }

        private void RenderSetupCamera(int camIndex)
        {
            RenderStartFrame(RenderStage.CameraSetup);

            var camera = (LingoPoint)Movie.global_gcameraprops.cameras[camIndex];
            _cameraIndex = camIndex;
            Movie.global_gcurrentrendercamera = camIndex;
            Movie.global_grendercameratilepos =
                new LingoPoint(
                    (camera.loch / (LingoDecimal)20.0 - (LingoDecimal)0.49999).integer,
                    (camera.locv / (LingoDecimal)20.0 - (LingoDecimal)0.49999).integer);

            Movie.global_grendercamerapixelpos = camera - (Movie.global_grendercameratilepos * 20);
            Movie.global_grendercamerapixelpos.loch = Movie.global_grendercamerapixelpos.loch.integer;
            Movie.global_grendercamerapixelpos.locv = Movie.global_grendercamerapixelpos.locv.integer;

            Movie.global_grendercameratilepos += new LingoPoint(-15, -10);
        }

        private void RenderLayers()
        {
            const int cols = 100;
            const int rows = 60;

            RenderStartFrame(RenderStage.RenderLayers);

            for (var i = 0; i < 30; i++)
            {
                _runtime.GetCastMember($"layer{i}")!.image = new LingoImage(cols * 20, rows * 20, 32);
                _runtime.GetCastMember($"gradientA{i}")!.image = new LingoImage(cols * 20, rows * 20, 32);
                _runtime.GetCastMember($"gradientB{i}")!.image = new LingoImage(cols * 20, rows * 20, 32);
                _runtime.GetCastMember($"layer{i}dc")!.image = new LingoImage(cols * 20, rows * 20, 32);
            }

            _runtime.GetCastMember("rainBowMask")!.image = new LingoImage(cols * 20, rows * 20, 32);

            var sw = Stopwatch.StartNew();
            Movie.global_gskycolor = new LingoColor(0, 0, 0);
            Movie.global_gtinysignsdrawn = 0;
            Movie.global_grendertrashprops = new LingoList();
            _runtime.GetCastMember(@"finalImage")!.image = new LingoImage(cols * 20, rows * 20, 32);
            _runtime.Global.the_randomSeed = Movie.global_gloprops.tileseed;

            for (var i = 3; i > 0; i--)
            {
                // Don't measure pauses as part of the stopwatch.
                sw.Stop();
                RenderStartFrame(new RenderStageStatusLayers(i));
                sw.Start();

                Movie.setuplayer(i);
            }

            Movie.global_glastimported = "";
            Log.Information("{LevelName} rendered layers in {ElapsedMilliseconds} ms",
                Movie.global_gloadedname, sw.ElapsedMilliseconds);

            Movie.global_c = 1;
        }

        private void RenderPropsPreEffects()
        {
            RenderStartFrame(RenderStage.RenderPropsPreEffects);
            _runtime.CreateScript<renderPropsStart>().exitframe();

            var script = _runtime.CreateScript<renderProps>();
            while (Movie.global_keeplooping == 1)
            {
                RenderStartFrame(RenderStage.RenderPropsPreEffects);
                script.newframe();
            }
        }

        private void RenderEffects()
        {
            RenderStartFrame(RenderStage.RenderEffects);
            _runtime.CreateScript<renderEffectsStart>().exitframe();

            var script = _runtime.CreateScript<renderEffects>();
            while (Movie.global_keeplooping == 1)
            {
                var effectsList = (LingoList)Movie.global_geeprops.effects;
                var effectNames = effectsList.List.Select(e => (string)((dynamic)e!).nm).ToArray();
                var totalCount = effectsList.count;
                var curr = (int)Movie.global_r;
                var vert = (int)Movie.global_vertrepeater;
                RenderStartFrame(new RenderStageStatusEffects(totalCount, curr, vert, effectNames));
                script.newframe();
            }
        }

        private void RenderPropsPostEffects()
        {
            RenderStartFrame(RenderStage.RenderPropsPostEffects);
            Movie.global_aftereffects = 1;
            _runtime.CreateScript<renderPropsStart>().exitframe();

            var script = _runtime.CreateScript<renderProps>();
            while (Movie.global_keeplooping == 1)
            {
                RenderStartFrame(RenderStage.RenderPropsPostEffects);
                script.newframe();
            }
        }

        private void RenderLight()
        {
            RenderStartFrame(RenderStage.RenderLight);
            _runtime.CreateScript<renderLightStart>().exitframe();

            var script = _runtime.CreateScript<renderLight>();
            while (Movie.global_keeplooping == 1)
            {
                var curr = (int)Movie.global_c;
                RenderStartFrame(new RenderStageStatusLight(curr));
                script.newframe();
            }
        }

        private void RenderFinalize()
        {
            RenderStartFrame(RenderStage.Finalize);
            _runtime.CreateScript<finalize>().exitframe();
        }

        private void RenderColors()
        {
            var script = _runtime.CreateScript<renderColors>();
            while (Movie.global_keeplooping == 1)
            {
                RenderStartFrame(RenderStage.RenderColors);
                script.newframe();
            }
        }

        private void RenderFinished()
        {
            RenderStartFrame(RenderStage.Finished);
            _runtime.CreateScript<finished>().exitframe();
        }
    }
}
