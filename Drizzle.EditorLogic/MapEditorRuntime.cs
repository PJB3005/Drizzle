using System;
using System.Diagnostics;
using Drizzle.Lingo.Runtime;
using Drizzle.Ported;
using Serilog;

namespace Drizzle.Logic
{
    public sealed class MapEditorRuntime
    {
        public LingoRuntime LingoRuntime { get; }

        public string? LoadProject { get; set; }
        public bool Render { get; set; }
        public RenderStage RenderStage { get; set; } = RenderStage.NoPause;
        public int AutoPauseOn { get; private set; } = -1;

        public MapEditorRuntime(LingoRuntime runtime)
        {
            LingoRuntime = runtime;
        }

        public void Init()
        {
            if (LoadProject == null)
                return;

            Log.Information("Loading project immediately: {ProjectName}", LoadProject);

            // Tick thrice, should get us onto LoadLevel.
            LingoRuntime.Tick();
            LingoRuntime.Tick();
            LingoRuntime.Tick();

            Debug.Assert(LingoRuntime.CurrentFrame == 3);

            var levelOverview = LingoRuntime.CreateScript<loadLevel>();
            levelOverview.loadlevel(LoadProject);

            LingoRuntime.ScoreGo(7);

            if (Render)
            {
                Log.Information("Starting automatic render");
                MovieScript.global_gviewrender = 1;
                LingoRuntime.ScoreGo(43);
            }

            var expectFrame = RenderStage switch
            {
                RenderStage.NoPause => -1,
                RenderStage.Start => 44,
                RenderStage.RenderLayers => 50,
                RenderStage.RenderProps => 53,
                RenderStage.RenderEffects => 56,
                RenderStage.RenderPropsAfterEffects => 64,
                RenderStage.RenderLights => 69,
                RenderStage.RenderColors => 73,
                _ => throw new ArgumentOutOfRangeException()
            };

            AutoPauseOn = expectFrame;
        }

        private MovieScript MovieScript => (MovieScript)LingoRuntime.MovieScriptInstance;
    }

    public enum RenderStage
    {
        Start,
        RenderLayers,
        RenderProps,
        RenderEffects,
        RenderPropsAfterEffects,
        RenderLights,
        RenderColors,
        NoPause
    }
}
