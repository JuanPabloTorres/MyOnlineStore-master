using MyOnlineStore.Application.Common.Interfaces.IViewModel;
using Microsoft.Extensions.DependencyInjection;
using Xamarin.Forms;

namespace MyOnlineStore.Application.Presentation.Views.Home
{
    [Xamarin.Forms.Xaml.XamlCompilation(Xamarin.Forms.Xaml.XamlCompilationOptions.Compile)]
    public partial class ExploreStores : ContentPage
    {
        public ExploreStores()
        {
            InitializeComponent();
            BindingContext = Startup.ServiceProvider.GetService<IExploreStoresViewModel>();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            MessagingCenter.Send(this, "FetchStoresToExplore");
        }
    }
}