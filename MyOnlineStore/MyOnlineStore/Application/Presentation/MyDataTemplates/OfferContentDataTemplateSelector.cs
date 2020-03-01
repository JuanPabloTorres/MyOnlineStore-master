using MyOnlineStore.Entities.Models.Stores;
using MyOnlineStore.Shared.Models.Purchases;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace MyOnlineStore.Application.Presentation.MyDataTemplates
{
    public class OfferContentDataTemplateSelector : DataTemplateSelector
    {

        public DataTemplate OfferTemplate { get; set; }
        public DataTemplate ContentTempate { get; set; }
        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {       
            if(item is Offer)
            {
                return OfferTemplate;
            }
            else
            {
                return ContentTempate;
            }
        }
    }
}
