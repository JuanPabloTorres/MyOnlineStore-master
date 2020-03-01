using MyOnlineStore.Application.Common.Interfaces.DataStore;
using MyOnlineStore.Application.Data.Presenters;
using MyOnlineStore.Entities.Models.Stores;
using MyOnlineStore.Entities.Models.Users;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;

namespace MyOnlineStore.Application.Common.Interfaces.IViewModel
{
   public interface IEmployeeViewModel
    {
        public ObservableCollection<EmployeePresenter> Employees { get; set; }

        public ObservableCollection<User> Users { get; set; }

        public ICommand SearchEmployeeCommand { get; set; }

        public ICommand SearchCommand { get; set; }
        public ICommand SwipeRequestCommand { get; set; }

        public string StoreId { get; set; }

        IStoresEmployeesDataStore EmployeeDataStore { get; set; }
        IUserDataStore UserDataStore { get; set; }
      

        public string EmployeeName { get; set; }

        public string SearchValue { get; set; }


        public string EmployeePhoneNumber { get; set; }

        public string EmployeeEmail { get; set; }

       

       
        public TimeSpan EndTime { get; set; }
    }
}
