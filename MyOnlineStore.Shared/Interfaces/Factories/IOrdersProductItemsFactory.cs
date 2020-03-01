using MyOnlineStore.Entities.Models.Purchases;
using MyOnlineStore.Shared.Models.Purchases;
using System;
using System.Collections.Generic;

namespace MyOnlineStore.Shared.Interfaces.Factories
{
    public interface IOrdersProductItemsFactory
    {
        OrdersProductItems CreateSimpleOrderProduct();

        OrdersProductItems CreateSimpleOrderProduct(Order order, ProductItem productItem);

        OrdersProductItems CreateSimpleOrderProduct(Guid orderId, Guid productId);

        OrdersProductItems CreateSimpleOrderProduct(Order order, ProductItem productItem, uint quantity);

        List<OrdersProductItems> CreateEmptyCart();
    }
}