using MyOnlineStore.Entities.Models.Purchases;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using Microsoft.Extensions.DependencyInjection;
using Xamarin.Essentials;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Input;
using MyOnlineStore.Application.Common.Interfaces.IViewModel;

namespace MyOnlineStore.Application.Presentation.Views.AdminScenarios
{
    [Xamarin.Forms.Xaml.XamlCompilation(Xamarin.Forms.Xaml.XamlCompilationOptions.Compile)]
    public partial class InventoryListPage : ContentPage
    {
        public static string Route = App.Current.Resources[$"{nameof(InventoryListPage)+nameof(Route)}"].ToString();
        public InventoryListPage()
        {
            InitializeComponent();           
            BindingContext = Startup.ServiceProvider.GetService<IInventoryViewModel>();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            MessagingCenter.Send(this, "update");
        }
    }
}