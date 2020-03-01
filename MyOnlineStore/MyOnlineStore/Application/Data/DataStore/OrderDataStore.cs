using MyOnlineStore.Application.Data.DataStore;
using MyOnlineStore.Application.Common.Utilities;
using MyOnlineStore.Dashboard.DashBoardModel;
using MyOnlineStore.Entities.Models.Purchases;
using MyOnlineStore.Interfaces.DataStore;
using MyOnlineStore.Shared.Models.DTOs.Orders;
using MyOnlineStore.Shared.Models.Purchases;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MyOnlineStore.DataStore
{
    public class OrderDataStore : DataStoreService<Order>, IOrdersDataStore
    {

        public OrderDataStore(IHttpClientFactory httpClientFactory)  : base(httpClientFactory)
        {

        }

        public async Task<bool> CreateOrder(NewOrder newOrder)
        {
            string searializedContent;
            HttpRequestMessage request;
            ByteArrayContent byteContent;
            byte[] buffer;
            HttpResponseMessage response;

            FullAPIUri = new Uri(BaseAPIUri, $"{nameof(CreateOrder)}");

            searializedContent = JsonConvert.SerializeObject(
                value: newOrder,
                formatting: Formatting.Indented,
                settings: new JsonSerializerSettings
                {
                    ContractResolver = new JsonPrivateResolver(),
                    ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor,
                    NullValueHandling = NullValueHandling.Include
                }
            );
            request = new HttpRequestMessage(HttpMethod.Post, FullAPIUri);
            buffer = Encoding.UTF8.GetBytes(searializedContent);
            byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            request.Content = byteContent;
            response = await HttpClient.PostAsync(FullAPIUri, request.Content);

            return response.IsSuccessStatusCode;
        }

        public int GetByCompleted(DateTime date)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Order>> GetClientPendingOrders(string clientId)
        {
            FullAPIUri = new Uri(BaseAPIUri, $"{nameof(GetClientPendingOrders)}/{clientId}");

            var response = await HttpClient.GetStringAsync(FullAPIUri);
            var result = response;
            var deserializeObject = JsonConvert.DeserializeObject<IEnumerable<Order>>(result, new JsonSerializerSettings
            {
                ContractResolver = new JsonPrivateResolver(),
                ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor
            });

            return deserializeObject ??= new List<Order>();
        }

        public IEnumerable<Order> GetOrdersByDate(DateTime date)
        {
            return new List<Order>();
        }

        public IEnumerable<Order> GetOrdersFromDateToDate(DateTime from, DateTime to)
        {
            return new List<Order>();
        }

        public IEnumerable<OrdersProductItems> GetOrderWithItems(string orderId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Order> GetProgressFromDateTo(DateTime from, DateTime to)
        {
            return new List<Order>();
        }

        public IEnumerable<Record> GetRecords(DateTime from, DateTime to)
        {
            return new List<Record>();
        }

        public double GetValueOfItemsPurchaseWithOrder(DateTime date)
        {
            throw new NotImplementedException();
        }

        public async Task RemoveItemFromOrder(string orderId, string productId)
        {
            FullAPIUri = new Uri(BaseAPIUri, $"{nameof(RemoveItemFromOrder)}/{orderId}/{productId}");
            await HttpClient.GetAsync(FullAPIUri);
        }

        public async Task UpdateOrder(Order order)
        {
            FullAPIUri = new Uri(BaseAPIUri, "");

            var serializeObj = JsonConvert
             .SerializeObject(order, Formatting.Indented, new JsonSerializerSettings
             {
                 ContractResolver = new JsonPrivateResolver(),
                 ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor,
                 NullValueHandling = NullValueHandling.Include
             });

            var buffer = Encoding.UTF8.GetBytes(serializeObj);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            await HttpClient.PutAsync(BaseAPIUri, byteContent);
        }

        IEnumerable<OrdersProductItems> IOrdersDataStore.GetOrderWithItems(string orderId)
        {
            throw new NotImplementedException();
        }
    }
}
