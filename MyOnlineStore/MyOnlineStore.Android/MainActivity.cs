using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.OS;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using ImageCircle.Forms.Plugin.Droid;
using Xamarin.Forms;
using Plugin.CurrentActivity;
using MyOnlineStore.Application;

namespace MyOnlineStore.Droid
{
    [Activity(Label = "MyOnlineStore", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);

            Forms.SetFlags("Shell_Experimental", "Visual_Experimental", "CollectionView_Experimental", "FastRenderers_Experimental");

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            Forms.Init(this, savedInstanceState);

            ImageCircleRenderer.Init();
            Lottie.Forms.Droid.AnimationViewRenderer.Init();
            Rg.Plugins.Popup.Popup.Init(this, savedInstanceState);
            CrossCurrentActivity.Current.Init(this, savedInstanceState);
            FFImageLoading.Forms.Platform.CachedImageRenderer.Init(enableFastRenderer: true);
            FFImageLoading.Forms.Platform.CachedImageRenderer.InitImageViewHandler();

            // Syncfusion

            // Override default BitmapDescriptorFactory by your implementation. 
            //var platformConfig = new Xamarin.Forms.GoogleMaps.Android.PlatformConfig
            //{
            //    BitmapDescriptorFactory = new CachingNativeBitmapDescriptorFactory()
            //};
            //Xamarin.FormsGoogleMaps.Init(this, savedInstanceState);

            LoadApplication(Startup.Init(ConfigureServices));
        }

        public override void OnBackPressed()
        {
            if (Rg.Plugins.Popup.Popup.SendBackPressed(base.OnBackPressed))
            {
                // Do something if there are some pages in the `PopupStack`
            }
            else
            {
                // Do something if there are not any pages in the `PopupStack`
            }

            base.OnBackPressed();
        }
       
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            Plugin.Permissions.PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
        void ConfigureServices(HostBuilderContext ctx, IServiceCollection services)
        {
            //services.AddSingleton<ISoftwareKeyboardService, SoftwareKeyboardService>();
            // services.AddSingleton<Activity,MainActivity>();
        }
    }
}