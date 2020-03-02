using MyOnlineStore.Entities.Models.Purchases;
using MyOnlineStore.Entities.Models.Stores;
using MyOnlineStore.Entities.Models.Users;
using MyOnlineStore.Shared.Interfaces.Factories;
using MyOnlineStore.Shared.Models.DTOs.Orders;
using MyOnlineStore.Shared.Models.Purchases;
using System;
using System.Collections.Generic;

namespace MyOnlineStore.Shared.Factories.OrderFactory
{
    public class OrderFactory : IOrderFactory
    {
        #region Methods

        public Order CreateOrderForStore(Store store, User clientUser, params ProductItem[] productItems)
        {
            Order order;

            if(store.Id != Guid.Empty)
            {
                order = new Order
                {
                    MyStoreId = store.Id,
                    OrderDate = DateTime.Today,
                    Id = Guid.NewGuid(),
                    MyClientId = clientUser.Id,
                };
            }
            else
            {
                order = new Order();
            }

            order.OrderStatus = OrderStatus.Pending;
            return order;
        }

        public Order CreateOrderForStore(Store store, User clientUser)
        {
            Order order;

            if(store.Id != Guid.Empty || !string.IsNullOrEmpty(store.Id.ToString()))
            {
                order = new Order
                {
                    MyStoreId = store.Id,
                    OrderDate = DateTime.Today,
                    Id = Guid.NewGuid(),
                    MyClientId = clientUser.Id,
                };
            }
            else
            {
                order = new Order();
            }

            order.OrderStatus = OrderStatus.Pending;
            order.OrderItems = new List<OrdersProductItems>();
            return order;
        }

        public Order CreateOrderFromNewOrder(NewOrder newOrder)
        {
            return new Order
            {
                Id = newOrder.Id,
                MyStoreId = newOrder.MyStoreId,
                OrderStatus = newOrder.Status,
                MyClientId = newOrder.ClientId
            };
        }

        public Order CreateSimpleOrder()
        {
            Order order;

            order = new Order
            {
                Id = Guid.NewGuid(),
                OrderStatus = OrderStatus.Pending
            };

            return order;
        }

        public Order CreateSimpleOrder(Order order)
        {
            if(order.Id == Guid.Empty)
            {
                order.Id = Guid.NewGuid();
                order.OrderStatus = OrderStatus.Pending;
                order.MyClientId = order.MyClientId;
            }

            return order;
        }

        public NewOrder CreatNewOrder(Order order)
        {
            return new NewOrder
            {
                Id = order.Id,
                MyStoreId = order.MyStoreId,
                Status = OrderStatus.Pending,
                ClientId = order.MyClientId
            };
        }

        public NewOrder CreatNewOrder(Guid orderId, Guid storeId, Guid clientId)
        {
            return new NewOrder
            {
                Id = orderId,
                MyStoreId = storeId,
                Status = OrderStatus.Pending,
                ClientId = clientId,
            };
        }

        #endregion Methods
    }
}