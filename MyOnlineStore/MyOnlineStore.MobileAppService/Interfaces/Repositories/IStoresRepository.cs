using MyOnlineStore.Entities.Models.Stores;
using MyOnlineStore.Entities.Models.Users;
using MyOnlineStore.Shared.Models.DTOs.Stores;
using MyOnlineStore.Shared.Models.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyOnlineStore.MobileAppService.Interfaces.Repositories
{
    public interface IStoresRepository : IRepository<Store>
    {
        Store GetByName(string name);
        IEnumerable<StorePresenterDTO> GetClientFavorites(Guid clientId);
        Task<Store> GetStore(string name, string ownerName, Location location);
        bool StoreExists(string ownerName, string storeName, Location location);
        Store GetStoreWithInventory(Guid storeId);
        IEnumerable<Store> GetStoresByOwnerId(string id);
        bool AddFeaturedItems(List<StoreFeaturedItem> featuredItems);
        IEnumerable<StorePresenterDTO> GetStoresToExplore(Guid viewingUserId);
        bool IsFavorite(Guid storeId, Guid clientId);
        bool RemoveFeatureItem(StoreFeaturedItem featuredItems);
        IEnumerable<StoreFeaturedItem> GetStoreFeaturedItems(Guid id);

        IEnumerable<StoresEmployees> GetStoreEmployees(string storeId);

        bool AddEmployeeToStore(User userinfo, Guid storeId);

        bool IsStoreEmployee(string userId);

        bool RemoveEmployee(string userId,string fromstoreId);

        bool UpdateFeatureItem(StoreFeaturedItem featuredItems);

        bool AddEmployeeWorkHours(List<EmployeeWorkHour> employeeWorkHours);

        bool UpdateStoreEmployee(StoresEmployees storesEmployees);

        StoresEmployees GetStoreEmployee(string employeeId,string storeId);

        IEnumerable<EmployeeWorkHour> GetEmployeeWorkHoursOfStore(string employeeId, string storeId);

        IEnumerable<StoresEmployees> GetStoresWorkSpaceFromEmployee(string employeeId);
    }
}
