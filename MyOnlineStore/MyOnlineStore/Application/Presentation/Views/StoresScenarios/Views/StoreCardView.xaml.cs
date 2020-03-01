
using Xamarin.Forms;

namespace MyOnlineStore.Application.Presentation.Views.StoresScenarios.Views
{
    [Xamarin.Forms.Xaml.XamlCompilation(Xamarin.Forms.Xaml.XamlCompilationOptions.Compile)]
    public partial class StoreCardView : ContentView
    {
        public StoreCardView()
        {
            InitializeComponent();
        }

       

        private void CachedImage_Finish(object sender, FFImageLoading.Forms.CachedImageEvents.FinishEventArgs e)
        {
            var image = (FFImageLoading.Forms.CachedImage)sender;
           
        }
    }
}