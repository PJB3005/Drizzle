namespace Drizzle.Lingo.Runtime.Cast
{
    public sealed partial class CastMember
    {
        public int _lineDirection;

        public int linedirection
        {
            get
            {
                AssertType(CastMemberType.Shape);
                return _lineDirection;
            }
            set
            {
                AssertType(CastMemberType.Shape);
                _lineDirection = value;
            }
        }
    }
}
