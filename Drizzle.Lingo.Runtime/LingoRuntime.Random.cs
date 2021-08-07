namespace Drizzle.Lingo.Runtime
{
    public partial class LingoRuntime
    {
        // Current RNG algorithm is Xorshift32, because it fits in 32 bit integers which the seed values are.
        // May want to consider using a better RNG algorithm
        // although the pain is that randomseed would have to return an opaque,
        // non-int value and idk if the editor code will allow that.

        public uint RngSeed { get; set; } = 1;

        public uint RngNext()
        {
            var x = RngSeed;
            x ^= x << 13;
            x ^= x >> 7;
            x ^= x << 17;
            return RngSeed = x;
        }
    }
}
