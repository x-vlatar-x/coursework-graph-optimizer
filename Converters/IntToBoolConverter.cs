using Avalonia.Data;
using Avalonia.Data.Converters;
using System;

namespace GraphOptimizer.Converters
{
    public class IntToBoolConverter : IValueConverter
    {
        public object Convert(object? value, Type targetType, object? parameter, System.Globalization.CultureInfo culture)
        {
            if (value is int count)
            {
                bool isEmpty = (count == 0);

                if (parameter as string == "Invert")
                    return isEmpty;

                return !isEmpty;
            }

            return true;
        }
        public object ConvertBack(object? value, Type targetType, object? parameter, System.Globalization.CultureInfo culture)
        {
            return BindingOperations.DoNothing;
        }
    }
}
