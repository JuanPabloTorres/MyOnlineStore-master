using System;
using System.Collections.Generic;
using System.Text;
using MyOnlineStore.Entities.Models.Roles;
using MyOnlineStore.Shared.Models.Roles;

namespace MyOnlineStore.Shared.Models.DTOs.Roles
{
    public class UsersRolesDTO
    {
        public Guid Id { get; set; }

        public Guid RoleId { get; set; }
        public Role? Role { get; set; }
        public Guid UserId { get; set; }

        public Guid StoreId { get; set; }

        public UsersRolesDTO(UsersRoles usersRoles)
        {
            Id = usersRoles.Id;
            Role = usersRoles.Role;
            RoleId = usersRoles.RoleId;
            UserId = usersRoles.UserId;
            StoreId = usersRoles.StoreId;
        }
    }
}
