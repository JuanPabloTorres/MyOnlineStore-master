using MyOnlineStore.Entities.Models.Users;
using System.Collections.Generic;

namespace MyOnlineStore.Entities.Models.Interfaces
{
    public interface ICardable
    {
        List<CardAccount>? UserCards { get; }

        bool CardsHasValue();

        bool AddCard(CardAccount userCard);
    }
}