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
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.Length < 2)
                return Colors.White;

            var discount = values[0] as DiscountCode;
            var selectedId = values[1] as Guid?;

            if (discount == null || selectedId == null)
                return Colors.White;

            if (!discount.IsValidForTotalCached)
                return Color.FromArgb("#eeeeee"); // không hợp lệ

            if (discount.Id == selectedId.Value)
                return Color.FromArgb("#D1C4E9"); // mã đang được chọn

            return Colors.White; // mặc định
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
            => throw new NotImplementedException();
    }
}

