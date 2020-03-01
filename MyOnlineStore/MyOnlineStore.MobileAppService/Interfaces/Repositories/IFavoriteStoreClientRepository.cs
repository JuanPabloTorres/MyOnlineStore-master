using MyOnlineStore.Entities.Models.Stores;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyOnlineStore.MobileAppService.Interfaces.Repositories
{
    public interface IFavoriteStoreClientRepository 
    {
        Task AddFavorite(Guid storeId, Guid clientId);
        Task RemoveFavorite(Guid storeId, Guid clientId);
        Task<List<ClientsFavoriteStores>> GetUserFavorites(Guid id);
    }
}
