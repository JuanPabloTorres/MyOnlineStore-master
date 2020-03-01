using MyOnlineStore.Entities.Models.Interfaces;
using MyOnlineStore.Entities.Models.Roles;
using MyOnlineStore.Entities.Models.Stores;
using System.Collections.Generic;

namespace MyOnlineStore.Entities.Models.Users
{
    public class EmployeeUser : ClientUser, IEmployeeUser
    {
        public List<StoreEmployee> MyWorkStore { get; protected set; }
        public List<Role>? Roles { get; protected set; }

        public EmployeeUser()
        {
            Discriminator = typeof(EmployeeUser).Name;
            MyWorkStore = new List<StoreEmployee>();
        }
    }
}
