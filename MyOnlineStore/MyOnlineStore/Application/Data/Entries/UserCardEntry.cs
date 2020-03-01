using MyOnlineStore.Entities.Models.Users;
using System;

namespace MyOnlineStore.Application.Data.Models.Entries
{
    public class UserCardEntry
    {
        public string Number { get; set; }
        public DateTime Exp { get; set; }
        public string Pin { get; set; }
        public string Type { get; set; }

        public UserCardEntry()
        {
            Number = string.Empty;
            Exp = new DateTime();
            Pin = string.Empty;
            Type = string.Empty;
        }

        public UserCardEntry(string num,DateTime expiration,string pin, string cardtype)
        {
            Number = num;
            Exp = expiration;
            Pin = pin;
            Type = cardtype;
        }
        public CardAccount ToUserCard()
        {
            return new CardAccount
            {
                Number = this.Number,
                Exp = this.Exp,
                Pin = this.Pin,
                Type = this.Type
            };
        }
    }
}
