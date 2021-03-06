﻿using System;
using System.Globalization;
using Xamarin.Forms;

namespace MyOnlineStore.Application.Presentation.PresentationHelpers.Converters
{
    public class ValidGreaterThanTimeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool isValid = false;

            if(value is TimeSpan time && parameter is TimeSpan time1)
            {
                if(time > time1) { isValid = true; }
            }

            return isValid;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}