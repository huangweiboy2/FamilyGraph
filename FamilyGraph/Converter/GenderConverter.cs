using System;
using System.Globalization;
using System.Linq;
using System.Windows.Data;
using FamilyGraph.Internal;

namespace FamilyGraph.Converter
{
    public class GenderConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value?.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Enum.Parse(typeof(Gender), value as string ?? "Male");
        }
    }
}