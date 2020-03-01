using Microsoft.Extensions.DependencyInjection;
using MyOnlineStore.Application.Common.Interfaces.DataStore;
using MyOnlineStore.Application.Common.Interfaces.Factories;
using MyOnlineStore.Application.Common.Interfaces.IViewModel;
using MyOnlineStore.Application.Common.Interfaces.Validations;
using MyOnlineStore.Application.Common.Validations.Rules;
using MyOnlineStore.Application.Data.Models.Entries;
using MyOnlineStore.Application.Presentation.ViewModels.Base;
using MyOnlineStore.Application.Presentation.Views.LoginScenarios;
using MyOnlineStore.Entities.Models.Users;
using MyOnlineStore.Shared.Interfaces.Factories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace MyOnlineStore.Application.Presentation.ViewModels.LoginScenarios
{
    public class RegisterViewModel : BaseViewModel, IRegisterViewModel
    {
        #region Fields

        private readonly IUserDataStore _UserDataStore;
        private readonly IUserFactory _UserFactory;
        private readonly IValidatableObjectFactory _ValidatableObjectFactory;

        #endregion Fields

        #region Constructors

        public RegisterViewModel(IUserDataStore userDataStoreFactory, IUserFactory userFactory, IValidatableObjectFactory validatableObjectFactory, RegisterEntry register)
        {
            RegisterModel = register;

            _UserDataStore = userDataStoreFactory;
            _UserFactory = userFactory;
            _ValidatableObjectFactory = validatableObjectFactory;

            RegisterCommand = new Command(async() => await RegisterProcedure());
            ToLoginCommand = new Command(async() => await NavigateToLogin());

            Email = _ValidatableObjectFactory.CreateSimpleValidatebleObject("torres@gmail.com");
            FullName = _ValidatableObjectFactory.CreateSimpleValidatebleObject("Ricardo Torres");
            Password = _ValidatableObjectFactory.CreateSimpleValidatebleObject("1234");
            BirthDate = _ValidatableObjectFactory.CreateSimpleValidatebleObject<DateTime>();
            ConfirmPassword = _ValidatableObjectFactory.CreateSimpleValidatebleObject("1234");
            PhoneNumber = _ValidatableObjectFactory.CreateSimpleValidatebleObject("777-777-7777");

            AddValidations();
        }

        #endregion Constructors

        #region Properties

        public RegisterEntry RegisterModel { get; set; }
        public bool IsValid { get; set; }
        public IValidateableObject<string> Email { get; set; }
        public IValidateableObject<string> Password { get; set; }
        public IValidateableObject<string> ConfirmPassword { get; set; }
        public IValidateableObject<string> PhoneNumber { get; set; }
        public IValidateableObject<DateTime> BirthDate { get; set; }
        public IValidateableObject<string> FullName { get; set; }

        public ICommand RegisterCommand { get; set; }
        public ICommand ToLoginCommand { get; set; }

        #endregion Properties

        #region Methods

        private async Task NavigateToLogin()
        {
            await App.Current.MainPage.Navigation
                .PopToRootAsync(animated: true);
            //App.Current.MainPage = Startup.ServiceProvider.GetService<LoginPage>();
        }

        private async Task NavigateToCardRegistration()
        {
            await App.Current.MainPage.Navigation
                .PushAsync(Startup.ServiceProvider.GetService<RegisterAccountPage>(), animated: true);
        }

        private async Task<bool> CreateNewUserInformation()
        {
            User? user;
            bool isValid;
            IsBusy = true;
            bool isCreated = false; ;

            //AddConfirmPasswordValidation(Password);
            isValid = ValidateEntries();

            if(isValid)
            {
                // Copy to Entry Model
                RegisterModel = CopyValuesToRegisterModel(RegisterModel);

                // Check using email, if not exist create

                user = await _UserDataStore.GetUserByEmail(RegisterModel.Email);
                if(user == null)
                {
                    user = _UserFactory.CreateUser(
                            FullName: RegisterModel.FullName,
                            BirthDate: RegisterModel.BirthDate,
                            Password: RegisterModel.Password,
                            PhoneNumber: RegisterModel.PhoneNumber,
                            Email: RegisterModel.Email
                        );

                    isCreated = _UserDataStore.AddItem(user);
                    if(isCreated)
                    {
                        await App.ApplicationManager.SetCredentialsInStorage(user);
                        await App.ApplicationManager.SetCurrentUserDataFromSecureStorage();
                    }
                    else
                    {
                        // Error, cant add
                        //  await Application.Current.MainPage.Navigation.PushAsync(new LoginPage());
                        // Pop Message of error.
                    }
                }
                else
                {
                    // Exist, should go to login page
                    //  await Application.Current.MainPage.Navigation.PushAsync(new LoginPage());
                    // Pop Message Already have an account.
                }
            }

            IsBusy = false;
            return isCreated;
        }
        private async Task RegisterProcedure()
        {
            bool registered = false;

            registered = await Task.Run(async () => await CreateNewUserInformation());

            Device.BeginInvokeOnMainThread(async() =>
            {
                if(registered)
                {
                    await NavigateToCardRegistration();
                }
            });
        }

        private bool ValidateEntries()
        {
            bool isValidUser = Email.Validate();
            bool isValidFullName = FullName.Validate();
            bool isValidBirthDate = BirthDate.Validate();
            bool isValidPhoneNumber = PhoneNumber.Validate();
            bool isValidPassword = Password.Validate();
            bool isValidConfirmPassword = ConfirmPassword.Validate();

            return isValidUser && isValidFullName && isValidBirthDate &&
                isValidPhoneNumber && isValidPassword && isValidConfirmPassword;
        }

        private void AddValidations()
        {
            //Email.ValidationsRules.AddRange(
            //    new List<IValidationRule<string>>
            //    { new IsNotNullOrEmptyRule<string> { ValidationMessage = "A Email is required." },
            //        new RegexValidationRule<string> { ValidationMessage = "Enter a proper Email text", RegexString = @"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$" }
            //    });

            //FullName.ValidationsRules.AddRange(
            //     new List<IValidationRule<string>>
            //     { new IsNotNullOrEmptyRule<string> { ValidationMessage = "A Name is required." },
            //        new RegexValidationRule<string> { ValidationMessage = "Enter a proper Name text.", RegexString = @"^[A-Z]{1}[a-z]+ [A-Z]{1}[a-z]+$" },
            //     });

            //BirthDate.ValidationsRules.Add(new BirthDateValidationRule { ValidationMessage = "Your are not that old." });

            //PhoneNumber.ValidationsRules.AddRange(
            //    new List<IValidationRule<string>>
            //     { new IsNotNullOrEmptyRule<string> { ValidationMessage = "A Phone Number is required." },
            //        new RegexValidationRule<string> { ValidationMessage = "Enter a proper Phone Number text.", RegexString = @"^[+]*[(]{0,1}[0-9]{1,4}[)]{0,1}[-\s\./0-9]*$" },
            //     });

            //Password.ValidationsRules.AddRange(new List<IValidationRule<string>>
            //     { new IsNotNullOrEmptyRule<string> { ValidationMessage = "A Password is required." },
            //        new RegexValidationRule<string> { ValidationMessage = "Password needs 1 Upper Case, Special Character, Number and length of 8-16", RegexString = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$_!%*?&])[A-Za-z\d@_$!%*?&]{8,16}$" },
            //     });
        }

        private RegisterEntry CopyValuesToRegisterModel(RegisterEntry registerEntry)
        {
            registerEntry.Email = Email.Value;
            registerEntry.BirthDate = BirthDate.Value;
            registerEntry.FullName = FullName.Value;
            registerEntry.Password = Password.Value;
            return registerEntry;
        }

        private void AddConfirmPasswordValidation(IValidateableObject<string> passwordEntered)
        {
            ConfirmPassword.ValidationsClear();
            ConfirmPassword.ValidationsRules.AddRange(new List<IValidationRule<string>>
                 { new IsNotNullOrEmptyRule<string> { ValidationMessage = "A Password is required." },
                    new CompareValidationRule<string>{ ValidationMessage="The Passwords are not the same.", CompareToString = passwordEntered.Value}
                 });
        }

        private async Task SetStorageCredentials(string email, string pass)
        {
            try
            {
                //=======================================================================
                //
                // TODO: Encrypt data values (email,pass)
                //
                //=======================================================================
                await SecureStorage.SetAsync("auth_token_email", email);
                await SecureStorage.SetAsync("auth_token_pass", pass);
            }
            catch(Exception)
            {
                // Possible that device doesn't support secure storage on device.
            }
        }

        #endregion Methods
    }
}