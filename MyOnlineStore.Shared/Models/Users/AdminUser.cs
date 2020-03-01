using MyOnlineStore.Entities.Models.Interfaces;
using MyOnlineStore.Entities.Models.Stores;
using System.Collections.Generic;

namespace MyOnlineStore.Entities.Models.Users
{
    public class AdminUser : EmployeeUser, IAdminUser
    {
        public List<Store> MyStores { get; set; }

        public AdminUser()
        {
            MyStores = new List<Store>();
        }
    }
}
