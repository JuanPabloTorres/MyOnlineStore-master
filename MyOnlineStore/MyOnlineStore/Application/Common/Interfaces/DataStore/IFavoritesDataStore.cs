using MyOnlineStore.Entities.Models.Stores;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyOnlineStore.Application.Common.Interfaces.DataStore
{
    public interface IFavoritesDataStore
    {
        Task<bool> AddFavorite(Guid storeId, Guid clientId);
        Task<bool> RemoveFavorite(Guid storeId, Guid clientId);
        Task<List<ClientsFavoriteStores>> GetUserFavorites(Guid id);
    }
}
