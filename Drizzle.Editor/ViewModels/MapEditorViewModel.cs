using Drizzle.Logic;

namespace Drizzle.Editor.ViewModels
{
    public sealed class MapEditorViewModel : ViewModelBase
    {
        public MapEditorRuntime MapEditor { get; }
        public LingoViewModel Lingo { get; }

        public MapEditorViewModel()
        {
            Lingo = new LingoViewModel();
            MapEditor = new MapEditorRuntime(Lingo.Runtime);
        }

        public void Init()
        {
            Lingo.Init();
        }
    }
}
