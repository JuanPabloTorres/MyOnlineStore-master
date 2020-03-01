using MyOnlineStore.Application.Common.Interfaces.IViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MyOnlineStore.Application.Presentation.Views.AdminScenarios.Stores
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MyStoresPage : ContentPage
    {
        public MyStoresPage()
        {
            InitializeComponent();
            BindingContext = Startup.ServiceProvider.GetService<IMyStoresViewModel>();
        }
    }
}