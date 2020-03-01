using MyOnlineStore.Entities.Models.Roles;
using System.Collections.Generic;

namespace MyOnlineStore.Entities.Models.Interfaces
{
    public interface IRoleableUser
    {
        List<Role>? Roles { get; }
    }
}