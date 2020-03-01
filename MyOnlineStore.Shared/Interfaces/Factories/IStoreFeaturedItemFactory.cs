using MyOnlineStore.Entities.Models.Stores;
using System;

namespace MyOnlineStore.Shared.Interfaces.Factories
{
    public interface IStoreFeaturedItemFactory
    {
        StoreFeaturedItem CreateSimpleFeaturedItem();

        StoreFeaturedItem CreateSimpleFeaturedItem(Store store);

        StoreFeaturedItem CreateSimpleFeaturedItem(byte[] image, Guid storeId);
    }
}