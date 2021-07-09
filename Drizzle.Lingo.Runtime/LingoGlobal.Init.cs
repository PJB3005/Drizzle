namespace Drizzle.Lingo.Runtime
{
    public sealed partial class LingoGlobal
    {
        public void Init()
        {
            _system = new System(this);
            _key = new Key(this);
            _mouse = new Mouse(this);
        }
    }
}
