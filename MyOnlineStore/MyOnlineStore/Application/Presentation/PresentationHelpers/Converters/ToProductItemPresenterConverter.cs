using MyOnlineStore.Application.Data.Models.Presenters;
using MyOnlineStore.Application.Data.Presenters;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace MyOnlineStore.Application.Presentation.PresentationHelpers.Converters
{
    public class ToProductItemPresenterConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var castedValue = value as ProductItemPresenter;

            if (castedValue is null)
            {
                return value;
            }
            else
            {
                return castedValue;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
