using MyOnlineStore.Application.Common.Interfaces.IViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Microsoft.Extensions.DependencyInjection;

namespace MyOnlineStore.Application.Presentation.Views.AdminScenarios
{
    [Xamarin.Forms.Xaml.XamlCompilation(Xamarin.Forms.Xaml.XamlCompilationOptions.Compile)]
    public partial class RegisterStore : ContentPage
    {
        public RegisterStore()
        {
            InitializeComponent();
            BindingContext = Startup.ServiceProvider.GetService<IRegisterStoreViewModel>();
        }
    }
}