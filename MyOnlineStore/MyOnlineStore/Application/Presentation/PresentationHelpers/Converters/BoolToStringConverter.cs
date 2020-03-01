using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace MyOnlineStore.Application.Presentation.PresentationHelpers.Converters
{
    public class BoolToStringConverter : IValueConverter
    {
        public string StringResult { get; set; } = string.Empty;
        public bool BooleanResult { get; set; }
        public bool BooleanValue { get; set; }
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (parameter!=null)
            {
                BooleanValue = !bool.Parse(value.ToString());
            }
            else
            {
                BooleanValue = bool.Parse(value.ToString());
            }

            StringResult = BooleanValue.ToString();
            
            return StringResult;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            BooleanValue = bool.Parse(value.ToString());
            BooleanResult = BooleanValue;

            return BooleanResult;
        }
    }
}
