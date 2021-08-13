using System;

namespace Drizzle.Logic.Rendering
{
    public abstract record RenderCmd;

    public record RenderCmdSetPaused(bool Paused) : RenderCmd;

    public record RenderCmdSingleStep : RenderCmd;

    public record RenderCmdCancel : RenderCmd;

    public record RenderCmdExec(Action Action) : RenderCmd;

    [Serializable]
    public class RenderCancelledException : OperationCanceledException
    {
        public RenderCancelledException()
        {
        }

        public RenderCancelledException(string message) : base(message)
        {
        }

        public RenderCancelledException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}
