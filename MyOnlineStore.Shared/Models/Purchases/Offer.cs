using System;

namespace MyOnlineStore.Shared.Models.Purchases
{

    public class Offer
    {
        public int BuyQuantity { get; set; }
        public string Description { get; set; } = string.Empty;
        public DateTime EndDate { get; set; }
        public Guid Id { get; set; }
        public Guid MyProductId { get; set; }
        public double OfferPrice { get; set; }
        public double Percent { get; set; }
        public DateTime StartDate { get; set; }
        public Guid StoreId { get; set; }
        public string Title { get; set; } = string.Empty;
    }
}