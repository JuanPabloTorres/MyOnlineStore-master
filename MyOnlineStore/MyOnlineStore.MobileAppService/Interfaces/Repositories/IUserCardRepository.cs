using MyOnlineStore.Entities.Models.Users;
using System;

namespace MyOnlineStore.MobileAppService.Interfaces.Repositories
{
    public interface IUserCardRepository : IRepository<CardAccount>
    {
        bool CardExist(Guid cardId);
    }
}
