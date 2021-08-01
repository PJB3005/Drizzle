using System.Collections.ObjectModel;
using System.Linq;
using Drizzle.Lingo.Runtime;
using DynamicData;
using ReactiveUI;

namespace Drizzle.Editor.ViewModels.LingoFrames
{
    public sealed class FrameLoadLevelViewModel : LingoFrameViewModel
    {
        public int SelectedIndex
        {
            get => MovieScript.global_ldprps.currproject - 1;
            set => MovieScript.global_ldprps.currproject = value + 1;
        }

        public ObservableCollection<string> ProjectsList { get; } = new();

        public override void OnLoad(LingoRuntime runtime)
        {
            base.OnLoad(runtime);

            var projects = (LingoList)MovieScript.global_projects;
            ProjectsList.AddRange(projects.Cast<string>());
        }

        public override void OnUpdate()
        {
            this.RaisePropertyChanged(nameof(SelectedIndex));
        }
    }
}
