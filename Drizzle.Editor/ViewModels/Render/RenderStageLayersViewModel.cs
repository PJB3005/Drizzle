using Drizzle.Logic;
using Drizzle.Logic.Rendering;

namespace Drizzle.Editor.ViewModels.Render
{
    public sealed class RenderStageLayersViewModel : RenderStageViewModelBase
    {
        public override (int max, int current)? Progress { get; }

        public int CurrentLayer { get; }

        public RenderStageLayersViewModel(RenderStageStatusLayers status)
        {
            CurrentLayer = status.CurrentLayer;
            Progress = (3, 3 - status.CurrentLayer);
        }
    }
}
