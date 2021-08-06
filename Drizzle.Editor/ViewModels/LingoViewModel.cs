using System;
using System.Diagnostics;
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

        [Reactive] public LingoFrameViewModel? Frame { get; private set; }

        [Reactive] public int LastFrame { get; private set; }
        [Reactive] public string? LastFrameName { get; private set; }

        [Reactive] public bool IsPaused { get; set; }
        private bool _singleStep;

        public event Action<int>? Update;

        public void Init(CommandLineArgs commandLineArgs)
        {
            IsPaused = commandLineArgs.AutoPause;

            // Avalonia doesn't actually tick the timer much faster than (what I hope) is monitor refresh, so...
            // We just set this stupid high then time it manually.
            // Yes I know this sucks for resource usage I didn't write the Lingo code.
            // TODO: Make Lingo only have higher tempo while rendering.
            _timer.Interval = TimeSpan.FromMilliseconds(1);
            _timer.Tick += TimerTickLingo;
            _timer.Start();

            Runtime.Init();
        }

        private void TimerTickLingo(object? sender, EventArgs e)
        {
            // Just run 16 lingo ticks every Avalonia tick.
            // TODO: less hilariously stupid/simplistic timing.
            for (var i = 0; i < 16; i++)
            {
                if (IsPaused && !_singleStep)
                    return;

                _singleStep = false;

                Runtime.Tick();

                Update?.Invoke(Runtime.CurrentFrame);
            }

            if (LastFrame != Runtime.CurrentFrame)
            {
                LastFrame = Runtime.CurrentFrame;
                LastFrameName = Runtime.LastFrameBehaviorName;

                var frameType = GetFrameViewModelType();
                if (frameType == null)
                {
                    if (Frame != null)
                    {
                        Frame = null;
                        Log.Debug("Clearing frame VM");
                    }
                }
                else
                {
                    Frame = (LingoFrameViewModel)Activator.CreateInstance(frameType)!;
                    try
                    {
                        Frame.OnLoad(this);
                    }
                    catch (Exception ex)
                    {
                        Log.Error(ex, "Exception in FrameVM load");
                        Frame = null;
                    }

                    Log.Debug("Switching frame VM to {FrameViewModelName}", Frame?.GetType().Name);
                }
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

        private Type? GetFrameViewModelType()
        {
            return LastFrame switch
            {
                3 => typeof(FrameLoadLevelViewModel),
                10 => typeof(FrameLevelOverviewViewModel),
                55 or 54 => typeof(FrameRenderEffectsViewModel),
                _ => null
            };
        }
    }
}
