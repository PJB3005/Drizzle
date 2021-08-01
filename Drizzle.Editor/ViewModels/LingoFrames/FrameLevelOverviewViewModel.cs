using Drizzle.Lingo.Runtime;

namespace Drizzle.Editor.ViewModels.LingoFrames
{
    public sealed class FrameLevelOverviewViewModel : LingoFrameViewModel
    {
        public string LevelName { get; private set; } = "";

        public override void OnLoad(LingoRuntime runtime)
        {
            base.OnLoad(runtime);

            LevelName = runtime.GetCastMember("Level Name")!.text;
        }

        public void Render()
        {
            MovieScript.global_gviewrender = 1;
            Runtime.ScoreGo(43);
        }
    }
}
