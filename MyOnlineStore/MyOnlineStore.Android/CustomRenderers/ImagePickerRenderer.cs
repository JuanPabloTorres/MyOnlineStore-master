using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Support.V4.Content;
using Android.Views;
using Com.Syncfusion.SfPicker;
using MyOnlineStore.Application.Presentation.CustomControls;
using MyOnlineStore.Droid.CustomRenderers;
using Syncfusion.SfPicker.XForms;
using Syncfusion.SfPicker.XForms.Droid;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;


[assembly: ExportRenderer(typeof(ImagePicker), typeof(ImagePickerRenderer))]
namespace MyOnlineStore.Droid.CustomRenderers
{
    public class ImagePickerRenderer : PickerRenderer
    {
        public ImagePickerRenderer(Context context) : base(context)
        {

        }
        ImagePicker element;

        //protected override void OnElementChanged(ElementChangedEventArgs<Syncfusion.SfPicker.XForms.SfPicker> e)
        //{
        //    base.OnElementChanged(e);

        //    element = (ImagePicker)this.Element;

        //    //if (Control != null && this.Element != null && !string.IsNullOrEmpty(element.DropDownImage))
        //    //{

        //    //    Control.Dra .SetCompoundDrawablesWithIntrinsicBounds(null, null, AddPickerStyles(element.DropDownImage, GravityFlags.Left), null);
        //    //    Control.SetHintTextColor(element.PlaceHolderColor.ToAndroid());
        //    //}

        //    //Control.CompoundDrawablePadding = 25;
        //}

        protected override void OnElementChanged(ElementChangedEventArgs<Picker> e)
        {
            base.OnElementChanged(e);

            element = (ImagePicker)this.Element;

            if (Control != null && this.Element != null && !string.IsNullOrEmpty(element.DropDownImage))
            {

                Control.SetCompoundDrawablesWithIntrinsicBounds(null, null, AddPickerStyles(element.DropDownImage, GravityFlags.Left), null);
                Control.SetHintTextColor(element.PlaceHolderColor.ToAndroid());
            }

            Control.CompoundDrawablePadding = 25;
        }

        public LayerDrawable AddPickerStyles(string imagePath, GravityFlags gravityFlags1)
        {
            Drawable[] layers = { GetDrawable(imagePath, gravityFlags1) };
            LayerDrawable layerDrawable = new LayerDrawable(layers);
            layerDrawable.SetLayerInset(0, 0, 0, 0, 0);

            return layerDrawable;
        }

        private BitmapDrawable GetDrawable(string imagePath, GravityFlags gravityFlags)
        {
            int resID = Resources.GetIdentifier(imagePath, "drawable", this.Context.PackageName);
            
            var drawable = ContextCompat.GetDrawable(this.Context, resID);
            var bitmap = ((BitmapDrawable)drawable).Bitmap;

            var result = new BitmapDrawable(Resources, Bitmap.CreateScaledBitmap(bitmap, 15, 15, true));
            result.Gravity = gravityFlags;

            return result;
        }
    }
}