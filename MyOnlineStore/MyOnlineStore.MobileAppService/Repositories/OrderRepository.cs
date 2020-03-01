using MyOnlineStore.MobileAppService.Context;
using MyOnlineStore.MobileAppService.Interfaces.Repositories;
using MyOnlineStore.Entities.Models.Purchases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MyOnlineStore.Shared.Models.Purchases;
using Microsoft.EntityFrameworkCore;

namespace MyOnlineStore.MobileAppService.Repositories
{
    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        public OrderRepository(MyContext context) : base(context)
        {

        }

        public void CreateOrderWithProucts(IEnumerable<OrdersProductItems> orderItem)
        {
            try
            {
                _Context.OrdersProductItems.AddRange(orderItem);
                Save();
            }
            catch (Exception ex)
            {

            }
           
        }

        public async Task<IEnumerable<Order>> GetClientPendingOrdersAsync(Guid clientId)
        {
            //var result = await _Context.Orders.Where(
            //    o => o.OrderItems.Select(
            //        orderItem => orderItem.ProductItemId).Contains(clientId) 
            //        && o.OrderStatus.NameOfStatus == OrderStatus.Pending.NameOfStatus
            //).Include(o => o.OrderItems).ToListAsync();

            var result = await _Context.Orders.Where(
                    o => o.MyClientId == clientId && o.Status == OrderStatus.Pending.NameOfStatus
                )
                .Include(o => o.OrderItems)
                    .ThenInclude(oi=>oi.ProductItem)
                .Include(o=>o.OrderStatus)
                .ToListAsync();

            return result;
        }

        public async Task RemoveItemFromOrder(Guid orderId, Guid productId)
        {
            var item = await _Context.OrdersProductItems
                .Where(op => 
                op.ProductItemId == productId 
                && op.OrderId == orderId).FirstOrDefaultAsync();
            _Context.OrdersProductItems.Remove(item);
        }

        public async Task<bool> UpdateOrder(Order order)
        {
            bool saved = false;

            var existingOrder = _Context.Orders
                .Where(o => o.Id == order.Id)
                .Include(o => o.OrderItems)
                .Include(o => o.OrderStatus)
                .SingleOrDefault();
            try
            {
                if (existingOrder != null)
                {
                    // Update Order
                    _Context.Entry(existingOrder).CurrentValues.SetValues(order);

                    // Delete OrderItems
                    foreach (var orderItem in existingOrder.OrderItems.ToList())
                    {
                        if (!order.OrderItems.Any(c => c.OrderId == orderItem.OrderId))
                            _Context.OrdersProductItems.Remove(orderItem);
                    }

                    // Update and Insert OrderItems
                    foreach (var newOrderItem in order.OrderItems)
                    {
                        var orderItem = existingOrder.OrderItems
                            .Where(c => c.OrderId == newOrderItem.OrderId && c.ProductItemId == newOrderItem.ProductItemId)
                            .SingleOrDefault();

                        if (orderItem != null)
                            // Update order items
                            _Context.Entry(orderItem).CurrentValues.SetValues(newOrderItem);
                        else
                        {
                            existingOrder.OrderItems.Add(newOrderItem);
                        }
                    }

                    saved = await SaveAsync();
                }
            }
            catch (Exception ex)
            {
                saved = false;
            }

            return saved;
        }
    }
}
