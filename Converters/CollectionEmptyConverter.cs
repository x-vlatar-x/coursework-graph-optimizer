using Avalonia.Data;
using Avalonia.Data.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphOptimizer.Converters
{
    public class CollectionEmptyConverter: IValueConverter
    {
        public object Convert(object? value, Type targetType, object? parameter, System.Globalization.CultureInfo culture)
        {
            if(value is System.Collections.IEnumerable enumerable)
            {
                return !enumerable.Cast<object>().Any();
            }

            return true;
        }
        public object ConvertBack(object? value, Type targetType, object? parameter, System.Globalization.CultureInfo culture)
        {
            return BindingOperations.DoNothing;
        }
    }
}
