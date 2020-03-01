using MyOnlineStore.Application.Common.Interfaces.Factories;
using MyOnlineStore.Application.Data.Models.Presenters;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace MyOnlineStore.Application.Data.Factories.Store
{
    public class StorePresenterFactory : IStorePresenterFactory
    {
        public StorePresenter CreateSimpleStorePresenter()
        {
            return new StorePresenter();
        }

        public StorePresenter CreateStorePresenter(StorePresenter storePresenter)
        {
            var storepresenter = new StorePresenter(storePresenter);

            if(storepresenter.IsFavorite) 
                { storepresenter.FillHeart(); }
            else 
                { storepresenter.EmptyHeart(); }

            return storepresenter;
        }

        public IEnumerable<StorePresenter> CreateStorePresenterCollection(IEnumerable<StorePresenter> stores)
        {
            List<StorePresenter> _stores = new List<StorePresenter>();
            foreach(var store in stores)
            {
                _stores.Add(CreateStorePresenter(store));
            }
            return _stores;
        }
    }
}
