using Microsoft.Extensions.DependencyInjection;
using MyOnlineStore.Application.Common.Interfaces.DataStore;
using MyOnlineStore.Application.Common.Interfaces.Factories;
using MyOnlineStore.Application.Common.Interfaces.IViewModel;
using MyOnlineStore.Application.Common.Interfaces.Validations;
using MyOnlineStore.Application.Common.Validations.Rules;
using MyOnlineStore.Application.Presentation.ViewModels.Base;
using MyOnlineStore.Application.Presentation.Views.LoginScenarios;
using MyOnlineStore.Entities.Models.Users;
using MyOnlineStore.Models.Utils;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace MyOnlineStore.Application.Presentation.ViewModels.LoginScenarios
{

    public class LoginViewModel : BaseViewModel, ILoginViewModel
    {
        protected readonly IValidatableObjectFactory _ValidateableObjectFactory;
        protected readonly IUserDataStore _UserDataStore;

        public IValidateableObject<string> Email { get; set; }
        public IValidateableObject<string> Password { get; set; }

        public ICommand LoginCommand { get; set; }
        public ICommand ForgotPasswordCommand { get; set; }
        public ICommand NavigateToRegisterCommand { get; set; }

        public LoginViewModel(IValidatableObjectFactory validatableObjectFactory, IUserDataStore userDataStore)
        {
            _ValidateableObjectFactory = validatableObjectFactory;
            _UserDataStore = userDataStore;

            LoginCommand = new Command(async () => await Login());
            NavigateToRegisterCommand = new Command( () =>  NavigateToRegisterPage());
            ForgotPasswordCommand = new Command(() => { });

            Email = _ValidateableObjectFactory.CreateSimpleValidatebleObject<string>("torres@gmail.com");
            Password = _ValidateableObjectFactory.CreateSimpleValidatebleObject<string>("1234");

            Init();
        }

        private void Init()
        {
            Email.ValidationsRules.Add(new RegexValidationRule<string> { ValidationMessage = "Email is not valid", RegexString = @"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$" });
            //Password.ValidationsRules.Add(new RegexValidationRule<string> { ValidationMessage = "8-16 characters with a special character, a number  and Upper Case", RegexString = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$_!%*?&])[A-Za-z\d@_$!%*?&]{8,16}$" });
        }

        public async Task Login()
        {
            IsBusy = true;
            bool canLogin = false;

            //if (Connectivity.NetworkAccess != NetworkAccess.Internet)
            //{
            //    await Xamarin.Forms.Application.Current.MainPage.DisplayAlert("Error", "Connect to internet", "Close");
            //    return;
            //}

            /*
             TODO: Change to a object wit all information
             Ej: If error what errro, user exist etc
             */
            canLogin = await LoginProcedure();

            if (canLogin)
            {
                App.HubManager.ConnectUser(App.HubManager.GetHubConnection.ConnectionId, App.ApplicationManager.CurrentUser!.Id);
                await Device.InvokeOnMainThreadAsync(() => {
                    App.Current.MainPage = Startup.ServiceProvider.GetService<AppShell>();
                });
            }
            else
            {
                await Xamarin.Forms.Application.Current.MainPage.DisplayAlert("Info", "No user is found with this credentials.", "Exit");
            }

            IsBusy = false;
        }

        private async Task<bool> LoginProcedure()
        {
            User? user;
            ValidUserCreateableToken? userCreateableToken;
            bool navigateToApp = false;

            if (Email.Validate() && Password.Validate())
            {
                user = await _UserDataStore.GetUserByEmail(Email.Value);

                if (user is object)
                {
                    await App.ApplicationManager.SetCredentialsInStorage(user);
                    App.ApplicationManager.CurrentUser = user;
                    navigateToApp = true;
                }
                else { navigateToApp = false; }
            }

            return navigateToApp;
        }

        public  void NavigateToRegisterPage()
        {

            //await App.Current.MainPage.Navigation.PushAsync(new RegisterPage());

            //await Shell.Current.GoToAsync($"{RegisterPage.Route}");




             App.Current.MainPage = Startup.ServiceProvider.GetService<RegisterPage>();
        }
    }
}