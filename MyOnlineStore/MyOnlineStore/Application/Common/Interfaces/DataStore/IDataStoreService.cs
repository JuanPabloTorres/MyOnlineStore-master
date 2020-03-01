using System.Collections.Generic;

namespace MyOnlineStore.Application.Common.Interfaces.DataStore
{
    public interface IEmployeeDataStore<T> where T : class
    {
        bool AddItem(T item);
        bool UpdateItem(T item);
        bool DeleteItem(string id);
        T GetItem(string id);
        IEnumerable<T> GetAll(bool forceRefresh = false);
    }
}
