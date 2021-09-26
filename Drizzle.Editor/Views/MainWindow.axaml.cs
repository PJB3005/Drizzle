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

namespace Drizzle.Editor.Views
{
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

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        private void OnKeyDown(object? sender, KeyEventArgs e)
        {
            /*
            if (e.Key == Key.Escape)
            {
                KeyboardDevice.Instance.SetFocusedElement(null, NavigationMethod.Unspecified, KeyModifiers.None);
                return;
            }

            if (DataContext is not MainWindowViewModel vm)
                return;

            if (!KeyMap.Map.TryGetValue(e.Key, out var code))
                return;

            vm.MapEditorVM.Lingo.Runtime.KeysDown.Add(code);
        */
        }

        private void OnKeyUp(object? sender, KeyEventArgs e)
        {
            /*if (DataContext is not MainWindowViewModel vm)
                return;

            if (!KeyMap.Map.TryGetValue(e.Key, out var code))
                return;

            vm.MapEditorVM.Lingo.Runtime.KeysDown.Remove(code);*/
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

            if (result.Length == 0)
                return;

            ViewModel!.OpenProjects(result);
        }
    }
}
