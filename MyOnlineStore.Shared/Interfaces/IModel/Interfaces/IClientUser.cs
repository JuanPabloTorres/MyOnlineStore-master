using MyOnlineStore.Entities.Models.Stores;
using System.Collections.Generic;

namespace MyOnlineStore.Entities.Models.Interfaces
{
    public interface IClientUser
    {
        List<ClientsFavoriteStores>? FavoriteStores { get; }
    }
}