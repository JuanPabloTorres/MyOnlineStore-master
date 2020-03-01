using MyOnlineStore.Application.Common.Interfaces.Services.Store;
using MyOnlineStore.Application.Presentation.ViewModels.Base;
using MyOnlineStore.Shared.Models.DTOs.Stores;
using System;
using System.IO;
using System.Windows.Input;
using Xamarin.Forms;
using Microsoft.Extensions.DependencyInjection;
using MyOnlineStore.Entities.Models.Stores;

namespace MyOnlineStore.Application.Data.Models.Presenters
{
    public class StorePresenter : BaseViewModel
    {
        private string _storeName = string.Empty;
        private string _storeOwner = string.Empty;
        private string _category = string.Empty;
        private string _description = string.Empty;
        private bool _isFavorite = false;
        private byte[]? _logo;
        private ImageSource _favoriteSource;

        public string StoreName
        {
            get => _storeName;
            set { _storeName = value;RaisePropertyChanged(() => StoreName); }
        }
        public string StoreOwner
        {
            get => _storeOwner;
            set { _storeOwner = value; RaisePropertyChanged(() => StoreOwner); }
        }
        public string Category
        {
            get => _category;
            set { _category = value; RaisePropertyChanged(() => Category); }
        }
        public string Description
        {
            get => _description;
            set { _description = value; RaisePropertyChanged(() => Description); }
        }
        public bool IsFavorite
        {
            get => _isFavorite;
            set { _isFavorite = value; RaisePropertyChanged(() => IsFavorite); }
        }
        public byte[]? Logo
        {
            get => _logo;
            set { _logo = value; RaisePropertyChanged(() => Logo); }
        }
        public Guid Id { get; set; }
        public ImageSource Source { get; set; }
        public ImageSource FavoriteSource
        {
            get => _favoriteSource;
            set { _favoriteSource = value;RaisePropertyChanged(() => FavoriteSource); }
        }
        public ICommand MakeFavorite { get; private set; } = Startup.ServiceProvider.GetService<IStoreCardService>().MakeStoreFavoriteClickCommand;

        public StorePresenter()
        {
            if(IsFavorite) 
                { FillHeart(); }
            else 
                { EmptyHeart(); }
        }

        public StorePresenter(StorePresenter storePresenter)
        {
            StoreName = storePresenter.StoreName;
            Category = storePresenter.Category;
            Description = storePresenter.Description;
            Id = storePresenter.Id;
            IsFavorite = storePresenter.IsFavorite;
            Logo = storePresenter.Logo;
            StoreOwner = storePresenter.StoreOwner;
            FavoriteSource = storePresenter.FavoriteSource;
        }

        public void FillHeart()
        {
            FavoriteSource = ImageSource.FromFile("like_heart_fill.svg");
            IsFavorite = true;
        }
        public void EmptyHeart()
        {
            IsFavorite = false;
            FavoriteSource = ImageSource.FromFile("like_heart_empty.svg");
        }

        public StorePresenter(StorePresenterDTO storePresenter)
        {
            if (storePresenter is object && storePresenter.Logo is object)
            {
                StoreName = storePresenter.StoreName;
                StoreOwner = storePresenter.StoreOwner;
                Category = storePresenter.Category;
                Description = storePresenter.Description;
                Logo = storePresenter.Logo;
                IsFavorite = storePresenter.IsFavorite;
                Id = storePresenter.Id;
                Source = ImageSource.FromStream(() => new MemoryStream(Logo));

                if (IsFavorite)
                {
                    FillHeart();
                }
                else
                {
                    EmptyHeart();
                }
                
            }
        }
        public StorePresenter(Store store, bool isFav)
        {
            if(store is object && store.Logo is object)
            {
                StoreName = store.StoreName;
                StoreOwner = store.StoreOwnerName;
                Category = store.Category;
                Description = store.Description;
                Logo = store.Logo;
                IsFavorite = isFav;
                Id = store.Id;
                Source = ImageSource.FromStream(() => new MemoryStream(Logo));

                if (IsFavorite)
                {
                    FillHeart();
                }
                else
                {
                    EmptyHeart();
                }

            }
        }
        
    }
}
