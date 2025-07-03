using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiAppUIDemo.Converter
{
    public class BoolToColorConverter : IValueConverter
    {
        public Color ValidColor { get; set; } = Colors.White;
        public Color InvalidColor { get; set; } = Color.FromRgba("#eeeeee");

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool isValid)
                return isValid ? ValidColor : InvalidColor;

            return InvalidColor;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new NotImplementedException();
    }
    public class BoolToppingToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (bool)value ? Color.FromArgb("#E0D7F5") : Color.FromArgb("#F5F5F5");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => throw new NotImplementedException();
    }
}

