using System.IO;

namespace Drizzle.Lingo.Runtime.Cast
{
    public sealed partial class CastMember
    {
        private string _text = "";

        public string text
        {
            get
            {
                AssertType(CastMemberType.Text);
                return _text;
            }
            set
            {
                AssertType(CastMemberType.Text);
                _text = value;
            }
        }

        public LingoSymbol alignment { get; set; }

        private void ImportFileImplText(string path)
        {
            text = File.ReadAllText(path);
        }
    }
}
