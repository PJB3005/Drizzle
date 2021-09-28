namespace Drizzle.Editor.ViewModels.EditorTabs
{
    public sealed class TabLevelOverviewViewModel : EditorTabViewModelBase
    {
        public EditorContentViewModel ParentVm { get; }
        public override string Title => "Overview";

        public TabLevelOverviewViewModel(EditorContentViewModel parentVm)
        {
            ParentVm = parentVm;
        }
    }
}
