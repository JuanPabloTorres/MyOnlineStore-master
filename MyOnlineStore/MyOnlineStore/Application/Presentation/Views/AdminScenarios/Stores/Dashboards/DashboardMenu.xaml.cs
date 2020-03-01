using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MyOnlineStore.Application.Presentation.Views.AdminScenarios.Stores.Dashboards
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DashboardMenu : ContentPage
    {
        public DashboardMenu()
        {
            InitializeComponent();
        }

        private void GoInventory(object sender, EventArgs e)
        {
            Shell.Current.Navigation.PushAsync(new InventoryDashboards());
        }

        private void GoGoals(object sender, EventArgs e)
        {
             Shell.Current.Navigation.PushAsync(new GoalsDashboard());
        }
    }
}