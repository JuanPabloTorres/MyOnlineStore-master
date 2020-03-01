using MyOnlineStore.Application.Common.Interfaces.DataStore;
using MyOnlineStore.Entities.Models.Roles;
using Newtonsoft.Json;
using System;
using System.Net.Http;

namespace MyOnlineStore.Application.Data.DataStore
{
    public class RoleDataStore : DataStoreService<Role>, IRoleDataStore
    {
        public RoleDataStore(IHttpClientFactory httpClientFactory)
            : base(httpClientFactory)
        {

        }

        //public RoleType GetByRoleName(string name)
        //{
        //    FullAPIUri = new Uri(BaseAPIUri, $"{name}");
        //    var response = HttpClient.GetStringAsync(FullAPIUri);
        //    var deserializeObject = JsonConvert.DeserializeObject<RoleType>(response.Result);

        //    return deserializeObject;
        //}
    }
}
