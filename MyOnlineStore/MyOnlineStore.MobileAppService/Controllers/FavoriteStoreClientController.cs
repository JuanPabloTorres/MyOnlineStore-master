using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyOnlineStore.Entities.Models.Stores;
using MyOnlineStore.MobileAppService.Interfaces.Repositories;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MyOnlineStore.MobileAppService.Controllers
{
    [Route("api/[controller]")]
    public class FavoriteStoreClientController : Controller
    {
        public IFavoriteStoreClientRepository _FavoriteStoreClientRepository { get; private set; }
        public FavoriteStoreClientController(IFavoriteStoreClientRepository favoriteStoreClientRepository)
        {
            _FavoriteStoreClientRepository = favoriteStoreClientRepository;
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<List<ClientsFavoriteStores>> Get(string id)
        {
            Guid userId;
            List<ClientsFavoriteStores> favorites;

            if (Guid.TryParse(id,out userId))
            {
                favorites = await _FavoriteStoreClientRepository.GetUserFavorites(userId);
            }
            else
            {
                favorites = new List<ClientsFavoriteStores>();
            }

            return favorites;
        }

        // POST api/<controller>
        [HttpPost("{storeId}/{userId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task CreateFavorite([FromRoute]string storeId,[FromRoute]string userId)
        {
            Guid guidstoreId, guiduserId;

            if (Guid.TryParse(storeId, out guidstoreId) && Guid.TryParse(userId, out guiduserId))
            {
                await _FavoriteStoreClientRepository.AddFavorite(guidstoreId, guiduserId);
            }
        }

        // DELETE api/<controller>/5
        [HttpDelete("{storeId}/{userId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task UnFavorite([FromRoute]string storeId, [FromRoute]string userId)
        {
            Guid guidstoreId, guiduserId;

            if (Guid.TryParse(storeId, out guidstoreId) && Guid.TryParse(userId, out guiduserId))
            {
                await _FavoriteStoreClientRepository.RemoveFavorite(guidstoreId, guiduserId);
            }
        }
    }
}
