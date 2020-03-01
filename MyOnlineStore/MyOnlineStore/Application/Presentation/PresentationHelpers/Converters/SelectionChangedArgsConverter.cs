using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace MyOnlineStore.Application.Presentation.PresentationHelpers.Converters
{
    public class SelectionChangedArgsConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var converterValue = value as SelectionChangedEventArgs;

            if (converterValue is object)
            {
                value = converterValue;
            }

            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
