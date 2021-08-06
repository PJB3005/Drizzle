using Drizzle.Lingo.Runtime;
using Drizzle.Ported;

namespace Drizzle.Editor.ViewModels
{
    /// <summary>
    ///
    /// </summary>
    public abstract class LingoFrameViewModel : ViewModelBase
    {
        public LingoRuntime Runtime => Lingo.Runtime;
        public LingoViewModel Lingo { get; private set; }

        public virtual void OnLoad(LingoViewModel lingo)
        {
            Lingo = lingo;
        }

        public virtual void OnUpdate()
        {
        }

        protected MovieScript MovieScript => (MovieScript)Runtime.MovieScriptInstance;
    }
}
