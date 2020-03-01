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

namespace MyOnlineStore.Application.Presentation.Views.Templates.Popups
{
    [Xamarin.Forms.Xaml.XamlCompilation(Xamarin.Forms.Xaml.XamlCompilationOptions.Compile)]
    public partial class SuccessPopup : PopupPage
    {
        public static BindableProperty LottieAnimationProperty = BindableProperty.Create(nameof(LottieAnimation),typeof(string),typeof(SuccessPopup),string.Empty, BindingMode.Default);

        public static BindableProperty MessageProperty = BindableProperty.Create(nameof(Message), typeof(string), typeof(SuccessPopup), string.Empty,BindingMode.Default);

        public string LottieAnimation
        {
            get { return (string)GetValue(LottieAnimationProperty); }
            set { SetValue(LottieAnimationProperty, value); }
        }

        public string Message
        {
            get { return (string)GetValue(MessageProperty); }
            set { SetValue(MessageProperty, value); }
        }

        public SuccessPopup()
        {
            InitializeComponent();
            BindingContext = this;
        }
       
        private void AnimationView_OnFinish(object sender, EventArgs e)
        {
            var t = Task.Run(async delegate
            {
                await Task.Delay(TimeSpan.FromSeconds(1.5));
            });

            Task.Run(async delegate
            {
                await t.ContinueWith(async (tt) => await PopupNavigation.Instance.PopAsync());
            });
        }
    }
}