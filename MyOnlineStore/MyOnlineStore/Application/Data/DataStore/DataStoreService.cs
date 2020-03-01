using MyOnlineStore.Application.Common.Global.Constants;
using MyOnlineStore.Application.Common.Interfaces.DataStore;
using MyOnlineStore.Application.Common.Interfaces.Services;
using MyOnlineStore.Application.Common.Utilities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;

namespace MyOnlineStore.Application.Data.DataStore
{
    public class DataStoreService<T> : IEmployeeDataStore<T> where T : class
    {
        protected readonly HttpClient HttpClient;
        protected readonly Uri BaseAPIUri;
        protected readonly INetworkService NetworkService;
        protected Uri FullAPIUri { get; set; }
        public DataStoreService(IHttpClientFactory httpClientFactory)
        {
            HttpClient = httpClientFactory.CreateClient("MyHttpClient");
            HttpClient.Timeout = TimeSpan.FromSeconds(5);
            BaseAPIUri = new Uri($"{APIKeys.LocalBackendUrl}/{typeof(T).Name}/");
            FullAPIUri = BaseAPIUri;
        }

        public bool AddItem(T item)
        {
            var serializeObj = JsonConvert
                .SerializeObject(item,Formatting.Indented, new JsonSerializerSettings
                {
                    ContractResolver = new JsonPrivateResolver(),
                    ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor,
                    NullValueHandling = NullValueHandling.Include
                });
        
            var buffer = System.Text.Encoding.UTF8.GetBytes(serializeObj);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            var response = HttpClient.PostAsync(BaseAPIUri, byteContent);

            if (response.Result.IsSuccessStatusCode)
            {
                return true;
            }

            return false;
        }

        public bool DeleteItem(string id)
        {
            FullAPIUri = new Uri(BaseAPIUri, $"{id}");
            var response = HttpClient.DeleteAsync(FullAPIUri);

            if (response.Result.IsSuccessStatusCode)
            {
                return true;
            }
            else return false;
        }

        public T GetItem(string id)
        {
            FullAPIUri = new Uri(BaseAPIUri, $"{id}");
            var response = HttpClient.GetStringAsync(FullAPIUri);
            T deserializeObject =  JsonConvert.DeserializeObject<T>(response.Result);
            return deserializeObject;
        }

        public IEnumerable<T> GetAll(bool forceRefresh = false)
        {
            string uriString = $"{typeof(T).Name}";
            var response = HttpClient.GetStringAsync(BaseAPIUri);
            IEnumerable<T> deserializeObjects = JsonConvert.DeserializeObject<IEnumerable<T>>(response.Result);
            return deserializeObjects;
        }

        public bool UpdateItem(T item)
        {
            FullAPIUri = new Uri(BaseAPIUri,"");

            var serializeObj = JsonConvert
             .SerializeObject(item, Formatting.Indented, new JsonSerializerSettings
             {
                 ContractResolver = new JsonPrivateResolver(),
                 ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor,
                 NullValueHandling = NullValueHandling.Include
             });

            var buffer = System.Text.Encoding.UTF8.GetBytes(serializeObj);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            var response = HttpClient.PutAsync(BaseAPIUri, byteContent);

            if (response.Result.IsSuccessStatusCode)
            {
                return true;
            }

            return false;
        }
    }
}
