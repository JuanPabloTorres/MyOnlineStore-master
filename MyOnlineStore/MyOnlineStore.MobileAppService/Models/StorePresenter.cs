using MyOnlineStore.Entities.Models.Stores;
using System;

namespace MyOnlineStore.Application.Data.Models.Presenters
{
    public class StorePresenter
    {
        public string StoreName { get; set; } = string.Empty;
        public string StoreOwner { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public bool IsFavorite { get; set; } = false;
        public byte[]? Logo { get; set; }
        public Guid Id { get; set; }
        public StorePresenter()
        {

        }
        public StorePresenter(Store store, bool isFav = false)
        {
            StoreName = store.StoreName;
            StoreOwner = store.StoreOwnerName;
            Category = store.Category;
            Description = store.Description;
            IsFavorite = isFav;
            Logo = store.Logo;
            Id = store.Id;
        }
    }
}
