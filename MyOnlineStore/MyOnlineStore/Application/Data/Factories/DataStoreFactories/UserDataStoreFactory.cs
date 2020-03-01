using MyOnlineStore.Application.Common.Interfaces.DataStore;
using MyOnlineStore.Application.Common.Interfaces.Factories;
using MyOnlineStore.Entities.Models.Users;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace MyOnlineStore.Application.Data.Factories.UserDataStoreFactories
{
    public class UserDataStoreFactory : IUserDataStoreFactory
    {
        private readonly IServiceProvider _serviceProvider;
        public UserDataStoreFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        public IUserDataStore<AdminUser> GetAdminUserDataStore()
        {
            return _serviceProvider.GetService<IUserDataStore<AdminUser>>();
        }

        public IUserDataStore<User> GetBaseUserDataStore()
        {
            return _serviceProvider.GetService<IUserDataStore<User>>();
        }

        public IUserDataStore<ClientUser> GetClientUserDataStore()
        {
            return _serviceProvider.GetService<IUserDataStore<ClientUser>>();
        }

        public IUserDataStore<EmployeeUser> GetEmployeeUserDataStore()
        {
            return _serviceProvider.GetService<IUserDataStore<EmployeeUser>>();
        }

        public IUserDataStore<TUser> GetDataStore<TUser>() where TUser : User
        {
            return _serviceProvider.GetService<IUserDataStore<TUser>>();
        }

        //public IUserDataStore<T> CreateStore()
        //{
        //    var userType = Utils.CreateType("");

        //    return GetClientUserDataStore();


        //    //if (userType == typeof(ClientUser))
        //    //{
        //    //    return GetClientUserDataStore() ;
        //    //}
        //    //else if (userType == typeof(EmployeeUser))
        //    //{
        //    //    return GetEmployeeUserDataStore();
        //    //}
        //}
    }
}
