using MauiAppUIDemo.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiAppUIDemo.Converter
{
    public class SelectedItemToTextColorConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            var selected = values[0] as MenuItemModel;
            var current = values[1] as MenuItemModel;

            if (selected == null || current == null)
                return Colors.White;

            if (selected == current || selected?.Parent == current)
                return Colors.Black;

            return Colors.White;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture) =>
            throw new NotImplementedException();
    }


}
