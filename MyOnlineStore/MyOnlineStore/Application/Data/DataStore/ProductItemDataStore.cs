using MyOnlineStore.Application.Common.Interfaces.DataStore;
using MyOnlineStore.Entities.Models.Purchases;
using MyOnlineStore.Shared.Models.Purchases;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace MyOnlineStore.Application.Data.DataStore
{
    public class ProductItemDataStore : DataStoreService<ProductItem>, IProductItemDataStore
    {
        public ProductItemDataStore(IHttpClientFactory httpClientFactory) 
            : base(httpClientFactory)
        {
        }

        public IEnumerable<ProductItem> GetInventoryForStore(string storeId)
        {
            FullAPIUri = new Uri(BaseAPIUri, $"{nameof(GetInventoryForStore)}/{storeId}");
            var response = HttpClient.GetStringAsync(FullAPIUri);
            var deserializedObj = JsonConvert.DeserializeObject<IEnumerable<ProductItem>>(response.Result);
            return deserializedObj;
        }

        public bool InsertOffer(Offer offer)
        {
           


            bool exists = false;
            FullAPIUri = new Uri(BaseAPIUri, $"{nameof(InsertOffer)}");
            var serializeObj = JsonConvert
               .SerializeObject(offer, Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Include });

            var buffer = Encoding.UTF8.GetBytes(serializeObj);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            var request = new HttpRequestMessage(HttpMethod.Put, FullAPIUri);
            request.Content = byteContent;

            var response = HttpClient.SendAsync(request); /*Task.Run(async()=> await HttpClient.GetStringAsync(urisString));*/

            if (response.GetAwaiter().GetResult().IsSuccessStatusCode)
            {

                return JsonConvert.DeserializeObject<bool>(response.GetAwaiter().GetResult().Content.ReadAsStringAsync().GetAwaiter().GetResult());
            }
            else
            {
                return false;
            }
            // FullAPIUri = new Uri(BaseAPIUri, $"{nameof(InsertOffer)}/{id}/{offer}");
            //var response = HttpClient.GetStringAsync(FullAPIUri);
            //var serializeobj= JsonConvert.SerializeObject(offer);


        }

        public ProductItem GetItemByNameFromInventory(string storeId, string productName)
        {
            FullAPIUri = new Uri(BaseAPIUri, $"{nameof(GetItemByNameFromInventory)}/{storeId}/{productName}");
            var response = HttpClient.GetStringAsync(FullAPIUri);
            var deserializedObj = JsonConvert.DeserializeObject<ProductItem>(response.Result);
            return deserializedObj;
        }

        public IEnumerable<Offer> GetOfferOfStore(string storeId)
        {
            FullAPIUri = new Uri(BaseAPIUri, $"{nameof(GetOfferOfStore)}/{storeId}");
            var response = HttpClient.GetStringAsync(FullAPIUri);
            var deserializedObj = JsonConvert.DeserializeObject<IEnumerable<Offer>>(response.Result);
            return deserializedObj;
        }

        public bool RemoveOffert(Offer offer)
        {
            FullAPIUri = new Uri(BaseAPIUri, $"{nameof(RemoveOffert)}");



            var serializeObj = JsonConvert
                .SerializeObject(offer, Formatting.Indented, new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Include,

                });


            var buffer = Encoding.UTF8.GetBytes(serializeObj);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            var request = new HttpRequestMessage(HttpMethod.Delete, FullAPIUri);
            request.Content = byteContent;

            var response = HttpClient.SendAsync(request);

            if (response.GetAwaiter().GetResult().IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<bool>(response.GetAwaiter().GetResult().Content.ReadAsStringAsync().GetAwaiter().GetResult());
            }
            else
                return false;
        }

        public IEnumerable<ProductItem> GetProductItemsWithQuantity(int quantity, string storeId)
        {
            FullAPIUri = new Uri(BaseAPIUri, $"{nameof(GetProductItemsWithQuantity)}/{quantity}/{storeId}");
            var response = HttpClient.GetStringAsync(FullAPIUri);
            var deserializedObj = JsonConvert.DeserializeObject<IEnumerable<ProductItem>>(response.Result);
            return deserializedObj;
        }

        public bool ProductHasOffer(string productId, string storeId)
        {
            FullAPIUri = new Uri(BaseAPIUri, $"{nameof(ProductHasOffer)}/{productId}/{storeId}");
            var response = HttpClient.GetStringAsync(FullAPIUri);
            var deserializedObj = JsonConvert.DeserializeObject<bool>(response.Result);
            return deserializedObj;
        }
    }
}
