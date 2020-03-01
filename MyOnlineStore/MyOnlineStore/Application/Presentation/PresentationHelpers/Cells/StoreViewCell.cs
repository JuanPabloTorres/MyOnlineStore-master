using FFImageLoading.Forms;
using MyOnlineStore.Application.Data.Models.Presenters;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace MyOnlineStore.Application.Presentation.PresentationHelpers.Cells
{
    public class StoreViewCell : ViewCell
    {
        readonly CachedImage cachedImage;

        public StoreViewCell()
        {
            cachedImage = new CachedImage();
            View = cachedImage;
        }

        protected override void OnBindingContextChanged()
        {
            // you can also put cachedImage.Source = null; here to prevent showing old images occasionally
            cachedImage.Source = null;
            var item = BindingContext as StorePresenter;

            if (item == null)
            {
                return;
            }

            cachedImage.Source = item.Source;

            base.OnBindingContextChanged();
        }
    }
}
