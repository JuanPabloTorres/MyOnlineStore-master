using MyOnlineStore.Entities.Models.Users;
using MyOnlineStore.Shared.Interfaces.Factories;
using System;

namespace MyOnlineStore.Application.Data.Factories.UsersFactories
{
    //==========================================================================================
    //
    // TODO: Assign roles when created
    //
    //==========================================================================================
    public class UserFactory : IUserFactory
    {
        public User CreateUser(string FullName, DateTime BirthDate, string Email, string Password, string PhoneNumber)
        {
            return new User
            {
                Id = Guid.NewGuid(),
                FullName = FullName,
                BirthDate = BirthDate,
                Email = Email,
                PasswordHash = Password,
                PhoneNumber = PhoneNumber,
                UserName = Email.Substring(0, Email.IndexOf('@')),
                AccessFailedCount = 0,
                SecurityStamp = Guid.NewGuid().ToString(),
                ConcurrencyStamp = Guid.NewGuid().ToString(),
                EmailConfirmed = false,
                LockoutEnabled = true,
                LockoutEnd = DateTime.Now,
                NormalizedUserName = Email.Substring(0, Email.IndexOf('@')).ToLower(),
                NormalizedEmail = Email.ToLower(),
                PhoneNumberConfirmed = false,
                TwoFactorEnabled = false,
                IsAlive = true
            };
        }

        

        public User CreateUser()
        {
            return new User
            {
                Id = Guid.NewGuid()
            };
        }
    }
}