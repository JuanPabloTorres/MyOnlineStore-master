using Syncfusion.XForms.Pickers.iOS;
using Syncfusion.XForms.iOS.Core;
using Syncfusion.SfRating.XForms.iOS;
using Syncfusion.XForms.iOS.TabView;
using Syncfusion.XForms.iOS.MaskedEdit;
using Syncfusion.SfNumericTextBox.XForms.iOS;
using Syncfusion.SfNumericUpDown.XForms.iOS;
using Syncfusion.XForms.iOS.BadgeView;
using Syncfusion.XForms.iOS.Border;
using Syncfusion.XForms.iOS.Shimmer;
using Syncfusion.SfSparkline.XForms.iOS;
using Syncfusion.XForms.iOS.Buttons;
using Syncfusion.SfAutoComplete.XForms.iOS;
using Syncfusion.ListView.XForms.iOS;
using Syncfusion.XForms.iOS.ComboBox;
using Syncfusion.SfPicker.XForms.iOS;
using Syncfusion.XForms.iOS.TextInputLayout;
using Foundation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using UIKit;

namespace MyOnlineStore.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            Xamarin.Forms.Forms.SetFlags("Shell_Experimental", "Visual_Experimental", "CollectionView_Experimental", "FastRenderers_Experimental");
            Xamarin.Forms.Forms.Init();

            ImageCircle.Forms.Plugin.iOS.ImageCircleRenderer.Init();
            Lottie.Forms.iOS.Renderers.AnimationViewRenderer.Init();
            Rg.Plugins.Popup.Popup.Init();
            FFImageLoading.Forms.Platform.CachedImageRenderer.Init();

            // Syncfusion
            Syncfusion.XForms.iOS.DataForm.SfDataFormRenderer.Init();
            Syncfusion.XForms.iOS.Border.SfBorderRenderer.Init();
            Syncfusion.XForms.iOS.Buttons.SfButtonRenderer.Init();
            SfListViewRenderer.Init();
            Syncfusion.XForms.iOS.EffectsView.SfEffectsViewRenderer.Init();

            UINavigationBar.Appearance.Translucent = false;

            LoadApplication(Startup.Init(ConfigureServices));

            return base.FinishedLaunching(app, options);
        }
        void ConfigureServices(HostBuilderContext ctx, IServiceCollection services)
        {
        }
    }
}
