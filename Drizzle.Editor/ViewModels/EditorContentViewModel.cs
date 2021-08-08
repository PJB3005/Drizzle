using System.Collections.Generic;
using Drizzle.Editor.ViewModels.EditorTabs;
using Drizzle.Lingo.Runtime;

namespace Drizzle.Editor.ViewModels
{
    public sealed class EditorContentViewModel : ViewModelBase
    {
        public LingoRuntime Runtime { get; }

        public IReadOnlyList<EditorTabViewModelBase> EditorTabs { get; }

        public EditorContentViewModel(LingoRuntime runtime)
        {
            Runtime = runtime;

            EditorTabs = new EditorTabViewModelBase[]
            {
                new TabLevelOverviewViewModel(),
                new TabGeometryEditorViewModel(),
                new TabTileEditorViewModel()
            };
        }
    }
}
