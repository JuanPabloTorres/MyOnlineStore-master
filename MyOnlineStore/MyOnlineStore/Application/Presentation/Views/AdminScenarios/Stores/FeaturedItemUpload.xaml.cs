using Microsoft.Extensions.DependencyInjection;
using MyOnlineStore.Application.Common.Interfaces.IViewModel;
using System;
using Xamarin.Forms;


namespace MyOnlineStore.Application.Presentation.Views.AdminScenarios
{
   
    [Xamarin.Forms.Xaml.XamlCompilation(Xamarin.Forms.Xaml.XamlCompilationOptions.Compile)]
    public partial class FeaturedItemUpload : ContentPage
    {
        public FeaturedItemUpload()
        {
            InitializeComponent();
            BindingContext = Startup.ServiceProvider.GetService<IFeaturedItemUploadViewModel>();
        }

      
    }
}