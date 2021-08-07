namespace Drizzle.Lingo.Runtime
{
    public sealed partial class LingoGlobal
    {
        public LingoDecimal the_randomSeed
        {
            get => (int) LingoRuntime.RngSeed;
            set => LingoRuntime.RngSeed = (uint) value.integer;
        }

        public int random(int max)
        {
            var sample = LingoRuntime.RngNext();
            var scaled = sample * (1.0 / uint.MaxValue);

            return (int)(scaled * max) + 1;
        }

        public int random(LingoDecimal max)
        {
            // todo: should this return int?
            return random((int) max);
        }

    }
}
