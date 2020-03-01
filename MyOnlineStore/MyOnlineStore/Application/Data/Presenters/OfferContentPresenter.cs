using MyOnlineStore.Entities.Models.Stores;
using MyOnlineStore.Shared.Models.Purchases;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyOnlineStore.Application.Data.Presenters
{
    public class OfferContentPresenter
    {

        public List<Offer> Offers { get; set; }
        public List<StoreFeaturedItem> FeatureItems { get; set; }


    }
}
 