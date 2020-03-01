using MyOnlineStore.Entities.Models.Models.DTOs;
using MyOnlineStore.Entities.Models.Users;
using MyOnlineStore.Models.Utils;
using MyOnlineStore.Shared.Models.Entries;
using MyOnlineStore.Shared.Models.Users;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyOnlineStore.Application.Common.Interfaces.DataStore
{
    public interface IUserDataStore : IEmployeeDataStore<User>
    {
        /// <summary>
        /// Get User By email and discriminator
        /// </summary>
        /// <param name="email"></param>
        /// <returns>A user as UserDataStore<User></returns>
        public User? GetUserByType(string email);
        /// <summary>
        /// Get user by email without Discrminator
        /// </summary>
        /// <param name="email"></param>
        /// <returns>User as its found</returns>
        public Task<User?> GetUserByEmail(string email);

        public Task<User> GetById(Guid id);

        /// <summary>
        /// Call to api for validation of credentials and creation of a user
        /// </summary>
        /// <param name="userLoginDTO">Data for login validation</param>
        /// <returns>A token with validations of credentials and type</returns>
        public ValidUserCreateableToken? GetValidUserCreateableToken(UserLoginDTO userLoginDTO);

        //public ValidUserCreateableToken? GetValidUserCreateableTokenWithRetryPolicy(UserLoginDTO userLogin)
        //{
        //    return NetworkService.Retry()
        //}

        public string GetTypeOfUser(string email);

        User AttatchAdminRole(Guid userId, Guid storeId);

        Task<IEnumerable<User>> GetUser(string userfilter);


        #region Hub Connection Method 


        public bool CreateUserConnection(UserConnection connection);

        bool UpdateUserConnection(UserConnection connection);

        public bool RemoveUserConnection(UserConnection userId);


        #endregion

        #region Job Request Methods
        public Task<bool> SendRequest(JobRequest request);


        public bool DeclineRequest(JobRequest requestId);

        public bool UpdateRequest(JobRequest toUpdate);
        #endregion
    }
}
