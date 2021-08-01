using Drizzle.Lingo.Runtime;
using Drizzle.Ported;

namespace Drizzle.Editor.ViewModels
{
    /// <summary>
    ///
    /// </summary>
    public abstract class LingoFrameViewModel : ViewModelBase
    {
        public LingoRuntime Runtime { get; private set; }

        public virtual void OnLoad(LingoRuntime runtime)
        {
            Runtime = runtime;
        }

        public virtual void OnUpdate()
        {
        }

        protected MovieScript MovieScript => (MovieScript)Runtime.MovieScriptInstance;
    }
}
