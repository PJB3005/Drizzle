namespace Drizzle.Lingo.Runtime
{
    public struct LingoPoint
    {
        // Yes, of course
        // Despite what the documentation clearly states
        // These contain float coordinates, not int.

        public LingoDecimal loch;
        public LingoDecimal locv;

        public LingoPoint(int loch, int locv)
        {
            this.loch = loch;
            this.locv = locv;
        }

        public LingoPoint(LingoDecimal loch, LingoDecimal locv)
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

        public static LingoPoint operator +(LingoPoint a, LingoDecimal b)
        {
            return new(a.loch + b, a.locv + b);
        }

        public static LingoPoint operator -(LingoPoint a, LingoDecimal b)
        {
            return new(a.loch - b, a.locv - b);
        }

        public static LingoPoint operator *(LingoPoint a, LingoDecimal b)
        {
            return new(a.loch * b, a.locv * b);
        }

        public static LingoPoint operator /(LingoPoint a, LingoDecimal b)
        {
            return new(a.loch / b, a.locv / b);
        }

        public int inside(LingoRect rect)
        {
            var b = rect.left >= loch && rect.top >= locv &&
                    rect.right <= loch && rect.bottom <= locv;

            return b ? 1 : 0;
        }
    }
}
