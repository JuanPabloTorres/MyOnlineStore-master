using MyOnlineStore.Application.Common.Interfaces.IViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Microsoft.Extensions.DependencyInjection;

namespace MyOnlineStore.Application.Presentation.Views.LoginScenarios
{
    [Xamarin.Forms.Xaml.XamlCompilation(Xamarin.Forms.Xaml.XamlCompilationOptions.Compile)]
    public partial class RegisterPage : ContentPage
    {

        public static string Route = App.Current.Resources[$"{nameof(RegisterPage) + nameof(Route)}"].ToString();
        public RegisterPage()
        {
            InitializeComponent();
            BindingContext = Startup.ServiceProvider.GetService<IRegisterViewModel>();
        }

        protected override bool OnBackButtonPressed()
        {
            //return base.OnBackButtonPressed();
            return true ;
        }
    }
}