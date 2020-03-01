using MyOnlineStore.Application.Presentation.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Microsoft.Extensions.DependencyInjection;

namespace MyOnlineStore.Views
{
    [Xamarin.Forms.Xaml.XamlCompilation(Xamarin.Forms.Xaml.XamlCompilationOptions.Compile)]
    public partial class StoreItemView : ContentView
    {
        public StoreItemView()
        {
            InitializeComponent();
            //BindingContext = new HomeViewModel();
        }
    }
}