using Microsoft.AspNet.SignalR.Client.Http;
using Microsoft.AspNetCore.SignalR;
using System;

namespace MyOnlineStore.MobileAppService.Interfaces.Hub.HubManegement
{
    public class SignalRUserIdProvider : IUserIdProvider
    {
        //public string GetUserId(IRequest connection)
        //{
        //    return Convert.ToString(connection.User.Identity.GetUserId<int>());
        //}

        public string GetUserId(HubConnectionContext connection)
        {
            return connection.User.Identity.Name;
        }
    }
}
