using MyOnlineStore.Entities.Models.Purchases;
using MyOnlineStore.Shared.Models.Purchases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyOnlineStore.MobileAppService.Interfaces.Repositories
{
    public interface IProductItemRepository : IRepository<ProductItem>
    {
        ProductItem GetItemByNameFromInventory(Guid storeId, string productName);
        IEnumerable<ProductItem> GetInventoryForStore(Guid storeId);
        bool Dead(ProductItem item);

        IEnumerable<Offer> GetOfferOfStore(string storeId);

        bool InsertProductOffer(Offer storeOffer);

        bool RemoveOffert(Offer offer);

        bool ProductHasOffer(string productId,string storeId);

        bool UpdateProductPrice(string productId, double newprice);

        IEnumerable<ProductItem> GetProductItemsWithQuantity(int quantity, string storeId);
    }
}
