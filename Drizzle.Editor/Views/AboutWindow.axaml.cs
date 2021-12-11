using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Intrinsics.X86;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace Drizzle.Editor.Views;

public sealed partial class AboutWindow : Window
{
    public AboutWindow()
    {
        AvaloniaXamlLoader.Load(this);

#if DEBUG
        this.AttachDevTools();
#endif

        var i = 0;
        var grid = this.FindControl<Grid>("InfoGrid");
        AddLine("Version:", Assembly.GetExecutingAssembly().GetName().Version?.ToString() ?? "");
        AddLine("Runtime:", RuntimeInformation.FrameworkDescription);
        AddLine("Platform:", RuntimeInformation.RuntimeIdentifier);
        AddLine("CPU features:", Avx2.IsSupported ? "AVX2" : "None");

        for (var j = 0; j < i; j++)
        {
            grid.RowDefinitions.Add(new RowDefinition(GridLength.Auto));
        }

        void AddLine(string desc, string value)
        {
            var txtDesc = new TextBlock { Text = desc };
            Grid.SetColumn(txtDesc, 0);
            Grid.SetRow(txtDesc, i);

            var txtValue = new TextBlock { Text = value };
            Grid.SetColumn(txtValue, 1);
            Grid.SetRow(txtValue, i);

            grid.Children.Add(txtDesc);
            grid.Children.Add(txtValue);

            i += 1;
        }
    }
}
