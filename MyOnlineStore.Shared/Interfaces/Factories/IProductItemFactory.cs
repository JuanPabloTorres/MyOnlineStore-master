using MyOnlineStore.Entities.Models.Purchases;
using MyOnlineStore.Entities.Models.Stores;
using System;

namespace MyOnlineStore.Shared.Interfaces.Factories
{
    public interface IProductItemFactory
    {
        ProductItem CreateSimpleProducItem();

        ProductItem CreateProductItem(ProductItem productItem);

        ProductItem CreateProductItem(string itemName = null!, string price = null!, string description = null!, string quantity = null!, string category = null!, byte[]? logo = null, Store mystore = null!, string? storeId = null, Guid? id = null);
    }
}