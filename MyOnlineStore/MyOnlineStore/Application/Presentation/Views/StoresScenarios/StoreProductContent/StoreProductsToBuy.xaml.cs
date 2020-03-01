using MyOnlineStore.Application.Common.Interfaces.IViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MyOnlineStore.Application.Presentation.Views.StoresScenarios.StoreProductContent
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StoreProductsToBuy : ContentPage
    {
        public static string Route = App.Current.Resources[$"{nameof(StoreProductsToBuy) + nameof(Route)}"].ToString();
        public StoreProductsToBuy()
        {
            InitializeComponent();
            BindingContext = Startup.ServiceProvider.GetService<IStoreHomeViewModel>();
        }
    }
}