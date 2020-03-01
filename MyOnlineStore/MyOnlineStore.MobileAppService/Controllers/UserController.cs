using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using MyOnlineStore.Entities.Models.Models.DTOs;
using MyOnlineStore.Entities.Models.Roles;
using MyOnlineStore.Entities.Models.Users;
using MyOnlineStore.MobileAppService.Hubs;
using MyOnlineStore.MobileAppService.Interfaces.Repositories;
using MyOnlineStore.Models.Utils;
using MyOnlineStore.Shared.Interfaces.Factories;
using MyOnlineStore.Shared.Models.DTOs.Users;
using MyOnlineStore.Shared.Models.Entries;
using MyOnlineStore.Shared.Models.Roles;
using MyOnlineStore.Shared.Models.Users;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

//=====================================================================================
//
// TODO: Change To Task type all the needed ones
// TODO: Implementen [Authorize] of controllers and Action. (See links)
// TODO: Check how to validate password. Hash with Hash? Pass hash?
//
//=====================================================================================
namespace MyOnlineStore.MobileAppService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[GenericControllerName]
    public class UserController : ControllerBase
    {
        #region Fields

        private readonly IUsersRepository _UsersRepo;
        private readonly IRoleFactory _RoleFactory;
        private readonly IEmployeeRepository employeeRepository;
        private readonly IStoresRepository storesRepository;

        IHubContext<NotificationHub> NotificationHubContext; 
        #endregion Fields

        #region Constructors

        public UserController(IUsersRepository usersRepository, IRoleFactory roleFactory,IEmployeeRepository employeeRepository,IHubContext<NotificationHub> hub,IStoresRepository storesRepository)
        {

            NotificationHubContext = hub;
            _UsersRepo = usersRepository;
            _RoleFactory = roleFactory;
            this.storesRepository = storesRepository;
            this.employeeRepository = employeeRepository;
        }

        #endregion Constructors

        #region Methods

        // GET: api/ClientUser/5
        [HttpGet("[action]/{id}")]
        public User GetById(Guid id)
        {
            var value = _UsersRepo.Get(id);
            return value;
        }

        [HttpGet("[action]/{email}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public User GetUserByType(string email)
        {
            var value = _UsersRepo.GetUserByType(email);
            return value;
        }

        [HttpGet("[action]/{email}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<UserDTO?> GetUserByEmail(string email)
        {
            UserDTO? userDto;
            User? user = await _UsersRepo.GetUserByEmail(email);

            if (user is null) { userDto = null; }
            else { userDto = new UserDTO(user); }

            return userDto;
        }

        // POST: api/ClientUser
        [HttpPost]
        public void Create([FromBody] User user)
        {
            if(user != null)
            {
                _UsersRepo.Add(user);
            }
        }

        // PUT: api/ClientUser/5
        [HttpPut]
        public bool Put([FromBody]User user)
        {
            bool added;
            try
            {
                _UsersRepo.Update(user);
                added = true;
            }
            catch(Exception ex)
            {
                added = false;
                Console.WriteLine($"Exception in User Controller: {ex.Message}");
            }

            return added;
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public bool Delete(int id)
        {
            return false;
        }

        //====================================================================================
        //
        // TODO: Check the proper way to validated password hash
        //
        [HttpGet("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ValidUserCreateableToken GetValidUserCreateableToken([FromBody]UserLoginDTO userLoginDTO)
        {
            //Tuple<bool,string> response = _UsersRepo.ValidateUserCrendentialsAndGetType
            //      (userLoginDTO.Email, userLoginDTO.Password).ToTuple();
            var token = new ValidUserCreateableToken();
            return token;
        }

        [HttpGet("[action]/{userId}/{storeId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public User AttatchAdminRole([FromRoute]Guid userId, [FromRoute]Guid storeId)
        {
            User user;
            Role adminRole = _UsersRepo.GetAdminRole();
            UsersRoles userRole = _RoleFactory.CreateUserRole(userId: userId, storeId: storeId, roleId: adminRole.Id);

            _UsersRepo.AddUserRole(userRole);
            user = _UsersRepo.Get(userId);

            return user;
        }

        [HttpGet("[action]/{filter}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IEnumerable<User> GetUser(string filter)
        {

            var users = this.employeeRepository.GetUsers(filter);

            return users;
        }

       



        #region Request Actions

        [HttpPut("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<bool> SendRequest([FromBody]JobRequest request)
        {
            var requestresponse = this.employeeRepository.SendRequest(request);

            if (requestresponse)
            {
                var user = _UsersRepo.GetUserConnection(request.UserReciverId.ToString());

                await NotificationHubContext.Clients.Client(user.ConnectionID).SendAsync("Notification", JsonConvert.SerializeObject(request));
            }


            return requestresponse;
        }

        [HttpPost("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public  bool DeclineRequest(JobRequest request)
        {

            var result = _UsersRepo.DeclineRequest(request);

            return result;
        }

        [HttpPut("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<bool> UpdateRequest(JobRequest toUpdate)
        {

            //Modificar el job request enviado
            var result = _UsersRepo.UpdateRequest(toUpdate);

            if (result)
            {
                //Obtener el usuario al que se le envio el job request
                var userdata = _UsersRepo.Get(toUpdate.UserReciverId);


                //Agregar el nuevo usuario como empleado de la tienda si ya es usuario no se insertara
                var newEmployeeAdded = storesRepository.AddEmployeeToStore(userdata, toUpdate.StoreId);
                if (newEmployeeAdded)
                {
                    //Obtener la connecion del usario
                    var user = _UsersRepo.GetUserConnection(toUpdate.UserReciverId.ToString());

                    var sender = _UsersRepo.GetUserConnection(toUpdate.UserSenderId.ToString());
                    //Enviar notification al sender de que el request fue aceptado
                    await NotificationHubContext.Clients.Client(sender.ConnectionID).SendAsync("RequestAccepted", JsonConvert.SerializeObject(toUpdate));
                }
            }

            return result;
        }

        #endregion

        #region Hub Connection Actions

        [HttpPost("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public  bool CreateUserConnection([FromBody] UserConnection userConnection)
        {

            var result = _UsersRepo.CreateUserConnection(userConnection);

          
            return result;
         

        }

        [HttpPost("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public bool UpdateUserConnection([FromBody] UserConnection userConnection)
        {

            var result = _UsersRepo.UpdateUserConnection(userConnection);

            return result;


        }


        [HttpPost("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public  bool RemoveUserConnection([FromBody] UserConnection userConnection)
        {

            var result = _UsersRepo.RemoveUserConnection(userConnection);

            return result;


        }
        #endregion



        #endregion Methods
    }
}