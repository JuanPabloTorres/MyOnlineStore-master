using MyOnlineStore.Application.Common.Interfaces.DataStore;
using MyOnlineStore.Application.Common.Utilities;
using MyOnlineStore.Entities.Models.Stores;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MyOnlineStore.Application.Data.DataStore
{
    class StoreEmployeeDataStore:DataStoreService<StoresEmployees>,IStoresEmployeesDataStore
    {

        public StoreEmployeeDataStore(IHttpClientFactory httpClientFactory) : base(httpClientFactory)
        {
        }

        public async Task< IEnumerable<StoresEmployees>> GetStoreEmployees(string storeId)
        {
            FullAPIUri = new Uri(BaseAPIUri, $"{nameof(GetStoreEmployees)}/{storeId}");

            var response = await HttpClient.GetStringAsync(FullAPIUri);
            var result = response;
            var deserializeObject = JsonConvert.DeserializeObject<IEnumerable<StoresEmployees>>(result, new JsonSerializerSettings
            {
                ContractResolver = new JsonPrivateResolver(),
                ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor
            });

            return deserializeObject ??= new List<StoresEmployees>();
        }

        public  IEnumerable<StoresEmployees> GetStoresWorkSpaceFromEmployee(string employee)
        {
            FullAPIUri = new Uri(BaseAPIUri, $"{nameof(GetStoresWorkSpaceFromEmployee)}/{employee}");

            var response =   HttpClient.GetStringAsync(FullAPIUri);
            IEnumerable<StoresEmployees> stores =
              JsonConvert.DeserializeObject<IEnumerable<StoresEmployees>>(response.GetAwaiter().GetResult());

            return stores;
        }
    }
}
