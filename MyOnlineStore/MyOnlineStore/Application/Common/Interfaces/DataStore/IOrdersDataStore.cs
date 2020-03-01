using MyOnlineStore.Application.Common.Interfaces.DataStore;
using MyOnlineStore.Dashboard.DashBoardModel;
using MyOnlineStore.Entities.Models.Purchases;
using MyOnlineStore.Shared.Models.DTOs.Orders;
using MyOnlineStore.Shared.Models.Purchases;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyOnlineStore.Interfaces.DataStore
{
    public interface IOrdersDataStore : IEmployeeDataStore<Order>
    {

        IEnumerable<Order> GetOrdersByDate(DateTime date);

        int GetByCompleted(DateTime date);

        IEnumerable<Order> GetOrdersFromDateToDate(DateTime from, DateTime to);

        double GetValueOfItemsPurchaseWithOrder(DateTime date);

        IEnumerable<Order> GetProgressFromDateTo(DateTime from, DateTime to);

        IEnumerable<Record> GetRecords(DateTime from, DateTime to);

        IEnumerable<OrdersProductItems> GetOrderWithItems(string orderId);
        Task<bool> CreateOrder(NewOrder order);
        Task<IEnumerable<Order>> GetClientPendingOrders(string clientId);
        Task UpdateOrder(Order order);
        Task RemoveItemFromOrder(string orderId, string productId);
    }
}
