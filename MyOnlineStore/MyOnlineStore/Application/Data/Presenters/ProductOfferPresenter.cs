using MyOnlineStore.Application.Presentation.Managers;
using MyOnlineStore.Application.Presentation.ViewModels.Base;
using MyOnlineStore.Entities.Models.Purchases;
using MyOnlineStore.Shared.Models.Purchases;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using Microsoft.Extensions.DependencyInjection;

namespace MyOnlineStore.Application.Data.Presenters
{
   public class ProductOfferPresenter:BaseViewModel
    {
        public Guid Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public Guid MyProductId { get; set; }
        public ProductItem Product { get; set; }

        public Guid StoreId { get; set; }

        public double Percent { get; set; }

        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public int BuyQuantity { get; set; }

        public double TotalPrice { get; set; }

        public double BuyOne { get; set; }

        double quantity;
        public double Quantity { get=>quantity;
            set
            {
                quantity = value;
                RaisePropertyChanged(() => Quantity);
                
              
            } }

        public ShoppingManager ShoppingManager { get; set; }

        public ICommand AddToCartCommand { get; set; }

        public ProductOfferPresenter(Offer ItemOffer,ShoppingManager shoppingManager)
        {
            this.Title = ItemOffer.Title;
            this.Description = ItemOffer.Description;
            this.StartDate = ItemOffer.StartDate;
            this.EndDate = ItemOffer.EndDate;
            this.Id = ItemOffer.Id;
            this.MyProductId = ItemOffer.MyProductId;
            this.StoreId = ItemOffer.StoreId;
           // this.Product = ItemOffer.Product;
            this.TotalPrice = ItemOffer.TotalPrice;
            this.BuyOne = ItemOffer.BuyOne;
            this.Percent = ItemOffer.Percent;
            Quantity = 0;

            this.ShoppingManager = shoppingManager;

            AddToCartCommand = new Command<ProductOfferPresenter>((item) =>
            {
                var value = item;
                ProductItemPresenter itemPresenter = new ProductItemPresenter()
                {
                    Id = this.MyProductId,
                    Name = this.Product.Name,
                    Description = this.Product.Description,
                    Price = this.Product.Price,
                    Logo = this.Product.Image,
                    Quantity = this.Product.Quantity,
                    Category = this.Product.Category,
                    SelectedItemCount = Convert.ToUInt32(Quantity),
                    StoreId = StoreId,
                    TotalPriceOfSelectedItems = (float)(Quantity * this.Product.Price)
                };
                shoppingManager.AddProductItemCommand.Execute(itemPresenter);



            });
        }
    }
}
