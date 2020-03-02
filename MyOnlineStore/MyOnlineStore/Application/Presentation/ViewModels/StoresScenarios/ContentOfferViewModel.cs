 
using MyOnlineStore.Application.Common.Interfaces.DataStore;
using MyOnlineStore.Application.Common.Interfaces.IViewModel;
using MyOnlineStore.Application.Data.Presenters;
using MyOnlineStore.Application.Presentation.Managers;
using MyOnlineStore.Application.Presentation.ViewModels.Base;
using MyOnlineStore.Shared.Models.Purchases;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;
using Microsoft.Extensions.DependencyInjection;
using MyOnlineStore.Entities.Models.Purchases;

namespace MyOnlineStore.Application.Presentation.ViewModels.StoresScenarios
{
    [QueryProperty("StoreId", "storeId")]
   public  class ContentOfferViewModel:BaseViewModel,IOfferContentViewModel
    {

        private ObservableCollection<ProductItem> storeoffers;
        public ObservableCollection<ProductItem> StoreOffers
        {
            get { return storeoffers; }
            set { storeoffers = value;
                RaisePropertyChanged(()=>StoreOffers);
            }
        }

        private ObservableCollection<ProductOfferPresenter> productofferpresenters;

        public ObservableCollection<ProductOfferPresenter> ProductOfferPresenters
        {
            get { return productofferpresenters; }
            set { productofferpresenters = value;
                RaisePropertyChanged(() => ProductOfferPresenters);
            }
        }

        private string storeid;

        public string StoreId
        {
            get { return storeid; }
            set { storeid = value;

                var productItems = ProductItemDataStore.GetInventoryForStore(StoreId);
                StoreOffers = new ObservableCollection<ProductItem>(productItems);
                SetDataToPresenter(StoreOffers);
            }
        }



        IProductItemDataStore ProductItemDataStore;

        public ShoppingManager ShoppingManager { get; set; }
        public ContentOfferViewModel(IProductItemDataStore productItemData,ShoppingManager shoppingManager)
        {
            ProductItemDataStore = productItemData;

            ShoppingManager = shoppingManager;

            //var offers = ProductItemDataStore.GetOfferOfStore(App.ApplicationManager.CurrenstStoreGuid.ToString());


            var products = ProductItemDataStore.GetInventoryForStore(StoreId);
            //StoreOffers = new ObservableCollection<Offer>(offers);
            //SetDataToPresenter(StoreOffers);
          
           

        }

        void SetDataToPresenter(ObservableCollection<ProductItem> OfferData)
        {
            ProductOfferPresenters = new ObservableCollection<ProductOfferPresenter>();
            foreach (var item in OfferData)
            {
                var ProductOfferPresenter = new ProductOfferPresenter(item,Startup.ServiceProvider.GetService<ShoppingManager>());

                ProductOfferPresenters.Add(ProductOfferPresenter);
            }
        }
    }
}
