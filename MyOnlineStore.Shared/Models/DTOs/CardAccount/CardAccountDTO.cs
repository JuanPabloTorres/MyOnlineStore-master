using MyOnlineStore.Entities.Models.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyOnlineStore.Shared.Models.DTOs.CardAccounts
{
    public class CardAccountDTO
    {
        public Guid Id { get; set; }
        public string Number { get; set; } = string.Empty;
        public DateTime Exp { get; set; }
        public string Pin { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public bool IsVerified { get; set; }
        public string CardHolderName { get; set; } = string.Empty;
        public string SecurityCode { get; set; } = string.Empty;
        public Guid MyUserId { get; set; }
        public Guid? MyStoreId { get; set; }

        public CardAccountDTO(CardAccount cardAccount)
        {
            Id = cardAccount.Id;
            Number = cardAccount.Number;
            Exp = cardAccount.Exp;
            Pin = cardAccount.Pin;
            Type = cardAccount.Type;
            IsVerified = cardAccount.IsVerified;
            CardHolderName = cardAccount.CardHolderName;
            SecurityCode = cardAccount.SecurityCode;
            MyStoreId = cardAccount.MyStoreId;
            MyUserId = cardAccount.MyUserId;
        }
    }
}
