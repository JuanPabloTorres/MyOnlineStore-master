using MyOnlineStore.Entities.Models.Stores;
using System.Collections.Generic;

namespace MyOnlineStore.Entities.Models.Interfaces
{
    public interface IAdminUser : IEmployeeUser, ICardable
    {
        List<Store>? MyStores { get; }
    }
}