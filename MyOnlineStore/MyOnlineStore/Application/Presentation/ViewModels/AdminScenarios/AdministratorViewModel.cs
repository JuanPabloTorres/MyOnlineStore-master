using Microcharts;
using MyOnlineStore.Interfaces.DataStore;
using MyOnlineStore.Shared.Models.Purchases;
using MyOnlineStore.ViewModels.Base;
using MyOnlineStore.Views.Administrator;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Entry = Microcharts.Entry;

namespace MyOnlineStore.ViewModels
{
    public class AdministratorViewModel : BaseViewModel
    {
        readonly IProductItemDataStore _ProductDataStore;
        public ICommand GetPhotoCommand { get; set; }
        public AdministratorViewModel(IProductItemDataStore productItemDataStore)
        {
            _ProductDataStore = productItemDataStore;
            //GetPhotoCommand = new Command()


            NewItem = new ProductItem();

            Data = new List<Entry>();
          
            StoreItems = new ObservableCollection<ProductItem>();
            StoreItems = GetData();

            Entry TotalAvailable = new Entry(ItemsAvailble(StoreItems))
            {
                Color = SkiaSharp.SKColor.Parse("#313d4e"),
                Label = "Available:" + ItemsAvailble(StoreItems).ToString(),
            };


            Data.Add(TotalAvailable);
            Entry TotalItems = new Entry(StoreItems.Count)
            {
                Color = SkiaSharp.SKColor.Parse("#49D19D"),
                Label = "Total" + StoreItems.Count.ToString(),
            };
            Data.Add(TotalItems);
            InventoryChart = new DonutChart()
            {
                Entries = Data,
                LabelTextSize = 50
            };


            SalesData = GetSalesData();

            SalesChart = new PointChart()
            {
                Entries = SalesData,
            };

            OrdersItems = new ObservableCollection<Order>();
            OrdersItems = GetOrders();

            DoneCommand = new Command( async() =>  await GoAdminHome());
            AddCommand = new Command(async () => await AddItem());
            UpdateCommand = new Command(async () => await UpdateItem());
        }


        #region New ProductItem Region
        public ProductItem NewItem
        {
            get;set;          
        }

        #endregion

        #region  ChartsRegion
        List<Entry> Data;
        public Chart InventoryChart { get; set; }     
        public ObservableCollection<Entry> InventoryData { get; set; }

        public Chart SalesChart { get; set; }
        public ObservableCollection<Entry> SalesData { get; set; }

        public Chart OrdersCompletedChart { get; set; }
        public ObservableCollection<Entry> OrdersData { get; set; }

        public ObservableCollection<ProductItem> StoreItems { get; set; }


        #endregion

        public ObservableCollection<Order> OrdersItems { get; set; }


        #region Commands Regions
        public ICommand DoneCommand { get; set; }
        public ICommand AddCommand { get; set; }
        public ICommand UpdateCommand { get; set; }

        public ICommand SelectedInventoryItemCommand { get; set; }

        #endregion



        #region To Fill Data Functions
        ObservableCollection<Entry> GetSalesData()
        {
            ObservableCollection<Entry> entries = new ObservableCollection<Entry>();



            var d = new Entry(200)
            {
                Color = SkiaSharp.SKColor.Parse("#313d4e"),
                Label = DateTime.Today.ToString(),
                ValueLabel = "200"
            };
            var d1 = new Entry(500)
            {
                Color = SkiaSharp.SKColor.Parse("#313d4e"),
                Label = DateTime.Today.ToString(),
                ValueLabel = "500"
            };
            var d2 = new Entry(100)
            {
                Color = SkiaSharp.SKColor.Parse("#313d4e"),
                Label = DateTime.Today.ToString(),
                ValueLabel = "100"
            };

            entries.Add(d);
            entries.Add(d1);
            entries.Add(d2);
            return entries;
        }
        public async Task GoAdminHome()
        {
            //await Application.Current.MainPage.Navigation.PushAsync(new AdminHomePage());
            await Shell.Current.Navigation.PopAsync(true);
        }

        public async Task AddItem()
        {
            //Make Logic to Save the new ProductItem
            await Application.Current.MainPage.Navigation.PushAsync(new DetailItempage(NewItem));
        }

        public async Task GetInventoryData()
        {
            ///Make logic to get inventory data
        }

        public async Task UpdateItem()
        {
            ///Make logic to update ProductItem
        }

        public ObservableCollection<ProductItem> GetData()
        {
            Image img = new Image();

            img.Source = "home.png";

            var InventoryItems = new ObservableCollection<ProductItem>()
            {
                new ProductItem()
                {
                    IsAvailable=false,
                    Name ="ProductItem 1",
                     Quantity=0,
                     Price=5.00,
                },
                new ProductItem()
                {
                    IsAvailable=true,
                    Name ="ProductItem 2",
                     Quantity=5,
                       Price=5.00,
                       Type="Food"
                },
                new ProductItem()
                {
                    IsAvailable=false,
                    Name ="ProductItem 3",
                     Quantity=0,
                       Price=5.00


                },
                new ProductItem()
                {
                    IsAvailable=true,
                    Name ="ProductItem 4",
                     Quantity=30,
                       Price=5.00
                },
                new ProductItem()
                {
                    IsAvailable=true,
                    Name ="ProductItem 5",
                     Quantity=20,
                       Price=5.00
                }
            };

            return InventoryItems;
        }

        public ObservableCollection<Order> GetOrders()
        {
            ObservableCollection<Order> Orders = new ObservableCollection<Order>()
            {
                new Order()
                {
                     OrderDate=DateTime.Now,
                      OrderId= Guid.NewGuid(),
                      OrderItems = new ObservableCollection<ProductItem>()
                       {
                           new ProductItem()
                           {

                               Name="ProductItem 1",
                                Price=5.00
                           },
                             new ProductItem()
                           {
                               Name="ProductItem 2",
                                Price=5.00
                           },
                               new ProductItem()
                           {
                               Name="ProductItem 3",
                                Price=5.00
                           }
                       },
                        
                },
                 new Order()
                {
                     OrderDate=DateTime.Now,
                      OrderId= Guid.NewGuid(),
                      OrderItems = new ObservableCollection<ProductItem>()
                       {
                           new ProductItem()
                           {

                               Name="ProductItem 1",
                                Price=5.00
                           },
                             new ProductItem()
                           {
                               Name="ProductItem 2",
                                Price=5.00
                           },
                               new ProductItem()
                           {
                               Name="ProductItem 3",
                                Price=5.00
                           }
                       },

                }
            };

            return Orders;
        }



        #endregion

        #region Data Calculator
        double CalculateInventoryValue(IList<ProductItem> inventoryItems)
        {
            double totalValue = 0;

            foreach (var ProductItem in inventoryItems)
            {
                totalValue += Convert.ToDouble(ProductItem.Quantity * ProductItem.Price);
            }

            return totalValue;
        }
        int ItemsAvailble(List<ProductItem> Items)
        {
            int totalAvailable = 0;

            foreach (var ProductItem in Items)
            {
                if (ProductItem.IsAvailable)
                {
                    totalAvailable++;
                }
            }

            return totalAvailable;
        }

        float ItemsAvailble(ObservableCollection<ProductItem> Items)
        {
            int totalAvailable = 0;

            foreach (var ProductItem in Items)
            {
                if (ProductItem.IsAvailable)
                {
                    totalAvailable++;
                }
            }

            return totalAvailable;
        }

        #endregion
    }
}
