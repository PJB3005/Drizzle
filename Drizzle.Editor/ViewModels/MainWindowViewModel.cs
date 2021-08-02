
namespace Drizzle.Editor.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public MapEditorViewModel MapEditorVM { get; } = new();

        public void Init()
        {
            MapEditorVM.Init();
        }
    }
}
