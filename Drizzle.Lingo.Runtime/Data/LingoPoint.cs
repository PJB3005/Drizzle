namespace Drizzle.Lingo.Runtime
{
    public struct LingoPoint
    {
        public int locH;
        public int locV;

        public LingoPoint(int locH, int locV)
        {
            this.locH = locH;
            this.locV = locV;
        }

        public static LingoPoint operator +(LingoPoint a, LingoPoint b)
        {
            return new(a.locH + b.locH, a.locV + b.locV);
        }
    }
}
