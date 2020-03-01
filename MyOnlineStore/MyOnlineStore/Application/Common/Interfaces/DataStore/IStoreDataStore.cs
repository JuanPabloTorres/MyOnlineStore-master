using MyOnlineStore.Application.Data.Models.Presenters;
using MyOnlineStore.Entities.Models.Stores;
using MyOnlineStore.Entities.Models.Users;
using MyOnlineStore.Shared.Models.DTOs.Stores;
using MyOnlineStore.Shared.Models.Entries;
using MyOnlineStore.Shared.Models.Stores;
using MyOnlineStore.Shared.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyOnlineStore.Application.Common.Interfaces.DataStore
{
    public interface IStoreDataStore : IEmployeeDataStore<Store>
    {
        Store GetByName(string name);
        bool IsFavorite(string storeId, string clientId);
        IEnumerable<Store> GetClientFavorite(Guid clientId);
        IEnumerable<StorePresenter> GetAllToExplore(string id);
        IEnumerable<StorePresenter> GetClientFavorites(string clientId);

        bool StoreExists(string storeName, string storeOwnerName, Location location);
        Store? GetStore(string storeName, string storeOwnerName, Location location);
        Store AddStoreAndGet(Store store);
        IEnumerable<Store>? GetStoresByOwnerId(string userid);
        Store GetStoreWithInventory(string storeId);
        ApiResponse<User> RegisterStoreAction(IStoreRegistrationEntry storeRegistrationEntry);
        bool AddFeaturedItem(IList<StoreFeaturedItem> featuredItems);

        bool UpdateFeature(StoreFeaturedItem featuredItems);

        bool RemoveFeatureItem(StoreFeaturedItem featuredItems);
        bool RemoveFeatureItems(IList<StoreFeaturedItem> featuredItems);

        bool UpdateFeatureItems(IList<StoreFeaturedItem> featuredItems);
         bool UpdateFeatureItem(StoreFeaturedItem featuredItems);
        IEnumerable<StoreFeaturedItem> GetFeaturedItemsOfStore(Guid storeId);

        bool RemoveEmployee(string userId, string storeId);

         bool AddEmployeeWorkHours(List<EmployeeWorkHour> employeeWorkHours);
         bool UpdateEmployee(StoresEmployees updatedEmployee);

        IEnumerable<EmployeeWorkHour> GetEmployeeWorkHours(string employeeId);

        StoresEmployees GetStoreEmployee(string employeeId, string storeId);

        public IEnumerable<EmployeeWorkHour> GetEmployeeWorkHoursOfStore(string employeeId, string storeId);



    }
}
