using Microsoft.AspNetCore.Identity;
using MyOnlineStore.Entities.Models.Interfaces;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyOnlineStore.Entities.Models.Roles
{
    [Table("Roles")]
    public class Role : IdentityRole<Guid>, IBaseEntity
    {
        #region Constructors

        public Role()
        {
            //Permissions = new List<Permission>();
        }

        #endregion Constructors

        #region Properties

        public bool IsAlive { get; set; }

        //public List<Permission> Permissions { get; set; }

        #endregion Properties

        #region NoUse

        [NotMapped]
        private new string? NormalizedName { get; set; }

        #endregion NoUse
    }
}