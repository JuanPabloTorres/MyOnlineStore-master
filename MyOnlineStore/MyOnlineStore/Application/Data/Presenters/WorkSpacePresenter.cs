using MyOnlineStore.Application.Common.Interfaces.DataStore;
using MyOnlineStore.Application.Presentation.Views.WorkSpace;
using MyOnlineStore.Entities.Models.Stores;
using MyOnlineStore.Shared.Models.Stores;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace MyOnlineStore.Application.Data.Presenters
{
   public class WorkSpacePresenter
    {
        public Guid Id { get; set; }

        public Guid UserEmployeeId { get; set; }

        public Guid StoreId { get; set; }
        public Store Store { get; set; }

        public string FullName { get; set; }

        public Position EmployeePosition { get; set; }

        public List<EmployeeWorkHour>? EmployeeWork { get; set; }

        public bool IsAlive { get; set; }

        public ICommand DetailCommand { get; set; }
        public ICommand DeleteCommand { get; set; }

        IStoreDataStore StoreDataStore;

        public WorkSpacePresenter(StoresEmployees storesEmployees,IStoreDataStore dataStore)
        {
            this.Store = storesEmployees.Store;
            this.EmployeePosition = storesEmployees.EmployeePosition;
            this.Id = storesEmployees.Id;
            this.UserEmployeeId = storesEmployees.UserEmployeeId;
            this.FullName = storesEmployees.FullName;

            StoreDataStore = dataStore;

            DetailCommand = new Command(async () => 
            {
                var work_hour_data = StoreDataStore.GetEmployeeWorkHoursOfStore(UserEmployeeId.ToString(), this.Store.Id.ToString());

              this. EmployeeWork = new List<EmployeeWorkHour>(work_hour_data);
                MessagingCenter.Send<WorkSpacePresenter, WorkSpacePresenter>(this, "WorkSpaceDetail", this);
                await Shell.Current.GoToAsync($"{DetailWorkSpace.Route}");

            });



        }
    }
}
