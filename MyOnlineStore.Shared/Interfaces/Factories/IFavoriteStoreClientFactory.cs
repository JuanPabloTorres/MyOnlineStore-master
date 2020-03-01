using MyOnlineStore.Entities.Models.Stores;
using MyOnlineStore.Entities.Models.Users;
using System;

namespace MyOnlineStore.Shared.Interfaces.Factories
{
    public interface IFavoriteStoreClientFactory
    {
        ClientsFavoriteStores CreateSimpleFavoriteStoreClient();

        bool CreateClientFavoriteStore(out ClientsFavoriteStores clientFavoriteStore, Guid? storeId = null, Guid? clientUserId = null, Store? store = null, User? clientUser = null);
    }
}