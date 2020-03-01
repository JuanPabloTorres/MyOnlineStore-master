
using MyOnlineStore.Application.Domain.Interfaces.IViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MyOnlineStore.Views.AdminScenarios
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OffersAndContent : ContentPage
    {
        public OffersAndContent()
        {
            InitializeComponent();
            BindingContext = Startup.ServiceProvider.GetService<IOfferViewModel>();
           
        }

       
    }
}