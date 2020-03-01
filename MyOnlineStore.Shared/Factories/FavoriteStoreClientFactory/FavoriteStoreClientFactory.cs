using MyOnlineStore.Entities.Models.Stores;
using MyOnlineStore.Entities.Models.Users;
using MyOnlineStore.Shared.Interfaces.Factories;
using System;

namespace MyOnlineStore.Application.Data.Factories.FavoriteStoreClientFactory
{
    public class FavoriteStoreClientFactory : IFavoriteStoreClientFactory
    {
        #region Methods

        public ClientsFavoriteStores CreateSimpleFavoriteStoreClient()
        {
            return new ClientsFavoriteStores();
        }

        public bool CreateClientFavoriteStore(out ClientsFavoriteStores clientFavoriteStore, Guid? storeId = null, Guid? clientUserId = null, Store? store = null, User? clientUser = null)
        {
            bool hasMinimiumToCreate = false;
            clientFavoriteStore = CreateSimpleFavoriteStoreClient();

            if(store is object)
            {
                clientFavoriteStore.Store = store;
                clientFavoriteStore.StoreId = store.Id;
            }
            else if(storeId is object)
            {
                clientFavoriteStore.StoreId = storeId.Value;
            }

            if(clientUserId is object)
            {
                clientFavoriteStore.ClientId = clientUserId.Value;
            }
            else if(clientUser is object)
            {
                clientFavoriteStore.ClientId = clientUser.Id;
            }

            if(clientFavoriteStore.ClientId != Guid.NewGuid() && clientFavoriteStore.StoreId != Guid.NewGuid()) { hasMinimiumToCreate = true; } else hasMinimiumToCreate = false;

            return hasMinimiumToCreate;
        }

        #endregion Methods
    }
}