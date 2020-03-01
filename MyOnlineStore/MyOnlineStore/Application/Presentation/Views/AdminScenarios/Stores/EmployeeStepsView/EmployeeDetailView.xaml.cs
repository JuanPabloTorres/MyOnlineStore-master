using MyOnlineStore.Application.Common.Interfaces.IViewModel;
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
    public partial class EmployeeDetailView : ContentPage
    {
        public static string Route = App.Current.Resources[$"{nameof(EmployeeDetailView) + nameof(Route)}"].ToString();
        public EmployeeDetailView()
        {
            InitializeComponent();
            BindingContext = Startup.ServiceProvider.GetService<IEmployeeViewModel>();
        }
    }
}