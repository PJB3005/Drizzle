using Drizzle.Logic;
using Drizzle.Logic.Rendering;

namespace Drizzle.Editor.ViewModels.Render
{
    public class RenderStageLightViewModel : RenderStageViewModelBase
    {
        public int CurrentLayer { get; }
        public override (int max, int current)? Progress { get; }

        public RenderStageLightViewModel(RenderStageStatusLight status)
        {
            CurrentLayer = status.CurrentLayer;
            Progress = (30, status.CurrentLayer);
        }
    }
}
