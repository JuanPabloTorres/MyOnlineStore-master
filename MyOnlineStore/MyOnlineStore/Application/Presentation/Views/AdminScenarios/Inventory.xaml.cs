using Microcharts;
using MyOnlineStore.DataStore;
using MyOnlineStore.Shared.Models;
using MyOnlineStore.Shared.Models.Purchases;
using MyOnlineStore.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Entry = Microcharts.Entry;
using Microsoft.Extensions.DependencyInjection;

namespace MyOnlineStore.Views.Administrator
{
    [Xamarin.Forms.Xaml.XamlCompilation(Xamarin.Forms.Xaml.XamlCompilationOptions.Compile)]
    public partial class Inventory : ContentPage
    {
        //List<ProductItem> InventoryItems;
        List<Entry> Data;

        AdministratorViewModel aVM;
     
        public Inventory()
        {
            InitializeComponent();

            aVM = Startup.ServiceProvider.GetService<AdministratorViewModel>();
            BindingContext = aVM;
            Data = new List<Entry>();

            Entry TotalAvailable = new Entry(ItemsAvailble(aVM.StoreItems.ToList()))
            {
                Color = SkiaSharp.SKColor.Parse("#313d4e"),
                Label = "Available:" + ItemsAvailble(aVM.StoreItems.ToList()).ToString(),
            };

            Entry TotalItems = new Entry(aVM.StoreItems.Count)
            {
                Color = SkiaSharp.SKColor.Parse("#49D19D"),
                Label = "Total" + aVM.StoreItems.Count.ToString(),
              
            };

            Data.Add(TotalAvailable);
            Data.Add(TotalItems);
            InventoryChart.Chart = new DonutChart()
            {
                Entries = Data,
                LabelTextSize=50
            };
          
        }

        float ItemsAvailble(List<ProductItem> Items)
        {
            int totalAvailable=0;

            foreach (var item in Items)
            {
                if(item.IsAvailable)
                {
                    totalAvailable++;
                }
            }

            return totalAvailable;
        }
      
        double CalculateInventoryValue(List<ProductItem> inventoryItems)
        {
            double totalValue = 0;

            foreach (var item in inventoryItems)
            {
                totalValue += Convert.ToDouble(item.Quantity * item.Price);
            }

            return totalValue;
        }


        protected override void OnAppearing()
        {
            base.OnAppearing();
            double w = this.Width;
            double h = this.Height;

            InventoryChart.WidthRequest = w;
            InventoryChart.HeightRequest = h;

           this.invetoryList.HeightRequest = h;

            invetoryList.WidthRequest = this.Width;
        }

        private async void OnSelectedItem(object sender, ItemTappedEventArgs e)
        {
            var item = e.Item as ProductItem;

            await Navigation.PushAsync(new DetailItempage(item));

        }
    }
}