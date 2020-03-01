using MyOnlineStore.Application.Common.Interfaces.IViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MyOnlineStore.Application.Presentation.Views.StoresScenarios.OffersAndContent
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OfferAndContentView : ContentPage
    {

        public static string Route = App.Current.Resources[$"{nameof(OfferAndContentView) + nameof(Route)}"].ToString();
        public OfferAndContentView()
        {
            InitializeComponent();
            BindingContext = Startup.ServiceProvider.GetService<IOfferContentViewModel>();
        }
    }
}