
namespace Drizzle.Editor.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public MapEditorViewModel MapEditorVM { get; } = new();

        public void Init(CommandLineArgs commandLineArgs)
        {
            MapEditorVM.Init(commandLineArgs);
        }
    }
}
