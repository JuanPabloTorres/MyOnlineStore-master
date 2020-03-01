using System;
using Xamarin.Forms;

namespace MyOnlineStore.Application.Presentation.PresentationHelpers.Converters
{
    class AvailableColorConverter:IValueConverter
    {
        #region IValueConverter implementation

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is bool)
            {
                if ((Boolean)value)
                    return Color.FromHex("#b2ff59");
                else
                    return Color.FromHex("#ff5252");
            }
            return Color.Black;
        }

        //public object Convert1(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        //{
        //    if (value is float)
        //    {
        //        if ((int)value >=5)
        //            return Color.FromHex("#b2ff59");
        //        else
        //            return Color.FromHex("#ff5252");
        //    }
        //    return Color.Black;
        //}

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion
    }

    public class SelectedToFontAttributeConverter : IValueConverter
    {

        #region IValueConverter implementation

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is bool)
            {
                if ((Boolean)value)
                    return FontAttributes.Bold;
                else
                    return FontAttributes.None;
            }
            return FontAttributes.None;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}

