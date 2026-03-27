using Avalonia.Data;
using Avalonia.Data.Converters;
using System;
using System.Globalization;
using Avalonia;
using GraphOptimizer.ViewModels.GraphCore;

namespace GraphOptimizer.Converters
{
    public class VertexToPointConverter : IValueConverter
    {
        public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if(value is VertexViewModel vertex)
            {
                return new Point(vertex.X, vertex.Y);
            }

            return new Point(0, 0);
        }

        public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            return BindingOperations.DoNothing;
        }
    }
}
