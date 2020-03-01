using MyOnlineStore.Application.Data.Models.Presenters;
using MyOnlineStore.Application.Common.Utilities;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace MyOnlineStore.Application.Common.Interfaces.IViewModel
{
    public interface IExploreStoresViewModel
    {
        ICommand SearchCommand { get; }
        ObservableCollection<StorePresenter>? Stores { get; }
        void Search(CommandEventData searchText);
    }
}
