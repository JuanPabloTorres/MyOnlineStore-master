using MyOnlineStore.Application.Data.DataStore;
using MyOnlineStore.Entities.Models.Purchases;
using MyOnlineStore.Application.Presentaion.ViewModels.AdminScenarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Microsoft.Extensions.DependencyInjection;
//using ZXing;

namespace MyOnlineStore.Application.Presentation.Views.AdminScenarios
{
    [Xamarin.Forms.Xaml.XamlCompilation(Xamarin.Forms.Xaml.XamlCompilationOptions.Compile)]
    public partial class ReadBarcode : ContentPage
    {

        ProductTest ProductTest;
        public ReadBarcode(ProductTest product)
        {
            InitializeComponent();
            ProductTest = product;
            BindingContext = Startup.ServiceProvider.GetService<ProductItemDetailViewModel>();

        }

        public  void Handle_OnScanResult(/*Result result*/)
        {
            //Device.BeginInvokeOnMainThread(async () =>
            //{
            //    await DisplayAlert("Scanned result", result.Text, "OK");
            //    BarcodeDataStore dataStore = new BarcodeDataStore();

            //    //ProductTest product = new ProductTest();
            //    ProductTest = dataStore.GetItem(result.ToString());
               
            //    await App.Current.MainPage.Navigation.PopAsync();
            //});
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            //_scanView.IsScanning = true;
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            //_scanView.IsScanning = false;
        }
    }
}