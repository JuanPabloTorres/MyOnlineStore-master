using MyOnlineStore.Entities.Models.Purchases;
using MyOnlineStore.Entities.Models.Stores;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyOnlineStore.Application.Common.Interfaces.IViewModel
{
    public interface IStoreHomeViewModel
    {
        static Dictionary<Store, Order> StoresOrders { get; }
        static Store CurrentShoppingStore { get; }
    }
}
