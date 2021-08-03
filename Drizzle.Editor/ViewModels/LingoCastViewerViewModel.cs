using System;
using System.Collections.ObjectModel;
using System.Reactive.Linq;
using Avalonia;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using Drizzle.Lingo.Runtime;
using Drizzle.Lingo.Runtime.Cast;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;

namespace Drizzle.Editor.ViewModels
{
    public sealed class LingoCastViewerViewModel : ViewModelBase
    {
        private readonly LingoViewModel _lingo;

        public ObservableCollection<LingoCastViewerCast> Casts { get; } = new();

        public LingoCastViewerViewModel(LingoViewModel lingo)
        {
            _lingo = lingo;

            this.WhenAnyValue(e => e.SelectedCastMember)
                .Select(e => e == null ? null : LingoImageToBitmap(e.CastMember.image!, false))
                .ToPropertyEx(this, x => x.CurrentImage);

            Casts.Add(new LingoCastViewerCast(_lingo.Runtime.GetCastLib("Internal")));
            Casts.Add(new LingoCastViewerCast(_lingo.Runtime.GetCastLib("levelEditor")));
        }

        [Reactive] public string Status { get; set; } = "A";

        [Reactive] public CastMemberViewModel? SelectedCastMember { get; set; }

        // ReSharper disable once UnassignedGetOnlyAutoProperty
        [ObservableAsProperty] public Bitmap? CurrentImage { get; }

        public void Refresh()
        {
            foreach (var castModel in Casts)
            {
                castModel.Entries.Clear();

                var cast = castModel.CastLib;
                for (var i = 1; i <= cast.NumMembers; i++)
                {
                    var member = cast.GetMember(i)!;

                    if (member.Type != CastMemberType.Bitmap)
                        continue;

                    var bitmap = LingoImageToBitmap(member.image!, true);
                    var entry = new CastMemberViewModel(bitmap, cast.name, member);

                    castModel.Entries.Add(entry);
                }
            }
        }

        private static unsafe Bitmap LingoImageToBitmap(LingoImage img, bool thumbnail)
        {
            var finalImg = img.Image;

            if (thumbnail)
                finalImg = finalImg.Clone(op => op.Resize(50, 50));

            var bgra = finalImg.CloneAs<Bgra32>();

            if (!bgra.TryGetSinglePixelSpan(out var span))
                throw new InvalidOperationException();

            fixed (Bgra32* data = span)
            {
                return new Bitmap(
                    PixelFormat.Bgra8888,
                    AlphaFormat.Unpremul,
                    (nint)data,
                    new PixelSize(bgra.Width, bgra.Height),
                    new Vector(96, 96),
                    sizeof(Bgra32) * bgra.Width);
            }
        }
    }

    public sealed class LingoCastViewerCast : ViewModelBase
    {
        public LingoCastViewerCast(LingoCastLib castLib)
        {
            CastLib = castLib;
        }

        public LingoCastLib CastLib { get; }

        public ObservableCollection<CastMemberViewModel> Entries { get; } = new();
    }

    public sealed class CastMemberViewModel
    {
        public Bitmap Thumbnail { get; }
        public string? Name => CastMember.name;
        public int Number => CastMember.Number;
        public string CastName { get; }
        public CastMember CastMember { get; }

        public string NameOrNumber => Name == null ? Number.ToString() : $"{Name} ({Number})";

        public CastMemberViewModel(Bitmap thumbnail, string castName, CastMember castMember)
        {
            Thumbnail = thumbnail;
            CastName = castName;
            CastMember = castMember;
        }
    }
}
