using Microsoft.EntityFrameworkCore;
using MyOnlineStore.MobileAppService.Context;
using MyOnlineStore.MobileAppService.Interfaces.Repositories;
using MyOnlineStore.Entities.Models.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using MyOnlineStore.Shared.Models.DTOs.Stores;
using MyOnlineStore.Entities.Models.Users;
using MyOnlineStore.Shared.Models.Stores;

namespace MyOnlineStore.MobileAppService.Repositories
{

    public class StoresRepository : Repository<Store>, IStoresRepository
    {
        public StoresRepository(MyContext context) : base(context)
        {
        }

        public bool AddFeaturedItems(List<StoreFeaturedItem> featuredItems)
        {


            foreach (var item in featuredItems)
            {
               
                if(_Context.FeaturedItems.Where(i=> i.Id== item.Id).ToList().Count == 0)
                {
                _Context.FeaturedItems.Add(item);
                }
            }

            return Save();
        }

        public Store GetByName(string name)
        {
            var result = _Context.Stores
                .Where(s => s.StoreName.Equals(name))
                .FirstOrDefault();
            return result;
        }

        public IEnumerable<StorePresenterDTO> GetClientFavorites(Guid clientId)
        {
            List<StorePresenterDTO> storePresenters = new List<StorePresenterDTO>();
            var clientFavorites = _Context.ClientsFavoriteStores
                .Where(fs => fs.ClientId == clientId).ToList();

            //var result = _Context.Stores
            //         .Join(clientFavorites, store => store.Id, favorite => favorite.StoreId, (store, favorite) => store)
            //         .ToList();
            //var result = _Context.Stores.Contains(clientFavorites.Select());
            /*_Context.Stores.Where(s => clientFavorites.Exists(f => f.StoreId == s.Id)).ToList();*/


            //var result = (from store in _Context.Stores
            //              join fav in _Context.ClientsFavoritesStores on store.Id equals fav.Store.Id
            //              where fav.Client.Id == clientId
            //              select store).ToList();
            var result = (from store in _Context.Stores
                          join fav in _Context.ClientsFavoriteStores on store.Id equals fav.StoreId
                          where fav.ClientId == clientId
                          select store).ToList();


            foreach (var item in result)
            {
                storePresenters.Add(new StorePresenterDTO(store: item, isFav: true));
            }
            return storePresenters;
        }

        public async Task<Store> GetStore(string name, string ownerName, Location location)
        {
          var result = await Task.Run(()=>_Context.Stores.Where(store => store.StoreName.Equals(name) && store.StoreOwnerName.Equals(ownerName) && store.Location.Latitude.Equals(location.Latitude) && store.Location.Longitude.Equals(location.Longitude)));
            return result.FirstOrDefault();
        }

        public IEnumerable<StoreFeaturedItem> GetStoreFeaturedItems(Guid storeid)
        {
           var data=  _Context.FeaturedItems.Where(i => i.MyStoreId == storeid).ToList();

            return data;
        }

        public IEnumerable<Store> GetStoresByOwnerId(string id)
        {
            Guid guid = Guid.Parse(id);
            var stores = _Context.Stores
                .Where(s => s.OwnerUserId == guid)
                .Include(s=>s.FeaturedItems)
                .ToList();
            return stores;
        }

        public IEnumerable<StorePresenterDTO> GetStoresToExplore(Guid viewingUserId)
        {
            // All store in the order that they will see it
            var allStoreToExplore = _Context.Stores.ToList();
            var allfavoritesStore = _Context.ClientsFavoriteStores.Where(s => s.ClientId == viewingUserId).ToList();
            List<StorePresenterDTO> storesToPresent = new List<StorePresenterDTO>();

            List<Store> userFavoritesStores = new List<Store>(allStoreToExplore.Where(s => allfavoritesStore.Exists(f => f.StoreId == s.Id)));
            var allOtherStore =  new List<Store>(allStoreToExplore.Except(userFavoritesStores));

            foreach (var item in userFavoritesStores)
            {
                storesToPresent.Add(new StorePresenterDTO(item, isFav: true)) ;
            }

            foreach (var item in allOtherStore)
            {
                storesToPresent.Add(new StorePresenterDTO(item));
            }

            return storesToPresent;
        }

        public Store GetStoreWithInventory(Guid storeId)
        {
            var store = _Context.Stores
                        .Where(s => s.Id == storeId)
                        .Include(s => s.FeaturedItems)
                        .Include(s => s.ProductItems)
                        .FirstOrDefault();

            //var items = _Context.ProductItems
            //             .Where(pi => pi.MyStoreId == storeId)
            //             .Include(pi => pi.MyStore)
            //             .ToList();

            //store.ProductItems.AddRange(items);

            return store;
        }

        public bool IsFavorite(Guid storeId, Guid clientId)
        {
            var result  = _Context.ClientsFavoriteStores
                .Where(fs => fs.StoreId == storeId && fs.ClientId == clientId)
                .FirstOrDefault();
            if (result is null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public bool StoreExists(string ownerName,string storeName,Location location)
        {
            bool exist = _Context.Stores.Include(store => store.Location)
                .Where(store => store.StoreName == storeName
                && store.StoreOwnerName == ownerName
                && store.Location.Latitude == location.Latitude
                && store.Location.Longitude == location.Longitude)
                .Count() == 0 ? false : true;
            return exist;  
        }

        public bool RemoveFeatureItems(List<StoreFeaturedItem> featuredItems)
        {
            _Context.FeaturedItems.RemoveRange(featuredItems);

            return Save();
        }

        public bool RemoveFeatureItem(StoreFeaturedItem featuredItems)
        {
            _Context.FeaturedItems.Remove(featuredItems);

            return Save();
        }

        public IEnumerable<StoresEmployees> GetStoreEmployees(string storeId)
        {
             return _Context.Employees.Where(e => e.StoreId.ToString() == storeId);
        }

        public bool AddEmployeeToStore(User userinfo,Guid storeId)
        {
            //var user = _Context.Users.Where(u => u.Id.ToString() == newEmployeeId).FirstOrDefault();


            if (_Context.Employees.Where(e=>e.UserEmployeeId == userinfo.Id && e.StoreId == storeId).ToList().Count == 0)
            {
                StoresEmployees newStoreEmployee = new StoresEmployees()
                {
                    Id = Guid.NewGuid(),
                    FullName = userinfo.FullName,
                    IsAlive = true,
                    StoreId = storeId,
                    UserEmployeeId = userinfo.Id,                   
                    EmployeePosition = Position.None
                };

                _Context.Employees.Add(newStoreEmployee);
                return Save();
            }
            else
            {
                return false;
            }

           

           
        }

        public bool IsStoreEmployee(string userId)
        {


            var result=_Context.Employees.Where(e => e.UserEmployeeId.ToString() == userId).ToList().Count;

            if (result > 0)
            {
                return true;
            }
            else
            {
               return false;
            }
        }

        public bool RemoveEmployee(string userId,string fromstoreId)
        {

            if(_Context.Employees.Where(e => e.StoreId.ToString() == fromstoreId && e.UserEmployeeId.ToString() == userId).ToList().Count > 0)
            {

            var usertoRemove=_Context.Employees.Where(e => e.StoreId.ToString() == fromstoreId && e.UserEmployeeId.ToString() == userId).FirstOrDefault();

            _Context.RemoveRange(usertoRemove);
            }

            return Save();
        }

        public bool AddEmployeeWorkHours(List<EmployeeWorkHour> employeeWorkHours)
        {

            var userId = employeeWorkHours[0].EmployeeId;
            var storeId = employeeWorkHours[0].StoreId;

            if (_Context.EmployeeWorkHours.Where(e => e.EmployeeId.ToString() == userId.ToString() && e.StoreId.ToString() == storeId.ToString()).ToList().Count > 0)
            {
                _Context.EmployeeWorkHours.RemoveRange(_Context.EmployeeWorkHours.Where(e => e.EmployeeId.ToString() == userId.ToString() && e.StoreId.ToString() == storeId.ToString()).ToList());

                foreach (var item in employeeWorkHours)
                {
                    _Context.EmployeeWorkHours.Add(item);
                }
            }
            else
            {
                foreach (var item in employeeWorkHours)
                {
                    _Context.EmployeeWorkHours.Add(item);
                }
            }

            return Save();
        }

        public bool UpdateStoreEmployee(StoresEmployees updatedemployee)
        {
            if (_Context.Employees.Where(e=> e.Id .ToString()== updatedemployee.Id.ToString() && e.StoreId.ToString() == updatedemployee.StoreId.ToString()).ToList().Count > 0)
            {

                var toRemove = _Context.Employees.Where(e => e.Id.ToString() == updatedemployee.Id.ToString() && e.StoreId.ToString() == updatedemployee.StoreId.ToString()).FirstOrDefault();
                _Context.Employees.RemoveRange(toRemove);
                               
                _Context.Employees.Add(updatedemployee);
            }

            return Save();
        }

        public StoresEmployees GetStoreEmployee(string employeeId, string storeId)
        {          
                var result = _Context.Employees.Where(e => e.StoreId.ToString() == storeId && e.UserEmployeeId.ToString() == employeeId).FirstOrDefault();

                return result;      
        }

        public IEnumerable<EmployeeWorkHour> GetEmployeeWorkHoursOfStore(string employeeId, string storeId)
        {

            return _Context.EmployeeWorkHours.Where(e => e.EmployeeId.ToString() == employeeId && storeId == e.StoreId.ToString()).ToList();
     
        }

        public bool UpdateFeatureItem(StoreFeaturedItem updatedfeatureitem)
        {
           
            if(_Context.FeaturedItems.Where(f=>f.Id == updatedfeatureitem.Id).ToList().Count > 0)
            {
                _Context.FeaturedItems.RemoveRange(_Context.FeaturedItems.Where(f => f.Id == updatedfeatureitem.Id).FirstOrDefault());

                _Context.FeaturedItems.Add(updatedfeatureitem);
            }

            return Save();
        }

        public IEnumerable<StoresEmployees> GetStoresWorkSpaceFromEmployee(string employeeId)
        {
            var result= _Context.Employees.Where(e => e.UserEmployeeId.ToString() == employeeId).Include(e=>e.Store).ToList();

            return result;
        }
    }
}
