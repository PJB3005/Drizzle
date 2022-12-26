using System;
using System.Diagnostics;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Threading;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using Avalonia.Threading;
using Drizzle.Editor.Helpers;
using Drizzle.Editor.Views;
using Drizzle.Lingo.Runtime;
using Drizzle.Logic;
using Drizzle.Logic.Rendering;
using Drizzle.Ported;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Serilog;

namespace Drizzle.Editor.ViewModels.Render;

public sealed class RenderViewModel : ViewModelBase, ILingoRuntimeManager
{
    private const int PreviewWidth = 1440;
    private const int PreviewHeight = 860;

    private LevelRenderer? _renderer;
    private Thread? _renderThread;
    private readonly Subject<RenderStatus> _statusObservable = new();
    private bool _isPaused;
    private readonly Stopwatch _renderStopwatch = new();

    [Reactive] public string LevelName { get; private set; } = "";
    [Reactive] public int CameraIndex { get; private set; }
    [Reactive] public RenderStage StageEnum { get; private set; }
    [Reactive] public RenderStageViewModelBase? StageViewModel { get; private set; }
    [Reactive] public int RenderProgressMax { get; private set; }
    [Reactive] public int RenderProgress { get; private set; }
    [Reactive] public bool RenderStageProgressAvailable { get; private set; }
    [Reactive] public int RenderStageProgressMax { get; private set; }
    [Reactive] public int RenderStageProgress { get; private set; }
    public WriteableBitmap? RendererPreview { get; }
    [Reactive] public bool PreviewEnabled { get; set; } = true;

    private readonly LingoImage? _previewBuffer;

    public RenderViewModel()
    {
        if (PreviewEnabled)
        {
            _previewBuffer = new LingoImage(PreviewWidth, PreviewHeight, 32);
            RendererPreview = new WriteableBitmap(
                new PixelSize(PreviewWidth, PreviewHeight),
                new Vector(96, 96),
                PixelFormat.Bgra8888,
                AlphaFormat.Unpremul);
        }
    }

    public TimeSpan RenderTimeElapsed => _renderStopwatch.Elapsed;

    public bool IsPaused
    {
        get => _isPaused;
        set
        {
            this.RaiseAndSetIfChanged(ref _isPaused, value);
            _renderer?.SetPaused(value);

            if (value)
                _renderStopwatch.Stop();
            else
                _renderStopwatch.Start();
        }
    }

    public void StartRendering(LingoRuntime clonedRuntime, int? singleCamera)
    {
        var movie = (MovieScript)clonedRuntime.MovieScriptInstance;
        LevelName = movie.gLoadedName;
        var countCameras = singleCamera != null ? 1 : (int)movie.gCameraProps.cameras.count;

        RenderProgressMax = countCameras * 10 + 1;

        _renderer = new LevelRenderer(clonedRuntime, null, singleCamera);
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
                    _renderStopwatch.Stop();
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
                    _renderStopwatch.Stop();
                });

        _renderer.PreviewSnapshot += RendererOnPreviewSnapshot;

        _renderStopwatch.Start();
        _renderThread.Start();

        if (PreviewEnabled)
            _renderer.RequestPreview();
    }

    private void RendererOnPreviewSnapshot(RenderPreview obj)
    {
        // ReSharper disable once AsyncVoidLambda
        Dispatcher.UIThread.Post(async () =>
        {
            await Task.Run(() => { MergePreview(_previewBuffer!, obj); });

            LingoImageAvaloniaHelper.CopyToBitmap(_previewBuffer!, RendererPreview!);

            _renderer?.RequestPreview();
        });
    }

    private static void MergePreview(LingoImage dst, RenderPreview preview)
    {
        switch (preview)
        {
            case RenderPreviewEffects effects:
                dst.fill(LingoColor.White);
                MergeFadingPreviewLayers(dst, effects.Layers);
                CopyLayerToPreview(dst, effects.BlackOut1);
                CopyLayerToPreview(dst, effects.BlackOut2);
                break;
            case RenderPreviewLights lights:
                dst.fill(LingoColor.Black);
                MergePreviewLayers(dst, lights.Layers);
                break;
            case RenderPreviewProps props:
                dst.fill(LingoColor.White);
                MergeFadingPreviewLayers(dst, props.Layers);
                break;
        }
    }

    private static void MergeFadingPreviewLayers(LingoImage dst, LingoImage[] layers)
    {
        for (var i = layers.Length - 1; i >= 0; i--)
        {
            var colorVal = (i + 1) / (float)layers.Length;
            var color = new LingoColor(
                (byte)(colorVal * 255),
                (byte)(colorVal * 255),
                (byte)(colorVal * 255));

            CopyLayerToPreview(dst, layers[i], i, color);
        }
    }

    private static void MergePreviewLayers(LingoImage dst, LingoImage[] layers)
    {
        for (var i = layers.Length - 1; i >= 0; i--)
        {
            CopyLayerToPreview(dst, layers[i], i, LingoColor.White);
        }
    }

    private static void CopyLayerToPreview(LingoImage dst, LingoImage src, int offset = 0, LingoColor color = default)
    {
        var srcX = (src.Width - dst.Width) / 2;
        var srcY = (src.Height - dst.Height) / 2;

        dst.copypixels(
            src,
            new LingoRect(0 - offset, 0 - offset, PreviewWidth - offset, PreviewHeight - offset),
            new LingoRect(srcX, srcY, srcX + PreviewWidth, srcY + PreviewHeight),
            new LingoPropertyList
            {
                [new LingoSymbol("ink")] = (LingoNumber)36, // bg transparent
                [new LingoSymbol("color")] = color
            });
    }

    private void RenderThread()
    {
        Debug.Assert(_renderer != null);

        try
        {
            _renderer.DoRender();
        }
        catch (RenderCancelledException)
        {
            // Nada.
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
