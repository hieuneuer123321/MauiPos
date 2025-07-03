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
            if (values.Length < 2 || values[0] is not DiscountCode code || values[1] is not Guid selectedId)
                return Colors.White;

            bool isSelected = code.Id == selectedId;
            bool isValid = code.Id == Guid.Empty || (code.IsValidNow && code.IsValidForTotalCached);

            if (isSelected)
                return Color.FromArgb("#E0D7F5"); // màu được chọn
            else if (!isValid)
                return Color.FromArgb("#EEEEEE"); // màu xám nhạt cho không hợp lệ
            else
                return Colors.White; // mặc định
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
            => throw new NotImplementedException();
    }
    public class GuidNotEmptyConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Guid id)
                return id != Guid.Empty;
            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => throw new NotImplementedException();
    }
    public class DiscountDisplayConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is MauiAppUIDemo.Models.DiscountCode discount)
            {
                return discount.Id == Guid.Empty ? "Chọn mã giảm giá" : discount.DisplayText;
            }

            return "Chọn mã giảm giá";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => throw new NotImplementedException();
    }
}

