using MyOnlineStore.Application.Common.Interfaces.DataStore;
using MyOnlineStore.Application.Common.Interfaces.IViewModel;
using MyOnlineStore.Application.Data.Presenters;
using MyOnlineStore.Application.Presentation.ViewModels.Base;
using MyOnlineStore.Entities.Models.Stores;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;

namespace MyOnlineStore.Application.Presentation.ViewModels.WorkSpaceScenarios
{
   public class WorkSpaceViewModel:BaseViewModel,IWorkSpaceViewModel
    {

        private ObservableCollection<StoresEmployees> workspace;

        public ObservableCollection<StoresEmployees> WorkSpace
        {
            get { return workspace; }
            set { workspace = value;
                RaisePropertyChanged(() => WorkSpace);
            }
        }

        private ObservableCollection<WorkSpacePresenter> workpresenter;

        public ObservableCollection<WorkSpacePresenter> WorkPresenter
        {
            get { return workpresenter; }
            set
            {
                workpresenter = value;
                RaisePropertyChanged(() => WorkPresenter);
            }
        }

        WorkSpacePresenter detailemployee;

        public WorkSpacePresenter DetailEmployee
        {
            get => detailemployee;

            set
            {
                detailemployee = value;
                RaisePropertyChanged(() => DetailEmployee);
            }
        }

        IStoresEmployeesDataStore StoresEmployeesDataStore;
        IStoreDataStore StoreDataStore;
        public WorkSpaceViewModel(IStoresEmployeesDataStore storesEmployeesData,IStoreDataStore dataStore)
        {
            StoresEmployeesDataStore = storesEmployeesData;
            StoreDataStore = dataStore;

            var workSpaceData = StoresEmployeesDataStore.GetStoresWorkSpaceFromEmployee(App.ApplicationManager.CurrentUser.Id.ToString());

            WorkSpace = new ObservableCollection<StoresEmployees>(workSpaceData);

            WorkPresenter = new ObservableCollection<WorkSpacePresenter>(DataToPresenter(WorkSpace));


            MessagingCenter.Subscribe<WorkSpacePresenter, WorkSpacePresenter>(this, "WorkSpaceDetail", (sender, arg) =>
            {

                DetailEmployee = arg;

                ////DetailEmployeeWorkHours = new ObservableCollection<EmployeeWorkHour>();
                //foreach (var item in arg.EmployeeWork)
                //{
                //    DetailEmployeeWorkHours.Add(item);
                //}


            });
           

        }

       void GetDetailInformation()
        {
          
        }

        private ObservableCollection<WorkSpacePresenter> DataToPresenter(ObservableCollection<StoresEmployees> Data)
        {
            ObservableCollection<WorkSpacePresenter> GetData = new ObservableCollection<WorkSpacePresenter>();

            foreach (var item in Data)
            {
                WorkSpacePresenter itempresenter = new WorkSpacePresenter(item,StoreDataStore);


                GetData.Add(itempresenter);
            }

            return GetData;
        }

    }

   

    


}
