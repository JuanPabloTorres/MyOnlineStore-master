using MyOnlineStore.Entities.Models.Stores;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;

namespace MyOnlineStore.Application.Common.Interfaces.IViewModel
{
    public interface IMyStoresViewModel
    {
         ObservableCollection<Store> MyStores { get; set; }
         ICommand SwipeDeleteCommand { get; set; }
    }
}
