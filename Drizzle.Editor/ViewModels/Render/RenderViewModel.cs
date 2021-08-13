using System;
using System.Diagnostics;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Threading;
using System.Threading.Tasks;
using Drizzle.Editor.Views;
using Drizzle.Lingo.Runtime;
using Drizzle.Logic;
using Drizzle.Logic.Rendering;
using Drizzle.Ported;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Serilog;

namespace Drizzle.Editor.ViewModels.Render
{
    public sealed class RenderViewModel : ViewModelBase, ILingoRuntimeManager
    {
        private LevelRenderer? _renderer;
        private Thread? _renderThread;
        private readonly Subject<RenderStatus> _statusObservable = new();
        private bool _isPaused;

        [Reactive] public string LevelName { get; private set; } = "";
        [Reactive] public int CameraIndex { get; private set; }
        [Reactive] public RenderStage StageEnum { get; private set; }
        [Reactive] public RenderStageViewModelBase? StageViewModel { get; private set; }
        [Reactive] public int RenderProgressMax { get; private set; }
        [Reactive] public int RenderProgress { get; private set; }
        [Reactive] public bool RenderStageProgressAvailable { get; private set; }
        [Reactive] public int RenderStageProgressMax { get; private set; }
        [Reactive] public int RenderStageProgress { get; private set; }

        public bool IsPaused
        {
            get => _isPaused;
            set
            {
                this.RaiseAndSetIfChanged(ref _isPaused, value);
                _renderer?.SetPaused(value);
            }
        }

        public void StartRendering(LingoRuntime clonedRuntime)
        {
            var movie = (MovieScript)clonedRuntime.MovieScriptInstance;
            LevelName = movie.gLoadedName;
            var countCameras = (int)movie.gCameraProps.cameras.count;

            RenderProgressMax = countCameras * 10 + 1;

            _renderer = new LevelRenderer(clonedRuntime, null);
            _renderThread = new Thread(RenderThread) { Name = $"Render {LevelName}" };
            _renderer.StatusChanged += status => _statusObservable.OnNext(status);

            _statusObservable
                .Sample(TimeSpan.FromMilliseconds(15))
                .ObserveOn(RxApp.MainThreadScheduler)
                .Subscribe(
                // onNext
                x =>
                {
                    Log.Debug("Render status next: {Status}", x);

                    CameraIndex = x.CameraIndex;
                    StageEnum = x.Stage.Stage;
                    StageViewModel = x.Stage switch
                    {
                        RenderStageStatusLayers layers => new RenderStageLayersViewModel(layers),
                        RenderStageStatusEffects effects => new RenderStageEffectsViewModel(effects),
                        RenderStageStatusLight light => new RenderStageLightViewModel(light),
                        _ => null
                    };
                    this.RaiseAndSetIfChanged(ref _isPaused, x.IsPaused, nameof(IsPaused));

                    RenderProgress = x.CountCamerasDone * 10 + StageEnum switch
                    {
                        RenderStage.Start => 0,
                        RenderStage.CameraSetup => 0,
                        RenderStage.RenderLayers => 1,
                        RenderStage.RenderPropsPreEffects => 2,
                        RenderStage.RenderEffects => 3,
                        RenderStage.RenderPropsPostEffects => 4,
                        RenderStage.RenderLight => 5,
                        RenderStage.Finalize => 6,
                        RenderStage.RenderColors => 7,
                        RenderStage.Finished => 8,
                        RenderStage.SaveFile => 9,
                        _ => throw new ArgumentOutOfRangeException()
                    };

                    if (StageViewModel?.Progress is var (max, current))
                    {
                        RenderStageProgressAvailable = true;
                        RenderStageProgress = current;
                        RenderStageProgressMax = max;
                    }
                    else
                    {
                        RenderStageProgressAvailable = false;
                    }
                },
                // onError
                e =>
                {
                    StageViewModel = new RenderStageErrorViewModel(e);
                },
                // onCompleted.
                () =>
                {
                    StageViewModel = new RenderStageCompletedViewModel();
                    RenderProgress = RenderProgressMax;
                    RenderStageProgressAvailable = true;
                    RenderStageProgress = 1;
                    RenderStageProgressMax = 1;
                });

            _renderThread.Start();
        }

        private void RenderThread()
        {
            Debug.Assert(_renderer != null);

            try
            {
                _renderer.DoRender();
            }
            catch (Exception e)
            {
                _statusObservable.OnError(e);
                Log.Error(e, "Exception while rendering!");
            }

            _statusObservable.OnCompleted();
        }

        public void StopRender()
        {
            if (_renderThread?.IsAlive == true)
            {
                _renderer!.CancelRender();
                _renderThread.Join();
            }
        }

        public void SingleStep()
        {
            _renderer?.SingleStep();
        }

        public void OpenCastViewer()
        {
            new LingoCastViewer { DataContext = new LingoCastViewerViewModel(this) }.Show();
        }

        public Task Exec(Action<LingoRuntime> action)
        {
            if (_renderer == null)
                throw new InvalidOperationException();

            return _renderer.Exec(action);
        }

        public Task<T> Exec<T>(Func<LingoRuntime, T> func)
        {
            if (_renderer == null)
                throw new InvalidOperationException();

            return _renderer.Exec(func);
        }
    }
}
