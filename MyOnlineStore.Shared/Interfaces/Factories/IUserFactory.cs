using MyOnlineStore.Entities.Models.Users;
using System;

namespace MyOnlineStore.Shared.Interfaces.Factories
{
    public interface IUserFactory
    {
        User CreateUser();

        User CreateUser(string FullName,
         DateTime BirthDate,
         string Email,
         string Password,
         string PhoneNumber);
    }
}