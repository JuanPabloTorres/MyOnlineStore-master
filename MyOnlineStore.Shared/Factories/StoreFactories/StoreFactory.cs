using MyOnlineStore.Entities.Models.Stores;
using MyOnlineStore.Shared.Interfaces.Factories;
using MyOnlineStore.Shared.Models.Stores;
using System;
using System.Collections.Generic;

namespace MyOnlineStore.Shared.Factories.StoreFactories
{
    public class StoreFactory : IStoreFactory
    {
        public Store CreateSimpleStore()
        {
            return new Store();
        }

        public Store CreateStore(string storeName, string ownerName, string description, Location location, string category, byte[] logo, string adminId, List<WorkingHour> workingHours)
        {
            return new Store
            {
                Id = Guid.NewGuid(),
                StoreName = storeName,
                StoreOwnerName = ownerName,
                Description = description,
                Location = location,
                Category = category,
                Logo = logo,
                OwnerUserId = Guid.Parse(adminId),
                WorkingHours = workingHours
            };
        }
    }
}