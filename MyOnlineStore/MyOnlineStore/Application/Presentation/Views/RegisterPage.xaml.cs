using MyOnlineStore.Interfaces.IViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MyOnlineStore.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegisterPage : ContentPage
    {
        public RegisterPage()
        {
            InitializeComponent();
            BindingContext = Startup.ServiceProvider.GetService<IRegisterViewModel>();       
        }
    }
}