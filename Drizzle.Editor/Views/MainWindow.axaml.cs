using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Markup.Xaml;
using Drizzle.Editor.ViewModels;

namespace Drizzle.Editor.Views
{
    public partial class MainWindow : Window
    {
        private MainWindowViewModel? _viewModel;

        public MainWindow()
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
        }

        protected override void OnDataContextChanged(EventArgs e)
        {
            base.OnDataContextChanged(e);

            if (_viewModel != null)
                _viewModel.Parent = null;

            _viewModel = DataContext as MainWindowViewModel;

            if (_viewModel != null)
                _viewModel.Parent = this;
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
    }
}
