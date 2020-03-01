using MyOnlineStore.Application.Common.Interfaces.DataStore;
using MyOnlineStore.Application.Common.Utilities;
using MyOnlineStore.Entities.Models.Stores;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace MyOnlineStore.Application.Data.DataStore
{
    public class EmployeeDataStore:DataStoreService<StoresEmployees>,IEmployeeDataStore
    {

        public EmployeeDataStore(IHttpClientFactory httpClientFactory) : base(httpClientFactory)
        {
            

        }

        public IEnumerable<StoresEmployees> GetEmployFromStore(string storeId)
        {


            FullAPIUri = new Uri(BaseAPIUri, $"{nameof(GetEmployFromStore)}/{storeId}");
            var response = HttpClient.GetStringAsync(FullAPIUri);
            IEnumerable<StoresEmployees> stores =
                JsonConvert.DeserializeObject<IEnumerable<StoresEmployees>>(response.GetAwaiter().GetResult());
            return stores;


        }
    }
}
