using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Linq;
using Avalonia.Media.Imaging;
using Drizzle.Editor.Helpers;
using Drizzle.Lingo.Runtime;
using Drizzle.Lingo.Runtime.Cast;
using Drizzle.Logic;
using DynamicData;
using DynamicData.Binding;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace Drizzle.Editor.ViewModels;

public sealed class LingoCastViewerViewModel : ViewModelBase
{
    private static readonly string[] CastsToLoad =
    {
        "Internal",
        "levelEditor"
    };

    private readonly ILingoRuntimeManager _lingo;

    public ObservableCollection<LingoCastViewerCast> Casts { get; } = new();

    public LingoCastViewerViewModel(ILingoRuntimeManager lingo)
    {
        _lingo = lingo;

        this.WhenAnyValue(e => e.SelectedCastMember)
            // ReSharper disable once AsyncVoidLambda
            .Subscribe(async e =>
            {
                if (e == null)
                {
                    CurrentLingoImage = null;
                    CurrentImage = null;
                    return;
                }

                var img = await _lingo.Exec(runtime => runtime.GetCastMember(e.Number)!.image!.duplicate());
                CurrentLingoImage = img;
                CurrentImage = LingoImageAvaloniaHelper.LingoImageToBitmap(img, false);
            });

        foreach (var castName in CastsToLoad)
        {
            Casts.Add(new LingoCastViewerCast(castName));
        }
    }

    [Reactive] public string Status { get; set; } = "A";

    [Reactive] public CastMemberViewModel? SelectedCastMember { get; set; }

    // ReSharper disable once UnassignedGetOnlyAutoProperty
    [Reactive] public Bitmap? CurrentImage { get; private set; }
    [Reactive] public LingoImage? CurrentLingoImage { get;  private set; }

    public async void Refresh()
    {
        var members = await _lingo.Exec(runtime =>
        {
            return CastsToLoad.Select(castName =>
            {
                var cast = runtime.GetCastLib(castName);
                var members = new List<CastMemberViewModel>();

                for (var i = 1; i <= cast.NumMembers; i++)
                {
                    var member = cast.GetMember(i)!;

                    if (member.Type != CastMemberType.Bitmap)
                        continue;

                    var img = member.image!;
                    var bitmap = LingoImageAvaloniaHelper.LingoImageToBitmap(img, true);
                    var entry = new CastMemberViewModel(
                        bitmap,
                        cast.name,
                        member.name, member.Number,
                        img.Width, img.Height, img.Type);

                    members.Add(entry);
                }

                return (castName, members);
            }).ToDictionary(c => c.castName, c => c.members);
        });

        foreach (var castModel in Casts)
        {
            castModel.UnfilteredEntries.Edit(e =>
            {
                e.Clear();

                e.AddRange(members[castModel.CastLibNameName]);
            });
        }
    }

    public void OpenImage()
    {
        CurrentLingoImage?.ShowImage();
    }

    public void Closed()
    {
    }

    public void Opened()
    {
        Refresh();
    }
}

public sealed class LingoCastViewerCast : ViewModelBase
{
    public LingoCastViewerCast(string castLibName)
    {
        CastLibNameName = castLibName;

        var searchSelect = this.WhenAnyValue(x => x.Search)
            .Select<string?, Func<CastMemberViewModel, bool>>(search =>
                string.IsNullOrWhiteSpace(search)
                    ? _ => true
                    : x => x.Name != null && x.Name.Contains(search));

        UnfilteredEntries
            .Connect()
            .Filter(searchSelect)
            .Sort(SortExpressionComparer<CastMemberViewModel>.Ascending(x => x.Number))
            .Bind(out ReadOnlyObservableCollection<CastMemberViewModel> entries)
            .Subscribe();

        Entries = entries;
    }

    public string CastLibNameName { get; }

    [Reactive] public string Search { get; set; } = "";
    public SourceList<CastMemberViewModel> UnfilteredEntries { get; } = new();
    public ReadOnlyObservableCollection<CastMemberViewModel> Entries { get; }
}

public sealed class CastMemberViewModel
{
    public Bitmap Thumbnail { get; }
    public string? Name { get; }
    public int Number { get; }
    public string CastName { get; }

    public string NameOrNumber => Name == null ? Number.ToString() : $"{Name} ({Number})";

    public int Width { get; }
    public int Height { get; }
    public ImageType ImageType { get; }

    public CastMemberViewModel(
        Bitmap thumbnail,
        string castName,
        string? name,
        int number,
        int imgWidth,
        int imgHeight,
        ImageType imgType)
    {
        Thumbnail = thumbnail;
        CastName = castName;
        Name = name;
        Number = number;
        Width = imgWidth;
        Height = imgHeight;
        ImageType = imgType;
    }
}
