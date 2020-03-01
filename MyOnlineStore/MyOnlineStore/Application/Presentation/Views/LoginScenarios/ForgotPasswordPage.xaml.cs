using MyOnlineStore.Application.Presentation.ViewModels.LoginScenarios;
using Xamarin.Forms;
using Microsoft.Extensions.DependencyInjection;

namespace MyOnlineStore.Application.Presentation.Views.LoginScenarios
{
    [Xamarin.Forms.Xaml.XamlCompilation(Xamarin.Forms.Xaml.XamlCompilationOptions.Compile)]
	public partial class ForgotPasswordPage : ContentPage
	{
		public ForgotPasswordPage ()
		{
			InitializeComponent ();
            BindingContext = new ForgotViewModel(Navigation);
		}
	}
}