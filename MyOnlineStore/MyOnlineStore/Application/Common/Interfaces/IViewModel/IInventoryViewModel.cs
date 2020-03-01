using MyOnlineStore.Entities.Models.Purchases;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace MyOnlineStore.Application.Common.Interfaces.IViewModel
{
    public interface IInventoryViewModel 
    {
        ObservableCollection<ProductItem> InventoryItems { get; }
        ICommand NavigateToNewProductItemDetailPageCommand { get;}
        ICommand NavigateToProductItemDetailPageCommand { get; }
        ObservableCollection<ProductItem> FetchStoreInventory();
    }
}
