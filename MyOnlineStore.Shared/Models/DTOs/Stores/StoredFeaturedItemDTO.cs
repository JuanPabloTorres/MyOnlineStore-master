using MyOnlineStore.Entities.Models.Stores;
using System;

namespace MyOnlineStore.Entities.Models.DTOs.Stores
{
    public class StoredFeaturedItemDTO
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public byte[] Image { get; set; }
        public Guid MyStoreId { get; set; }

        public StoredFeaturedItemDTO(StoreFeaturedItem item)
        {
            Id = item.Id;
            Title = item.Title;
            Description = item.Description;
            Image = item.Image;
            MyStoreId = item.MyStoreId;
        }

        public StoreFeaturedItem ToStoreFeaturedItem()
        {
            return new StoreFeaturedItem
            {
                Description = this.Description,
                Image = this.Image,
                Id = this.Id,
                MyStoreId = this.MyStoreId,
                Title = this.Title
            };
        }
    }
}