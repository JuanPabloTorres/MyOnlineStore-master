using MyOnlineStore.Application.Common.Interfaces.IViewModel;
using MyOnlineStore.Application.Presentation.ViewModels.AdminScenarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MyOnlineStore.Application.Presentation.Views.AdminScenarios.Stores.EmployeeStepsView
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ConfigureEmployeePage : ContentPage
    {

        public static string Route = App.Current.Resources[$"{nameof(ConfigureEmployeePage) + nameof(Route)}"].ToString();
        public ConfigureEmployeePage()
        {
            InitializeComponent();
            BindingContext = Startup.ServiceProvider.GetService<IEmployeeViewModel>();
        }
    }
}