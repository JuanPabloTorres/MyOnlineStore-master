using MyOnlineStore.MobileAppService.Context;
using MyOnlineStore.MobileAppService.Interfaces.Repositories;
using MyOnlineStore.Entities.Models.Users;
using System;
using System.Linq;

namespace MyOnlineStore.MobileAppService.Repositories
{
    public class UserCardRepository : Repository<CardAccount>, IUserCardRepository
    {
        public UserCardRepository(MyContext context) : base(context)
        {
            
        }

        public bool CardExist(Guid cardId)
        {
            bool exist;
            var card =_Context.CardAccounts.Where(c => c.Id == cardId).FirstOrDefault();

            if (card is null) 
                { exist = false;}
            else 
                { exist = true; }

            return exist;
        }
    }
}
