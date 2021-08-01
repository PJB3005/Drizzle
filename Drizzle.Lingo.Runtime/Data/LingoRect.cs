namespace Drizzle.Lingo.Runtime
{
    public struct LingoRect
    {
        public int left;
        public int top;
        public int right;
        public int bottom;

        public int width => right - left;
        public int height => bottom - top;

        public LingoRect(int left, int top, int right, int bottom)
        {
            this.left = left;
            this.top = top;
            this.right = right;
            this.bottom = bottom;
        }

        public LingoRect(LingoPoint lt, LingoPoint rb) : this(lt.loch, lt.locv, rb.loch, rb.locv)
        {
        }

        public static LingoRect operator +(LingoRect a, LingoRect b)
        {
            return new(
                a.left + b.left,
                a.top + b.top,
                a.right + b.right,
                a.bottom + b.bottom);
        }

        public static LingoRect operator *(LingoRect a, int b)
        {
            return new(
                a.left * b,
                a.top * b,
                a.right * b,
                a.bottom * b);
        }
    }
}
