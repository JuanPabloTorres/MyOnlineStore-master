using MyOnlineStore.Shared.Models.Purchases;
using MyOnlineStore.ViewModels.Base;

namespace MyOnlineStore.ViewModels
{
    public class ItemDetailViewModel : BaseViewModel
    {
        public ProductItem Item { get; set; }
        public ItemDetailViewModel(ProductItem item = null)
        {
            Title = item?.Name;
            Item = item;
        }
    }
}
