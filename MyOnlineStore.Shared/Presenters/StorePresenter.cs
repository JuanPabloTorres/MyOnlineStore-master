using System;
using System.Collections.Generic;
using System.Text;

namespace MyOnlineStore.Shared.Presenters
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
    }
}
