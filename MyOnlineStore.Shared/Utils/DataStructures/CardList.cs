using MyOnlineStore.Entities.Models.Users;
using System;
using System.Collections.Generic;

namespace MyOnlineStore.Shared.Utils.DataStructures
{
    public class CardList : List<CardAccount>
    {
        public CardList()
        {
        }

        public CardList(IEnumerable<CardAccount> collection) : base(collection)
        {
        }

        public CardList(int capacity) : base(capacity)
        {
            Remove(c => c.Id == Guid.Empty);
        }

        public CardList Remove(Predicate<CardAccount> predicate)
        {
            //this.Bin
            return this;
        }
    }
}