using MyOnlineStore.Entities.Models.Stores;
using MyOnlineStore.Entities.Models.Users;
using MyOnlineStore.Shared.Interfaces.Factories;
using MyOnlineStore.Shared.Models.Roles;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyOnlineStore.Shared.Factories.RolesFactory
{
    public class RoleFactory : IRoleFactory
    {
        public UsersRoles CreateUserRole(Guid userId, Guid storeId, Guid roleId)
        {
            return new UsersRoles
            {
                UserId = userId,
                StoreId = storeId,
                RoleId = roleId,
                IsAlive = true
            };
        }
    }
}
