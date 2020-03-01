using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyOnlineStore.MobileAppService.Interfaces.Repositories;
using MyOnlineStore.Entities.Models.Stores;
using MyOnlineStore.Entities.Models.DTOs.Stores;

using MyOnlineStore.Shared.Models.DTOs.Stores;
using MyOnlineStore.Entities.Models.Users;
using MyOnlineStore.Entities.Models.Roles;
using MyOnlineStore.Shared.Models.Roles;
using MyOnlineStore.Shared.Interfaces.Factories;
using MyOnlineStore.Shared.Utils;
using MyOnlineStore.Shared.Models.Entries;
using MyOnlineStore.Shared.Models.Stores;
using Microsoft.AspNetCore.SignalR;
using MyOnlineStore.MobileAppService.Hubs;

namespace MyOnlineStore.MobileAppService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StoreController : ControllerBase
    {
        #region Fields

        private readonly IStoresRepository _StoreRepository;
        private readonly IUserCardRepository _CardRepo;
        private readonly IUsersRepository _UserRepo;
        private readonly IRoleFactory _RoleFactory;

        IHubContext<NotificationHub> NotificationHubContext;

        #endregion Fields

        #region Constructors

        public StoreController(IStoresRepository storesRepository, IUserCardRepository cardRepository,IUsersRepository usersRepository, IRoleFactory roleFactory , IHubContext<NotificationHub> hub)
        {
            _StoreRepository = storesRepository;
            _CardRepo = cardRepository;
            _UserRepo = usersRepository;
            _RoleFactory = roleFactory;

            NotificationHubContext = hub;
        }

        #endregion Constructors

        #region Methods

        [HttpGet("[action]/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public Store GetbyId(Guid id)
        {
            var result = _StoreRepository.Get(id);
            return result;
        }

        [HttpGet("[action]/{name}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public Store GetByName(string name)
        {
            var result = _StoreRepository.GetByName(name);
            return result;
        }


        [HttpGet("[action]/{ownerName}/{storeName}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<Store> GetStore([FromRoute]string ownerName, [FromRoute]string storeName, [FromBody]Location location)
        {
            var result = await _StoreRepository.GetStore(name: storeName,
                ownerName: ownerName,
                location: location);
            return result;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IEnumerable<Store> GetAll()
        {
            var result = _StoreRepository.GetAll();
            return result;
        }

        [HttpGet("[action]/{viewingUserId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public List<StorePresenterDTO> GetAllToExplore([FromRoute]string viewingUserId)
        {
            Guid guid = Guid.Parse(viewingUserId);
            var result = new List<StorePresenterDTO>(_StoreRepository.GetStoresToExplore(guid));
            return result;
        }

        [HttpGet("[action]/{clientId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IEnumerable<StorePresenterDTO> GetClientFavorites(string clientId)
        {
            Guid id;
            IEnumerable<StorePresenterDTO> result = null;

            if (Guid.TryParse(clientId,out id))
            {
                result = _StoreRepository.GetClientFavorites(id);
            }

            return result;
        }

        [HttpGet("[action]/{storeowner}/{storename}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public bool StoreExists([FromRoute]string storeOwner, [FromRoute]string storeName, [FromBody]Location location)
        {
            bool exists = _StoreRepository.StoreExists(storeOwner, storeName, location);
            return exists;
        }

        //=============================================================================
        //
        // TODO: AddStore,GetClientsStoresNearBy, UpdateStore
        //
        //=============================================================================
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public bool UpdateStore([FromBody]Store store)
        {
            bool updated = _StoreRepository.Update(store);
            return updated;
        }

        [HttpPost(template:"[action]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public bool AddFeaturedItem([FromBody]List<StoreFeaturedItem> featuredItems)
        {
            bool updated = _StoreRepository.AddFeaturedItems(featuredItems);
            return updated;
        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public bool CreateStore([FromBody]StoreDTO store)
        {
            var s = store.ToStore();
            bool exists = _StoreRepository.Add(s);
            return exists;
        }

        [HttpGet("[action]/{ownerId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IEnumerable<Store> GetStoresByOwnerId(string ownerId)
        {
            var list = _StoreRepository.GetStoresByOwnerId(ownerId);
            return list;
        }

        [HttpGet("[action]/{storeId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public StoreDTO GetStoreWithInventory(string storeId)
        {
            Guid guid = Guid.Parse(storeId);
            var store = _StoreRepository.GetStoreWithInventory(guid);
            var storeDto = new StoreDTO(store);
            return storeDto;
        }

        [HttpGet("[action]/{storeId}/{clientId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public bool IsFavorite(string storeId, string clientId)
        {
            Guid storeGuid, clientGuid;
            bool isFav = false;

            if (Guid.TryParse(storeId,out storeGuid) && Guid.TryParse(clientId, out clientGuid))
            {
                isFav = _StoreRepository.IsFavorite(storeGuid, clientGuid);
            }

            return isFav;
        }


        [HttpPost("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ApiResponse<User> RegisterStoreAction(StoreRegistrationEntry storeRegistration)
        {
            ApiResponse<User> response = new ApiResponse<User>();
            bool accountExist;
            bool savedAccount;
            bool savedStore;
            bool storeExist;
            bool addedRoleToUser;
            Role adminRole;

            try
            {
                if(storeRegistration.Store is object && 
                    storeRegistration.Account is object && 
                    storeRegistration.User is object &&
                    storeRegistration.Store.HasAllRequieredFieldsForRegistration() && storeRegistration.Account.HasAllRequierdFields() && storeRegistration.User.HasId())
                {
                    //Store
                    storeExist = _StoreRepository.StoreExists(
                            storeRegistration.Store.StoreOwnerName,
                            storeRegistration.Store.StoreName,
                            storeRegistration.Store.Location!
                        );

                    if(storeExist)
                    {
                        storeRegistration.Store.DateUpdated = DateTime.UtcNow;
                        savedStore = _StoreRepository.Update(storeRegistration.Store); 
                    }
                    else
                    {
                        storeRegistration.Store.DateCreated = DateTime.UtcNow;
                        savedStore = _StoreRepository.Add(storeRegistration.Store); 
                    }

                    // Card
                    accountExist = _CardRepo.CardExist(storeRegistration.Account.Id);

                    storeRegistration.Account.MyUserId = storeRegistration.User.Id;
                    storeRegistration.Account.MyStoreId = storeRegistration.Store.Id;

                    if(accountExist)
                    { savedAccount = _CardRepo.Update(storeRegistration.Account); }
                    else
                    { savedAccount = _CardRepo.Add(storeRegistration.Account); }

                    // Role
                    adminRole = _UserRepo.GetAdminRole();
                    UsersRoles userRole = _RoleFactory.CreateUserRole(
                            userId: storeRegistration.User.Id,
                            storeId: storeRegistration.Store.Id,
                            roleId: adminRole.Id
                        );

                    addedRoleToUser = _UserRepo.AddUserRole(userRole);

                    if(savedAccount is false || savedStore is false || addedRoleToUser is false)
                    {
                        response.Failure.ErrorsMessages.Add($"Can't complete the registration process. Try again.");
                    }
                    else
                    {
                        response.Success.SuccessMessages.Add($"The registration if the store has been succesfull.");
                        response.Success.Data = _UserRepo.GetUserByEmailWithAll(storeRegistration.User.Email);

                        if(response.Success.Data.MyRoles is object)
                        {
                            foreach(var role in response.Success.Data.MyRoles)
                            {
                                role.User = null;
                                role.Store = null;
                            }
                        }

                        if(response.Success.Data.MyStores is object)
                        {
                            foreach(var store in response.Success.Data.MyStores)
                            {
                                //store.
                            }
                        }
                    }
                }
                else
                {
                    response.Failure.ErrorsMessages.Add($"Can't complete the registration process due to uncompleted data.");
                }
            }
            catch(Exception ex)
            {

                response.Failure.ErrorsMessages.Add($"Opps, something wrong happened... Try Again!");
            }

            if(response.Failure.ErrorsMessages.Count < 1) 
                { response.IsSuccess = true; }

            return response;
        }


        //Obtener los feature items de un store en especifico
        [HttpGet("[action]/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IEnumerable<StoreFeaturedItem> GetFeaturedItemsOfStore(Guid id)
        {
            return _StoreRepository.GetStoreFeaturedItems(id);
        }

        [HttpDelete(template: "[action]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public bool RemoveFeatureItem([FromBody]StoreFeaturedItem featuredItems)
        {

            bool updated = _StoreRepository.RemoveFeatureItem(featuredItems);
            return updated;
        }

        [HttpPut(template: "[action]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public bool UpdateFeatureItem(StoreFeaturedItem updatefeatureItem)
        {
            var result = _StoreRepository.UpdateFeatureItem(updatefeatureItem);

            return result;
        }

        [HttpDelete("[action]/{userId}/{storeId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public bool RemoveEmployee(string userId, string storeId)
        {

            bool removed = _StoreRepository.RemoveEmployee(userId,storeId);

            if(removed)
            {
               bool result =  _UserRepo.RemoveRequest(userId, storeId);
            }
            return removed;
        }

        [HttpPut("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public bool AddEmployeeWorkHours(List<EmployeeWorkHour> employeeWorkHours)
        {
            var result = _StoreRepository.AddEmployeeWorkHours(employeeWorkHours);

            return result;
        }

        [HttpPut("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public bool UpdateEmployee(StoresEmployees updatedEmployee)
        {

            var result = _StoreRepository.UpdateStoreEmployee(updatedEmployee);         

           
            return result;
          
        }

        [HttpGet("[action]/{employeeId}/{storeId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public StoresEmployees GetStoreEmployee(string employeeId,string storeId)
        {

            var result = _StoreRepository.GetStoreEmployee(employeeId,storeId);

            return result;

        }

        //Obtener los work hours de un employee en especifico store
        [HttpGet("[action]/{employeeId}/{storeId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
       public IEnumerable<EmployeeWorkHour> GetEmployeeWorkHoursOfStore(string employeeId, string storeId)
        {
            return _StoreRepository.GetEmployeeWorkHoursOfStore(employeeId, storeId);

            //return _StoreRepository.GetStoreFeaturedItems(id);
        }



    #endregion Methods

}
}