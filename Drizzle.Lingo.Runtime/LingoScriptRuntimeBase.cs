namespace Drizzle.Lingo.Runtime
{
    // NOT a script instance, those are different!
    public abstract class LingoScriptRuntimeBase
    {
        public abstract void Init(object movieScript, LingoGlobal global);
    }
}
