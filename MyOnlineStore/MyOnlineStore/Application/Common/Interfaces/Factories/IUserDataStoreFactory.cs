using MyOnlineStore.Application.Common.Interfaces.DataStore;
using MyOnlineStore.Entities.Models.Users;

namespace MyOnlineStore.Application.Common.Interfaces.Factories
{
    public interface IUserDataStoreFactory
    {
        IUserDataStore<ClientUser> GetClientUserDataStore();
        IUserDataStore<EmployeeUser> GetEmployeeUserDataStore();
        IUserDataStore<AdminUser> GetAdminUserDataStore();
        IUserDataStore<User> GetBaseUserDataStore();
        IUserDataStore<TUser> GetDataStore<TUser>() where TUser : User;
    }
}
