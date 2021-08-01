using Drizzle.Lingo.Runtime;
using ReactiveUI;

namespace Drizzle.Editor.ViewModels.LingoFrames
{
    public sealed class FrameLoadLevelViewModel : LingoFrameViewModel
    {
        private string _selectedLevel = "A";

        public string SelectedLevel
        {
            get => _selectedLevel;
            private set => this.RaiseAndSetIfChanged(ref _selectedLevel, value);
        }

        public override void OnUpdate()
        {
            var loadLevelProps = (LingoPropertyList) MovieScript.global_ldprps;
            var projects = (LingoList)MovieScript.global_projects;
            int curProject = loadLevelProps[new LingoSymbol("currproject")];
            SelectedLevel = projects[curProject].ToString();
        }
    }
}
