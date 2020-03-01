using MyOnlineStore.Entities.Models.Purchases;
using System;

namespace MyOnlineStore.Entities.Models.Models.DTOs
{
    public class ProductItemDTO
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public float Price { get; set; } = 0.0f;

        public byte[]? Image { get; set; }

        public uint Quantity { get; set; } = uint.MinValue;

        public string Category { get; set; } = string.Empty;
        public Guid MyStoreId { get; set; }

        public ProductItemDTO()
        {
        }

        public ProductItemDTO(ProductItem productItem)
        {
            Id = productItem.Id;
            Name = productItem.Name;
            Description = productItem.Description;
            Price = productItem.Price;
            Image = productItem.Image;
            Quantity = productItem.Quantity;
            Category = productItem.Category;
            MyStoreId = productItem.MyStoreId;
        }

        public ProductItem ToProductItem()
        {
            return new ProductItem
            {
                Description = this.Description,
                Image = this.Image,
                MyStoreId = this.MyStoreId,
                Name = this.Name,
                Price = this.Price,
                Quantity = this.Quantity,
                Category = this.Category
            };
        }
    }
}