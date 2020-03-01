using MyOnlineStore.Entities.Models.Stores;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace MyOnlineStore.Application.Presentation.PresentationHelpers.Converters
{
    public class StoresToStoreNameConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Store store = (Store)value;
            return store.StoreName;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
