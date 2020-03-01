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
using MyOnlineStore.Application.Presentation.PresentationHelpers.CustomControls;
using MyOnlineStore.Droid.CustomRenderers;
using Syncfusion.SfPicker.XForms;
using Syncfusion.SfPicker.XForms.Droid;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(SFDatePicker), typeof(SFDatePickerRenderer))]
namespace MyOnlineStore.Droid.CustomRenderers
{
    public class SFDatePickerRenderer : SfPickerRenderer
    {
        SFDatePicker _datePicker;

        public SFDatePickerRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<SfPicker> e)
        {
            base.OnElementChanged(e);
        }

    }
}