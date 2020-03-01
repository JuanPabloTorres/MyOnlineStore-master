using MyOnlineStore.Entities.Models.DTOs.Stores;
using MyOnlineStore.Entities.Models.Users;
using MyOnlineStore.Shared.Models.DTOs.CardAccounts;
using MyOnlineStore.Shared.Models.DTOs.Orders;
using MyOnlineStore.Shared.Models.DTOs.Roles;
using MyOnlineStore.Shared.Models.DTOs.Stores;
using MyOnlineStore.Shared.Models.Roles;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyOnlineStore.Shared.Models.DTOs.Users
{
    public class UserDTO
    {
        //
        // Summary:
        //     Gets or sets a telephone number for the user.
        public virtual string PhoneNumber { get; set; }

        //
        // Summary:
        //     Gets or sets a salted and hashed representation of the password for this user.
        public virtual string PasswordHash { get; set; }
      
        // Summary:
        //     Gets or sets the email address for this user.
        public virtual string Email { get; set; }

        //
        // Summary:
        //     Gets or sets the user name for this user.
        public virtual string UserName { get; set; }
        //
        // Summary:
        //     Gets or sets the primary key for this user.
        public virtual Guid Id { get; set; }


        public string FullName { get; set; } = string.Empty;

        public DateTime BirthDate { get; set; }

        public List<UsersRolesDTO>? MyRoles { get; set; }
        public List<CardAccountDTO>? MyCardAccount { get; set; }
        public List<ClientsFavoriteStoresDTO>? MyFavorites { get; set; }
        public List<StoreDTO>? MyStores { get; set; }
        public List<OrderDTO>? MyOrders { get; set; }

        public UserDTO(User user)
        {
            BirthDate = user.BirthDate;
            Email = user.Email;
            FullName = user.FullName;
            Id = user.Id;
            PasswordHash = user.PasswordHash;
            UserName = user.UserName;
            PhoneNumber = user.PhoneNumber;

            if(user.MyRoles is object && user.MyRoles.Count > 0)
            {
                MyRoles = new List<UsersRolesDTO>();
                foreach(var role in user.MyRoles)
                {
                    MyRoles.Add(new UsersRolesDTO(role));
                }
            }

            if(user.MyCardAccounts is object && user.MyCardAccounts.Count > 0)
            {
                MyCardAccount = new List<CardAccountDTO>();
                foreach(var account in user.MyCardAccounts)
                {
                    MyCardAccount.Add(new CardAccountDTO(account));
                }
            }

            if(user.MyFavorites is object && user.MyFavorites.Count> 0)
            {
                MyFavorites = new List<ClientsFavoriteStoresDTO>();
                foreach(var fav in user.MyFavorites)
                {
                    MyFavorites.Add(new ClientsFavoriteStoresDTO(fav));
                }
            }

            if(user.MyStores is object && user.MyStores.Count > 0)
            {
                MyStores = new List<StoreDTO>();
                foreach(var store in user.MyStores)
                {
                    MyStores.Add(new StoreDTO(store));
                }
            }

            if(user.MyOrders is object && user.MyOrders.Count > 0)
            {
                MyOrders = new List<OrderDTO>();
                foreach(var order in user.MyOrders)
                {
                    MyOrders.Add(new OrderDTO(order));
                }
            }
        }

    }
}
