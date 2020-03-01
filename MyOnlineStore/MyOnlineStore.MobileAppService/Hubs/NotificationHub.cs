using Microsoft.AspNetCore.SignalR;
using MyOnlineStore.MobileAppService.Context;
using MyOnlineStore.MobileAppService.Hubs.Mapping;
using MyOnlineStore.MobileAppService.Interfaces.Hub;
using MyOnlineStore.MobileAppService.Interfaces.Repositories;
using MyOnlineStore.Shared.Models.Entries;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MyOnlineStore.MobileAppService.Hubs
{

    public class NotificationHub:Hub
    {
        //private readonly static ConnectionMapping<string> _connections =
        //      new ConnectionMapping<string>();


        IUsersRepository UsersRepository;
        public NotificationHub(IUsersRepository usersRepository )
        {

            UsersRepository = usersRepository;
           // _userConnectionManager = userConnectionManager;
        }

       

        public async Task SendJobNotificationToSpecific(JobRequest jobsRequest)
        {

            var result=UsersRepository.GetUserConnection(jobsRequest.UserReciverId.ToString());

           await Clients.All.SendAsync("RecievedNotification", jobsRequest);
            
           // await  Clients.Client(result.ConnectionID).SendAsync("RecievedNotification", jobsRequest);

          

        }

      
    }
}
