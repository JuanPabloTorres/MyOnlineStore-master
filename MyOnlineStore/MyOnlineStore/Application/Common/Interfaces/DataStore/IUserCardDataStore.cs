using MyOnlineStore.Entities.Models.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyOnlineStore.Application.Common.Interfaces.DataStore
{
    public interface IUserCardDataStore : IEmployeeDataStore<CardAccount>
    {
        bool CardExist(CardAccount userCard);
    }
}
