using MyOnlineStore.Application.Common.Interfaces.DataStore;
using MyOnlineStore.Application.Presentation.Views.AdminScenarios.Stores.EmployeeStepsView;
using MyOnlineStore.Entities.Models.Stores;
using MyOnlineStore.Shared.Models.Stores;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace MyOnlineStore.Application.Data.Presenters
{
    public class EmployeePresenter
    {
        public Guid Id { get; set; }

        public Guid UserEmployeeId { get; set; }

        public Guid StoreId { get; set; }
        public Store? Store { get; set; }

        public string FullName { get; set; }

        public List<EmployeeWorkHour> EmployeeWork { get; set; }

        public Status EmployeeStatus { get; set; }

        public Position EmployeePosition { get; set; }

        public ICommand EditCommand { get; set; }

        public ICommand DeleteCommand { get; set; }

        public ICommand DetailCommand { get; set; }

        IStoreDataStore StoreDataStore;

        public bool IsAlive { get; set; }

        public EmployeePresenter(StoresEmployees storesEmployees, IStoreDataStore storeDataStore)
        {
            this.Id = storesEmployees.Id;
            this.UserEmployeeId = storesEmployees.UserEmployeeId;
            this.StoreId = storesEmployees.StoreId;
            this.IsAlive = storesEmployees.IsAlive;
            this.EmployeeWork = storesEmployees.EmployeeWork;
            this.FullName = storesEmployees.FullName;
            //this.EmployeeStatus = storesEmployees.EmployeeStatus;
            this.EmployeePosition = storesEmployees.EmployeePosition;

            StoreDataStore = storeDataStore;

            DeleteCommand = new Command(() => 
            {

                var result = storeDataStore.RemoveEmployee(UserEmployeeId.ToString(),StoreId.ToString());

                if (result)
                {
                    MessagingCenter.Send<EmployeePresenter, EmployeePresenter>(this, "ToRemove", this);
                }


            });

            EditCommand = new Command(async () => 
            {
                await Shell.Current.GoToAsync($"{ConfigureEmployeePage.Route}?" + $"employeeID={UserEmployeeId.ToString()}");

            

            });

            DetailCommand = new Command(async () => 
            {
               var work_hour_data = StoreDataStore.GetEmployeeWorkHoursOfStore(UserEmployeeId.ToString(), StoreId.ToString());

                EmployeeWork = new List<EmployeeWorkHour>(work_hour_data);
                MessagingCenter.Send<EmployeePresenter, EmployeePresenter>(this, "Detail", this);

                await Shell.Current.GoToAsync($"{EmployeeDetailView.Route}");





            });
            
        }
    }
}
