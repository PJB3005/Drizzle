using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Drizzle.Editor.ViewModels.EditorTabs;
using Drizzle.Lingo.Runtime;
using Drizzle.Logic;
using Drizzle.Ported;

namespace Drizzle.Editor.ViewModels
{
    public sealed class EditorContentViewModel : ViewModelBase, ILingoRuntimeManager
    {
        public LingoRuntime Runtime { get; }

        public IReadOnlyList<EditorTabViewModelBase> EditorTabs { get; }
        public int CountCameras => (int) MovieScript.gCameraProps.cameras.count;
        private MovieScript MovieScript => (MovieScript)Runtime.MovieScriptInstance;

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

        public Task Exec(Action<LingoRuntime> action)
        {
            action(Runtime);
            return Task.CompletedTask;
        }

        public Task<T> Exec<T>(Func<LingoRuntime, T> func)
        {
            return Task.FromResult(func(Runtime));
        }
    }
}
