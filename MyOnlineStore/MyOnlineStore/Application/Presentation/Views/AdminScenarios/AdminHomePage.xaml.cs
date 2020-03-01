using MyOnlineStore.Application.Common.Interfaces.DataStore;
using MyOnlineStore.Application.Data.DataStore;
using MyOnlineStore.Application.Presentation.ViewModels;
using MyOnlineStore.Application.Presentation.Views.AdminScenarios.Dashboard.Sections;
using MyOnlineStore.Application.Presentation.Views.AdminScenarios.Stores;
using MyOnlineStore.Application.Presentation.Views.AdminScenarios.Stores.ContentStepView;
using MyOnlineStore.Application.Presentation.Views.AdminScenarios.Stores.Dashboards;
using MyOnlineStore.Application.Presentation.Views.AdminScenarios.Stores.EmployeeStepsView;
using MyOnlineStore.Application.Presentation.Views.AdminScenarios.Stores.OfferStepView;
using MyOnlineStore.Views.AdminScenarios;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MyOnlineStore.Application.Presentation.Views.AdminScenarios
{
    [Xamarin.Forms.Xaml.XamlCompilation(Xamarin.Forms.Xaml.XamlCompilationOptions.Compile)]
    public partial class AdminHomePage : ContentPage
    {
        public static string Route = App.Current.Resources[$"{nameof(AdminHomePage) + nameof(Route)}"].ToString();
        public AdminHomePage()
        {
            InitializeComponent();
        }
        private async void GoInventory(object sender, EventArgs e)
        {
           
              
                await Shell.Current.GoToAsync($"{InventoryListPage.Route}?" +
                $"owenerstoreid={App.ApplicationManager.CurrentStore!.Id.ToString()}");
            
        }

        private void goReports(object sender, EventArgs e)
        {
            // Shell.Current.Navigation.PushAsync(new GoalsDashboard());
            // Shell.Current.Navigation.PushAsync(new InventoryDashboards());
            Shell.Current.Navigation.PushAsync(new DashboardMenu());

        }

        private async void goSetting(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new StoreSetting());
        }

        private async void goOfferandcotent(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync($"{OffersHomePage.Route}?" + $"storeID={App.ApplicationManager.CurrentStore!.Id.ToString()}");
        }

        private async void gocontent(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync($"{HomeContentPage.Route}?" + $"storeID={App.ApplicationManager.CurrentStore!.Id.ToString()}");
           
        }

        private void goorders(object sender, EventArgs e)
        {

        }

        private async void goemployeemanager(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync($"{EmployeeHomePage.Route}?" + $"storeID={App.ApplicationManager.CurrentStore!.Id.ToString()}");
        }


        //private void SfButton_Clicked_Inventory(object sender, EventArgs e)
        //{
        //    if (ShellViewModel.Current_Store != null && ShellViewModel.Current_Store.Id != Guid.Empty)
        //    {
        //        Task.Delay(550);
        //        Device.InvokeOnMainThreadAsync(async () => 
        //        await Shell.Current.GoToAsync($"{InventoryListPage.Route}?" +
        //        $"owenerstoreid={ShellViewModel.Current_Store.Id.ToString()}",
        //        animate: false));
        //    }
        //}

        //private void SfButton_Clicked_Settings
        //    (object sender, EventArgs e)
        //{
        //    Task.Delay(550);
        //    Device.InvokeOnMainThreadAsync(async () =>
        //    await Shell.Current.GoToAsync($"{StoreSett}" ,animate: false));
        //}
    }
}