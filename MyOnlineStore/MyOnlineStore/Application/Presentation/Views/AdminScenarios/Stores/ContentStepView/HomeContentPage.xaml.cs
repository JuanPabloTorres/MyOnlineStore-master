using MyOnlineStore.Application.Common.Interfaces.IViewModel;
using System;
using Xamarin.Forms.Xaml;
using Xamarin.Forms;

namespace MyOnlineStore.Application.Presentation.Views.AdminScenarios.Stores.ContentStepView
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomeContentPage : ContentPage
    {

        public static string Route = App.Current.Resources[$"{nameof(HomeContentPage) + nameof(Route)}"].ToString();
        #region Constructors

        public HomeContentPage()
        {
            InitializeComponent();
            BindingContext = Startup.ServiceProvider.GetService<IFeaturedItemUploadViewModel>();
        }

        #endregion Constructors

        #region Methods

        private async void go(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SelectedDate());
        }

        #endregion Methods
    }
}