using MyOnlineStore.Entities.Models.Purchases;
using MyOnlineStore.Shared.Interfaces.Factories;
using MyOnlineStore.Shared.Models.Purchases;
using System;
using System.Collections.Generic;

namespace MyOnlineStore.Shared.Factories.OrderFactory
{
    public class OrdersProductItemsFactory : IOrdersProductItemsFactory
    {
        public List<OrdersProductItems> CreateEmptyCart()
        {
            return new List<OrdersProductItems>();
        }

        public OrdersProductItems CreateSimpleOrderProduct()
        {
            return new OrdersProductItems();
        }

        public OrdersProductItems CreateSimpleOrderProduct(Order order, ProductItem productItem)
        {
            return new OrdersProductItems
            {
                OrderId = order.Id,
                ProductItem = productItem,
                ProductItemId = productItem.Id
            };
        }

        public OrdersProductItems CreateSimpleOrderProduct(Guid orderId, Guid productId)
        {
            return new OrdersProductItems { OrderId = orderId, ProductItemId = productId };
        }

        public OrdersProductItems CreateSimpleOrderProduct(Order order, ProductItem productItem, uint quantity)
        {
            return new OrdersProductItems
            {
                OrderId = order.Id,
                ProductItem = productItem,
                ProductItemId = productItem.Id,
                QuantityOfItem = quantity
            };
        }
    }
}