using Microsoft.Extensions.DependencyInjection;
using Xamarin.Forms;
using MyOnlineStore.Application.Common.Interfaces.IViewModel;


namespace MyOnlineStore.Application.Presentation.Views.StoresScenarios
{
    [Xamarin.Forms.Xaml.XamlCompilation(Xamarin.Forms.Xaml.XamlCompilationOptions.Compile)]
    public partial class StoreShopPage : ContentPage
    {
        public static string Route = App.Current.Resources[$"{nameof(StoreShopPage) + nameof(Route)}"].ToString();
        public StoreShopPage()
        {
            InitializeComponent();
            BindingContext = Startup.ServiceProvider.GetService<IStoreShopViewModel>();
        }
    }
}