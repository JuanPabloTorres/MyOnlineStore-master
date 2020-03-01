using MyOnlineStore.Entities.Models.Interfaces;
using MyOnlineStore.Entities.Models.Purchases;
using MyOnlineStore.Entities.Models.Users;
using MyOnlineStore.Shared.Models.Roles;
using MyOnlineStore.Shared.Models.Stores;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyOnlineStore.Entities.Models.Stores
{
    [Table("Stores")]
    public class Store : IBaseEntity
    {
        #region Constructors

        public Store()
        {
            ProductItems = new List<ProductItem>();
        }

        #endregion Constructors

        #region Properties

        public Guid Id { get; set; }
        public string StoreName { get; set; } = string.Empty;
        public string StoreOwnerName { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public byte[]? Logo { get; set; }
        public Location? Location { get; set; }
        public List<ProductItem> ProductItems { get; set; }
        public Guid OwnerUserId { get; set; }
        public List<StoreFeaturedItem>? FeaturedItems { get; set; }
        public List<Order>? StoreOrders { get; set; }
        public List<WorkingHour>? WorkingHours { get; set; }
        public List<CardAccount>? MyAccounts { get; set; }
        public List<UsersRoles>? Employees { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
        public bool IsAlive { get; set; }

        #endregion Properties


        public bool HasAllRequieredFieldsForRegistration()
        {
            return StoreOwnerName is object && StoreOwnerName is object && Category is object
                && Id != null && Id != Guid.Empty && Location is object
                && OwnerUserId != null && OwnerUserId != Guid.Empty;
        }

    }
}