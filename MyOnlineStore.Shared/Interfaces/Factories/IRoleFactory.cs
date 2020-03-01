using MyOnlineStore.Entities.Models.Users;
using MyOnlineStore.Shared.Models.Roles;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyOnlineStore.Shared.Interfaces.Factories
{
    public interface IRoleFactory
    {
        UsersRoles CreateUserRole(Guid userId, Guid storeId, Guid roleId);
    }
}
