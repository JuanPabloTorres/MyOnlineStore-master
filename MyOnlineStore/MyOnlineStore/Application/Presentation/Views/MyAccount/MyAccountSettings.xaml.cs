using MyOnlineStore.Application.Common.Interfaces.IViewModel;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MyOnlineStore.Application.Presentation.Views.MyAccount
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MyAccountSettings : ContentPage
    {
        public MyAccountSettings()
        {
            InitializeComponent();
            BindingContext = Startup.ServiceProvider.GetService<IMyAccountSettingsViewModel>();
        }
    }
}