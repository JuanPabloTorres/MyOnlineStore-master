
using Microsoft.AspNetCore.SignalR.Client;
using MyOnlineStore.Application.Common.Interfaces.DataStore;
using MyOnlineStore.Application.Common.Interfaces.IViewModel;
using MyOnlineStore.Application.Data.Presenters;
using MyOnlineStore.Application.Presentation.ViewModels.Base;
using MyOnlineStore.Shared.Models.Entries;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace MyOnlineStore.Application.Presentation.ViewModels.Notification
{
    public class NotificationViewModel:BaseViewModel,INotificationViewModel
    {

		public INotificationDataStore NotificationDataStore { get; set; }

		IUserDataStore userDataStore { get; set; }

		private ObservableCollection<JobRequest> jobRequest;

		public ObservableCollection<JobRequest> JobRequests 
		{
			get { return jobRequest; }
			set 
			{ 
				jobRequest = value;

				RaisePropertyChanged(() => JobRequests);
			}
		}


		private ObservableCollection<JobRequestPresenter> jobrequestpresenter;

		public ObservableCollection<JobRequestPresenter> JobRequestPreseters
		{
			get { return jobrequestpresenter; }
			set
			{
				jobrequestpresenter = value;

				RaisePropertyChanged(() => JobRequestPreseters);
			}
		}


		HubConnection hubConnection;
		public NotificationViewModel(INotificationDataStore notificationData, IUserDataStore dataStore)
		{
			NotificationDataStore = notificationData;

			userDataStore = dataStore;

			JobRequestPreseters = new ObservableCollection<JobRequestPresenter>();	

			GetRequest();

			App.HubManager.GetHubConnection.On<string>("Notification", (item) =>
			{

				MainThread.BeginInvokeOnMainThread(() =>
				{
					try
					{
						UpdateUI(item);
					}
					catch (Exception ex)
					{
						App.Current.MainPage.DisplayAlert("Notification Error", ex.Message, "OK");
					}
				});


			});

			MessagingCenter.Subscribe<JobRequestPresenter, JobRequestPresenter>(this, "ToRemove", (sender, arg) =>
			{

				var result = JobRequestPreseters.Remove(arg);

				//var tempdata = JobRequestPreseters;

				//JobRequestPreseters = new ObservableCollection<JobRequestPresenter>(tempdata);
			});

		}

		public async  void GetRequest()
		{
			var requestdata = await NotificationDataStore.GetJobRequest(App.ApplicationManager.CurrentUser.Id);
			JobRequests = new ObservableCollection<JobRequest>(requestdata);


			foreach (var item in JobRequests)
			{

                if (item.RequestAnwser != Shared.Models.Entries.RequestFlag.Approved)
                {

                    JobRequestPreseters.Add(
                        new JobRequestPresenter(userDataStore)
                        {
                            ExpDate = item.ExpDate,
                            SenderFullName = item.SenderFullName,
                            SenderStoreName = item.SenderStoreName,
                            Id = item.Id,
                            IsAlive = item.IsAlive,
                            UserReciverId = item.UserReciverId,
                            UserSenderId = item.UserSenderId,
                            StoreId = item.StoreId,
                        });

                }
            }

		}

		public void UpdateUI(string json)
		{
			var item = JsonConvert.DeserializeObject<JobRequest>(json);

			JobRequestPresenter data = new JobRequestPresenter(userDataStore)
			{
				ExpDate = item.ExpDate,
				SenderFullName = item.SenderFullName,
				SenderStoreName = item.SenderStoreName,
				Id = item.Id,
				IsAlive = item.IsAlive,
				UserReciverId = item.UserReciverId,
				UserSenderId = item.UserSenderId,
				StoreId = item.StoreId,
			};

			JobRequestPreseters.Add(data);

			ObservableCollection<JobRequestPresenter> templist = new ObservableCollection<JobRequestPresenter>(JobRequestPreseters);

			JobRequestPreseters = new ObservableCollection<JobRequestPresenter>(templist);

		}

	}
}
