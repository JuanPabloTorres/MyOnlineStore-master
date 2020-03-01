using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace MyOnlineStore.Application.Presentation.PresentationHelpers.Converters
{
    public class TextToCheckConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {

            string check=string.Empty;
            if (value is bool)
            {
                if ((bool)value)
                    return check = "✓";
                else
                    return check = "X";
            }
            return check = "-";

        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }

    }
}
