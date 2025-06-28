﻿using System;
using System.Globalization;
using Microsoft.Maui.Controls;

namespace MauiAppUIDemo.Converter;

public class BoolToArrowConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        bool isVisible = (bool)value;
        return isVisible ? "▼" : "▶";
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        => throw new NotImplementedException();
}
