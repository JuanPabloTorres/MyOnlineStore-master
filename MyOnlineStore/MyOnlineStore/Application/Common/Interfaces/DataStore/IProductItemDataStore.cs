using MyOnlineStore.Entities.Models.Purchases;
using MyOnlineStore.Shared.Models.Purchases;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyOnlineStore.Application.Common.Interfaces.DataStore
{
    public interface IProductItemDataStore : IEmployeeDataStore<ProductItem>
    {
        ProductItem GetItemByNameFromInventory(string storeId, string productName);
        IEnumerable<ProductItem> GetInventoryForStore(string storeId);
        bool InsertOffer(Offer offer);

        bool RemoveOffert(Offer offer);

        bool ProductHasOffer(string productId, string storeId);
        IEnumerable<Offer> GetOfferOfStore(string storeId);

        IEnumerable<ProductItem> GetProductItemsWithQuantity(int quantity, string storeId);
    }
}
