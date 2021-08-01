using Drizzle.Lingo.Runtime.Scripting;

namespace Drizzle.Lingo.Runtime
{
    public sealed partial class LingoGlobal
    {
        public LingoRuntime LingoRuntime { get; }

        public LingoGlobal(LingoRuntime lingoRuntime)
        {
            LingoRuntime = lingoRuntime;
        }

        public void Init()
        {
            _system = new System(this);
            _key = new Key(this);
            _mouse = new Mouse(this);
            _movie = new Movie(this);
            _global = new Global(this);
            ScriptRuntime = new LingoScriptRuntime(this);
        }
    }
}
