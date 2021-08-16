using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using Avalonia.Controls;
using Drizzle.Editor.ViewModels.Render;
using Drizzle.Editor.Views;
using Drizzle.Editor.Views.Render;
using Drizzle.Lingo.Runtime;
using Drizzle.Logic;
using Drizzle.Ported;
using DynamicData;
using ReactiveUI.Fody.Helpers;
using Serilog;

namespace Drizzle.Editor.ViewModels
{
    public class MainWindowViewModel : ViewModelBase, ILingoRuntimeManager
    {
        private readonly LingoRuntime _zygoteRuntime = new(typeof(MovieScript).Assembly);
        // public MapEditorViewModel MapEditorVM { get; } = new();

        public ReadOnlyObservableCollection<MainEditorTabViewModel> MainTabs { get; }
        private readonly SourceList<MainEditorTabViewModel> _tabsList = new();

        [Reactive] public MainEditorTabViewModel? SelectedTab { get; set; }
        public Window? Parent { get; set; }

        private Task<LingoRuntime> _zygoteInitialized = default!;

        public MainWindowViewModel()
        {
            _tabsList.Connect()
                .Bind(out ReadOnlyObservableCollection<MainEditorTabViewModel> tabs)
                .Subscribe();

            MainTabs = tabs;
        }

        public void Init(CommandLineArgs commandLineArgs)
        {
            _zygoteInitialized = Task.Run(InitZygote);
            // MapEditorVM.Init(commandLineArgs);

            if (commandLineArgs.Project != null)
                DoOpenProject(commandLineArgs.Project);
        }

        private LingoRuntime InitZygote()
        {
            Log.Debug("Initializing zygote runtime...");
            _zygoteRuntime.Init();

            Log.Debug("Running zygote startup...");
            var sw = Stopwatch.StartNew();
            EditorRuntimeHelpers.RunStartup(_zygoteRuntime);

            Log.Debug("Done initializing zygote runtime in {ZygoteStartupTime}!", sw.Elapsed);

            return _zygoteRuntime;
        }

        public void NewProject()
        {
        }

        public async void OpenProject()
        {
            var dialog = new OpenFileDialog
            {
                Filters = new List<FileDialogFilter>
                {
                    new()
                    {
                        Name = "Level editor projects",
                        Extensions = { "txt" }
                    }
                },
            };

            var result = await dialog.ShowAsync(Parent);

            if (result.Length == 0)
                return;

            var file = result[0];

            DoOpenProject(file);
        }

        private void DoOpenProject(string fileName)
        {
            var vm = new MainEditorTabViewModel(Path.GetFileNameWithoutExtension(fileName));
            vm.InitLoad(_zygoteInitialized, fileName);

            _tabsList.Add(vm);

            SelectedTab = vm;
        }

        public void SaveProject()
        {
        }

        public void SaveAsProject()
        {
        }

        public void RenderProject()
        {
            if (SelectedTab?.Content == null)
                return;

            var runtime = SelectedTab.Content.Runtime;

            var renderViewModel = new RenderViewModel();
            renderViewModel.StartRendering(runtime.Clone());

            var window = new RenderWindow { DataContext = renderViewModel };
            window.Show();
        }

        public void CastViewerProject()
        {
            if (SelectedTab?.Content == null)
                return;

            new LingoCastViewer { DataContext = new LingoCastViewerViewModel(SelectedTab.Content) }.Show();
        }

        public void CastViewerZygote()
        {
            new LingoCastViewer { DataContext = new LingoCastViewerViewModel(this) }.Show();
        }

        public void RunGC()
        {
            GC.Collect();
        }

        public async Task Exec(Action<LingoRuntime> action)
        {
            var runtime = await _zygoteInitialized;

            action(runtime);
        }

        public async Task<T> Exec<T>(Func<LingoRuntime, T> func)
        {
            var runtime = await _zygoteInitialized;

            return func(runtime);
        }
    }
}
