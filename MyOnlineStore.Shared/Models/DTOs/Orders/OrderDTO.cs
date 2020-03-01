using MyOnlineStore.Entities.Models.DTOs.Stores;
using MyOnlineStore.Entities.Models.Purchases;
using MyOnlineStore.Shared.Models.Purchases;
using System;
using System.Collections.Generic;

namespace MyOnlineStore.Shared.Models.DTOs.Orders
{
    public class OrderDTO
    {
        public Guid Id { get; set; }
        public List<OrdersProductItemsDTO>? OrderItems { get; set; }

        public StoreDTO? MyStore { get; set; }
        public Guid MyStoreId { get; set; }
        public Guid MyClientId { get; set; }
        public DateTime OrderDate { get; set; }
        public string PurchaseBy { get; set; } = string.Empty;
        public bool PayedWithCash { get; set; }
        public OrderStatus? OrderStatus { get; set; }
        public string Status { get; private set; } = string.Empty;

        public OrderDTO()
        {
        }

        public OrderDTO(Order order)
        {
            Id = order.Id;
            MyStoreId = order.MyStoreId;
            OrderDate = order.OrderDate;
            PayedWithCash = order.PayedWithCash;
            MyClientId = order.MyClientId;
            OrderStatus = order.OrderStatus;
            Status = order.Status;

            if(order.OrderItems is object && order.OrderItems.Count > 0)
            {
                OrderItems = new List<OrdersProductItemsDTO>();
                foreach(var orderItem in order.OrderItems)
                {
                    OrderItems.Add(new OrdersProductItemsDTO(orderItem));
                }
            }
        }

        public Order ToOrder()
        {
            var order = new Order
            {
                Id = Id,
                MyStoreId = MyStoreId,
                OrderDate = OrderDate,
                MyClientId = MyClientId,
                PayedWithCash = PayedWithCash
            };

            if(order.OrderItems is object)
            {
                OrderItems = new List<OrdersProductItemsDTO>();
                OrderItems.ForEach((item) => order.OrderItems.Add(new Purchases.OrdersProductItems()));
            }

            return order;
        }
    }
}