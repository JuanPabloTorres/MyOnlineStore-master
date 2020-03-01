using MyOnlineStore.Application.Common.Global.Constants;
using MyOnlineStore.Application.Common.Interfaces.DataStore;
using MyOnlineStore.Application.Common.Interfaces.Factories;
using MyOnlineStore.Application.Common.Interfaces.IViewModel;
using MyOnlineStore.Application.Common.Interfaces.Validations;
using MyOnlineStore.Application.Common.Utilities;
using MyOnlineStore.Application.Presentation.ViewModels.Base;
using MyOnlineStore.Entities.Models.Users;
using MyOnlineStore.Shared.Interfaces.Factories;
using Syncfusion.XForms.ComboBox;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace MyOnlineStore.Application.Presentation.ViewModels.MyAccount
{
    public class MyAccountViewModel : BaseViewModel, IMyAccountSettingsViewModel
    {
        private User user;
        private CardAccount? userCard;
        private IValidateableObject<string> cardnumber;
        private IValidateableObject<DateTime> expdate;
        private IValidateableObject<string> type;
        private IValidateableObject<string> cardholdername;
        private IValidateableObject<string> securitycode;
        private bool _isCreditCard = false;
        private string displayPath = "Number";
        public IUserDataStore UserDataStore { get; protected set; }
        public IUserCardDataStore CardDataStore { get; protected set; }
        public ICardFactory CardFactory { get;protected set; }
        public IValidatableObjectFactory ValidatableFactory { get; protected set; }

        public User User
        {
            get => user;
            set
            {
                user = value;
                RaisePropertyChanged(() => User.MyCardAccounts);
            }
        }
        public CardAccount? SelectedUserCard
        {
            get => userCard;
            set { userCard = value; RaisePropertyChanged(() => SelectedUserCard); }
        }
        public string DisplayPath
        {
            get=> displayPath;
            set{ displayPath = value; RaisePropertyChanged(() => DisplayPath); }
        }
        public Guid CurrentCardId { get; set; }
        public IValidateableObject<string> CardNumber
        {
            get => cardnumber;
            set 
            { 
                cardnumber = value; 
                RaisePropertyChanged(() => CardNumber);
            }
        }
        public IValidateableObject<DateTime> ExpDate
        {
            get => expdate;
            set { expdate = value; RaisePropertyChanged(() => ExpDate); }
        }
        public IValidateableObject<string> Type
        {
            get => type;
            set { type = value; RaisePropertyChanged(() => Type); }
        }
        public List<string> TypeOfCards { get; set; } = CardValues.TypeOfCards;
        public bool IsCreditCard
        {
            get => _isCreditCard;
            set { _isCreditCard = value; RaisePropertyChanged(() => IsCreditCard); }
        }
        public IValidateableObject<string> CardHolderName
        {
            get => cardholdername;
            set { cardholdername = value; RaisePropertyChanged(() => CardHolderName); }
        }
        public IValidateableObject<string> SecCode
        {
            get => securitycode;
            set { securitycode = value; RaisePropertyChanged(() => SecCode); }
        }

        public ICommand AddOrUpdateCardCommand { get; set; }
        public ICommand DeleteCardCommand { get; set; }
        public ICommand TypeOfCardChangedCommand { get; set; }
        public ICommand CardSelectionChangedCommand { get; set; }
        public ICommand NewCardCommand { get; set; }

        public MyAccountViewModel(IUserDataStore userDataStore, IUserCardDataStore cardDataStore, ICardFactory cardFactory, IValidatableObjectFactory validatableObjectFactory)
        {
            UserDataStore = userDataStore;
            CardDataStore = cardDataStore;
            CardFactory = cardFactory;
            ValidatableFactory = validatableObjectFactory;

            TypeOfCardChangedCommand = new Command<CommandEventData>((data) => TypeOfCardSelectionChangedHandler(data));
            AddOrUpdateCardCommand = new Command(async () => await AddOrUpdateCardHandler());
            CardSelectionChangedCommand = new Command<CommandEventData>((data) => CardSelectionChangedHandler(data));
            NewCardCommand = new Command<object>((parameter) => NewCardHandler(parameter));

            CardHolderName = ValidatableFactory.CreateSimpleValidatebleObject<string>();
            CardNumber= ValidatableFactory.CreateSimpleValidatebleObject<string>();
            Type = ValidatableFactory.CreateSimpleValidatebleObject<string>();
            ExpDate = ValidatableFactory.CreateSimpleValidatebleObject<DateTime>();
            SecCode = ValidatableFactory.CreateSimpleValidatebleObject<string>();

            if (App.ApplicationManager.IsLogged(logoutProcedure: true) 
                && App.ApplicationManager.CurrentUser is User user)
            {
                User = user;
            }
            
        }

        private void CardSelectionChangedHandler(CommandEventData data)
        {
            if (data is object && data.Args is Syncfusion.XForms.ComboBox.SelectionChangedEventArgs changedArgs && changedArgs.Value is CardAccount selectedCard && data.Sender is SfComboBox comboBox)
            {
                CurrentCardId = selectedCard.Id;
                CardHolderName.Set(selectedCard.CardHolderName);
                ExpDate.Set(selectedCard.Exp);
                Type.Set(selectedCard.Type);
                CardNumber.Set(selectedCard.Number);
                SecCode.Set(selectedCard.SecurityCode);
                SelectedUserCard = selectedCard;

                //CardHolderName = ValidatableFactory.CreateSimpleValidatebleObject(selectedCard.CardHolderName);
                //ExpDate = ValidatableFactory.CreateSimpleValidatebleObject(selectedCard.Exp);
                //Type = ValidatableFactory.CreateSimpleValidatebleObject(selectedCard.Type);
                //CardNumber = ValidatableFactory.CreateSimpleValidatebleObject(selectedCard.Number);
                //SecCode = ValidatableFactory.CreateSimpleValidatebleObject(selectedCard.SecurityCode);
                //CurrentCardId = selectedCard.Id;
            }
        }
        private void NewCardHandler(object parameter)
        {
            if (parameter is SfComboBox comboBoxCards)
            {
                comboBoxCards.Clear();
            }

            CurrentCardId = Guid.Empty;
            CardHolderName.Set("");
            ExpDate.Set(DateTime.Today);
            Type.Set("");
            CardNumber.Set("");
            SecCode.Set("");
            SelectedUserCard = null;

        }
        private void TypeOfCardSelectionChangedHandler(CommandEventData data)
        {
            if (data.Args is Syncfusion.XForms.ComboBox.SelectionChangedEventArgs selectionChanged)
            {
                if (selectionChanged.Value is string value)
                {
                    if (value == TypeOfCards[1])// Credit
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
        public async Task AddOrUpdateCardHandler()
        {
            await Task.Run(() =>
            {
                CardAccount userCard;
                bool isUpdateableCard;

                if (Validate() && User.MyCardAccounts is object)
                {
                    if (CurrentCardId == Guid.Empty)
                    {
                        CurrentCardId = Guid.NewGuid();
                    }

                    userCard = CardFactory.CreateCard(
                           //cardId: CurrentCardId,
                           holderName: CardHolderName.Value,
                           number: CardNumber.Value,
                           type: Type.Value,
                           expDate: ExpDate.Value,
                           secCode: SecCode.Value,
                           userId: user.Id
                       );

                    isUpdateableCard = CardDataStore.CardExist(userCard);

                    if (isUpdateableCard)
                    {
                        int index = User.MyCardAccounts.FindIndex(c => c.Id == CurrentCardId);
                        User.MyCardAccounts.RemoveAt(index);
                        User.MyCardAccounts.Insert(index, userCard);
                        CardDataStore.UpdateItem(userCard);
                        SelectedUserCard = userCard;
                        DisplayPath = "Number";
                    }
                    else
                    {
                        User.MyCardAccounts.Add(userCard);
                        CardDataStore.AddItem(userCard);
                    }
                }
            });
        }
        bool Validate()
        {
            bool basicEntrysOK = CardNumber.Validate() && CardHolderName.Validate() && Type.Validate() && ExpDate.Validate();

            if (IsCreditCard)
            {
                basicEntrysOK = basicEntrysOK && SecCode.Validate();
            }

            return basicEntrysOK;
        }
    }
}
