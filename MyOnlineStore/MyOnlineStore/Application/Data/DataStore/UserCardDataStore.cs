using MyOnlineStore.Application.Common.Interfaces.DataStore;
using MyOnlineStore.Entities.Models.Users;
using Newtonsoft.Json;
using System.Net.Http;

namespace MyOnlineStore.Application.Data.DataStore
{
    public class UserCardDataStore : DataStoreService<CardAccount>, IUserCardDataStore
    {
        public UserCardDataStore(IHttpClientFactory httpClientFactory) : base(httpClientFactory)
        {
            
        }

        public bool CardExist(CardAccount userCard)
        {
            FullAPIUri = new System.Uri(BaseAPIUri, $"{nameof(CardExist)}/{userCard.Id.ToString()}");
            HttpResponseMessage response;
            bool exist;
            
            response = HttpClient.GetAsync(FullAPIUri).Result;

            exist = JsonConvert.DeserializeObject<bool>(response.Content.ReadAsStringAsync().Result);

            return exist;
        }
    }
}
