using MyOnlineStore.Entities.Models.Stores;
using MyOnlineStore.Entities.Models.Users;
using MyOnlineStore.MobileAppService.Context;
using MyOnlineStore.MobileAppService.Interfaces.Repositories;
using MyOnlineStore.Shared.Models.Entries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyOnlineStore.MobileAppService.Repositories
{
    public class EmployeesRepository : Repository<StoresEmployees>, IEmployeeRepository
    {

        public EmployeesRepository(MyContext context):base(context)
        {

        }

        public IEnumerable<User> GetUsers(string filter)
        {
            var users = _Context.Users.Where(i => i.Email == filter || i.PhoneNumber == filter);
            return users;
        }

        public IEnumerable<StoresEmployees> GetEmployeeFromStore(string storeId)
        {
            var employees = _Context.Employees.Where(e => e.StoreId.ToString() == storeId);

            return employees;
        }

        public bool SendRequest(JobRequest request)
        {

            if (_Context.JobRequest.Where(j => j.UserReciverId == request.UserReciverId).ToList().Count() == 0)
            {

                _Context.JobRequest.Add(request);
                return Save();
            }
            else
            {
                return false;
            }
        }
    }
}
