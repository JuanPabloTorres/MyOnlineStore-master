using System.Drawing;
using Foundation;
using MyOnlineStore.Application.Presentation.CustomControls;
using MyOnlineStore.iOS.CustomRenderers;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;


[assembly: ExportRenderer(typeof(ImagePicker), typeof(ImagePickerRenderer))]
namespace MyOnlineStore.iOS.CustomRenderers
{
    public class ImagePickerRenderer : PickerRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Picker> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null || e.NewElement == null)
                return;

            var element = (ImagePicker)this.Element;
            var textfield = this.Control;

            if (this.Control != null && this.Element != null && !string.IsNullOrEmpty(element.DropDownImage))
            {
                textfield.RightViewMode = UITextFieldViewMode.Always;
                textfield.RightView = GetImageView(element.DropDownImage, 15, 15);
                textfield.BorderStyle = UITextBorderStyle.None;
                
                // border.frame = CGRect(x: 0, y: self.frame.size.height - width, width:  self.frame.size.width, height: self.frame.size.height)
                var frame = this.Frame.Width;
                var bottomBorder = new CoreAnimation.CALayer()
                {
                    Frame = new CoreGraphics.CGRect(5.0, element.HeightRequest - 10, element.WidthRequest-5.0, 1.0),
                    BorderWidth = 2.0f,
                    BorderColor = Xamarin.Forms.Color.Black.ToCGColor(),

                };
                textfield.Layer.AddSublayer(bottomBorder) ;
                textfield.Layer.MasksToBounds = true;
                textfield.AttributedPlaceholder = new NSAttributedString(textfield.AttributedPlaceholder.Value, foregroundColor: element.PlaceHolderColor.ToUIColor());
            }
        }
        private UIView GetImageView(string imagePath, int height, int width)
        {
            var uiImageView = new UIImageView(UIImage.FromBundle(imagePath))
            {
                Frame = new RectangleF(0, 0, width, height)
            };
            UIView objLeftView = new UIView(new System.Drawing.Rectangle(0, 0, width, height));
            objLeftView.AddSubview(uiImageView);

            return objLeftView;
        }
    }
}