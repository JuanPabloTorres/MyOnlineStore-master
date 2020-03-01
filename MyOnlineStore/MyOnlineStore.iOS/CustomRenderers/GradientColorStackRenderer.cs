using CoreAnimation;
using CoreGraphics;
using MyOnlineStore.Application.Presentation.CustomControls;
using MyOnlineStore.iOS.CustomRenderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;


[assembly: ExportRenderer(typeof(GradientContentPage), typeof(GradientColorStackRenderer))]
namespace MyOnlineStore.iOS.CustomRenderers
{
    public class GradientColorStackRenderer : VisualElementRenderer<StackLayout>
    {
        public override void Draw(CGRect rect)
        {
            base.Draw(rect);
            GradientContentPage stack = (GradientContentPage)this.Element;
            CGColor startColor = stack.StartColor.ToCGColor();
            CGColor endColor = stack.EndColor.ToCGColor();
            #region for Vertical Gradient  
            var gradientLayer = new CAGradientLayer()
            {
                StartPoint = new CGPoint(1, 0),
                EndPoint = new CGPoint(1, 1)
            };
            #endregion
            #region for Horizontal Gradient  
            //var gradientLayer = new CAGradientLayer()
            //{
            //    StartPoint = new CGPoint(1, 0.5),
            //    EndPoint = new CGPoint(0, 0.5)
            //};
            #endregion
            gradientLayer.Frame = rect;
            gradientLayer.Colors = new CGColor[] {
                startColor,
                endColor
            };
            NativeView.Layer.InsertSublayer(gradientLayer, 0);
        }
    }
}