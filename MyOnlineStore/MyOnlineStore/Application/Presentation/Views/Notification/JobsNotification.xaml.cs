using MyOnlineStore.Application.Common.Interfaces.IViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MyOnlineStore.Application.Presentation.Views.Notification
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class JobsNotification : ContentPage
    {
        public JobsNotification()
        {
            InitializeComponent();
            BindingContext = Startup.ServiceProvider.GetService<INotificationViewModel>();
        }
    }
}