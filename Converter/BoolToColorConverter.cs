using MauiAppUIDemo.Models;
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
    public class DiscountSelectedColorConverter : IMultiValueConverter
    {
        public Color SelectedColor { get; set; } = Color.FromArgb("#D1C4E9"); // màu khi chọn
        public Color DefaultColor { get; set; } = Colors.White;               // màu bình thường

        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.Length >= 2 &&
                values[0] is MauiAppUIDemo.Models.DiscountCode discount &&
                values[1] is Guid selectedId)
            {
                return discount.Id == selectedId ? SelectedColor : DefaultColor;
            }

            return DefaultColor;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
            => throw new NotImplementedException();
    }
}

