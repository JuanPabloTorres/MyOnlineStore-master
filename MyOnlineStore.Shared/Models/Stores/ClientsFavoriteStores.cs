using MyOnlineStore.Entities.Models.Interfaces;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyOnlineStore.Entities.Models.Stores
{
    [Table("ClientsFavoriteStores")]
    public class ClientsFavoriteStores : IBaseEntity
    {
        #region Constructors

        public ClientsFavoriteStores()
        {
        }

        #endregion Constructors

        #region Properties

        public Guid ClientId { get; set; }

        public Guid StoreId { get; set; }
        public Store? Store { get; set; }

        public bool IsAlive { get; set; }

        public Guid Id { get; set; }

        #endregion Properties
    }
}