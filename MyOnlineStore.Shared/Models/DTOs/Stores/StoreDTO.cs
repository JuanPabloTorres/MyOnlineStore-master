using MyOnlineStore.Entities.Models.Models.DTOs;
using MyOnlineStore.Entities.Models.Stores;
using System;
using System.Collections.Generic;

namespace MyOnlineStore.Entities.Models.DTOs.Stores
{
    public class StoreDTO
    {
        public Guid Id { get; set; }
        public string StoreName { get; set; } = string.Empty;
        public string StoreOwnerName { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public bool IsFavorite { get; set; } = false;
        public byte[]? Logo { get; set; }
        public Guid OwnerUserId { get; set; }
        public List<ProductItemDTO>? ProductItems { get; set; }
        public List<StoredFeaturedItemDTO>? FeaturedItems { get; set; }
        public Location Location { get; set; }

        public StoreDTO()
        {
            Location = new Location();
        }

        public StoreDTO(Store store)
        {
            Id = store.Id;
            StoreName = store.StoreName;
            StoreOwnerName = store.StoreOwnerName;
            Category = store.Category;
            Description = store.Description;
            IsFavorite = true;
            Logo = store.Logo;
            OwnerUserId = store.OwnerUserId;
            ProductItems = new List<ProductItemDTO>();
            FeaturedItems = new List<StoredFeaturedItemDTO>();
            Location = new Location();

            if(store.Location is object)
            {
                Location = store.Location;
            }

            if(store.ProductItems is object)
            {
                foreach(var item in store.ProductItems)
                {
                    ProductItems.Add(new ProductItemDTO(item));
                }
            }

            if(store.FeaturedItems is object)
            {
                foreach(var item in store.FeaturedItems)
                {
                    FeaturedItems.Add(new StoredFeaturedItemDTO(item));
                }
            }
        }

        public Store ToStore()
        {
            Store store = new Store
            {
                Id = this.Id,
                Category = this.Category,
                Description = this.Description,
                Location = this.Location,
                OwnerUserId = this.OwnerUserId,
                Logo = this.Logo,
                StoreName = this.StoreName,
                StoreOwnerName = this.StoreOwnerName,
            };

            if(ProductItems is object)
            {
                foreach(var item in ProductItems)
                {
                    store.ProductItems.Add(item.ToProductItem());
                }
            }

            if(FeaturedItems is object)
            {
                store.FeaturedItems = new List<StoreFeaturedItem>();

                foreach(var item in FeaturedItems)
                {
                    store.FeaturedItems.Add(item.ToStoreFeaturedItem());
                }
            }

            return store;
        }
    }
}