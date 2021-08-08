using System;

namespace Drizzle.Logic
{
    public abstract record RenderCmd;

    public record RenderCmdSetPaused(bool Paused) : RenderCmd;

    public record RenderCmdSingleStep : RenderCmd;

    public record RenderCmdCancel : RenderCmd;

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
