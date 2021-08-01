using System;
using System.Diagnostics;

namespace Drizzle.Lingo.Runtime.Cast
{
    public sealed partial class CastMember
    {
        private string? _name;
        public LingoRuntime Runtime { get; }
        public int Number { get; }

        public CastMemberType Type { get; set; }

        public string? name
        {
            get => _name;
            set
            {
                _name = value;
                Runtime.UpdateNameIndex();
            }
        }

        public string Cast { get; }

        public CastMember(LingoRuntime runtime, int number, string cast)
        {
            Runtime = runtime;
            Number = number;
            Cast = cast;
        }

        public void erase()
        {
            Type = CastMemberType.Empty;
            _text = null;
            _image = null;
        }

        public void importfileinto(string path, LingoPropertyList? propList = null)
        {
            ImportFileImpl(Runtime.GetFilePath(path));
        }

        public void ImportFileImpl(string fullPath)
        {
            switch (Type)
            {
                case CastMemberType.Bitmap:
                    ImportFileImplBitmap(fullPath);
                    break;
                case CastMemberType.Script:
                    break;
                case CastMemberType.Shape:
                    break;
                case CastMemberType.Text:
                    ImportFileImplText(fullPath);
                    break;
            }
        }

        private void AssertType(CastMemberType type)
        {
            Debug.Assert(Type == type, "Wrong cast member type!");
        }
    }

    public enum CastMemberType
    {
        Empty,
        Bitmap,
        Script,
        Text,
        Shape
    }
}
