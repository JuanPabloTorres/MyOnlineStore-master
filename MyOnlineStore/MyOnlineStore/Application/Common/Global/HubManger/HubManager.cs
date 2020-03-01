
using Microsoft.AspNetCore.SignalR.Client;
using MyOnlineStore.Application.Common.Interfaces.DataStore;
using MyOnlineStore.Application.Data.Presenters;
using MyOnlineStore.Shared.Models.Entries;
using MyOnlineStore.Shared.Models.Users;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MyOnlineStore.Application.Common.Global.HubManger
{
    public class HubManager
    {

        public HubConnection GetHubConnection { get; set; }

        IUserDataStore UserDataStore { get; set; }

        public UserConnection userConnection { get; set; }

        public HubManager(IUserDataStore userDataStore)
        {
            UserDataStore = userDataStore;

            //Connect();
            Task.Run(async () => await Connect());  
        }

       public async Task Connect()
        {
            try
            {

                var ip = "localhost";

                if (Device.RuntimePlatform == Device.Android)
                    ip = "192.168.1.132";


                GetHubConnection = new HubConnectionBuilder()
                .WithUrl($"http://{ip}:5000/hubs/notification")
                .Build();

                await GetHubConnection.StartAsync();

             

            }
            catch (Exception ex)
            {
                // Something has gone wrong
            }
        }

       public async Task Disconnect()
        {
            try
            {

              
                await GetHubConnection.StopAsync();



            }
            catch (Exception ex)
            {
                // Something has gone wrong
            }
        }

        public bool ConnectUser(string connectionId, Guid UsertoConnectId)
        {

             userConnection = new UserConnection()
            {
                Connected = true,
                ConnectionID = connectionId,
                UserId = UsertoConnectId,

            };

            var result = UserDataStore.CreateUserConnection(userConnection);

            return result;

        }

       public async Task SendMessage(JobRequest jobRequest)
        {
            try
            {
               
                await GetHubConnection.InvokeAsync("SendJobNotificationToSpecific", jobRequest);
            }
            catch (Exception ex)
            {
                // send failed
            }
        }

       //public void ReceivedNotification()
       // {
           

       //     GetHubConnection.On<JobRequest>("Notification", (jobrequest) =>
       //     {

       //         JobRequestPresenter presenter = new JobRequestPresenter()
       //         {
       //             ExpDate = jobrequest.ExpDate,
       //             SenderFullName = jobrequest.SenderFullName,
       //             UserReciverId = jobrequest.UserReciverId,
       //             Id = jobrequest.Id,

       //             SenderStoreName = jobrequest.SenderStoreName,
       //             StoreId = jobrequest.StoreId,
       //             UserSenderId = jobrequest.UserSenderId
       //         };
       //         //JobRequestPreseters.Add(presenter);

       //         //ObservableCollection<JobRequestPresenter> tempJobRequest = new ObservableCollection<JobRequestPresenter>(JobRequestPreseters);


               

       //         //JobRequestPreseters = new ObservableCollection<JobRequestPresenter>(tempJobRequest);
       //         // Update the UI
       //     });

       //     //return presenter;

       // }
    }
}
