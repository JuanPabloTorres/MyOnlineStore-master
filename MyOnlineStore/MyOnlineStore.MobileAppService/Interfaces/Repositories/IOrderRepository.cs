using MyOnlineStore.Entities.Models.Purchases;
using MyOnlineStore.Shared.Models.Purchases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyOnlineStore.MobileAppService.Interfaces.Repositories
{
    public interface IOrderRepository : IRepository<Order>
    {
        void CreateOrderWithProucts(IEnumerable<OrdersProductItems> orderItem);
        Task<IEnumerable<Order>> GetClientPendingOrdersAsync(Guid clientId);
        Task<bool> UpdateOrder(Order order);
        Task RemoveItemFromOrder(Guid orderId, Guid productId);
    }
}
