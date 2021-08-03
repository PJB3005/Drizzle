using System;
using Avalonia.Threading;
using Drizzle.Editor.ViewModels.LingoFrames;
using Drizzle.Editor.Views;
using Drizzle.Lingo.Runtime;
using Drizzle.Ported;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Serilog;

namespace Drizzle.Editor.ViewModels
{
    public sealed class LingoViewModel : ViewModelBase
    {
        public LingoRuntime Runtime { get; } = new(typeof(MovieScript).Assembly);
        private readonly DispatcherTimer _timer = new();

        public LingoFrameViewModel? Frame { get; private set; }

        [Reactive] public int LastFrame { get; private set; }
        [Reactive] public string? LastFrameName { get; private set; }

        public bool IsPaused { get; set; }
        private bool _singleStep;

        public void Init(CommandLineArgs commandLineArgs)
        {
            IsPaused = commandLineArgs.AutoPause;

            // Run lingo at 60 FPS for now.
            _timer.Interval = TimeSpan.FromSeconds(1 / 999f);
            _timer.Tick += TimerTickLingo;
            _timer.Start();

            Runtime.Init();
        }

        private void TimerTickLingo(object? sender, EventArgs e)
        {
            if (IsPaused && !_singleStep)
                return;

            _singleStep = false;

            Runtime.Tick();

            if (LastFrame != Runtime.CurrentFrame)
            {
                LastFrame = Runtime.CurrentFrame;
                LastFrameName = Runtime.LastFrameBehaviorName;

                Frame = GetFrameViewModel();
                try
                {
                    Frame?.OnLoad(Runtime);
                }
                catch (Exception ex)
                {
                    Log.Error(ex, "Exception in FrameVM load");
                    Frame = null;
                }

                Log.Debug("Switching frame VM to {FrameViewModelName}", Frame?.GetType().Name);

                this.RaisePropertyChanged(nameof(Frame));
            }

            try
            {
                Frame?.OnUpdate();
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Exception in FrameVM update");
            }
        }

        public void SingleStep()
        {
            _singleStep = true;
        }

        public void OpenCastViewer()
        {
            new LingoCastViewer { DataContext = new LingoCastViewerViewModel(this) }.Show();
        }

        private LingoFrameViewModel? GetFrameViewModel()
        {
            return LastFrame switch
            {
                3 => new FrameLoadLevelViewModel(),
                10 => new FrameLevelOverviewViewModel(),
                _ => null
            };
        }
    }
}
