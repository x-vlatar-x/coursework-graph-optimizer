using Avalonia.Data;
using Avalonia.Data.Converters;
using GraphOptimizer.Enums;
using System;
using System.ComponentModel;
using System.Globalization;
using System.Reflection;

namespace GraphOptimizer.Converters
{
    public class EnumToDescriptionConverter : IValueConverter
    {
        public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is Enum enumValue)
            {
                string stringValue = enumValue.ToString();
                FieldInfo? field = enumValue.GetType().GetField(stringValue);

                if (field != null)
                {
                    var attribute = field.GetCustomAttribute<DescriptionAttribute>();
                    if (attribute != null)
                    {
                        return attribute.Description;
                    }
                }
                return stringValue;
            }

            //return "Оберіть метод розв'язання";
            return parameter?.ToString() ?? "";
        }

        public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            return BindingOperations.DoNothing;
        }
    }
}