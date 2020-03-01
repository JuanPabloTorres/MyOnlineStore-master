using MyOnlineStore.Application.Common.Interfaces.DataStore;
using MyOnlineStore.Application.Common.Interfaces.IViewModel;
using MyOnlineStore.Application.Presentation.ViewModels;
using MyOnlineStore.Application.Common.Utilities;
using MyOnlineStore.Application.Presentation.ViewModels.Base;
using MyOnlineStore.Entities.Models.Purchases;
using MyOnlineStore.Entities.Models.Stores;
using MyOnlineStore.Application.Presentation.Views.AdminScenarios;
using MyOnlineStore.Application.Common.Global.Constants;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Syncfusion.ListView.XForms;

namespace MyOnlineStore.Application.Presentaion.ViewModels.AdminScenarios
{
    [QueryProperty("OwnerStoreId","owenerstoreid")]
    public class InventoryViewModel : BaseViewModel, IInventoryViewModel
    {
        protected readonly IProductItemDataStore _ProductItemData;
        protected readonly IStoreDataStore _StoreDataStore;

        private ObservableCollection<ProductItem> _inventoryList = null!;
        public ObservableCollection<ProductItem> InventoryItems
        {
            get => _inventoryList;
            set
            {
                _inventoryList = value;
                RaisePropertyChanged(() => InventoryItems);
            }
        }

        public ICommand NavigateToNewProductItemDetailPageCommand { get; set; } 
        public ICommand NavigateToProductItemDetailPageCommand { get; set; }
        public ICommand RefreshCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand GoHomeCommand { get; set; }

        /// <summary>
        /// Query property parameter of store owner
        /// </summary>
        private string owenerstoreid = string.Empty;
        public string OwnerStoreId
        {
            get => owenerstoreid;
            set
            { 
                owenerstoreid = value;

                var data = _ProductItemData.GetInventoryForStore(OwnerStoreId);

                
                InventoryItems = new ObservableCollection<ProductItem>(data);

                RaisePropertyChanged(() => OwnerStoreId);
            }
        }

        private ProductItem? currentItem;
        public ProductItem? CurrentItem
        {
            get => currentItem;
            set
            {
                currentItem = value;
                RaisePropertyChanged(() => CurrentItem);
            }
        }

        public InventoryViewModel(IProductItemDataStore productItemDataStore, IStoreDataStore storeDataStore)
        {

          
            _ProductItemData = productItemDataStore;
            _StoreDataStore = storeDataStore;

            NavigateToNewProductItemDetailPageCommand = new Command(async () => await NavigateToNewProductItemDetailView());
            NavigateToProductItemDetailPageCommand = new Command<CommandEventData>(async (data) => await NavigateToProductItemDetailView(data));
            //RefreshCommand = new Command(() => Refresh());
            DeleteCommand = new Command<CommandEventData>((data) => DeleteSwipe(data));

            GoHomeCommand = new Command(async () => {

                await Shell.Current.Navigation.PopAsync();
            });

            //Init();
        }

        private void Init()
        {
            RefreshCommand.Execute(null);
        }

        private void Refresh()
        {
            IsBusy = true;

            MessagingCenter.Subscribe<InventoryListPage>(this, "update", (sender) =>
            {
                InventoryItems = FetchStoreInventory();
            });

            IsBusy = false;
        }

        private async Task NavigateToProductItemDetailView(CommandEventData data)
        {
            await Task.Delay(250);

            if (App.ApplicationManager.CurrentStore is object)
            {
                OwnerStoreId = App.ApplicationManager.CurrentStore.Id.ToString();

                if (data.Args is ItemSelectionChangedEventArgs args)
                {
                    CurrentItem = args.AddedItems[0] as ProductItem;

                    if (CurrentItem != null)
                    {
                        await Shell.Current.GoToAsync($"{ProductItemDetailPage.Route}?" +
                       $"storeId={OwnerStoreId}" +
                       $"&productId={CurrentItem.Id.ToString()}" +
                       $"&isEditable={true}",
                       animate: true);
                    }
                }
            }
            else
            {
                // TODO: Logout
            }
        }
        private async Task NavigateToNewProductItemDetailView()
        {          

                await Shell.Current.GoToAsync($"{ProductItemDetailPage.Route}?" +
                    $"storeId={OwnerStoreId}" +
                    $"&productId={ViewType.DetailView.New.ToString()}" +
                    $"&isEditable={true}",
                    animate: true);
            
        }

        public ObservableCollection<ProductItem> FetchStoreInventory()
        {
            IsBusy = true;
            ObservableCollection<ProductItem> products = new ObservableCollection<ProductItem>();

            if (App.ApplicationManager.CurrentStore is object)
            {
                var inventory = _ProductItemData.GetInventoryForStore(OwnerStoreId);
                App.ApplicationManager.CurrentStore.ProductItems = new List<ProductItem>(inventory);

                IsBusy = false;

                products = new ObservableCollection<ProductItem>(App.ApplicationManager.CurrentStore.ProductItems);
            }

            return products;
        }

        private async Task<Store> FetchOwnerStore()
        {
            return await Task.Run(()=>_StoreDataStore.GetItem(Uri.UnescapeDataString(OwnerStoreId))); 
        }
        private void DeleteSwipe(CommandEventData data)
        {
            if (data.Args is Syncfusion.ListView.XForms.SwipeEndedEventArgs args 
                && args.ItemData is ProductItem item)
            {
                InventoryItems.Remove(item);
                
                /*
                    TODO: Implement Is Not Alive command to remove from inventory but not delete.
                 */
                _ProductItemData.DeleteItem(item.Id.ToString());
            }
        }
    }
}
