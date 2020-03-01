using MyOnlineStore.Entities.Models.Purchases;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MyOnlineStore.Shared.Models.Purchases
{
   public class Offer
    {
        public Guid Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public Guid MyProductId { get; set; }

        //[ForeignKey("MyProductId")]
        //public ProductItem? Product { get; set; }

        public Guid StoreId { get; set; }

        public double Percent { get; set; }

        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public int BuyQuantity { get; set; }

        public double TotalPrice { get; set; }

        public double  BuyOne { get; set; }




    }
}
