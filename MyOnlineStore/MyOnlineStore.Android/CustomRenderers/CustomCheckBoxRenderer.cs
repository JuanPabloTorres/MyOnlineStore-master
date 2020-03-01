using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xamarin.Forms.Platform.Android;

namespace MyOnlineStore.Droid.CustomRenderers
{
    public class CustomCheckBoxRenderer : CheckBoxRenderer
    {
        public CustomCheckBoxRenderer(Context context) : base(context)
        {
        }
    }
}