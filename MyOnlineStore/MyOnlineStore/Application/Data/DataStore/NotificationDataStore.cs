using MyOnlineStore.Application.Common.Interfaces.DataStore;
using MyOnlineStore.Application.Common.Utilities;
using MyOnlineStore.Shared.Models.Entries;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MyOnlineStore.Application.Data.DataStore
{
    public class NotificationDataStore : DataStoreService<JobRequest>, INotificationDataStore
    {

        public NotificationDataStore(IHttpClientFactory httpClientFactory) : base(httpClientFactory)
        {


        }
        public async Task<IEnumerable<JobRequest>> GetJobRequest(Guid UserId)
        {
            FullAPIUri = new Uri(BaseAPIUri, $"{nameof(GetJobRequest)}/{UserId}");

            var response = await HttpClient.GetStringAsync(FullAPIUri);
            var result = response;
            var deserializeObject = JsonConvert.DeserializeObject<IEnumerable<JobRequest>>(result, new JsonSerializerSettings
            {
                ContractResolver = new JsonPrivateResolver(),
                ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor
            });

            return deserializeObject ??= new List<JobRequest>();
        }
    }
}
