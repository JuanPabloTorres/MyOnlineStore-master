using MyOnlineStore.Entities.Models.Stores;
using MyOnlineStore.Shared.Interfaces.Factories;
using System;

namespace MyOnlineStore.Shared.Factories.StoreFeaturedItemFactories
{
    public class StoreFeaturedItemFactory : IStoreFeaturedItemFactory
    {
        public StoreFeaturedItem CreateSimpleFeaturedItem()
        {
            return new StoreFeaturedItem();
        }

        public StoreFeaturedItem CreateSimpleFeaturedItem(Store store)
        {
            return new StoreFeaturedItem
            {
                MyStoreId = store.Id
            };
        }

        public StoreFeaturedItem CreateSimpleFeaturedItem(byte[] image, Guid storeId)
        {
            return new StoreFeaturedItem
            {
                Image = image,
                MyStoreId = storeId
            };
        }
    }
}