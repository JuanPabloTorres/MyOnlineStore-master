using MyOnlineStore.Entities.Models.Stores;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyOnlineStore.Application.Common.Interfaces.DataStore
{
   public interface IEmployeeDataStore:IEmployeeDataStore<StoresEmployees>
    {

       IEnumerable<StoresEmployees> GetEmployFromStore(string storeId);
    }
}
