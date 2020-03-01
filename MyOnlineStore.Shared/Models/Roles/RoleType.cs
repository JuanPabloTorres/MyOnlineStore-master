using MyOnlineStore.Entities.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyOnlineStore.Entities.Models.Roles
{
    [Table("RolesTypes")]
    public class RoleType /*: IBaseEntity*/
    {
        public Guid Id { get; set; }

        [Required]
        public string RoleName { get; set; } = string.Empty;
        public IList<Role> Roles { get; set; }
        //public string CreatedBy { get;  set; } = string.Empty;

        //public string UpdatedBy { get;  set; } = string.Empty;

        //public DateTime CreatedDateTime { get; set; }

        //public DateTime UpdateDateTime { get; set; }

        //public bool IsAlive { get; set; }
        public RoleType()
        {
            Roles = null!;
        }

    }
}
