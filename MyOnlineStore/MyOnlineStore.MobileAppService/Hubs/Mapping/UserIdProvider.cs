using Microsoft.AspNet.SignalR;
using MyOnlineStore.MobileAppService.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyOnlineStore.MobileAppService.Hubs.Mapping
{
    public class UserIdProvider : IUserIdProvider
    {

       

        public UserIdProvider()
        {
           
        }

        public string GetUserId(IRequest request)
        {
            //var userId = UsersRepository.GetUserConnection(request.User.Identity.Name);

            //return userId.ConnectionID;

            var x= string.Empty;
             return  x;
        }
    }
}
