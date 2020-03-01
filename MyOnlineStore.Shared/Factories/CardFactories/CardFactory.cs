using MyOnlineStore.Entities.Models.Users;
using MyOnlineStore.Shared.Interfaces.Factories;
using System;

namespace MyOnlineStore.Shared.Factories.CardFactories
{
    public class CardFactory : ICardFactory
    {
        public CardAccount CreateCard(string holderName, string number, string type, DateTime expDate, Guid userId, string? secCode = null, Guid? storeId = null)
        {
            return new CardAccount
            {
                Id = Guid.NewGuid(),
                CardHolderName = holderName,
                Number = number,
                Type = type,
                SecurityCode = secCode ?? string.Empty,
                Exp = expDate,
                MyUserId = userId,
                IsVerified = false,
                MyStoreId = storeId ?? null
            };
        }

        public CardAccount CreateCardFromCard(CardAccount card)
        {
            return new CardAccount
            {
                CardHolderName = card.CardHolderName,
                Number = card.Number,
                Type = card.Type,
                SecurityCode = card.SecurityCode ?? string.Empty,
                Exp = card.Exp,
                Id = card.Id,
                IsVerified = card.IsVerified
            };
        }

        public CardAccount CreateSimpleCard()
        {
            return new CardAccount();
        }
    }
}