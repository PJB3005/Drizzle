namespace Drizzle.Lingo.Runtime
{
    public sealed partial class LingoGlobal
    {
        public LingoNumber the_randomSeed
        {
            get => (int) LingoRuntime.RngSeed;
            set => LingoRuntime.RngSeed = (uint) value.integer;
        }

        public LingoNumber random(LingoNumber max)
        {
            // todo: should this return int?
            var sample = LingoRuntime.RngNext();
            var scaled = sample * (1.0 / uint.MaxValue);

            return (int)(scaled * max + 1).DecimalValue;
        }
    }
}
