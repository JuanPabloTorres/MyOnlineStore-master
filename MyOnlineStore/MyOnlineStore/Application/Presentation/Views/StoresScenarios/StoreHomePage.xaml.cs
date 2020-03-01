using MyOnlineStore.Application.Common.Interfaces.IViewModel;
using Xamarin.Forms;
using Microsoft.Extensions.DependencyInjection;

namespace MyOnlineStore.Application.Presentation.Views.StoresScenarios
{
    [Xamarin.Forms.Xaml.XamlCompilation(Xamarin.Forms.Xaml.XamlCompilationOptions.Compile)]
    public partial class StoreHomePage : ContentPage
    {
        public static string Route = App.Current.Resources[$"{nameof(StoreHomePage) + nameof(Route)}"].ToString();
        public StoreHomePage()
        {
            InitializeComponent();
            BindingContext = Startup.ServiceProvider.GetService<IStoreHomeViewModel>();
        }
    }
}