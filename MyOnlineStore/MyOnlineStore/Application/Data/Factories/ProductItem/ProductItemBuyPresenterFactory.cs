using MyOnlineStore.Application.Data.Models.Presenters;
using MyOnlineStore.Application.Common.Interfaces.Factories;
using MyOnlineStore.Entities.Models.Purchases;
using System;
using System.Collections.Generic;
using System.Text;
using MyOnlineStore.Application.Data.Presenters;

namespace MyOnlineStore.Application.Data.Factories.ProductItemFactories
{
    public class ProductItemBuyPresenterFactory : IProductItemBuyPresenterFactory
    {
        public ProductItemPresenter CreateProductBuyPresenter()
        {
            return new ProductItemPresenter();
        }

        public ProductItemPresenter CreateProductBuyPresenter(ProductItemPresenter productPresenter)
        {
            return new ProductItemPresenter(productPresenter);
        }

        public ProductItemPresenter CreateProductBuyPresenter(ProductItem product, uint selectQuantity = 0, float totalprice = 0.0f)
        {
            return new ProductItemPresenter(product, selectQuantity, totalprice);
        }
    }
}
