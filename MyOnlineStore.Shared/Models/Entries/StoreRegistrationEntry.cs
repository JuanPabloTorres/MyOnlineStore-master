using MyOnlineStore.Entities.Models.Stores;
using MyOnlineStore.Entities.Models.Users;
using System;

namespace MyOnlineStore.Shared.Models.Entries
{
    public interface IStoreRegistrationEntry
    {
        #region Properties

        Store? Store { get; set; }
        User? User { get; set; }
        CardAccount? Account { get; set; }

        #endregion Properties
    }

    public class StoreRegistrationEntry : IStoreRegistrationEntry
    {
        #region Constructors

        public StoreRegistrationEntry()
        {
            
        }

        #endregion Constructors

        #region Properties

        public Store? Store { get; set; }
        public User? User { get; set; }
        public CardAccount? Account { get; set; }

        #endregion Properties
    }
}