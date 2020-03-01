using MyOnlineStore.Application.Common.Interfaces.DataStore;
using MyOnlineStore.Entities.Models.Stores;
using MyOnlineStore.Application.Common.Utilities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using MyOnlineStore.Shared.Models.DTOs.Stores;
using MyOnlineStore.Application.Data.Models.Presenters;
using MyOnlineStore.Entities.Models.Users;
using MyOnlineStore.Shared.Models.Entries;
using MyOnlineStore.Shared.Utils;
using Polly;
using MyOnlineStore.Shared.Models.Stores;

namespace MyOnlineStore.Application.Data.DataStore
{
    public class StoreDataStore : DataStoreService<Store>, IStoreDataStore
    {
        public StoreDataStore(IHttpClientFactory httpClientFactory) : base(httpClientFactory)
        {
        }

        public Store GetByName(string name)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<StorePresenter> GetClientFavorites(string clientId)
        {
            FullAPIUri = new Uri(BaseAPIUri, $"{nameof(GetClientFavorites)}/{clientId}") ;
            var response = HttpClient.GetStringAsync(FullAPIUri);
            var stores = JsonConvert.DeserializeObject<IEnumerable<StorePresenter>>(response.Result);
            return stores;
        }

        public bool StoreExists(string storeName, string storeOwnerName, Location location)
        {
            bool exists = false;
            FullAPIUri = new Uri(BaseAPIUri, $"{nameof(StoreExists)}/{storeOwnerName}/{storeName}");
            var serializeObj = JsonConvert
               .SerializeObject(location, Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Include });

            var buffer = Encoding.UTF8.GetBytes(serializeObj);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            var request = new HttpRequestMessage(HttpMethod.Get, FullAPIUri);
            request.Content = byteContent;
            
            var response = HttpClient.SendAsync(request); /*Task.Run(async()=> await HttpClient.GetStringAsync(urisString));*/

            try
            {
                if (response.Result.IsSuccessStatusCode 
                    && response.Result.Content.ReadAsStringAsync().GetAwaiter().GetResult().Contains("true"))
                {
                    exists = true;
                }
                else
                    exists = false;
            }
            catch (Exception)
            {
                exists = false;
            }
            

            return exists;
        }

        public Store? GetStore(string storeName, string storeOwner, Location location )
        {
            Store? store = null;

            FullAPIUri = new Uri(BaseAPIUri, $"{nameof(GetStore)}/{storeOwner}/{storeName}");
            
            var serializeObj = JsonConvert
               .SerializeObject(location, Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Include });

            var buffer = Encoding.UTF8.GetBytes(serializeObj);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            var request = new HttpRequestMessage(HttpMethod.Get, FullAPIUri);
            request.Content = byteContent;

            var response = HttpClient.SendAsync(request); /*Task.Run(async()=> await HttpClient.GetStringAsync(uriString));*/

            if (response.GetAwaiter().GetResult().IsSuccessStatusCode)
            {
                store = JsonConvert.DeserializeObject<Store>(response.GetAwaiter().GetResult()
               .Content.ReadAsStringAsync().GetAwaiter().GetResult());
            }

            return store;
        }

        public Store AddStoreAndGet(Store store)
        {
            return new Store();
        }

        public IEnumerable<Store>? GetStoresByOwnerId(string userid)
        {
            FullAPIUri = new Uri(BaseAPIUri, $"{nameof(GetStoresByOwnerId)}/{userid}");

            var response = HttpClient.GetStringAsync(FullAPIUri);

            var stores = JsonConvert.DeserializeObject<IEnumerable<Store>>(response.Result, new JsonSerializerSettings
            {
                ContractResolver = new JsonPrivateResolver(),
                ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor
            });

            return stores;
        }

        public Store GetStoreWithInventory(string storeId)
        {
            FullAPIUri = new Uri(BaseAPIUri, $"{nameof(GetStoreWithInventory)}/{storeId}");
            var response = HttpClient.GetStringAsync(FullAPIUri);
            var store = JsonConvert.DeserializeObject<Store>(response.Result);
            return store;
        }

        public bool AddFeaturedItem(IList<StoreFeaturedItem> featuredItems)
        {
            FullAPIUri = new Uri(BaseAPIUri, $"{nameof(AddFeaturedItem)}");
            List<string> serializedList = new List<string>(); ;

            var serializeObj = JsonConvert
                .SerializeObject(featuredItems, Formatting.Indented, new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Include,

                });

            // var stringSerializedList = serializedList.ToArray().ToString();
            var buffer = Encoding.UTF8.GetBytes(serializeObj);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            var request = new HttpRequestMessage(HttpMethod.Post, FullAPIUri);
            request.Content = byteContent;

            var response = HttpClient.SendAsync(request);

            if (response.GetAwaiter().GetResult().IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<bool>(response.GetAwaiter().GetResult().Content.ReadAsStringAsync().GetAwaiter().GetResult());
            }
            else
                return false;
        }

        public IEnumerable<StorePresenter> GetAllToExplore(string id)
        {
            FullAPIUri = new Uri(BaseAPIUri, $"{nameof(GetAllToExplore)}/{id}");
            var response = HttpClient.GetStringAsync(FullAPIUri);
            var result = response.Result;
            var store = JsonConvert.DeserializeObject<List<StorePresenter>>(result);
            return store;
        }

        public bool IsFavorite(string storeId, string clientId)
        {
            FullAPIUri = new Uri(BaseAPIUri, $"{nameof(IsFavorite)}/{storeId}/{clientId}");
            var response = HttpClient.GetStringAsync(FullAPIUri);
            var result = response.Result;
            var isFav = JsonConvert.DeserializeObject<bool>(result);
            return isFav;
        }

        public ApiResponse<User> RegisterStoreAction(IStoreRegistrationEntry storeRegistrationEntry)
        {
            ApiResponse<User> apiResponse = new ApiResponse<User>();
            HttpResponseMessage httpResponse;

            FullAPIUri = new Uri(BaseAPIUri, $"{nameof(RegisterStoreAction)}");

            string serializedObjString = JsonConvert.SerializeObject(storeRegistrationEntry);

            byte[] bytes = Encoding.UTF8.GetBytes(serializedObjString);
            ByteArrayContent byteContent = new ByteArrayContent(bytes);
            byteContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");


            Policy.Handle<Exception>()
                //.WaitAndRetryForever
                .Retry(
                    retryCount: 0,
                    //sleepDurationProvider: retryAttemp => TimeSpan.FromSeconds(Math.Pow(1, retryAttemp)),
                    onRetry: (exception, /*timeSpan,*/ retryCount, context) =>
                    {
                        // Add logic to be executed before each retry, such as logging
                        System.Diagnostics.Debug.WriteLine($"Retry #{retryCount} due to exception '{(exception.InnerException ?? exception).Message}'");
                        HttpClient.CancelPendingRequests();

                        apiResponse = new ApiResponse<User>();
                        apiResponse.Failure.ErrorsMessages.Add("Request was unsucessfull.");
                        apiResponse.IsSuccess = false;
                    }
                ).ExecuteAndCapture(() =>
                {
                    httpResponse = HttpClient.PostAsync(FullAPIUri, byteContent).Result;

                    if(httpResponse.IsSuccessStatusCode)
                    {
                        apiResponse = JsonConvert.DeserializeObject<ApiResponse<User>>(httpResponse.Content.ReadAsStringAsync().Result);
                    }
                    else
                    {
                        apiResponse.Failure.ErrorsMessages.Add("Request was unsucessfull.");
                        apiResponse.IsSuccess = false;
                    }
                });

            

            return apiResponse;

        }

        public IEnumerable<Store> GetClientFavorite(Guid clientId)
        {
            FullAPIUri = new Uri(BaseAPIUri, $"{nameof(GetClientFavorite)}/{clientId}");
            var response = HttpClient.GetStringAsync(FullAPIUri);
            IEnumerable<Store> stores =
                JsonConvert.DeserializeObject<IEnumerable<Store>>(response.GetAwaiter().GetResult());
            return stores;
        }


        public bool RemoveFeatureItem(StoreFeaturedItem featuredItems)
        {
            FullAPIUri = new Uri(BaseAPIUri, $"{nameof(RemoveFeatureItem)}");



            var serializeObj = JsonConvert
                .SerializeObject(featuredItems, Formatting.Indented, new JsonSerializerSettings
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

        public bool RemoveFeatureItems(IList<StoreFeaturedItem> featuredItems)
        {
            FullAPIUri = new Uri(BaseAPIUri, $"{nameof(RemoveFeatureItems)}");
            List<string> serializedList = new List<string>(); ;

            //foreach (var item in featuredItems)
            //{
            //   var serializeObj = JsonConvert
            //   .SerializeObject(item, Formatting.Indented, new JsonSerializerSettings
            //   {
            //       NullValueHandling = NullValueHandling.Include,

            //   });
            //    serializedList.Add(serializeObj);

            //}
            var serializeObj = JsonConvert
                .SerializeObject(featuredItems, Formatting.Indented, new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Include,

                });

            // var stringSerializedList = serializedList.ToArray().ToString();
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

        public bool UpdateFeatureItem(StoreFeaturedItem featuredItems)
        {

            FullAPIUri = new Uri(BaseAPIUri, $"{nameof(UpdateFeatureItem)}");


            var serializeObj = JsonConvert
                .SerializeObject(featuredItems, Formatting.Indented, new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Include,

                });


            var buffer = Encoding.UTF8.GetBytes(serializeObj);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            var request = new HttpRequestMessage(HttpMethod.Put, FullAPIUri);
            request.Content = byteContent;

            var response = HttpClient.SendAsync(request);

            if (response.GetAwaiter().GetResult().IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<bool>(response.GetAwaiter().GetResult().Content.ReadAsStringAsync().GetAwaiter().GetResult());
            }
            else
                return false;
        }

        public bool UpdateFeatureItems(IList<StoreFeaturedItem> featuredItems)
        {

            FullAPIUri = new Uri(BaseAPIUri, $"{nameof(UpdateFeatureItems)}");


            var serializeObj = JsonConvert
                .SerializeObject(featuredItems, Formatting.Indented, new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Include,

                });


            var buffer = Encoding.UTF8.GetBytes(serializeObj);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            var request = new HttpRequestMessage(HttpMethod.Put, FullAPIUri);
            request.Content = byteContent;

            var response = HttpClient.SendAsync(request);

            if (response.GetAwaiter().GetResult().IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<bool>(response.GetAwaiter().GetResult().Content.ReadAsStringAsync().GetAwaiter().GetResult());
            }
            else
                return false;
        }

        public IEnumerable<StoreFeaturedItem> GetFeaturedItemsOfStore(Guid storeId)
        {
            FullAPIUri = new Uri(BaseAPIUri, $"{nameof(GetFeaturedItemsOfStore)}/{storeId}");
            var response = HttpClient.GetStringAsync(FullAPIUri);
            IEnumerable<StoreFeaturedItem> stores =
                JsonConvert.DeserializeObject<IEnumerable<StoreFeaturedItem>>(response.GetAwaiter().GetResult());
            return stores;
        }

        public bool RemoveEmployee(string userId, string storeId)
        {
            FullAPIUri = new Uri(BaseAPIUri, $"{nameof(RemoveEmployee)}/{userId}/{storeId}");

           
            var response = HttpClient.DeleteAsync(FullAPIUri);



            //return response.IsCompletedSuccessfully;

            if(response.GetAwaiter().GetResult().IsSuccessStatusCode)
            {

                 return  JsonConvert.DeserializeObject<bool>(response.GetAwaiter().GetResult().Content.ReadAsStringAsync().GetAwaiter().GetResult());
            }
            else
            {
                return false;
            }




        }

        public bool AddEmployeeWorkHours(List<EmployeeWorkHour> employeeWorkHours)
        {
            FullAPIUri = new Uri(BaseAPIUri, $"{nameof(AddEmployeeWorkHours)}");
            List<string> serializedList = new List<string>(); ;

            var serializeObj = JsonConvert
                .SerializeObject(employeeWorkHours, Formatting.Indented, new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Include,

                });

            // var stringSerializedList = serializedList.ToArray().ToString();
            var buffer = Encoding.UTF8.GetBytes(serializeObj);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            var request = new HttpRequestMessage(HttpMethod.Put, FullAPIUri);
            request.Content = byteContent;

            var response = HttpClient.SendAsync(request);

            if (response.GetAwaiter().GetResult().IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<bool>(response.GetAwaiter().GetResult().Content.ReadAsStringAsync().GetAwaiter().GetResult());
            }
            else
                return false;
        }

        public bool UpdateEmployee(StoresEmployees updatedEmployee)
        {

            FullAPIUri = new Uri(BaseAPIUri, $"{nameof(UpdateEmployee)}");


            var serializeObj = JsonConvert
                .SerializeObject(updatedEmployee, Formatting.Indented, new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Include,

                });


            var buffer = Encoding.UTF8.GetBytes(serializeObj);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            var request = new HttpRequestMessage(HttpMethod.Put, FullAPIUri);
            request.Content = byteContent;

            var response = HttpClient.SendAsync(request);

            if (response.GetAwaiter().GetResult().IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<bool>(response.GetAwaiter().GetResult().Content.ReadAsStringAsync().GetAwaiter().GetResult());
            }
            else
                return false;
        }

        public StoresEmployees GetStoreEmployee(string employeeId, string storeId)
        {
            StoresEmployees? store = null;

            FullAPIUri = new Uri(BaseAPIUri, $"{nameof(GetStoreEmployee)}/{employeeId}/{storeId}");

            //var serializeObj = JsonConvert
            //   .SerializeObject(location, Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Include });

            //var buffer = Encoding.UTF8.GetBytes(serializeObj);
            //var byteContent = new ByteArrayContent(buffer);
            //byteContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            var request = new HttpRequestMessage(HttpMethod.Get, FullAPIUri);
           // request.Content = byteContent;

            var response = HttpClient.SendAsync(request); /*Task.Run(async()=> await HttpClient.GetStringAsync(uriString));*/

            if (response.GetAwaiter().GetResult().IsSuccessStatusCode)
            {
                store = JsonConvert.DeserializeObject<StoresEmployees>(response.GetAwaiter().GetResult()
               .Content.ReadAsStringAsync().GetAwaiter().GetResult());
            }

            return store;
        }
       

        public IEnumerable<EmployeeWorkHour> GetEmployeeWorkHoursOfStore(string employeeId, string storeId)
        {
            FullAPIUri = new Uri(BaseAPIUri, $"{nameof(GetEmployeeWorkHoursOfStore)}/{employeeId}/{storeId}");
            var response = HttpClient.GetStringAsync(FullAPIUri);
            IEnumerable<EmployeeWorkHour> stores =
                JsonConvert.DeserializeObject<IEnumerable<EmployeeWorkHour>>(response.GetAwaiter().GetResult());
            return stores;
        }

        public IEnumerable<EmployeeWorkHour> GetEmployeeWorkHours(string employeeId)
        {
            throw new NotImplementedException();
        }

        public bool UpdateFeature(StoreFeaturedItem featuredItems)
        {
            throw new NotImplementedException();
        }
    }
}
