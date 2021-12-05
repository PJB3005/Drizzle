using System;
using System.Globalization;
using Avalonia.Data;
using Avalonia.Data.Converters;

namespace Drizzle.Editor.Helpers;

public sealed class EnumBoolConverter : IValueConverter
{
    public static EnumBoolConverter Instance { get; } = new();

    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        return value?.Equals(parameter) ?? false;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        return value?.Equals(true) == true ? parameter : BindingOperations.DoNothing;
    }
}