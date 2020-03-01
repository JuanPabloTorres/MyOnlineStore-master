using MyOnlineStore.Entities.Models.Stores;
using MyOnlineStore.Entities.Models.Users;
using MyOnlineStore.Shared.Models.Entries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyOnlineStore.MobileAppService.Interfaces.Repositories
{
     public interface IEmployeeRepository:IRepository<StoresEmployees>
    {
        IEnumerable<StoresEmployees> GetEmployeeFromStore(string storeId);
        
        IEnumerable<User> GetUsers(string userfilter);

        bool SendRequest(JobRequest request);

    }
}
