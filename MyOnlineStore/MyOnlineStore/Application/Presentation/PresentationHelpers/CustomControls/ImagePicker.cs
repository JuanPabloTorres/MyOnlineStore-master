using Syncfusion.SfPicker.XForms;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace MyOnlineStore.Application.Presentation.CustomControls
{
    public class ImagePicker : Picker
    {
       
        public static BindableProperty DropDownImageProperty = BindableProperty.Create(nameof(DropDownImage), typeof(string), typeof(ImagePicker), string.Empty);
        public static BindableProperty PlaceHolderColorProperty = BindableProperty.Create(nameof(PlaceHolderColor), typeof(Color), typeof(ImagePicker), Color.White);
        public string DropDownImage
        {
            get
            {
                return (string)GetValue(DropDownImageProperty);
            }
            set
            {
                SetValue(DropDownImageProperty, value);
            }
        }

        public Color PlaceHolderColor
        {
            get
            {
                return (Color)GetValue(PlaceHolderColorProperty);
            }
            set
            {
                SetValue(PlaceHolderColorProperty, value);
            }
        }
    }
}
