namespace Drizzle.Lingo.Runtime.Cast
{
    public partial class CastMember
    {
        private LingoImage? _image;

        public LingoImage? image
        {
            get
            {
                AssertType(CastMemberType.Bitmap);
                return _image;
            }
            set
            {
                AssertType(CastMemberType.Bitmap);
                _image = value;
            }
        }

        public LingoRect rect
        {
            get
            {
                AssertType(CastMemberType.Bitmap);
                return _image.rect;
            }
        }

        public dynamic getpixel(int x, int y) => image.getpixel(x, y);

        public LingoPoint regpoint { get; set; }

        private void ImportFileImplBitmap(string path)
        {
            image = LingoImage.LoadFromPath(path);
        }
    }
}
