using FFImageLoading.Forms;
using MyOnlineStore.Application.Common.Interfaces.DataStore;
using MyOnlineStore.Application.Common.Interfaces.ViewInterfaces;
using MyOnlineStore.Application.Presentation.PresentationHelpers.Converters;
using MyOnlineStore.Application.Presentation.ViewModels.Base;
using MyOnlineStore.Application.Presentation.Views.AdminScenarios;
using MyOnlineStore.Application.Presentation.Views.AdminScenarios.Stores.ContentStepView;
using MyOnlineStore.Application.Presentation.Views.AdminScenarios.Stores.EmployeeStepsView;
using MyOnlineStore.Application.Presentation.Views.AdminScenarios.Stores.OfferStepView;
using MyOnlineStore.Application.Presentation.Views.Home;
using MyOnlineStore.Application.Presentation.Views.LoginScenarios;
using MyOnlineStore.Application.Presentation.Views.OrdersScenarios;
using MyOnlineStore.Application.Presentation.Views.StoresScenarios;
using MyOnlineStore.Application.Presentation.Views.StoresScenarios.OffersAndContent;
using MyOnlineStore.Application.Presentation.Views.StoresScenarios.StoreProductContent;
using MyOnlineStore.Application.Presentation.Views.WorkSpace;
using MyOnlineStore.Entities.Models.Stores;
using Syncfusion.XForms.ComboBox;
using Syncfusion.XForms.TextInputLayout;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Xamarin.Forms;

namespace MyOnlineStore.Application.Presentation.ViewModels
{

    public class ShellViewModel : BaseViewModel, IShellViewModel
    {

        #region Fields

        //public static Store? Admin_Current_Store;
        protected readonly IStoreDataStore _StoreDataStore;

        protected readonly Dictionary<string, Type> routes = new Dictionary<string, Type>();
        private static View? headerContent;

        /// <summary>
        /// Starts get value with CurrentStoreKey. If null sets Empty
        /// </summary>
        private Guid currentStoreId = App.ApplicationManager.CurrenstStoreGuid;

        private byte[]? storeLogoBytes;

        #endregion Fields

        #region Constructors

        public ShellViewModel(IStoreDataStore storeDataStore)
        {
            _StoreDataStore = storeDataStore;

            Init();
        }

        #endregion Constructors

        #region Properties

        private ObservableCollection<Store>? mystores;
        //private ObservableCollection<Store>? mystores =
        //    new ObservableCollection<Store>(
        //            App.ApplicationManager.IsLogged(logoutProcedure: true)
        //        ?
        //            new ObservableCollection<Store>(App.ApplicationManager.CurrentUser!.MyStores)
        //        :
        //            new ObservableCollection<Store>()
        //        );

        public Dictionary<string, Type> Routes { get { return routes; } }

        public Guid CurrentStoreId
        {
            get => currentStoreId;
            set
            {
                if (currentStoreId != value) { currentStoreId = value; }
            }
        }

        public byte[]? StoreLogoBytes
        {
            get
            {
                if (CurrentStore is object)
                {
                    storeLogoBytes = CurrentStore.Logo;
                }
                else { storeLogoBytes = new byte[1]; }

                return storeLogoBytes;
            }
        }

        public Store? CurrentStore
        {
            get => App.ApplicationManager.CurrentStore;
        }

        public ObservableCollection<Store>? MyStores
        {
            get => mystores;
            set
            {
                mystores = value;
                RaisePropertyChanged(() => MyStores);
            }
        }

        public View? HeaderContent
        {
            get => headerContent;
            set
            {
                headerContent = value;
                RaisePropertyChanged(() => HeaderContent);
            }
        }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Create Shell Header
        /// A Picker and Image when more than one Store
        /// </summary>
        public void CreateHeader()
        {
            var stack = new StackLayout()
            {
                Padding = 5,
                BackgroundColor = (Color)App.Current.Resources["Primary"],
                Visual = VisualMarker.Material,
                Spacing = -5
            };

            var image = new CachedImage
            {
                Aspect = Aspect.AspectFill,
                Opacity = 0.7,
                CacheType = FFImageLoading.Cache.CacheType.Disk,
                HeightRequest = 135
            };

            SfComboBox storesBox = new SfComboBox
            {
                DataSource = MyStores,
                WatermarkColor = Color.Black,
                SelectedItem = CurrentStore,
                TextColor = Color.Black,
                HeightRequest = 50,
                Margin = 0
            };
            SfTextInputLayout inputLayout = new SfTextInputLayout
            {
                ContainerBackgroundColor = (Color)App.Current.Resources["CardWhite"],
                ContainerType = ContainerType.Outlined,
                OutlineCornerRadius = 5,
                Visual = VisualMarker.Material,
                InputView = storesBox,
                HeightRequest = 55,
                Padding = 0,
                Margin = 0,
                ReserveSpaceForAssistiveLabels = false
            };

            storesBox.SetBinding(
                    targetProperty: SfComboBox.DataSourceProperty,
                    path: nameof(MyStores),
                    mode: BindingMode.Default
                 );
            storesBox.SetBinding(
                    targetProperty: SfComboBox.SelectedItemProperty,
                    path: nameof(CurrentStore),
                    mode: BindingMode.Default
                );
            storesBox.DisplayMemberPath = nameof(CurrentStore.StoreName);

            image.SetBinding(
                    targetProperty: CachedImage.SourceProperty,
                    path: nameof(StoreLogoBytes),
                    mode: BindingMode.Default,
                    converter: new ByteArrayToImageSourceConverter()
                );

            stack.Children.Add(image);
            stack.Children.Add(inputLayout);
            HeaderContent = stack;
        }

        /// <summary>
        /// Initialize ViewModel necesary methods
        /// </summary>
        private void Init()
        {
            //if(App.ApplicationManager.CurrentStore is null)
            //{
            //    MyStores = FetchStores();
            //}

            RegisterRoutes();
        }

        /// <summary>
        /// Fetch <cref>CurrentUser</cref> Stores
        /// </summary>
        private ObservableCollection<Store>? FetchStores()
        {
            ObservableCollection<Store>? stores = null;
            string ownerId;
            IEnumerable<Store>? ownerStores;

            if (App.ApplicationManager.IsLogged())
            {
                ownerId = App.ApplicationManager.CurrentUser!.Id.ToString();
                ownerStores = _StoreDataStore.GetStoresByOwnerId(ownerId);

                if (ownerStores is object)
                {
                    stores = new ObservableCollection<Store>(ownerStores);
                }
            }

            return stores;
        }

        /// <summary>
        ///
        /// </summary>
        private void RegisterRoutes()
        {
            routes.Add(HomePage.Route, typeof(HomePage));
            routes.Add(ProductItemDetailPage.Route, typeof(ProductItemDetailPage));
            routes.Add(InventoryListPage.Route, typeof(InventoryListPage));
            routes.Add(StoreShopPage.Route, typeof(StoreShopPage));
            routes.Add("FeaturedItemUpload", typeof(FeaturedItemUpload));
            routes.Add(CheckoutListPage.Route, typeof(CheckoutListPage));
            routes.Add(StoreHomePage.Route, typeof(StoreHomePage));
            //routes.Add(LoginPage.Route, typeof(LoginPage));
            //routes.Add(AppShell.Route, typeof(AppShell));
            routes.Add(AdminHomePage.Route, typeof(AdminHomePage));
            routes.Add(HomeContentPage.Route, typeof(HomeContentPage));
            routes.Add(OffersHomePage.Route, typeof(OffersHomePage));
            routes.Add(EmployeeHomePage.Route, typeof(EmployeeHomePage));
            routes.Add(RegisterPage.Route, typeof(RegisterPage));
            routes.Add(ConfigureEmployeePage.Route, typeof(ConfigureEmployeePage));
            routes.Add(EmployeeDetailView.Route, typeof(EmployeeDetailView));
            routes.Add(UpdatePicturePage.Route, typeof(UpdatePicturePage));
            routes.Add(DetailWorkSpace.Route, typeof(DetailWorkSpace));
            routes.Add(OfferAndContentView.Route, typeof(OfferAndContentView));
            routes.Add(StoreProductsToBuy.Route, typeof(StoreProductsToBuy));

            foreach (var item in routes)
            {
                Routing.RegisterRoute(item.Key, item.Value);
            }
        }

        /// <summary>
        /// Sets CurrentStore
        /// if there is any currentstoreid saved localy
        /// else get the first one of already fetched
        /// </summary>
        private void SetCurrentStore()
        {
            Store? store;

            if (App.ApplicationManager.IsLogged()
                && App.ApplicationManager.CurrentUser!.HasStore())
            {
                if (App.ApplicationManager.CurrenstStoreGuid == Guid.Empty)
                {
                    store = MyStores.FirstOrDefault();
                    CurrentStoreId = store.Id;
                }
            }
        }

        #endregion Methods
    }
}