
using MyOnlineStore.Entities.Models.Roles;
using MyOnlineStore.Entities.Models.Users;
using MyOnlineStore.Shared.Models.Entries;
using MyOnlineStore.Shared.Models.Roles;
using MyOnlineStore.Shared.Models.Users;
using System;
using System.Threading.Tasks;

namespace MyOnlineStore.MobileAppService.Interfaces.Repositories
{
    public interface IUsersRepository : IRepository<User>
    {
        // -----------------------------------------------------
        //
        // Write the Specific query for this Interface
        // Ex: GetTopClient(int count);
        //
        // -----------------------------------------------------

        User GetUserByType(string email);
        Task<User> GetUserByEmail(string email);
        Role GetAdminRole();
        bool AddUserRole(UsersRoles userRole);
        User GetUserByEmailWithAll(string email);

        bool CreateUserConnection(UserConnection connection);

        bool UpdateUserConnection(UserConnection connection);

        public UserConnection GetUserConnection(string userId);

        public bool RemoveUserConnection(UserConnection userId);

        public bool DeclineRequest(JobRequest requestId);

        public bool RemoveRequest(string requestId,string storeId);

        public bool UpdateRequest(JobRequest toUpdate);

    }
}
