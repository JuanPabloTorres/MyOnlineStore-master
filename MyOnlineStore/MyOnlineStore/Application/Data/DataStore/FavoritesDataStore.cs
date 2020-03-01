using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using MyOnlineStore.Application.Common.Global.Constants;
using MyOnlineStore.Application.Common.Interfaces.DataStore;
using MyOnlineStore.Entities.Models.Stores;

namespace MyOnlineStore.Application.Data.DataStore
{
    public class FavoritesDataStore : IFavoritesDataStore
    {
        public Uri UriAPI { get; private set; }
        public HttpClient HttpClient { get; private set; }
        public FavoritesDataStore(IHttpClientFactory httpClientFactory)
        {
            HttpClient = httpClientFactory.CreateClient("MyHttpClient");
        }
        public async Task<bool> AddFavorite(Guid storeId, Guid clientId)
        {
            UriAPI = new Uri($"{APIKeys.LocalBackendUrl}/{nameof(ClientsFavoriteStores)}/{storeId}/{clientId}");
            var request = new HttpRequestMessage(HttpMethod.Post, UriAPI);
            //request.Headers.Add("Content-Type", "application/json");
            var result  = await HttpClient.SendAsync(request);

            return result.IsSuccessStatusCode;
        }

        public Task<List<ClientsFavoriteStores>> GetUserFavorites(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> RemoveFavorite(Guid storeId, Guid clientId)
        {
            UriAPI = new Uri($"{APIKeys.LocalBackendUrl}/{nameof(ClientsFavoriteStores)}/{storeId}/{clientId}");
            var request = new HttpRequestMessage(HttpMethod.Delete, UriAPI);
            //request.Headers.Add("Content-Type", "application/json");
            var result = await HttpClient.SendAsync(request);

            return result.IsSuccessStatusCode;
        }
    }
}
