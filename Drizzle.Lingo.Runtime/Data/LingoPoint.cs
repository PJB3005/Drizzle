namespace Drizzle.Lingo.Runtime
{
    public struct LingoPoint
    {
        public int loch;
        public int locv;

        public LingoPoint(int loch, int locv)
        {
            this.loch = loch;
            this.locv = locv;
        }

        public static LingoPoint operator +(LingoPoint a, LingoPoint b)
        {
            return new(a.loch + b.loch, a.locv + b.locv);
        }

        public static LingoPoint operator -(LingoPoint a, LingoPoint b)
        {
            return new(a.loch - b.loch, a.locv - b.locv);
        }

        public static LingoPoint operator *(LingoPoint a, LingoPoint b)
        {
            return new(a.loch * b.loch, a.locv * b.locv);
        }

        public static LingoPoint operator /(LingoPoint a, LingoPoint b)
        {
            return new(a.loch / b.loch, a.locv / b.locv);
        }

        public static LingoPoint operator +(LingoPoint a, int b)
        {
            return new(a.loch + b, a.locv + b);
        }

        public static LingoPoint operator -(LingoPoint a, int b)
        {
            return new(a.loch - b, a.locv - b);
        }

        public static LingoPoint operator *(LingoPoint a, int b)
        {
            return new(a.loch * b, a.locv * b);
        }

        public static LingoPoint operator /(LingoPoint a, int b)
        {
            return new(a.loch / b, a.locv / b);
        }

        public int inside(LingoRect rect)
        {
            var b = rect.intLeft >= loch && rect.intTop >= locv &&
                    rect.intRight <= loch && rect.intBottom <= locv;

            return b ? 1 : 0;
        }
    }
}
