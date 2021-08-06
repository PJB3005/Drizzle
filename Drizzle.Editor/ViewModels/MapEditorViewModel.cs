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

        public void Init(CommandLineArgs commandLineArgs)
        {
            Lingo.Init(commandLineArgs);
            Lingo.Update += Update;

            MapEditor.RenderStage = commandLineArgs.RenderStage;
            MapEditor.LoadProject = commandLineArgs.Project;
            MapEditor.Render = commandLineArgs.Render;
            MapEditor.Init();
        }

        private void Update(int newFrame)
        {
            if (MapEditor.AutoPauseOn == -1)
                Lingo.IsPaused = true;
        }
    }
}

