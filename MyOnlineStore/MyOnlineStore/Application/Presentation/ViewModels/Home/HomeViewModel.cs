using MyOnlineStore.Application.Common.Interfaces.DataStore;
using MyOnlineStore.Application.Common.Interfaces.Factories;
using MyOnlineStore.Application.Common.Interfaces.IViewModel;
using MyOnlineStore.Application.Common.Interfaces.Services.Store;
using MyOnlineStore.Application.Data.Models.Presenters;
using MyOnlineStore.Application.Presentation.ViewModels.Base;
using MyOnlineStore.Application.Presentation.Views.Home;
using MyOnlineStore.Entities.Models.Users;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace MyOnlineStore.Application.Presentation.ViewModels.Home
{
    public class HomeViewModel : BaseViewModel, IHomeViewModel
    {
        public readonly IStoreCardService StoreCardService;
        public readonly IStoreDataStore StoreDataStore;
        public readonly IStorePresenterFactory StorePresenterFactory;
        private bool isLoading = true;
        private ObservableCollection<StorePresenter> _stores;

        public HomeViewModel(IStoreDataStore storeDataStore, IStoreCardService storeCardService, IStorePresenterFactory storePresenterFactory)
        {
            StoreDataStore = storeDataStore;
            StoreCardService = storeCardService;
            StorePresenterFactory = storePresenterFactory;

            FetchFavoriteStores();

            RefreshCommand = new Command(() => Refresh());

            Init();
        }

        public StorePresenter CurrentStore { get; set; }

        public ObservableCollection<StorePresenter> FavoriteStores
        {
            get => _stores;
            set { _stores = value; RaisePropertyChanged(() => FavoriteStores); }
        }

        public bool IsLoading
        {
            get => isLoading;
            set { isLoading = value; RaisePropertyChanged(() => IsLoading); }
        }

        public ICommand RefreshCommand { get; set; }

        public void FetchFavoriteStores()
        {
            IEnumerable<StorePresenter> storePresenters;

            if(App.ApplicationManager.IsLogged()
                && App.ApplicationManager.CurrentUser is User user)
            {
                storePresenters = StoreDataStore.GetClientFavorites(user.Id.ToString());
                FavoriteStores = new ObservableCollection<StorePresenter>(StorePresenterFactory.CreateStorePresenterCollection(storePresenters));
            }
        }

        private void Init()
        {
            RefreshCommand.Execute(null);
        }

        private void Refresh()
        {
            IsBusy = true;
            MessagingCenter.Subscribe<HomePage>(this, "UpdateFavorites", (sender) =>
            {
                FetchFavoriteStores();
            });
            IsBusy = false;
        }
    }
}