using MyOnlineStore.Application.Common.Interfaces.IViewModel;
using Xamarin.Forms;
using Microsoft.Extensions.DependencyInjection;

namespace MyOnlineStore.Application.Presentation.Views.LoginScenarios
{
	[Xamarin.Forms.Xaml.XamlCompilation(Xamarin.Forms.Xaml.XamlCompilationOptions.Compile)]
	public partial class RegisterAccountPage : ContentPage
	{
		public RegisterAccountPage ()
		{
			InitializeComponent ();

            BindingContext = Startup.ServiceProvider.GetService<IRegisterCardViewModel>();
		}
	}
}