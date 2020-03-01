using MyOnlineStore.Application.Data.Models.Presenters;
using MyOnlineStore.Application.Common.Interfaces.DataStore;
using MyOnlineStore.Application.Common.Interfaces.Services.Store;
using MyOnlineStore.Application.Common.Utilities;
using MyOnlineStore.Application.Presentation.Views.StoresScenarios;
using MyOnlineStore.Entities.Models.Stores;
using MyOnlineStore.Entities.Models.Users;
using Syncfusion.ListView.XForms;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace MyOnlineStore.Application.Presentation.Services.Stores
{
    public class StoreCardService : IStoreCardService
    {
        public IFavoritesDataStore FavoriteDataStore { get; private set; }
        public ICommand NavigateToStoreDetailCommand { get; set; }
        public ICommand MakeStoreFavoriteClickCommand { get; set; }

        public static int Counter { get; set; }

        public StoreCardService(IFavoritesDataStore favoriteDataStore)
        {
            FavoriteDataStore = favoriteDataStore;
            Init();
        }

        private void Init()
        {
            NavigateToStoreDetailCommand = new Command<CommandEventData>(async (data) => await NavigateToStoreDetail(data));
           
            MakeStoreFavoriteClickCommand = new Command<object>(async (store) => await MakeStoreFavoriteClickHandler(store));
        }

        private async Task MakeStoreFavoriteClickHandler(object storeObj)
        {
            if (App.ApplicationManager.IsLogged() 
                && App.ApplicationManager.CurrentUser is User client 
                && storeObj is StorePresenter store)
            {
                if (store.IsFavorite)
                {
                    await FavoriteDataStore.RemoveFavorite(storeId: store.Id, clientId: client.Id);
                    store.EmptyHeart();
                }
                else
                {
                    await FavoriteDataStore.AddFavorite(storeId: store.Id, clientId: client.Id);
                    store.FillHeart();
                }
            }
        }
        private async Task NavigateToStoreDetail(CommandEventData data)
        {
            await Device.InvokeOnMainThreadAsync(async () =>
             {
                 if (data is object
                    && data.Args is Syncfusion.ListView.XForms.ItemTappedEventArgs args
                    && args.ItemData is StorePresenter store)
                 {
                     if (store.Id.ToString() is string id)
                     {
                         await Task.Delay(350);
                         await Shell.Current.GoToAsync($"{StoreHomePage.Route}?storeId={id}",
                             animate: true);
                     }
                 }
             });
        }
        
        
    }
}
