using System;
using Rg.Plugins.Popup.Services;
using Rg.Plugins.Popup.Pages;

namespace MyOnlineStore.Application.Presentation.Views.Templates.Popups
{
    [Xamarin.Forms.Xaml.XamlCompilation(Xamarin.Forms.Xaml.XamlCompilationOptions.Compile)]
    public partial class PopupView : PopupPage
    {
        public PopupView()
        {
           InitializeComponent();
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            PopupNavigation.Instance.PopAsync();
        }
    }
}