using System;
using Avalonia.Threading;
using Drizzle.Editor.ViewModels.LingoFrames;
using Drizzle.Lingo.Runtime;
using Drizzle.Ported;
using ReactiveUI;
using Serilog;

namespace Drizzle.Editor.ViewModels
{
    public sealed class LingoViewModel : ViewModelBase
    {
        public LingoRuntime Runtime { get; } = new(typeof(MovieScript).Assembly);
        private readonly DispatcherTimer _timer = new();

        public LingoFrameViewModel? Frame { get; private set; }

        private int _lastFrame;

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

            if (_lastFrame != Runtime.CurrentFrame)
            {
                _lastFrame = Runtime.CurrentFrame;

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
            return _lastFrame switch
            {
                3 => new FrameLoadLevelViewModel(),
                _ => null
            };
        }
    }
}
