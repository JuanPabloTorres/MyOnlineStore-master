using MyOnlineStore.Application.Common.Interfaces.DataStore;
using MyOnlineStore.Application.Common.Interfaces.IViewModel;
using MyOnlineStore.Application.Common.Utilities;
using MyOnlineStore.Application.Presentation.ViewModels.Base;
using MyOnlineStore.Application.Presentation.Views.AdminScenarios;
using MyOnlineStore.Entities.Models.Stores;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace MyOnlineStore.Application.Presentation.ViewModels.StoresScenarios
{

  
    public class MyStoresViewModel:BaseViewModel,IMyStoresViewModel
    {

		private ObservableCollection<Store> mystores;

		public ObservableCollection<Store> MyStores
		{
			get { return mystores; }
			set { mystores = value;
				RaisePropertyChanged(() => MyStores);
			}
		}

		public ICommand SwipeDeleteCommand { get; set; }

		public MyStoresViewModel(IStoreDataStore storeData)
		{
			var data = storeData.GetAll();

			MyStores = new ObservableCollection<Store>(data);

            SwipeDeleteCommand = new Command<CommandEventData>(async (data) => {


                var eventArgs = data.Args as Syncfusion.ListView.XForms.SwipeEndedEventArgs;

                var selectedstore = eventArgs.ItemData as Store;

                var featureitemdata = storeData.GetFeaturedItemsOfStore(selectedstore.Id);

                if (eventArgs.SwipeDirection == Syncfusion.ListView.XForms.SwipeDirection.Right)
                {
                    if (eventArgs.SwipeOffset >= 100)
                    {
                        if (selectedstore != null)
                        {

                            //App.Store = selectedstore;
                            App.ApplicationManager.CurrenstStoreGuid = selectedstore.Id;
                            await Shell.Current.GoToAsync($"{AdminHomePage.Route}");
                   
                            // App.Store.FeaturedItems = new List<StoreFeaturedItem>(featureitemdata);
                           // await Shell.Current.Navigation.PushAsync(new AdminHomePage());
                        }
                    }

                }
               

            });
        }

	}
}
