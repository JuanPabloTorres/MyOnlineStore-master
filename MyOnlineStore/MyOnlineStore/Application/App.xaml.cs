using Microsoft.Extensions.DependencyInjection;
using MyOnlineStore.Application;
using MyOnlineStore.Application.Common.Global.HubManger;
using MyOnlineStore.Application.Common.Interfaces.DataStore;
using MyOnlineStore.Application.Presentation.Managers;
using MyOnlineStore.Application.Presentation.Views;
using MyOnlineStore.Application.Presentation.Views.LoginScenarios;
using MyOnlineStore.Entities.Models.Stores;
using MyOnlineStore.Views;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MyOnlineStore
{

    public partial class App : Xamarin.Forms.Application
    {

        IUserDataStore userDataStore;

        #region Constructors

        public App(ApplicationManager applicationManager, HubManager hubManager,IUserDataStore userDataStore)
        {
            InitializeComponent();


            this.userDataStore = userDataStore;
            ApplicationManager = applicationManager;
            HubManager = hubManager;

            Current.MainPage = new Page();
            Task.Run(async () => await Init());
        }

        private async Task Init()
        {
            bool hasLoginCredentials;

         
            await Device.InvokeOnMainThreadAsync(() => {
                Current.MainPage = Startup.ServiceProvider.GetService<LoginPage>();
            });

            //hasLoginCredentials = await ApplicationManager.HasStorageCredentials();

            //if (hasLoginCredentials)
            //{
            //    await ApplicationManager.SetCurrentUserDataFromSecureStorage();

            //    if (ApplicationManager.IsLogged())
            //    {
            //        HubManager.ConnectUser(HubManager.GetHubConnection.ConnectionId, ApplicationManager.CurrentUser!.Id);
            //    }

            //    HubManager.ConnectUser(HubManager.GetHubConnection.ConnectionId, ApplicationManager.CurrentUser!.Id);


            //    Device.BeginInvokeOnMainThread(() => {
            //        Current.MainPage = Startup.ServiceProvider.GetService<AppShell>();
            //    });

            //}
            //else
            //{
            //    await Device.InvokeOnMainThreadAsync(() => {
            //        Current.MainPage = Startup.ServiceProvider.GetService<LoginPage>();
            //    });
            //}
        }

        #endregion Constructors

        #region Properties

        public static StoreFeaturedItem ContentTempStore { get; set; } = new StoreFeaturedItem();
        public static ApplicationManager ApplicationManager { get; set; }
        public static HubManager HubManager { get; set; }


        #endregion Properties

        #region Methods

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {

            var result = userDataStore.RemoveUserConnection(HubManager.userConnection);
            if(result)
            {

            HubManager.Disconnect();
            }
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }

        #endregion Methods
    }
}