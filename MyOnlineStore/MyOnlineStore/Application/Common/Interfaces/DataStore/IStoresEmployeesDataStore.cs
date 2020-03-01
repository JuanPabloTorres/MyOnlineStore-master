using MyOnlineStore.Application.Data.DataStore;
using MyOnlineStore.Entities.Models.Stores;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyOnlineStore.Application.Common.Interfaces.DataStore
{
    public interface IStoresEmployeesDataStore
    {
        public Task<IEnumerable<StoresEmployees>> GetStoreEmployees(string storeId);

        public IEnumerable<StoresEmployees> GetStoresWorkSpaceFromEmployee(string employee);
    }
}
