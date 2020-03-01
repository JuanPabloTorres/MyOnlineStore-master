using Microsoft.EntityFrameworkCore;
using MyOnlineStore.MobileAppService.Context;
using MyOnlineStore.MobileAppService.Interfaces.Repositories;
using MyOnlineStore.Entities.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyOnlineStore.Entities.Models.Roles;
using MyOnlineStore.Shared.Models.Roles;
using MyOnlineStore.Shared.Models.Users;
using MyOnlineStore.Shared.Models.Entries;

namespace MyOnlineStore.MobileAppService.Repositories
{
    public class UsersRepository : Repository<User>, IUsersRepository
    {
        public UsersRepository(MyContext context) 
            : base(context)
        {

        }

        public new async Task<IEnumerable<User>> GetAll()
        {
            return await _Context.Set<User>().ToListAsync();
        }

        public User GetUserByType(string email)
        {
            User user = new User();

            return user;
        }

        public User GetUserByType(string email, Type userType)
        {
            User user = new User(); ;

            //if (userType == typeof(AdminUser))
            //{
            //    user = _Context.Users
            //        .Where(u => u.Email == email && u.Discriminator == typeof(AdminUser).Name)
            //        .FirstOrDefault();
            //}
            //else if (userType == typeof(EmployeeUser))
            //{
            //    user = _Context.Users
            //        .Where(u => u.Email == email && u.Discriminator == typeof(EmployeeUser).Name)
            //        .FirstOrDefault();
            //}
            //else //ClientUser
            //{
            //    user = _Context.Users
            //        .Where(u => u.Email == email && u.Discriminator == typeof(ClientUser).Name)
            //        .FirstOrDefault();
            //}

            return user as User;
        }

        public async Task<User> GetUserByEmail(string email)
        {
            var user = await _Context.Users
                            .Where(user => user.Email == email)
                            .Include(_ => _.MyCardAccounts)
                            .Include(_ => _.MyFavorites)
                            .Include(_ => _.MyOrders)
                            .Include(_ => _.MyRoles)
                            .Include(_ => _.MyStores)
                            .Include(_ => _.MyWorkStores)
                            .FirstOrDefaultAsync();

            return user;
        }

        public Role GetAdminRole()
        {
            Role role;

            role = _Context.Roles.Where(_ => _.Name == "Admin" && _.IsAlive).FirstOrDefault();

            return role;
        }

        public bool AddUserRole(UsersRoles userRole)
        {
            bool added;

            _Context.UsersRoles.Add(userRole);
            added = Save();

            return added;
        }

        public User GetUserByEmailWithAll(string email)
        {
            var user = _Context.Users
                            .Where(user => user.Email == email)
                            .Include(_=>_.MyCardAccounts)
                            .Include(_ => _.MyFavorites)
                            .Include(_ => _.MyOrders)
                            .Include(_ => _.MyRoles)
                            .Include(_ => _.MyStores)
                            .Include(_ => _.MyWorkStores)
                            .FirstOrDefault();

            return user;
        }

        public bool CreateUserConnection(UserConnection connection)
        {
            _Context.UserConnections.Add(connection);

            return Save();
        }

        public bool UpdateUserConnection(UserConnection updateconnection)
        {

            var result = _Context.UserConnections.Where(u => u.ConnectionID == updateconnection.ConnectionID).Count();
           if(result > 1)
            {
                _Context.UserConnections.RemoveRange(_Context.UserConnections.Where(u => u.ConnectionID == updateconnection.ConnectionID).FirstOrDefault());

                _Context.UserConnections.Add(updateconnection);
            }

            return Save();
        }


        public UserConnection GetUserConnection(string userId)
        {
            var userconnectiondata = _Context.UserConnections.Where(u => u.UserId.ToString() == userId).FirstOrDefault();

            return userconnectiondata;
        }

        public bool RemoveUserConnection(UserConnection user)
        {
            if(_Context.UserConnections.Contains(user))
            {
             _Context.UserConnections.RemoveRange(user);

            }


            return Save();
        }

        public bool DeclineRequest(JobRequest request)
        {

           if(_Context.JobRequest.Contains(request))
            {            

                _Context.JobRequest.RemoveRange(request);
            }

            return Save();
        }

        public bool UpdateRequest(JobRequest Updated)
        {
            if (_Context.JobRequest.Where(r=>r.Id == Updated.Id).ToList().Count > 0)
            {
                _Context.JobRequest.RemoveRange(_Context.JobRequest.Where(r => r.Id == Updated.Id).FirstOrDefault());

                _Context.JobRequest.Add(Updated);

            }

            return Save();
        }

        public bool RemoveRequest(string requestId,string storeId)
        {
            if (_Context.JobRequest.Where(r=> r.UserReciverId.ToString() == requestId && r.StoreId.ToString() == storeId).ToList().Count() > 0)
            {
                var toRemove = _Context.JobRequest.Where(r => r.UserReciverId.ToString() == requestId).FirstOrDefault();
                _Context.JobRequest.RemoveRange(toRemove);
            }

            return Save();
        }
    }
}
