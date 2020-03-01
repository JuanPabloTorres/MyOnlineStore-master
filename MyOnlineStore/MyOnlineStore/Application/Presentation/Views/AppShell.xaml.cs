using MyOnlineStore.Application.Common.Interfaces.ViewInterfaces;
using Microsoft.Extensions.DependencyInjection;
using Xamarin.Forms;
using MyOnlineStore.Application.Common.Global.HubManger;
using MyOnlineStore.Application.Common.Interfaces.DataStore;
using MyOnlineStore.Application.Presentation.Views.LoginScenarios;

namespace MyOnlineStore.Application
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            BindingContext = Startup.ServiceProvider.GetService<IShellViewModel>();

           
        }

      
    }
}
