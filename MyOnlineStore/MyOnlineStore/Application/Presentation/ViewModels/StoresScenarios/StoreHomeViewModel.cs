using MyOnlineStore.Application.Common.Interfaces.IViewModel;
using MyOnlineStore.Application.Presentation.ViewModels.Base;
using MyOnlineStore.Application.Common.Interfaces.DataStore;
using MyOnlineStore.Application.Common.Interfaces.Factories;
using MyOnlineStore.Application.Common.Utilities;
using MyOnlineStore.Entities.Models.Stores;
using Xamarin.Forms;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Threading.Tasks;
using MyOnlineStore.Application.Presentation.Views.OrdersScenarios;
using MyOnlineStore.Application.Data.Models.Presenters;
using MyOnlineStore.Interfaces.DataStore;
using MyOnlineStore.Application.Presentation.Managers;
using MyOnlineStore.Entities.Models.Users;
using MyOnlineStore.Application.Presentation.Views.StoresScenarios;
using MyOnlineStore.Application.Presentation.Views.StoresScenarios.OffersAndContent;
using MyOnlineStore.Application.Data.Presenters;
using MyOnlineStore.Application.Presentation.Views.StoresScenarios.StoreProductContent;
using MyOnlineStore.Entities.Models.Purchases;
using System;

namespace MyOnlineStore.Application.Presentation.ViewModels.StoresScenarios
{
    [QueryProperty("StoreId", "storeId")]
    public class StoreHomeViewModel : BaseViewModel, IStoreHomeViewModel
    {
        private StorePresenter store;
        protected IStoreDataStore StoreDataStore { get; private set; }
        protected IOrdersDataStore OrdersDataStore { get; private set; }
        IProductItemDataStore productItemDataStore { get;  set; }

        public ICommand SearchCommand { get; set; }
        public ICommand NavigateToShopCommand { get; set; }
        public ICommand NavigateToOfferAndContentCommand { get; set; }

        public StorePresenter Store
        {
            get => store;
            set { store = value; RaisePropertyChanged(() => Store); }
        }
        public string CurrentClientId { get; set; }

        private byte[] storelogo;

        public byte[] StoreLogo
        {
            get { return storelogo; }
            set { storelogo = value;
                RaisePropertyChanged(() => StoreLogo);
            }
        }


        /// <summary>
        /// Manages the Cart, Total Price & the Order
        /// </summary>
        public ShoppingManager ShoppingManager { get; private set; }

        /// <summary>
        /// Id of the Store to fetch the inventory items
        /// </summary>
        private string storeId = string.Empty;
        public string StoreId
        {
            get => storeId;
            set
            {
                storeId = value;
                ShoppingManager.CurrentShoppingStore = StoreDataStore.GetStoreWithInventory(storeId);
                Store = new StorePresenter(
                    ShoppingManager.CurrentShoppingStore,
                    StoreDataStore.IsFavorite(storeId, CurrentClientId)
                );
                Init();

                var products = this.productItemDataStore.GetInventoryForStore(StoreId);
                Products = new ObservableCollection<ProductItemPresenter>();

                foreach (var item in products)
                {
                    ProductItemPresenter itemPresenter = new ProductItemPresenter(item);

                    Products.Add(itemPresenter);
                }
               
            }
        }

        /// <summary>
        /// Inventory Items to be displayed 
        /// </summary>
        private ObservableCollection<ProductItemPresenter> productItems
            = new ObservableCollection<ProductItemPresenter>();
        public ObservableCollection<ProductItemPresenter> Products
        {
            get => productItems;
            set { productItems = value; RaisePropertyChanged(() => Products); }
        }

        
        /// <summary>
        /// Content to be displayed
        /// </summary>
        private ObservableCollection<StoreFeaturedItem>? _featuredItems;
        public ObservableCollection<StoreFeaturedItem>? FeaturedItems
        {
            get => _featuredItems;
            set
            {
                _featuredItems = value;
                RaisePropertyChanged(() => FeaturedItems);
            }
        }

        /// <summary>
        /// Has Content to be displayed
        /// </summary>
        private bool _hasHeaderContent = false;
        public bool HasHeaderContent
        {
            get => _hasHeaderContent;
            set
            {
                _hasHeaderContent = value;
                RaisePropertyChanged(() => HasHeaderContent);
            }
        }

        /// <summary>
        /// Text to be searched in properties of the Products
        /// </summary>
        public string SearchText { get; set; } = string.Empty;

        public ObservableCollection<ProductItem> StoreProducts { get; set; }

        public StoreHomeViewModel(IStoreDataStore storeDataStore, 
            IOrdersDataStore ordersDataStore, ShoppingManager shoppingManager,IProductItemDataStore productItemDataStore)
        {
            StoreDataStore = storeDataStore;
            OrdersDataStore = ordersDataStore;
            this.productItemDataStore = productItemDataStore;

           

            ShoppingManager = shoppingManager;

            SearchCommand = new Command<CommandEventData>((data) => SearchProduct(data));
            NavigateToShopCommand = new Command(async() => await NavigateToShopHandler());
            NavigateToOfferAndContentCommand = new Command(async () => await Shell.Current.GoToAsync($"{OfferAndContentView.Route}?storeId={StoreId}"));
            if (App.ApplicationManager.IsLogged())
            {
                CurrentClientId = App.ApplicationManager.CurrentUser!.Id.ToString();
            }
        }

        private async Task NavigateToShopHandler()
        {
            await Device.InvokeOnMainThreadAsync(async () =>
            {
                //await Shell.Current.GoToAsync($"{StoreProductsToBuy.Route}?" + $"storeId={StoreId}");

                await Shell.Current.GoToAsync($"{StoreShopPage.Route}",
                       animate: true);
            });
        }

        #region Methods
        private void Init()
        {
            /// Set Content
            //FeaturedItems = new ObservableCollection<StoreFeaturedItem>(ShoppingManager.CurrentShoppingStore.FeaturedItems);
            FeaturedItems = CreateMockFeaturedItem();
            if (ShoppingManager.CurrentShoppingStore.Logo is byte[] logo)
            {
                StoreLogo = logo;
                FeaturedItems.Insert(0, new StoreFeaturedItem { Image = logo });


                Guid storeidguid = Guid.Parse(StoreId);
                var feartureitems = StoreDataStore.GetFeaturedItemsOfStore(storeidguid);

                foreach (var item in feartureitems)
                {
                    FeaturedItems.Add(item);
                }
            }
            HasHeaderContent = FeaturedItems.Count > 0 ? true : false;

            /// Create or get an order 
            Task.Run(async()=>await ShoppingManager.PrepareOrder()).Wait();

            /// Update the Cart and Total Price 
            ShoppingManager.UpdateCartNumbers();
        }

        private ObservableCollection<StoreFeaturedItem> CreateMockFeaturedItem()
        {
            return new ObservableCollection<StoreFeaturedItem>
            {
                //new StoreFeaturedItem(),
                //new StoreFeaturedItem(),
                //new StoreFeaturedItem(),
            };
        }

        private void SearchProduct(CommandEventData searchData)
        {
            //if (searchData is object && searchData.Parameter is object)
            //{
            //    SearchText = searchData.Parameter.ToString();

            //    if (string.IsNullOrEmpty(SearchText))
            //    {
            //        Products = new ObservableCollection<ProductItem>(AvailableItems);
            //    }
            //    else
            //    {
            //        Products = new ObservableCollection<ProductItem>(AvailableItems
            //             .Where(i => i.Name.Contains(SearchText)
            //             || i.Price.ToString().Contains(SearchText)
            //             || i.Category.Contains(SearchText)
            //             || i.Description.Contains(SearchText)));
            //    }
            //}
        }

        #endregion
    }
}
