using MyOnlineStore.Application.Data.Models.Presenters;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyOnlineStore.Application.Common.Interfaces.Factories
{
    public interface IStorePresenterFactory
    {
        StorePresenter CreateSimpleStorePresenter();
        StorePresenter CreateStorePresenter(StorePresenter storePresenter);
        IEnumerable<StorePresenter> CreateStorePresenterCollection(IEnumerable<StorePresenter> stores);
    }
}
