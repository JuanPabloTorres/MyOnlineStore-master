using MyOnlineStore.Application.Data.Models.Presenters;
using MyOnlineStore.Application.Data.Presenters;
using MyOnlineStore.Entities.Models.Purchases;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyOnlineStore.Application.Common.Interfaces.Factories
{
    public interface IProductItemBuyPresenterFactory
    {
        ProductItemPresenter CreateProductBuyPresenter();
        ProductItemPresenter CreateProductBuyPresenter(ProductItemPresenter productPresenter);
        ProductItemPresenter CreateProductBuyPresenter(ProductItem product, uint selectQuantity = 0, float totalprice = 0.0f);
    }
}
