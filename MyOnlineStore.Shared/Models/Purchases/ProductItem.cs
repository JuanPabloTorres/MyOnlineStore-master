using MyOnlineStore.Shared.Models.Purchases;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyOnlineStore.Entities.Models.Purchases
{
    [Table("ProductItems")]
    public class ProductItem /*: IBaseEntity*/
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public float Price { get; set; } = 0.0f;

        public double RealPrice { get; set; }

        public byte[]? Image { get; set; }

        public uint Quantity { get; set; } = uint.MinValue;

        public string Category { get; set; } = string.Empty;

        public Guid MyStoreId { get; set; }

        //public Guid OfferId { get; set; }

        public Guid MyOfferId { get; set; }
        public Offer? ProductOffer { get; set; }

        public ProductItem()
        {
        }

        public bool HasOffer()
        {
            return this.ProductOffer is object;
        }
    }
}