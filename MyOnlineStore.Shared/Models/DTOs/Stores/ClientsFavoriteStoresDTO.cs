using MyOnlineStore.Entities.Models.DTOs.Stores;
using MyOnlineStore.Entities.Models.Stores;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyOnlineStore.Shared.Models.DTOs.Stores
{
    public class ClientsFavoriteStoresDTO
    {
        public Guid ClientId { get; set; }

        public Guid StoreId { get; set; }
        public StoreDTO? Store { get; set; }

        public bool IsAlive { get; set; }

        public Guid Id { get; set; }
        public ClientsFavoriteStoresDTO(ClientsFavoriteStores clientsFavoriteStores)
        {
            ClientId = clientsFavoriteStores.ClientId;
            Id = clientsFavoriteStores.Id;
            StoreId = clientsFavoriteStores.StoreId;

            if(clientsFavoriteStores.Store is object) 
            { Store = new StoreDTO(clientsFavoriteStores.Store); }

            
        }
    }
}
