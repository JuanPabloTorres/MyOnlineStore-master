using MyOnlineStore.Application.Common.Interfaces.IViewModel;
using MyOnlineStore.Application.Presentation.ViewModels.Home;
using Xamarin.Forms;
using Microsoft.Extensions.DependencyInjection;
using MyOnlineStore.Application.Common.Global.HubManger;
using MyOnlineStore.Application.Common.Interfaces.DataStore;

namespace MyOnlineStore.Application.Presentation.Views.Home
{
    [Xamarin.Forms.Xaml.XamlCompilation(Xamarin.Forms.Xaml.XamlCompilationOptions.Compile)]
    public partial class HomePage : ContentPage
    {
        public static string Route = App.Current.Resources[$"{nameof(HomePage) + nameof(Route)}"].ToString();

        public HomePage()
        {
            InitializeComponent();
            BindingContext = Startup.ServiceProvider.GetService<IHomeViewModel>();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();

           



            MessagingCenter.Send(this, "UpdateFavorites");
        }
        
    }
}