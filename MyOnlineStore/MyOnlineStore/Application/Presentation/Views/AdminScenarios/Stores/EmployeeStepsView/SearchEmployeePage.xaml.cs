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
    public partial class SearchEmployeePage : ContentPage
    {
        public SearchEmployeePage()
        {
            InitializeComponent();

            BindingContext = Startup.ServiceProvider.GetService<IEmployeeViewModel>();
        }

        private void SwipeBackToNormal(object sender, Syncfusion.ListView.XForms.SwipeEndedEventArgs e)
        {
            if (e.SwipeOffset == 100)
                e.SwipeOffset = 0;

        }

      
    }
}