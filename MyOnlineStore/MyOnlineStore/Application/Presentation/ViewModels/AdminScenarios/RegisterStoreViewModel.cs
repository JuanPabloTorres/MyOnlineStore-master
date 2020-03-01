using Microsoft.Extensions.DependencyInjection;
using MyOnlineStore.Application.Common.Global.Constants;
using MyOnlineStore.Application.Common.Interfaces.DataStore;
using MyOnlineStore.Application.Common.Interfaces.Factories;
using MyOnlineStore.Application.Common.Interfaces.IViewModel;
using MyOnlineStore.Application.Common.Interfaces.Validations;
using MyOnlineStore.Application.Common.Interfaces.ViewInterfaces;
using MyOnlineStore.Application.Common.Utilities;
using MyOnlineStore.Application.Presentation.ViewModels;
using MyOnlineStore.Application.Presentation.ViewModels.Base;
using MyOnlineStore.Application.Presentation.Views.AdminScenarios;
using MyOnlineStore.Application.Presentation.Views.Templates.Popups;
using MyOnlineStore.Entities.Models.Stores;
using MyOnlineStore.Entities.Models.Users;
using MyOnlineStore.Shared.Interfaces.Factories;
using MyOnlineStore.Shared.Models.Entries;
using MyOnlineStore.Shared.Models.Stores;
using MyOnlineStore.Shared.Utils;
using Rg.Plugins.Popup.Services;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace MyOnlineStore.Application.Presentaion.ViewModels.AdminScenarios
{
    #region Notes

    /*
     * Notes of the current state of this project in relation with the project/task
     *
     * To do list 
     * TODO:
     */

    #endregion Notes

    //====================================================================================
    //
    // TODO: Create a validation rule for checking if store owner name exist
    //
    //====================================================================================
    public class RegisterStoreViewModel : BaseViewModel, IRegisterStoreViewModel
    {
        #region Fields

        protected readonly IUserDataStore _UserDataStore;
        protected readonly IStoreDataStore _StoreDataStore;
        protected readonly IStoreFactory _StoreFactory;
        protected readonly IUserFactory _UserFactory;
        protected readonly IValidatableObjectFactory _validatableObjectFactory;
        private byte[] _productImageSource;
        private List<WorkingHour> workingDays = new List<WorkingHour>(WorkingHour.AllWeekDaysInit());
        private bool _isCreditCard = false;

        #endregion Fields

        #region Constructors

        public RegisterStoreViewModel(IValidatableObjectFactory validatableObjectFactory, IUserDataStore userDataStore, IStoreDataStore storeDataStore, 
            IStoreFactory storeFactory, IUserFactory userFactory, 
            IRegisterCardViewModel cardViewModel, IStoreRegistrationEntry storeRegistrationEntry)
        {
            _validatableObjectFactory = validatableObjectFactory;
            _UserDataStore = userDataStore;
            _StoreDataStore = storeDataStore;
            _StoreFactory = storeFactory;
            _UserFactory = userFactory;
            CardViewModel = cardViewModel;
            StoreRegistrationEntry = storeRegistrationEntry;

            StoreName = _validatableObjectFactory.CreateSimpleValidatebleObject<string>("Store 1");
            CardNumber = _validatableObjectFactory.CreateSimpleValidatebleObject<string>();
            Longitude = _validatableObjectFactory.CreateSimpleValidatebleObject<string>("1");
            Latitude = _validatableObjectFactory.CreateSimpleValidatebleObject<string>("2");
            StoreOwnerName = _validatableObjectFactory.CreateSimpleValidatebleObject<string>("Owner 1");
            Description = _validatableObjectFactory.CreateSimpleValidatebleObject<string>("Description");
            Category = _validatableObjectFactory.CreateSimpleValidatebleObject<string>("Category");

            StoreNameValidationCommand = new Command(() => StoreName.Validate());
            LongitudeValidationCommand = new Command(() => Longitude.Validate());
            LatitudeValidationCommand = new Command(() => Latitude.Validate());
            StoreOwnerNameValidationCommand = new Command(() => StoreOwnerName.Validate());
            MoreInfoCommand = new Command<CommandEventData>((commandData) => MoreInfoPopup(commandData));
            RegisterStoreCommand =
                new Command<CommandEventData>(async (data) => await NavigateToInventoryProductItem(data));
            DescriptionValidationCommand = new Command(() => Description.Validate());
            GetLogoPhotoCommand = new Command(async () => ProductImageSource = await Utils.PickPhoto());
            TakeLogoPhotoCommand = new Command(() => { });
            GetLocationCommand = new Command(async () => await GetLocation());
            TypeOfCardChangedCommand = new Command<CommandEventData>((data) => TypeOfCardSelectionChangedHandler(data));

            Init();
        }

        #endregion Constructors

        #region Properties

        public IStoreRegistrationEntry StoreRegistrationEntry { get; set; }
        public Store? StoreCurrentlyRegistering { get; set; }

        public IValidateableObject<string> CardNumber { get; set; }

        public IRegisterCardViewModel CardViewModel { get; private set; }

        public IValidateableObject<string> Category { get; set; }

        public IValidateableObject<string> Description { get; set; }

        public IValidateableObject<string> Latitude { get; set; }

        public IValidateableObject<string> Longitude { get; set; }

        public byte[] ProductImageSource
        {
            get { return _productImageSource; }
            set { _productImageSource = value; RaisePropertyChanged(() => ProductImageSource); }
        }

        public IValidateableObject<string> StoreName { get; set; }

        public IValidateableObject<string> StoreOwnerName { get; set; }

        public bool VerifiedCard { get; set; }

        public List<WorkingHour> WorkingDays
        {
            get => workingDays;
            set { workingDays = value; RaisePropertyChanged(() => WorkingDays); }
        }

        public List<string> TypeOfCards { get; set; } = CardValues.TypeOfCards;

        public bool IsCreditCard
        {
            get => _isCreditCard;
            set { _isCreditCard = value; RaisePropertyChanged(() => IsCreditCard); }
        }

        public User User { get; set; }

        #region Commands

        public ICommand AddWorkHourCommand { get; set; }
        public ICommand DescriptionValidationCommand { get; set; }
        public ICommand GetLocationCommand { get; set; }
        public ICommand GetLogoPhotoCommand { get; set; }
        public ICommand LatitudeValidationCommand { get; set; }
        public ICommand LongitudeValidationCommand { get; set; }
        public ICommand MoreInfoCommand { get; set; }
        public ICommand RegisterStoreCommand { get; set; }
        public ICommand StoreNameValidationCommand { get; set; }
        public ICommand StoreOwnerNameValidationCommand { get; set; }
        public ICommand TypeOfCardChangedCommand { get; set; }
        public ICommand TakeLogoPhotoCommand { get; set; }

        #endregion Commands

        #endregion Properties

        #region Methods

        private void Init()
        {
            //Set Placeholder Image
            //Utils.GetByteArrayFromPath(out _productImageSource, LocalStorage.ImagePlaceHolderPath);

            //Landing Information Popup
            Task.Run(() => MoreInfoCommand.Execute(null));

            if(App.ApplicationManager.IsLogged())
            {
                User = App.ApplicationManager.CurrentUser!;
                CardViewModel.LoadFirstCard(User);
            }
        }

        #endregion Methods

        #region Services

        public async Task GetLocation()
        {
            var locationRequest = new GeolocationRequest(GeolocationAccuracy.Best);
            var location = await Geolocation.GetLocationAsync(locationRequest);
            Latitude.Set(location.Latitude.ToString());
            Longitude.Set(location.Longitude.ToString());
        }

        public void MoreInfoPopup(CommandEventData commandEventData)
        {
            PopupNavigation.Instance.PushAsync(new PopupView());
        }

        public bool SaveStore()
        {
            bool addedStore = false;
            bool updatedStore = false;
            ApiResponse<User> apiResponse;
            var shellViewModel = Startup.ServiceProvider.GetService<IShellViewModel>();

            if(Validate() && App.ApplicationManager.IsLogged(logoutProcedure: true))
            {
                StoreRegistrationEntry.Store = _StoreFactory.CreateStore(storeName: StoreName.Value,
                       ownerName: StoreOwnerName.Value,
                       description: Description.Value,
                       location: new Entities.Models.Stores.Location
                       {
                           Latitude = Latitude.Value,
                           Longitude = Longitude.Value
                       },
                       category: Category.Value,
                       logo: ProductImageSource,
                       adminId: App.ApplicationManager.CurrentUser!.Id.ToString(),
                       workingHours: WorkingDays);

                StoreRegistrationEntry.User = App.ApplicationManager.CurrentUser;

                StoreRegistrationEntry.Account = CardViewModel.CardFactory.CreateCard(
                                        holderName: CardViewModel.CardHolderName.Value,
                                        number: CardViewModel.CardNumber.Value,
                                        type: CardViewModel.TypeOfCard.Value,
                                        expDate: CardViewModel.ExpirationDate.Value,
                                        userId: StoreRegistrationEntry.User.Id,
                                        secCode: CardViewModel.SecurityCode.Value,
                                        storeId: StoreRegistrationEntry.Store.Id
                                    );

                apiResponse = _StoreDataStore.RegisterStoreAction(StoreRegistrationEntry);

                if(apiResponse.IsSuccess)
                {
                    App.ApplicationManager.CurrentUser = apiResponse.Success.Data;
                    shellViewModel.MyStores = new ObservableCollection<Store>(App.ApplicationManager.CurrentUser.MyStores);
                    StoreCurrentlyRegistering = apiResponse.Success.Data.MyStores.OrderByDescending(_ => _.DateCreated).FirstOrDefault();
                    App.ApplicationManager.CurrenstStoreGuid =  StoreCurrentlyRegistering.Id;
                    //TODO: User Animation or Pop to display Success messages.
                }
                else
                {
                    //TODO: User Animation or Pop to display Failure messages.
                }
            }

            return addedStore || updatedStore;
        }

        public bool Validate()
        {
            return StoreName.Validate() && CardNumber.Validate() && Longitude.Validate() && Latitude.Validate();
        }

        private void AddWorkingHour(WorkingHour workingHour)
        {
            if(StoreCurrentlyRegistering is object)
            {
                if(StoreCurrentlyRegistering.WorkingHours is object)
                {
                    for(int i = 0; i < StoreCurrentlyRegistering.WorkingHours.Count; i++)
                    {
                        if(StoreCurrentlyRegistering.WorkingHours[i].Day.DayName == workingHour.Day.DayName)
                        {
                            StoreCurrentlyRegistering.WorkingHours.RemoveAt(i);
                            StoreCurrentlyRegistering.WorkingHours.Insert(i, workingHour);
                        }
                        else
                        {
                            StoreCurrentlyRegistering.WorkingHours.Add(workingHour);
                        }
                    }
                }
                else
                {
                    StoreCurrentlyRegistering.WorkingHours = new List<WorkingHour>
                    {
                        workingHour
                    };
                }
            }
        }

        private async Task NavigateToInventoryProductItem(CommandEventData commandEventData)
        {
            if(StoreCurrentlyRegistering == null)
                SaveStore();

            if(StoreCurrentlyRegistering is object)
            {
                await Shell.Current.GoToAsync($"{InventoryListPage.Route}?owenerstoreid={StoreCurrentlyRegistering.Id.ToString()}", animate: true);
            }
        }

        private bool UpdateCurrentUserToAdmin()
        {
            bool updated = false;
            User user;

            if(App.ApplicationManager.IsLogged())
            {
                user = App.ApplicationManager.CurrentUser!;
                updated = _UserDataStore.UpdateItem(user);

                if(updated)
                {
                    App.ApplicationManager.CurrentUser = user;
                }
            }

            return updated;
        }

        private void TypeOfCardSelectionChangedHandler(CommandEventData data)
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

        #endregion Services
    }
}