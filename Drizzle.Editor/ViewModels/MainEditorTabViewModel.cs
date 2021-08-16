using System.IO;
using System.Threading.Tasks;
using Drizzle.Lingo.Runtime;
using Drizzle.Logic;
using Drizzle.Ported;
using ReactiveUI.Fody.Helpers;
using Serilog;

namespace Drizzle.Editor.ViewModels
{
    public sealed class MainEditorTabViewModel : ViewModelBase
    {
        [Reactive] public string LevelName { get; private set; }
        [Reactive] public EditorContentViewModel? Content { get; private set; }

        public MainEditorTabViewModel(string levelName)
        {
            LevelName = levelName;
        }

        public async void InitLoad(Task<LingoRuntime> zygote, string fullPath)
        {
            var zygoteInstance = await zygote;
            var runtime = await Task.Run(() =>
            {
                var cloned = zygoteInstance.Clone();

                Log.Debug("Loading level...");

                EditorRuntimeHelpers.RunLoadLevel(cloned, fullPath);

                return cloned;
            });

            Content = new EditorContentViewModel(runtime);
        }
    }
}
