using Microsoft.Extensions.DependencyInjection;
using MyOnlineStore.Application.Common.Interfaces.DataStore;
using MyOnlineStore.Application.Common.Interfaces.Factories;
using MyOnlineStore.Application.Common.Interfaces.IViewModel;
using MyOnlineStore.Application.Common.Interfaces.Validations;
using MyOnlineStore.Application.Common.Utilities;
using MyOnlineStore.Application.Data.Models.Entries;
using MyOnlineStore.Application.Presentation.ViewModels.Base;
using MyOnlineStore.Entities.Models.Users;
using MyOnlineStore.Shared.Interfaces.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace MyOnlineStore.Application.Presentation.ViewModels.LoginScenarios
{
    public class RegisterCardViewModel : BaseViewModel, IRegisterCardViewModel
    {
        #region Fields

        private bool _isCreditCard = false;
        private IValidateableObject<string> cardNumber;
        private IValidateableObject<string> securityCode;
        private IValidateableObject<string> cardHolderName;
        private IValidateableObject<DateTime> expirationDate;

        #endregion Fields

        #region Constructors

        public RegisterCardViewModel(IUserDataStore userDataStoreFactory, IUserCardDataStore userCardDataStore, IValidatableObjectFactory validatableObjectFactory, RegisterEntry registerEntry, ICardFactory cardFactory)
        {
            _UserDataStore = userDataStoreFactory;
            _UserCardDataStore = userCardDataStore;
            _ValidatableObjectFactory = validatableObjectFactory;
            CardFactory = cardFactory;

            UserRegistrationInfo = registerEntry;

            RegisterCardCommand = new Command(() => RegisterCard());
            TypeOfCardChangedCommand = new Command<CommandEventData>((data) => TypeOfCardSelectionChangedHandler(data));
            SkipToHomeCommand = new Command(() => SkipToHome());

            CardNumber = _ValidatableObjectFactory.CreateSimpleValidatebleObject<string>("1111111111111111111111111");
            TypeOfCard = _ValidatableObjectFactory.CreateSimpleValidatebleObject<string>("Debit");
            SecurityCode = _ValidatableObjectFactory.CreateSimpleValidatebleObject<string>();
            ExpirationDate = _ValidatableObjectFactory.CreateSimpleValidatebleObject(DateTime.Today);
            CardHolderName = _ValidatableObjectFactory.CreateSimpleValidatebleObject<string>("Ricardo");

            Init();
        }

        #endregion Constructors

        #region Properties

        public IUserCardDataStore _UserCardDataStore { get; private set; }
        public IUserDataStore _UserDataStore { get; private set; }
        public IValidatableObjectFactory _ValidatableObjectFactory { get; private set; }
        public ICardFactory CardFactory { get; private set; }
        public RegisterEntry UserRegistrationInfo { get; set; }
        public List<string> TypeOfCards { get; set; } = new List<string> { "Debit", "Credit" };

        public bool IsCreditCard
        {
            get => _isCreditCard;
            set { _isCreditCard = value; RaisePropertyChanged(() => IsCreditCard); }
        }

        public IValidateableObject<string> CardNumber
        {
            get => cardNumber;
            set
            {
                if(cardNumber != value)
                {
                    cardNumber = value;
                    CardAccount.Number = cardNumber.Value;
                    RaisePropertyChanged(() => CardNumber);
                }
            }
        }

        public IValidateableObject<string> TypeOfCard { get; set; }

        public IValidateableObject<string> SecurityCode
        {
            get => securityCode;
            set
            {
                if(securityCode != value)
                {
                    securityCode = value;
                    CardAccount.SecurityCode = securityCode.Value;
                    RaisePropertyChanged(() => SecurityCode);
                }
            }
        }

        public IValidateableObject<DateTime> ExpirationDate
        {
            get => expirationDate;
            set
            {
                if(expirationDate != value)
                {
                    expirationDate = value;
                    CardAccount.Exp = ExpirationDate.Value;
                    RaisePropertyChanged(() => ExpirationDate);
                }
            }
        }

        public IValidateableObject<string> CardHolderName
        {
            get => cardHolderName;
            set
            {
                if(cardHolderName != value)
                {
                    cardHolderName = value;
                    CardAccount.CardHolderName = cardHolderName.Value;
                    RaisePropertyChanged(() => CardHolderName);
                }
            }
        }

        public ICommand RegisterCardCommand { get; set; }
        public ICommand TypeOfCardChangedCommand { get; set; }
        public ICommand SkipToHomeCommand { get; set; }

        public User? CurrentUser { get; set; }
        public CardAccount CardAccount { get; set; } = new CardAccount();

        #endregion Properties

        #region Methods

        public void TypeOfCardSelectionChangedHandler(CommandEventData data)
        {
            if(data.Args is Syncfusion.XForms.ComboBox.SelectionChangedEventArgs selectionChanged)
            {
                if(selectionChanged.Value is string value)
                {
                    if(value == TypeOfCards[1])// Credit
                    {
                        IsCreditCard = true;
                    }
                    else
                    {
                        IsCreditCard = false;
                    }
                }
            }
        }

        public async Task<bool> RegisterCard(Guid? storeId = null)
        {
            bool addedCard = false, updateUser = false;
            int cardCount;

            if(UserRegistrationInfo is object && Validate())
            {
                if(App.ApplicationManager.IsLogged())
                {
                }
                else
                {
                    CurrentUser =await _UserDataStore.GetUserByEmail(UserRegistrationInfo.Email);
                }

                if(CurrentUser is object)
                {
                    CardAccount = CardFactory.CreateCard(holderName: CardHolderName.Value,
                                        number: CardNumber.Value,
                                        type: TypeOfCard.Value,
                                        expDate: ExpirationDate.Value,
                                        userId: CurrentUser.Id,
                                        secCode: SecurityCode.Value,
                                        storeId: storeId);

                    addedCard = CurrentUser.TryAddCard(CardAccount);
                    updateUser = _UserCardDataStore.AddItem(CurrentUser.MyCardAccounts.First());
                }
                else
                {
                    //User doesnt exist, go to register.
                }
            }
            else
            {
                //TODO: Message of no registration info
            }

            return addedCard && updateUser;
        }

        public void SkipToHome()
        {
            App.Current.MainPage = Startup.ServiceProvider.GetService<AppShell>();
        }

        public bool Validate()
        {
            bool isValidCardNumber = CardNumber.Validate();
            bool isValidTypeOfCard = TypeOfCard.Validate();
            bool isValidPin = SecurityCode.Validate();
            bool isValidExpirationDate = ExpirationDate.Validate();

            return isValidCardNumber && isValidExpirationDate && isValidPin && isValidTypeOfCard;
        }

        public void LoadFirstCard(User? user)
        {
            if(user is object && user.MyCardAccounts is object && user.MyCardAccounts.Count > 0)
            {
                CardHolderName.Set(user.MyCardAccounts.First().CardHolderName);
                CardNumber.Set(user.MyCardAccounts.First().Number);
                ExpirationDate.Set(user.MyCardAccounts.First().Exp);
                TypeOfCard.Set(user.MyCardAccounts.First().Type);

                if(TypeOfCard.Value == TypeOfCards.First())
                {
                    SecurityCode.Set(user.MyCardAccounts.First().SecurityCode);
                }
            }
        }

        private void Init()
        {
            if(App.ApplicationManager.IsLogged())
            {
                CurrentUser = App.ApplicationManager.CurrentUser;
                LoadFirstCard(CurrentUser);
            }

            AddValidationRules();
        }

        private async Task Login()
        {
            bool registered = false;

            registered = await RegisterCard();

            if(registered && CurrentUser is object)
            {
                // Log to app
                App.ApplicationManager.CurrentUser = CurrentUser;
                await App.ApplicationManager.SetCredentialsInStorage(CurrentUser);
                App.Current.MainPage = Startup.ServiceProvider.GetService<AppShell>();
            }
            else
            {
                //TODO: Message: Unable to add card
            }
        }

        private void AddValidationRules()
        {
            //----------------------------------------------------------
            // TODO: Add Regex for every entry
            //CardNumber.AddRange(
            //    new List<Interfaces.IValidationRule<string>>
            //    { new IsNotNullOrEmptyRule<string> { ValidationMessage = "A Card Number is required." },
            //        new RegexValidationRule<string> { ValidationMessage = "Enter correct format for Card Number", RegexString = @"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$" }
            //    });
        }

        #endregion Methods
    }
}