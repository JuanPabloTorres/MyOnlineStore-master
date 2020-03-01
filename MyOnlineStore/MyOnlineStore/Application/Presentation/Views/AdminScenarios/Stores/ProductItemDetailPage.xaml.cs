using MyOnlineStore.Application.Common.Interfaces.IViewModel;
using Xamarin.Forms;
using Microsoft.Extensions.DependencyInjection;

namespace MyOnlineStore.Application.Presentation.Views.AdminScenarios
{
    [Xamarin.Forms.Xaml.XamlCompilation(Xamarin.Forms.Xaml.XamlCompilationOptions.Compile)]
    public partial class ProductItemDetailPage : ContentPage
    {
        public static string Route = App.Current.Resources[$"{nameof(ProductItemDetailPage)+nameof(Route)}"].ToString();
        
        public ProductItemDetailPage()
        {
            InitializeComponent();
            BindingContext = Startup.ServiceProvider.GetService<IProductItemDetailViewModel>();
        }
    }
}