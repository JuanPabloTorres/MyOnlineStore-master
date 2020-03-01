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
    public partial class OfferTitleAndDescriptionPage : ContentPage
    {
        public OfferTitleAndDescriptionPage()
        {
            InitializeComponent();
            BindingContext = Startup.ServiceProvider.GetService<IOfferViewModel>();
        }

        private async void GoBack(object sender, EventArgs e)
        {
            await Shell.Current.Navigation.PopAsync();
        }
    }
}