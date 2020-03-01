using MyOnlineStore.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Microsoft.Extensions.DependencyInjection;

namespace MyOnlineStore.Views.Administrator
{
    [Xamarin.Forms.Xaml.XamlCompilation(Xamarin.Forms.Xaml.XamlCompilationOptions.Compile)]
    public partial class ReportPage : ContentPage
    {
        public ReportPage()
        {
            InitializeComponent();
            BindingContext = Startup.ServiceProvider.GetService<AdministratorViewModel>();
        }
    }
}