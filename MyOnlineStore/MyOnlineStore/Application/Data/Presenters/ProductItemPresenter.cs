using MyOnlineStore.Application.Presentation.ViewModels.Base;
using MyOnlineStore.Entities.Models.Purchases;
using MyOnlineStore.Shared.Interfaces.Factories;
using System;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Text;
using MyOnlineStore.Shared.Models.Purchases;

namespace MyOnlineStore.Application.Data.Presenters
{
   public class ProductItemPresenter:BaseViewModel
    {
        private uint selectedItemCount = uint.MinValue;
        private float totalPriceOfSelectedItem = 0.0f;
        public Guid Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public double Price { get; set; } = 0.0;

        public byte[]? Logo { get; set; }

        public uint Quantity { get; set; } = uint.MinValue;

        public string Category { get; set; } = string.Empty;
        public uint SelectedItemCount
        {
            get => selectedItemCount;
            set { selectedItemCount = value; RaisePropertyChanged(() => SelectedItemCount); }
        }

        public float TotalPriceOfSelectedItems
        {
            get => totalPriceOfSelectedItem;
            set { totalPriceOfSelectedItem = value; RaisePropertyChanged(() => TotalPriceOfSelectedItems); }
        }
        public Guid StoreId { get; set; }

        public Offer ProductOffer { get; set; }

        public bool hasoffer { get; set; }
        public bool HasOffer
        { get => hasoffer;
            set {

                hasoffer = value;

                RaisePropertyChanged(() => HasOffer);
            } }

        public ProductItemPresenter()
        {

        }


        public ProductItemPresenter(ProductItemPresenter product)
        {
            Id = product.Id;
            Name = product.Name;
            Description = product.Description;
            Price = product.Price;
            Logo = product.Logo;
            Quantity = product.Quantity;
            Category = product.Category;
            SelectedItemCount = product.SelectedItemCount;
            StoreId = product.StoreId;
            TotalPriceOfSelectedItems = product.TotalPriceOfSelectedItems;
        }


        public ProductItemPresenter(ProductItemPresenter product,Offer offer)
        {
            Id = product.Id;
            Name = product.Name;
            Description = product.Description;
            Price = product.Price;
            Logo = product.Logo;
            Quantity = product.Quantity;
            Category = product.Category;
            SelectedItemCount = product.SelectedItemCount;
            StoreId = product.StoreId;
            TotalPriceOfSelectedItems = product.TotalPriceOfSelectedItems;
            this.ProductOffer = offer;
        }


        public ProductItemPresenter(ProductItem product, uint selectQuantity = 0, float totalprice = 0.0f)
        {
            Id = product.Id;
            Name = product.Name;
            Description = product.Description;
            Price = product.Price;
            Logo = product.Image;
            Quantity = product.Quantity;
            Category = product.Category;
            StoreId = product.MyStoreId;
            SelectedItemCount = selectQuantity;
            TotalPriceOfSelectedItems = totalprice;
            HasOffer = product.HasOffer();
        }


        public ProductItemPresenter(ProductItem product)
        {
            Id = product.Id;
            Name = product.Name;
            Description = product.Description;
            Price = product.Price;
            Logo = product.Image;
            Quantity = product.Quantity;
            Category = product.Category;
            StoreId = product.MyStoreId;
            HasOffer = product.HasOffer();
           
        }


        public ProductItem ToProductItem()
        {
            return Startup.ServiceProvider.GetService<IProductItemFactory>()
                 .CreateProductItem(
                    itemName: Name,
                    price: Price.ToString(),
                    description: Description,
                    quantity: Quantity.ToString(),
                    category: Category,
                    logo: Logo,
                    storeId: StoreId.ToString(),
                    id: Id
                );
        }
    }
}
