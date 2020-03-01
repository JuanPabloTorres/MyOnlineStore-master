using MyOnlineStore.Entities.Models.Stores;
using MyOnlineStore.Shared.Models.Stores;
using System;
using System.Collections.Generic;

namespace MyOnlineStore.Shared.Interfaces.Factories
{
    public interface IStoreFactory
    {
        Store CreateSimpleStore();

        Store CreateStore(string storeName, string ownerName, string description, Location location, string category, byte[] logo, string adminId, List<WorkingHour> workingHours);
    }
}