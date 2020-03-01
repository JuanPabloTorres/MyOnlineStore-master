using System;
using Android.Content;
using MyOnlineStore.Application.Presentation.CustomControls;
using MyOnlineStore.Droid.CustomRenderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;



[assembly: ExportRenderer(typeof(GradientContentPage), typeof(GradientColorStackRenderer))]
namespace MyOnlineStore.Droid.CustomRenderers
{
    public class GradientColorStackRenderer : VisualElementRenderer<StackLayout>
    {
        private Color StartColor { get; set; }
        private Color EndColor { get; set; }
        public GradientColorStackRenderer(Context context) : base(context)
        {
        }
        protected override void DispatchDraw(global::Android.Graphics.Canvas canvas)
        {
            
            var gradient = new Android.Graphics.LinearGradient(0, 0, 0, Height,
                this.StartColor.ToAndroid(),
                this.EndColor.ToAndroid(),
                Android.Graphics.Shader.TileMode.Mirror);

            var paint = new Android.Graphics.Paint()
            {
                Dither = true,
            };

            
            paint.SetShader(gradient);
            canvas.DrawPaint(paint);
            base.DispatchDraw(canvas);
        }

        protected override void OnElementChanged(ElementChangedEventArgs<StackLayout> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null || Element == null)
            {
                return;
            }

            try
            {
                var page = e.NewElement as GradientContentPage;
                this.StartColor = page.StartColor;
                this.EndColor = page.EndColor;
            }
            catch (Exception)
            {
                //Publish the error
            }
        }

    }
}


