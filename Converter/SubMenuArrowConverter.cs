using MauiAppUIDemo.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiAppUIDemo.Converter
{
    public class SubMenuArrowConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is MenuItemModel menu)
            {
                if (menu.SubMenuItems?.Count > 0)
                    return menu.IsSubMenuVisible ? "▼" : "▶";
            }
            return null; // Không hiện gì nếu không có submenu
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => throw new NotImplementedException();
    }
}
