using FFImageLoading.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MyOnlineStore.Application.Presentation.PresentationHelpers.Cells
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StoreCardViewCell : ViewCell
    {
        public StoreCardViewCell()
        {
            InitializeComponent();
        }

        public class CustomCacheKeyFactory : ICacheKeyFactory
        {
            public string GetKey(ImageSource imageSource, object bindingContext)
            {
                var keySource = imageSource as ByteArrayListPageModel.CustomStreamImageSource;

                if (keySource == null)
                    return null;

                return keySource.Key;
            }
        }
    }
}