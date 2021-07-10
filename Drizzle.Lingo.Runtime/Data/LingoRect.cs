namespace Drizzle.Lingo.Runtime
{
    public struct LingoRect
    {
        public int intLeft;
        public int intTop;
        public int intRight;
        public int intBottom;

        public LingoRect(int left, int top, int right, int bottom)
        {
            intLeft = left;
            intTop = top;
            intRight = right;
            intBottom = bottom;
        }

        public LingoRect(LingoPoint lt, LingoPoint rb) : this(lt.loch, lt.locv, rb.loch, rb.locv)
        {
        }

        public static LingoRect operator +(LingoRect a, LingoRect b)
        {
            return new(
                a.intLeft + b.intLeft,
                a.intTop + b.intTop,
                a.intRight + b.intRight,
                a.intBottom + b.intBottom);
        }

        public static LingoRect operator *(LingoRect a, int b)
        {
            return new(
                a.intLeft * b,
                a.intTop * b,
                a.intRight * b,
                a.intBottom * b);
        }
    }
}
