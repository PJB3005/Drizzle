using Drizzle.Lingo.Runtime;

namespace Drizzle.Ported
{
    public abstract class LingoScriptBase
    {
        protected MovieScript _movieScript;
        protected LingoGlobal _global;

        public void Init(MovieScript movieScript, LingoGlobal global)
        {
            _movieScript = movieScript;
            _global = global;
        }
    }
}
