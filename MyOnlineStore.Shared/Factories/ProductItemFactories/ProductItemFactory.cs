using MyOnlineStore.Entities.Models.Purchases;
using MyOnlineStore.Entities.Models.Stores;
using MyOnlineStore.Shared.Interfaces.Factories;
using System;

namespace MyOnlineStore.Shared.Factories.ProductItemFactories
{
    public class ProductItemFactory : IProductItemFactory
    {
        public ProductItem CreateProductItem(ProductItem productItem)
        {
            return new ProductItem
            {
                Id = productItem.Id,
                Name = productItem.Name,
                Description = productItem.Description,
                Price = productItem.Price,
                Quantity = productItem.Quantity,
                Image = productItem.Image,
                Category = productItem.Category,
                MyStoreId = productItem.MyStoreId
            };
        }

        public ProductItem CreateProductItem(string? itemName = null, string? price = null, string? description = null, string? quantity = null, string? category = null, byte[]? logo = null, Store? mystore = null, string? storeId = null, Guid? id = null)
        {
            float priceNum = float.Parse(price);
            uint qty = uint.Parse(quantity);
            Guid guid = Guid.Parse(storeId);
            return new ProductItem
            {
                Id = id ??= Guid.Empty,
                Name = itemName ?? string.Empty,
                Description = description ?? string.Empty,
                Price = priceNum,
                Quantity = qty,
                Image = logo,
                Category = category ?? string.Empty,
                MyStoreId = guid,
                //MyStore = mystore
            };
        }

        public ProductItem CreateSimpleProducItem()
        {
            return new ProductItem();
        }
    }
}