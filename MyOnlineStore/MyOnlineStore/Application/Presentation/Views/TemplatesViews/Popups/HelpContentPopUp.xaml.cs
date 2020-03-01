using Lottie.Forms;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Microsoft.Extensions.DependencyInjection;
using Xamarin.Forms.Xaml;

namespace MyOnlineStore.Application.Presentation.Views.TemplatesViews.Popups
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HelpContentPopUp : PopupPage
    {
        public HelpContentPopUp()
        {
            InitializeComponent();
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            PopupNavigation.Instance.PopAsync();
        }
    }
}