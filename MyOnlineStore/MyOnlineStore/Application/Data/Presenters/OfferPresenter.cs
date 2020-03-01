using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace MyOnlineStore.Application.Data.Presenters
{
   public class OfferPresenter
    {
        public Guid Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public Guid MyProductId { get; set; }

        public Guid StoreId { get; set; }

        public double Percent { get; set; }

        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public int BuyQuantity { get; set; }

        public double TotalPrice { get; set; }

        public double BuyOne { get; set; }

        public string ProductName { get; set; }


        public ICommand AddToCartCommand { get; set; }
        public OfferPresenter()
        {

        }

    }
}
