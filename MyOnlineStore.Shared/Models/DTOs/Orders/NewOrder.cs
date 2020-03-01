using MyOnlineStore.Shared.Models.Purchases;
using System;

namespace MyOnlineStore.Shared.Models.DTOs.Orders
{
    public class NewOrder
    {
        public Guid Id { get; set; }

        public Guid MyStoreId { get; set; }
        public Guid ClientId { get; set; }

        public OrderStatus Status { get; set; } = OrderStatus.Pending;
    }
}