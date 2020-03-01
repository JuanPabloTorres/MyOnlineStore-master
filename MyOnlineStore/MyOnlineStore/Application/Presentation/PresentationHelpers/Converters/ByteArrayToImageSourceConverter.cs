using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MyOnlineStore.Application.Presentation.PresentationHelpers.Converters
{
    public class ByteArrayToImageSourceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            ImageSource castedValue;
            MemoryStream stream;

            if (value is object)
            {
                stream = new MemoryStream((byte[])value);
                castedValue = ImageSource.FromStream(() => stream);
                value = castedValue;
            }

            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            //ImageSource imageSource;
            //byte[] imageBytes;

            //if(value is ImageSource source)
            //{
            //    var x = new MemoryStream();
                
            //}

            return (byte[])value;
        }
    }
}
