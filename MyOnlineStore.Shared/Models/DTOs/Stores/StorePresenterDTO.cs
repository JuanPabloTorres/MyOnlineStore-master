using MyOnlineStore.Entities.Models.Stores;
using System;

namespace MyOnlineStore.Shared.Models.DTOs.Stores
{
    public class StorePresenterDTO
    {
        public string StoreName { get; set; } = string.Empty;
        public string StoreOwner { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public bool IsFavorite { get; set; } = false;
        public byte[]? Logo { get; set; }
        public Guid Id { get; set; }

        public StorePresenterDTO(Store store, bool isFav = false)
        {
            StoreName = store.StoreName;
            StoreOwner = store.StoreOwnerName;
            Category = store.Category;
            Description = store.Description;
            IsFavorite = isFav;
            Logo = store.Logo;
            Id = store.Id;
        }

        public StorePresenterDTO(StorePresenterDTO storePresenter)
        {
            StoreName = storePresenter.StoreName;
            StoreOwner = storePresenter.StoreOwner;
            Category = storePresenter.Category;
            Description = storePresenter.Description;
            IsFavorite = storePresenter.IsFavorite;
            Logo = storePresenter.Logo;
            Id = storePresenter.Id;
        }

        public StorePresenterDTO()
        {
        }
    }
}