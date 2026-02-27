using Avalonia.Data;
using Avalonia.Data.Converters;
using System;
using System.Globalization;

namespace GraphOptimizer.Converters
{
    internal class EqualityConverter: IValueConverter
    {
        public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            return value?.ToString() == parameter?.ToString();
        }
        public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            return BindingOperations.DoNothing;
        }
    }
}
