using System.Collections.ObjectModel;
using MyOnlineStore.Application.Data.Models.Presenters;

namespace MyOnlineStore.Application.Common.Interfaces.IViewModel
{
    public interface IHomeViewModel
    {
        //Command<CommandEventData> NavigateToStoreDetailCommand { get; }
        ObservableCollection<StorePresenter>? FavoriteStores { get; }
        void FetchFavoriteStores();
    }
}
