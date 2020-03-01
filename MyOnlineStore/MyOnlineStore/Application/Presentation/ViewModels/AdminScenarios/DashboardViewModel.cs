using MyOnlineStore.Application.Common.Interfaces.IViewModel;
using MyOnlineStore.Application.Presentation.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace MyOnlineStore.Application.Presentation.ViewModels.AdminScenarios
{
   public  class DashboardViewModel:BaseViewModel,IReportViewModel
    {

        public ICommand FilterCommand { get; set; }

        private DateTime fromdatetime;

        public DateTime FromDateTime
        {
            get { return fromdatetime; }
            set { fromdatetime = value;
                RaisePropertyChanged(() => FromDateTime);
            }
        }

        private DateTime todatetime;

        public DateTime ToDateTime
        {
            get { return todatetime; }
            set { todatetime = value;
                RaisePropertyChanged(() => ToDateTime);
            }
        }

        private string selectedunittime;

        public string SelectedUnitTime
        {
            get { return selectedunittime; }
            set { selectedunittime = value;
                RaisePropertyChanged(() => SelectedUnitTime);
            }
        }





        public DashboardViewModel()
        {

        }
    }
}
