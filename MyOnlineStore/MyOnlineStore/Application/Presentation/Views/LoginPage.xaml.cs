using MyOnlineStore.CustomControls;
using MyOnlineStore.Interfaces.IViewModel;
using MyOnlineStore.ViewModel;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MyOnlineStore.View
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LoginPage : ContentPage
	{
		public LoginPage ()
		{
			InitializeComponent ();

            BindingContext = Startup.ServiceProvider.GetService<ILoginViewModel>();
		}

        private async void Go(object sender, EventArgs e)
        {
            //await Navigation.PushAsync(new CustomTest());
        }
    }
}