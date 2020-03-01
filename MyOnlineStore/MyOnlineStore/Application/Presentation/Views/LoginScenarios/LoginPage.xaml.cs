using MyOnlineStore.Application.Common.Interfaces.IViewModel;
using Xamarin.Forms;
using Microsoft.Extensions.DependencyInjection;

namespace MyOnlineStore.Application.Presentation.Views.LoginScenarios
{
    [Xamarin.Forms.Xaml.XamlCompilation(Xamarin.Forms.Xaml.XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
            BindingContext = Startup.ServiceProvider.GetService<ILoginViewModel>();
        }
    }
}