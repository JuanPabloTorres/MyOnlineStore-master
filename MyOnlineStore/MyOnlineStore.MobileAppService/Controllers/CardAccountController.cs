using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyOnlineStore.MobileAppService.Interfaces.Repositories;
using MyOnlineStore.Entities.Models.Users;
using System;

namespace MyOnlineStore.MobileAppService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CardAccountController : ControllerBase
    {
        private readonly IUserCardRepository UserCardRepo;
        public CardAccountController(IUserCardRepository userCardRepository)
        {
            UserCardRepo = userCardRepository;
        }

        // GET: api/UserCard/5
        [HttpGet("{id}")]
        public string Get(string userId)
        {
            return "value";
        }

        [HttpGet("[action]/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public bool CardExist(string id)
        {
            Guid cardId;
            bool exist = false;

            if (Guid.TryParse(id,out cardId))
            {
               exist =  UserCardRepo.CardExist(cardId);
            }

            return exist;
        }

        // POST: api/UserCard
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public bool Post([FromBody] CardAccount userCard)
        {
            bool added = UserCardRepo.Add(userCard);

            return added;
        }

        // PUT: api/UserCard/5
        [HttpPut]
        public void Put([FromBody]CardAccount userCard)
        {
            UserCardRepo.Update(userCard);
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(string cardId)
        {
        }
    }
}
