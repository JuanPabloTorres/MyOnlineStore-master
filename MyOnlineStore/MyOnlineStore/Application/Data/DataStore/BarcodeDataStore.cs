using MyOnlineStore.Entities.Models.Purchases;
using Newtonsoft.Json;
using System.Net.Http;

namespace MyOnlineStore.Application.Data.DataStore
{
    public class BarcodeDataStore
    {

        protected readonly HttpClient HttpClient;

        public BarcodeDataStore()
        {
            HttpClient = new HttpClient();
        }
        public ProductTest GetItem(string id)
        {
            var response = HttpClient.GetStringAsync("https://api.upcitemdb.com/prod/trial/lookup?upc=" +id);
            ProductTest deserializeObject = JsonConvert.DeserializeObject<ProductTest>(response.Result);
            return deserializeObject;
        }


    }
}
