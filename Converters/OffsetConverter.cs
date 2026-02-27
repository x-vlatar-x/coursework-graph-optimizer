using Avalonia.Data;
using Avalonia.Data.Converters;
using System;
using System.Globalization;

namespace GraphOptimizer.Converters
{
    internal class OffsetConverter: IValueConverter
    {
        public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if(value is double coord && parameter is string offsetStr && double.TryParse(offsetStr, out var offset))
            {
                return coord + offset;
            }
            return value;
        }

        public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            return BindingOperations.DoNothing;
        }
    }
}
