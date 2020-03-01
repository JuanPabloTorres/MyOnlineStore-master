using MyOnlineStore.Entities.Models.Roles;

namespace MyOnlineStore.Application.Common.Interfaces.DataStore
{
    public interface IRoleDataStore : IEmployeeDataStore<Role>
    {
        //RoleType GetByRoleName(string name);
    }
}
