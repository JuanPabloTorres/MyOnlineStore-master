using MyOnlineStore.Entities.Models.Roles;

namespace MyOnlineStore.MobileAppService.Interfaces.Repositories
{
    public interface IRoleRefRepository : IRepository<RoleType>
    {
        RoleType GetByRoleName(string roleName);
    }
}
