using Microsoft.EntityFrameworkCore;
using MyOnlineStore.Entities.Models.Stores;
using MyOnlineStore.MobileAppService.Context;
using MyOnlineStore.MobileAppService.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyOnlineStore.MobileAppService.Repositories
{
    public class FavoriteStoreClientRepository : IFavoriteStoreClientRepository
    {
        public MyContext _Context { get; private set; }
        public FavoriteStoreClientRepository(MyContext context)
        {
            _Context = context;
        }
        public async Task AddFavorite(Guid storeId, Guid clientId)
        {
            await _Context.ClientsFavoriteStores.AddAsync(new ClientsFavoriteStores { StoreId = storeId, ClientId = clientId });
            await _Context.SaveChangesAsync();
        }

        public async Task RemoveFavorite(Guid storeId, Guid clientId)
        {
            _Context.ClientsFavoriteStores.Remove(new ClientsFavoriteStores { StoreId = storeId, ClientId = clientId });
            await _Context.SaveChangesAsync();
        }

        public async Task<List<ClientsFavoriteStores>> GetUserFavorites(Guid id)
        {
            var result = await _Context.ClientsFavoriteStores.Where(f => f.ClientId == id).ToListAsync();
            return result;
        }
    }
}
