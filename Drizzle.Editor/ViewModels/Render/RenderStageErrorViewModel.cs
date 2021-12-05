using System;

namespace Drizzle.Editor.ViewModels.Render;

public sealed class RenderStageErrorViewModel : RenderStageViewModelBase
{
    public string ExceptionMessage { get; }

    public RenderStageErrorViewModel(Exception exception)
    {
        ExceptionMessage = exception.ToString();
    }
}