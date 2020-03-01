using MyOnlineStore.Entities.Models.Users;
using System;

namespace MyOnlineStore.Shared.Interfaces.Factories
{
    public interface ICardFactory
    {
        CardAccount CreateSimpleCard();

        CardAccount CreateCardFromCard(CardAccount card);

        CardAccount CreateCard(string holderName, string number, string type, DateTime expDate, Guid userId, string? secCode = null, Guid? storeId = null);
    }
}