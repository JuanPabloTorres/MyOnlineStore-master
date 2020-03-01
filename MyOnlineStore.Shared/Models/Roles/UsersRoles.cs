using MyOnlineStore.Entities.Models.Interfaces;
using MyOnlineStore.Entities.Models.Roles;
using MyOnlineStore.Entities.Models.Stores;
using MyOnlineStore.Entities.Models.Users;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyOnlineStore.Shared.Models.Roles
{
    [Table("UsersRoles")]
    public class UsersRoles : IBaseEntity
    {
        #region Constructors

        public UsersRoles()
        {
        }

        #endregion Constructors

        #region Properties

        public Guid Id { get; set; }

        public bool IsAlive { get; set; }

        public Guid RoleId { get; set; }
        public virtual Role? Role { get; set; }

        public Guid UserId { get; set; }
        public virtual User? User { get; set; }

        public Guid StoreId { get; set; }
        public virtual Store? Store { get; set; }

        #endregion Properties
    }
}