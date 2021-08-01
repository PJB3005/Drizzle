using System;
using Avalonia.Input;
using Avalonia.Threading;
using Drizzle.Editor.ViewModels.LingoFrames;
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

        public void Init()
        {
            // Run lingo at 60 FPS for now.
            _timer.Interval = TimeSpan.FromSeconds(1 / 60f);
            _timer.Tick += TimerTickLingo;
            _timer.Start();

            Runtime.Init();
        }

        private void TimerTickLingo(object? sender, EventArgs e)
        {
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

        private LingoFrameViewModel? GetFrameViewModel()
        {
            return LastFrame switch
            {
                3 => new FrameLoadLevelViewModel(),
                _ => null
            };
        }
    }
}
