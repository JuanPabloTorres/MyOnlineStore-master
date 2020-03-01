using MyOnlineStore.Entities.Models.Purchases;
using MyOnlineStore.Entities.Models.Stores;
using MyOnlineStore.Entities.Models.Users;
using MyOnlineStore.Shared.Models.DTOs.Orders;
using System;

namespace MyOnlineStore.Shared.Interfaces.Factories
{
    public interface IOrderFactory
    {
        Order CreateSimpleOrder();

        Order CreateSimpleOrder(Order order);

        Order CreateOrderForStore(Store store, User clientUser, params ProductItem[] productItems);

        Order CreateOrderForStore(Store store, User clientUser);

        Order CreateOrderFromNewOrder(NewOrder newOrder);

        NewOrder CreatNewOrder(Order order);

        NewOrder CreatNewOrder(Guid orderId, Guid storeId, Guid clientId);
    }
}