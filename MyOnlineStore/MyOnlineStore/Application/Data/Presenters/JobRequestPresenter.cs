using MyOnlineStore.Application.Common.Interfaces.DataStore;
using MyOnlineStore.Application.Presentation.ViewModels.Base;
using MyOnlineStore.Shared.Models.Entries;
using MyOnlineStore.Shared.Models.Users;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace MyOnlineStore.Application.Data.Presenters
{
    public class JobRequestPresenter:BaseViewModel
    {
        public Guid UserReciverId { get; set; }

        public Guid UserSenderId { get; set; }

        public Guid StoreId { get; set; }

        public string SenderFullName { get; set; } = string.Empty;

        public string SenderStoreName { get; set; } = string.Empty;


        RequestFlag requestFlag;
        public RequestFlag RequestAnwser
        {
            get => requestFlag;

            set
            {
                requestFlag = value;
                RaisePropertyChanged(() => RequestAnwser);
            }

        }

        public DateTime ExpDate { get; set; }

        public Guid Id { get; set; }

        public bool IsAlive { get; set; }

        public ICommand CompleteCommand
        {
            get;set;


        }

        public ICommand DeclineCommand { get; set; }

        IUserDataStore UserDataStore { get; set; }

        public JobRequestPresenter(IUserDataStore dataStore)
        {
            UserDataStore = dataStore;

            CompleteCommand = new Command(() =>
            {

                JobRequest toUpdate = new JobRequest()
                {
                    Id = this.Id,
                    RequestAnwser = Shared.Models.Entries.RequestFlag.Approved,
                    SenderFullName = this.SenderFullName,
                    ExpDate = this.ExpDate,
                    IsAlive = true,
                    StoreId = this.StoreId,
                    UserReciverId = this.UserReciverId,
                    SenderStoreName = this.SenderStoreName,
                    UserSenderId = this.UserSenderId
                };

                var result = UserDataStore.UpdateRequest(toUpdate);

                if (result)
                {

                    MessagingCenter.Send<JobRequestPresenter, JobRequestPresenter>(this, "ToRemove", this);
                }
                
            });

            DeclineCommand = new Command( () =>
            {

                var request = new JobRequest()
                {

                    Id = this.Id,
                    ExpDate = this.ExpDate,
                    IsAlive = this.IsAlive,
                    SenderFullName = this.SenderFullName,
                    SenderStoreName = this.SenderStoreName,
                    StoreId = this.StoreId,
                    UserReciverId = this.UserReciverId,
                    UserSenderId = this.UserSenderId,
                     RequestAnwser=Shared.Models.Entries.RequestFlag.Rejected,
                     

                };


                var result = UserDataStore.DeclineRequest(request);

                if (result)
                {
                    MessagingCenter.Send<JobRequestPresenter, JobRequestPresenter>(this, "ToRemove", this);
                }
            
            });
        }
    }

    public enum RequestFlag
    {
        None,
        Approved,
        Rejected,

    };
}

