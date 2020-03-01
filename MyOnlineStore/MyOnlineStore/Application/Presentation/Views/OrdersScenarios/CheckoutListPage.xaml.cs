using Microsoft.Extensions.DependencyInjection;
using MyOnlineStore.Application.Common.Interfaces.IViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;


namespace MyOnlineStore.Application.Presentation.Views.OrdersScenarios
{
    [Xamarin.Forms.Xaml.XamlCompilation(Xamarin.Forms.Xaml.XamlCompilationOptions.Compile)]
    public partial class CheckoutListPage : ContentPage
    {
        public static string Route = App.Current.Resources[$"{nameof(CheckoutListPage) + nameof(Route)}"].ToString();
        public CheckoutListPage()
        {
            InitializeComponent();
            BindingContext = Startup.ServiceProvider.GetService<ICheckoutViewModel>();
        }
    }
}