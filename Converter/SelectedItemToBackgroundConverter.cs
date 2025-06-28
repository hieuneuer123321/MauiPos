using System.Globalization;
using Microsoft.Maui.Controls;
using System;
using MauiAppUIDemo.Models;

namespace MauiAppUIDemo.Converter
{
    public class SelectedItemToBackgroundConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            var selected = values[0] as MenuItemModel;
            var current = values[1] as MenuItemModel;

            if (selected == null || current == null)
                return Colors.Transparent;

            // Màu khi menu con được chọn
            var selectedColor = Color.FromArgb("#FF9900");
            // Màu khi menu cha của menu con được chọn
            var parentColor = Color.FromArgb("#FF9933"); // tông gần giống hơn

            if (selected == current)
                return selectedColor;

            if (selected.Parent == current)
                return parentColor;

            return Colors.Transparent;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture) =>
            throw new NotImplementedException();
    }
}
