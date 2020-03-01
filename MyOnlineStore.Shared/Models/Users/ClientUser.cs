using MyOnlineStore.Models.CustomAttribute;
using MyOnlineStore.Entities.Models.Interfaces;
using MyOnlineStore.Entities.Models.Stores;
using System;
using System.Collections.Generic;

namespace MyOnlineStore.Entities.Models.Users
{
    public class ClientUser : User, IClientUser, ICardable
    {
        public List<CardAccount>? UserCards { get; protected set; }
        public DateTime BirthDate { get; set; }
        public List<FavoriteStoreClient>? FavoriteStores { get; set; }

        public ClientUser()
        {
            Discriminator = typeof(ClientUser).Name;

            UserCards = new List<CardAccount>();

            if (FavoriteStores == null)
                FavoriteStores = new List<FavoriteStoreClient>();
        }
       
        public bool CardsHasValue()
        {
            return UserCards == null ? false : true;
        }
        public bool AddCard(CardAccount userCard)
        {
            bool added = false;
            int count;
            try
            {
                if (UserCards==null)
                {
                    UserCards = new List<CardAccount>();
                }

                userCard.MyUserId = Id;
                count = UserCards.Count;
                UserCards.Add(userCard);

                if ((count + 1) == UserCards.Count)
                {
                    added = true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception in AddCard User: {ex.Message}");
                added = false;
            }

            return added;
        }
    }
}
