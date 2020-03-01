using MyOnlineStore.Application.Domain.Interfaces.IViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MyOnlineStore.Application.Presentation.Views.AdminScenarios.Stores.OfferStepView
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SelectProductToSetOfferPage : ContentPage
    {
        public SelectProductToSetOfferPage()
        {
            InitializeComponent();
            BindingContext = Startup.ServiceProvider.GetService<IOfferViewModel>();
        }
    }
}