using MyOnlineStore.Entities.Models.Models.DTOs;
using MyOnlineStore.Shared.Models.Purchases;
using System;

namespace MyOnlineStore.Shared.Models.DTOs.Orders
{
    public class OrdersProductItemsDTO
    {
        public Guid OrderId { get; set; }
        public Guid ProductItemId { get; set; }
        public ProductItemDTO? ProductItem { get; set; }
        public uint QuantityOfItem { get; set; } = uint.MinValue;

        public OrdersProductItemsDTO()
        {
        }

        public OrdersProductItemsDTO(OrdersProductItems ordersProductItems)
        {
            OrderId = ordersProductItems.OrderId;
            ProductItemId = ordersProductItems.ProductItemId;
            ProductItem = new ProductItemDTO(ordersProductItems.ProductItem);
            QuantityOfItem = ordersProductItems.QuantityOfItem;
        }

        public OrdersProductItems ToOrdersProductsItems()
        {
            return new OrdersProductItems
            {
                OrderId = OrderId,
                ProductItem = ProductItem.ToProductItem(),
                ProductItemId = ProductItemId
            };
        }
    }
}