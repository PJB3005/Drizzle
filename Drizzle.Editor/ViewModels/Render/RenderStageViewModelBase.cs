using Drizzle.Logic;

namespace Drizzle.Editor.ViewModels.Render
{
    public abstract class RenderStageViewModelBase : ViewModelBase
    {
        public virtual (int max, int current)? Progress => default;
    }
}
