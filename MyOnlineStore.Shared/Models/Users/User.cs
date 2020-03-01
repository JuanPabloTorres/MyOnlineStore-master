using Microsoft.AspNetCore.Identity;
using MyOnlineStore.Entities.Models.Interfaces;
using MyOnlineStore.Entities.Models.Purchases;
using MyOnlineStore.Entities.Models.Stores;
using MyOnlineStore.Shared.Models.Entries;
using MyOnlineStore.Shared.Models.Roles;
using MyOnlineStore.Shared.Models.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace MyOnlineStore.Entities.Models.Users
{
    [Table("Users")]
    //[ApiGenericEntity]
    public class User : IdentityUser<Guid>, IBaseEntity
    {
        private List<Store>? myStores;

        #region Constructors

        public User()
        {
            MyRoles = new List<UsersRoles>();
        }

        #endregion Constructors

        #region Properties

        public string FullName { get; set; } = string.Empty;

        public DateTime BirthDate { get; set; }

        public bool IsAlive { get; set; }

        public virtual List<UsersRoles>? MyRoles { get; set; }

        public virtual List<CardAccount>? MyCardAccounts { get; set; }

        public virtual List<ClientsFavoriteStores>? MyFavorites { get; set; }

        public virtual List<StoresEmployees>? MyWorkStores { get; set; }

        public virtual List<Store>? MyStores { get; set; }

       

        public virtual List<Order>? MyOrders { get; set; }
        public List<JobRequest>? JobRequests { get; set; }

        #endregion Properties

        #region Methods

        public bool IsAdminUserOfThisStore(Store store)
        {
            bool isAdminForStore;

            isAdminForStore = HasAccountForStore(store);

            return isAdminForStore;
        }

        public bool IsAdminUserOfThisStore(Guid storeId)
        {
            bool isAdminForStore;

            isAdminForStore = HasAccountForStore(storeId: storeId);

            return isAdminForStore;
        }

        public bool HasAccountForStore(Store? store = null, Guid? storeId = null)
        {
            bool hasAccountForStore = false;

            if(storeId.HasValue && HasStore() && HasAccount())
            {
                hasAccountForStore = MyStores.Any(s =>
                    MyCardAccounts.Where(_ => _.Id == s.Id).FirstOrDefault() is object);
            }

            return hasAccountForStore;
        }

        public bool HasAccount()
        {
            return MyCardAccounts is object && MyCardAccounts.Count > 0 ? true : false;
        }

        public bool HasStore()
        {
            return MyStores is object && MyStores.Count > 0 ? true : false;
        }

        public bool HasWork()
        {
            return MyWorkStores is object && MyWorkStores.Count > 0 ? true : false;
        }

        public bool IsEmployeeFor(Store store)
        {
            bool isEmployeeFor;

            isEmployeeFor = MyWorkStores.Where(_ => _.StoreId == store.Id)
                                        .FirstOrDefault() is object ? true : false;

            return isEmployeeFor;
        }

        public bool TryAddCard(CardAccount account)
        {
            uint start, end;
            bool isInserted;
            try
            {
                if(MyCardAccounts is null)
                {
                    MyCardAccounts = new List<CardAccount>();
                }

                start = (uint)MyCardAccounts.Count;
                MyCardAccounts.Add(account);
                end = (uint)MyCardAccounts.Count;

                isInserted = end > start ? true : false;
            }
            catch(Exception)
            {
                isInserted = false;
            }

            return isInserted;
        }

        public bool HasId()
        {
            return !(Id == null || Id == Guid.Empty);
        }

        #endregion Methods
    }
}