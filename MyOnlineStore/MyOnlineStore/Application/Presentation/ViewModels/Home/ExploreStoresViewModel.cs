using MyOnlineStore.Application.Common.Interfaces.IViewModel;
using MyOnlineStore.Application.Common.Utilities;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Xamarin.Forms;
using MyOnlineStore.Application.Common.Interfaces.Services.Store;
using System.Windows.Input;
using MyOnlineStore.Application.Common.Interfaces.DataStore;
using MyOnlineStore.Entities.Models.Users;
using MyOnlineStore.Application.Presentation.Views.Home;
using MyOnlineStore.Application.Data.Models.Presenters;
using MyOnlineStore.Application.Common.Interfaces.Factories;

namespace MyOnlineStore.Application.Presentation.ViewModels.Home
{
    public class ExploreStoresViewModel : Base.BaseViewModel, IExploreStoresViewModel
    {
        public IStoreCardService StoreCardService { get; private set; }

        public IStoreDataStore StoreDataStore { get; private set; }

        public IStorePresenterFactory StorePresenterFactory { get; set; }

        public ICommand SearchCommand { get; set; }
        public ICommand RefreshCommand { get; set; }
        public IEnumerable<StorePresenter>? AvailableStores { get; set; }

        private ObservableCollection<StorePresenter> _storeList;
        public ObservableCollection<StorePresenter> Stores
        {
            get { return _storeList; }
            set { _storeList = value; RaisePropertyChanged(()=>Stores); }
        }
        public ExploreStoresViewModel(IStoreDataStore storeDataStore,IStoreCardService storeCardService,IStorePresenterFactory storePresenterFactory)
        {
            StoreCardService = storeCardService;
            StoreDataStore = storeDataStore;
            StorePresenterFactory = storePresenterFactory;

            SearchCommand = new Command<CommandEventData>(Search);
            RefreshCommand = new Command(() => Refresh());
            FetchStores();
            Stores = new ObservableCollection<StorePresenter>(AvailableStores);
            Init();
        }
        private void Init()
        {
            RefreshCommand.Execute(null);
        }
        private void Refresh()
        {
            MessagingCenter.Subscribe<ExploreStores>(this, "FetchStoresToExplore", sender =>
            {
                FetchStores();
                Stores = new ObservableCollection<StorePresenter>(AvailableStores);
            });
        }
        
        public void Search(CommandEventData searchData)
        {
            if (searchData is object && searchData.Parameter is object)
            {
                string searchText = searchData.Parameter.ToString();

                if (string.IsNullOrEmpty(searchText))
                {
                    Stores = new ObservableCollection<StorePresenter>(AvailableStores);
                }
                else
                {
                    Stores = new ObservableCollection<StorePresenter>(AvailableStores
                        .Where(t => t.StoreName.ToLower().Contains(searchText.ToLower()) || t.Category.ToLower().Contains(searchText.ToLower())));
                }
            }
        }
        public void FetchStores()
        {
            IEnumerable<StorePresenter> storePresenters;

            if (App.ApplicationManager.IsLogged() && App.ApplicationManager.CurrentUser is User user)
            {
                storePresenters = StoreDataStore.GetAllToExplore(user.Id.ToString());
                AvailableStores = new ObservableCollection<StorePresenter>(
                        StorePresenterFactory.CreateStorePresenterCollection(storePresenters)
                    );
            }
        }
    }
}
