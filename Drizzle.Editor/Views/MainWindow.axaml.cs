using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Disposables;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using Drizzle.Editor.ViewModels;
using ReactiveUI;

namespace Drizzle.Editor.Views;

public partial class MainWindow : ReactiveWindow<MainWindowViewModel>
{
    public MainWindow()
    {
        InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif

        this.WhenActivated(disposables =>
        {
            this.WhenAnyValue(x => x.ViewModel!.TabContent!.CountCameras)
                .Subscribe(cameras =>
                {
                    var menu = this.FindControl<MenuItem>("MenuRenderCamera");/*= Enumerable.Range(0, cameras);*/
                    menu.Items = Enumerable.Range(0, cameras).Select(c => new MenuItem
                    {
                        Header = $"Camera {c+1}",
                        Command = ReactiveCommand.Create(() => ViewModel!.RenderCamera(c))
                    }).ToList();
                })
                .DisposeWith(disposables);
        });
    }

    public async void OpenProject()
    {
        var dialog = new OpenFileDialog
        {
            AllowMultiple = true,
            Filters = new List<FileDialogFilter>
            {
                new()
                {
                    Name = "Level editor projects",
                    Extensions = { "txt" }
                }
            },
        };

        var result = await dialog.ShowAsync(this);

        if (result == null || result.Length == 0)
            return;

        ViewModel!.OpenProjects(result);
    }

    private void OpenAbout(object? sender, RoutedEventArgs e)
    {
        new AboutWindow().Show(this);
    }
}
