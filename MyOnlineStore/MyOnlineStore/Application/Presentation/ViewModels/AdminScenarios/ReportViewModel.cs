using MyOnlineStore.Dashboard.DashBoardModel;
using MyOnlineStore.Interfaces.DataStore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using MyOnlineStore.Application.Common.Interfaces.DataStore;
using MyOnlineStore.Application.Presentation.ViewModels.Base;
using MyOnlineStore.Entities.Models.Purchases;
using MyOnlineStore.Dashboard.Services;
using System.Windows.Input;
using System.Globalization;
using MyOnlineStore.Shared.Models.Purchases;
using Xamarin.Forms;
using MyOnlineStore.Application.Common.Utilities.Dashboards;
using Syncfusion.SfChart.XForms;
using Syncfusion.SfCalendar.XForms;
using MyOnlineStore.Application.Common.Interfaces.IViewModel;

namespace MyOnlineStore.ViewModels.Admin
{
   
    public class ReportViewModel : BaseViewModel, IReportViewModel
    {
        //Aplicara el filtro para el dashboard
        public ICommand OrderFilterCommand { get; set; }
        public ICommand InventoryFilterCommand { get; set; }
        public ICommand GoalFilterCommand { get; set; }


        //Presentara toda la data generica y de importancia al usuario
        private OrderGenericData ordergeneric;

        public OrderGenericData OrderGenerric
        {
            get { return ordergeneric; }
            set
            { ordergeneric = value;
                RaisePropertyChanged(() => OrderGenerric);
            }
        }
        //Obtiene los record seleccion por el filtro 
        //Presenta la data en el dashboard
        ObservableCollection<Record> orderdata { get; set; }
        public ObservableCollection<Record> OrderData 
        {
            get => orderdata;
            set
            {
                orderdata = value;
                RaisePropertyChanged(() => OrderData);
            }
        }

        private ObservableCollection<ProductItem> inventorydata;

        public ObservableCollection<ProductItem> InventoryData
        {
            get { return inventorydata; }
            set {
                inventorydata = value;
                RaisePropertyChanged(() => InventoryData);

            }
        }

        ObservableCollection<Record> goaldata { get; set; }
        public ObservableCollection<Record> GoalData
        {
            get => goaldata;
            set
            {
                goaldata = value;
                RaisePropertyChanged(() => GoalData);
            }
        }


        //Listado de filtracion por unidad de tiempo
        public List<string> TimeUnit { get; set; }
        //Listado de filtracion por tipo de producto

        //Guarda las acciones previsas para seccion de orders
        public List<string> Types { get; set; }
        public List<string> ActionsOrders { get; set; }
        List<Action<DateTime, DateTime, IEnumerable<Order>>> OrderActions { get; set; }
        //Guarda las acciones previstas para seccion de inventory
        public List<string> InventoryActions { get; set; }

        //Guardan las acciones previstas para ordenes
        public List<string> GoalActions { get; set; }
        List<Action<IEnumerable<ProductItem>, int, string>> InventoryActionsList { get; set; }
        //Coje toda las ordenes de la tienda por el orderdatastore
        ObservableCollection<Order> Orders { get; set ; }
        //Coje toda las ordenes de la tienda por el productdatastore
        public ObservableCollection<ProductItem> Products { get ; set ; }

       

        SelectionRange selectionrange;
        public SelectionRange SelectedRange 
        {
            get => selectionrange;
            set
            {
                selectionrange = value;
                RaisePropertyChanged(() => SelectedRange);
            }
        }

        private string selectedorderaction;
        public string SelectedOrderAction
        {
            get { return selectedorderaction; }
            set
            {
                selectedorderaction = value;
                RaisePropertyChanged(() => SelectedOrderAction);
            }
        }

        private string selectedinvetoryaction;

        public string SelectedInventoryAction
        {
            get { return selectedinvetoryaction; }
            set { 
                selectedinvetoryaction = value;
                RaisePropertyChanged(() => SelectedInventoryAction);

                if (SelectedInventoryAction =="Quantity")
                {
                    IsCounterDisable = true;
                }
            }
        }


        private string selectedunittime;

        public string SelectedUnitTime
        {
            get { return selectedunittime; }
            set 
            { 
                selectedunittime = value;
                RaisePropertyChanged(() => SelectedUnitTime);
            }
        }

        private string selectedtype;

        public string SelectedType
        {
            get { return selectedtype; }
            set 
            { 
                selectedtype = value;
                RaisePropertyChanged(() => SelectedType);
            }
        }

        private string inventory_xdata;

        public string Inventory_XData
        {
            get { return inventory_xdata; }
            set 
            { 
                inventory_xdata = value;
                RaisePropertyChanged(() => Inventory_XData);
            }
        }

        private string inventory_ydata;

        public string Inventory_YData
        {
            get { return inventory_ydata; }
            set
            {
                inventory_ydata = value;
                RaisePropertyChanged(() => Inventory_YData);
            }
        }

        private string charttitle;

        public string ChartTitle
        {
            get { return charttitle; }
            set
            {
                charttitle = value;
                RaisePropertyChanged(() => ChartTitle);
            }
        }


        private string xtitle;

        public string XTitle
        {
            get { return xtitle; }
            set 
            { 
                xtitle = value;
                RaisePropertyChanged(() => XTitle);
                
            }
        }
        private string ytitle;

        public string YTitle
        {
            get { return ytitle; }
            set
            {
                ytitle = value;
                RaisePropertyChanged(() => YTitle);

            }
        }

        private double goaltoreach;

        public double GoalToReach
        {
            get { return goaltoreach; }
            set 
            { goaltoreach = value;
                RaisePropertyChanged(() => GoalToReach);
            }
        }

        private string goalselectedaction;

        public string GoalSelectedAction
        {
            get { return goalselectedaction; }
            set 
            { 
                goalselectedaction = value;
                RaisePropertyChanged(() => GoalSelectedAction);
            }
        }

        private InventoryInformation inventorygeneric;

        public InventoryInformation InventoryGeneric
        {
            get { return inventorygeneric; }
            set 
            {
                inventorygeneric = value;
                RaisePropertyChanged(() => InventoryGeneric);
            }
        }

        private DateTime fromdate;

        public DateTime FromDate
        {
            get { return fromdate; }
            set { fromdate = value;
                RaisePropertyChanged(() => FromDate);
            }
        }

        private DateTime todate;

        public DateTime ToDate
        {
            get { return todate; }
            set
            {
                todate = value;
                RaisePropertyChanged(() => ToDate);
            }
        }

        private int selectedquantity;

        public int SelectedQuantity
        {
            get { return selectedquantity; }
            set { selectedquantity = value;
                RaisePropertyChanged(() => SelectedQuantity);
            }
        }

        private bool iscounterdisable;

        public bool IsCounterDisable
        {
            get { return iscounterdisable; }
            set { iscounterdisable = value;
                RaisePropertyChanged(() => IsCounterDisable);
            }
        }



        List<OrdersProductItems> items;

        private ChartSeriesCollection datagraph;

        public ChartSeriesCollection DataGraph
        {
            get { return datagraph; }
            set 
            { 
                datagraph = value;
                RaisePropertyChanged(() => DataGraph);
            }
        }


        public ReportViewModel(IProductItemDataStore productItemDataStore, IOrdersDataStore ordersDataStore)
        {
            TimeUnit = new List<string>(GetUnit());
            Types = new List<string>(GetTypes());

            SelectedRange = new SelectionRange()
            {
                StartDate = DateTime.Today,
                EndDate = DateTime.Today
            };

            DataGraph = new ChartSeriesCollection();


            AddActions();
            
            items = new List<OrdersProductItems>()
            {
                new OrdersProductItems()
                {
                     ProductItem = new ProductItem()
                     {
                          Description="This is an item",
                          Quantity=10,
                          Name="Item",
                          Price=10.00f
                     },
                     
                      
                },
                 new OrdersProductItems()
                {
                     ProductItem = new ProductItem()
                     {
                          Description="This is an item",
                          Quantity=10,
                          Name="Item",
                           Price=10.00f,
                           
                     },


                }
            };
            Orders = new ObservableCollection<Order>()
            {
                new Order
                { 
                    Id = Guid.NewGuid(), OrderDate = DateTime.Today.AddDays(-1), OrderStatus = OrderStatus.Pending, OrderItems = items, PayedWithCash = true 
                },
                new Order
                { 
                    Id = Guid.NewGuid(), OrderDate = DateTime.Today.AddMonths(-1), OrderStatus = OrderStatus.Pending, OrderItems = items, PayedWithCash = true
                },
                new Order 
                { 
                    Id = Guid.NewGuid(), OrderDate = DateTime.Today.AddDays(-1), OrderStatus = OrderStatus.Completed, OrderItems = items, PayedWithCash = true 
                },
                new Order 
                {
                    Id = Guid.NewGuid(), OrderDate = DateTime.Today.AddDays(-1), OrderStatus = OrderStatus.Completed, OrderItems = items, PayedWithCash = true 
                },
                new Order 
                {
                    Id = Guid.NewGuid(), OrderDate = DateTime.Today.AddDays(-2), OrderStatus = OrderStatus.Completed, OrderItems = items, PayedWithCash = true 
                },
                new Order 
                {
                    Id = Guid.NewGuid(), OrderDate = DateTime.Today.AddMonths(-2), OrderStatus = OrderStatus.Completed, OrderItems = items, PayedWithCash = true 
                },
                new Order 
                {
                    Id = Guid.NewGuid(), OrderDate = DateTime.Today.AddDays(-3), OrderStatus = OrderStatus.Completed, OrderItems = items, PayedWithCash = true 
                },
                new Order 
                {
                    Id = Guid.NewGuid(), OrderDate = DateTime.Today.AddDays(-3), OrderStatus = OrderStatus.Completed, OrderItems = items, PayedWithCash = true 
                },
            };

            DateTime start = DateTime.Today.AddDays(-5);

            DateTime end = DateTime.Today.AddDays(1);

            string call = ActionsOrders[0] + TimeUnit[0];
            OrderActions.Where(a => a.Method.Name == call).FirstOrDefault().Invoke(start, end, Orders);

            var inventory = new List<ProductItem>()
            {
                new ProductItem()
                {
                    Name="Item",
                    Price=10.00f,
                    Quantity=10,
                    Category="Food"
                    
                },
                new ProductItem()
                {
                    Name="Item",
                    Price=10.00f,
                    Quantity=10,
                    Category="Food"

                },
                new ProductItem()
                {
                    Name="Item",
                    Price=10.00f,
                    Quantity=5,

                },
                new ProductItem()
                {
                    Name="Item",
                    Price=10.00f,
                    Quantity=5,

                }
            };

            OrderFilterCommand = new Command(() => 
            {
                string call = SelectedOrderAction + SelectedUnitTime;

                if(!string.IsNullOrEmpty(SelectedOrderAction) && !string.IsNullOrEmpty(SelectedUnitTime))
                {
                    if(SelectedRange.StartDate <= SelectedRange.EndDate)
                    {
                        OrderActions.Where(a => a.Method.Name == call).FirstOrDefault().Invoke(SelectedRange.StartDate, SelectedRange.EndDate, Orders);

                    }
                    else
                    {
                        ShowError();
                    }
                }
                else
                {
                    ShowError();
                }
            });
          
            InventoryFilterCommand = new Command(() =>
            {
                if (!string.IsNullOrEmpty(SelectedInventoryAction))
                {
                    //InventoryActionsList .Where(a => a.Method.Name == SelectedInventoryAction).FirstOrDefault().Invoke(inventory,5,SelectedType);      

                    if (SelectedInventoryAction == "Quantity")
                    {

                        var quantityData = productItemDataStore.GetProductItemsWithQuantity(SelectedQuantity, App.ApplicationManager.CurrentStore.Id.ToString());

                        InventoryData = new ObservableCollection<ProductItem>();
                        foreach (var item in quantityData)
                        {
                            InventoryData.Add(item);
                        }
                        Inventory_YData = "Quantity";
                        Inventory_XData = "Name";

                        XTitle = "Names";
                        YTitle = "Quantity";

                        //SelectedInventoryAction = string.Empty;
                        InventoryGeneric = new InventoryInformation()
                        {
                            TotalItems = InventoryData.Count,
                            InventoryValue = InventoryData.Sum(i => i.Price * i.Quantity),
                            Title = "Quantity"
                        };

                    }
                }


            });

            GoalFilterCommand = new Command(() =>
            {

                //ProgessDay(OrderDateFrom, OrderDateTo, Orders);
                //if (!string.IsNullOrEmpty())
                //{
                //    //InventoryActionsList.Where(a => a.Method.Name == SelectedInventoryAction).FirstOrDefault().Invoke(inventory, 5, SelectedType);
                //}


            });
        }


        bool NullValidation(object tovalidate)
        {
            bool result = false;

            if(tovalidate != null)
            {
                result = true;
            }
            else
            {
                result = false;
            }

            return result;
        }
        async void ShowError()
        {
           await App.Current.MainPage.DisplayAlert("Notification", "The Filter is not in a correct format or something is empty", "OK");
        }

        ObservableCollection<object> GetToday()
        {
            ObservableCollection<object> todaycollection = new ObservableCollection<object>();

            //Select today dates
            todaycollection.Add(CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(DateTime.Now.Date.Month).Substring(0, 3));
            if (DateTime.Now.Date.Day < 10)
                todaycollection.Add("0" + DateTime.Now.Date.Day);
            else
                todaycollection.Add(DateTime.Now.Date.Day.ToString());
            todaycollection.Add(DateTime.Now.Date.Year.ToString());

            return todaycollection;
        }
       
        List<string> GetUnit()
        {
            return new List<string>()
            {
                "Day","Week","Month","Year"
            };
        } 
        List<string> GetOrdersActions()
        {
            return new List<string>()
            {
                "OrdersCompleted"
            };
        }

        List<string> GetTypes()
        {
            return new List<string>()
            {
                "Home Furniture","Food","Car Tools","Cars","Clothes"
            };
        }
        List<string> GetInventoryActions()
        {
            return new List<string>()
            {
                "Quantity","Top Sales"
            };
        }

        List<string> GetGoalActions()
        {
            return new List<string>()
            {
                "Progess"
            };
        }
   //TopSales
   //                                   <x:String>Inventory Information</x:String>
   //                                   <x:String>Products Earning</x:String>



        #region Order Methods
        void OrdersCompletedDay(DateTime datefrom,DateTime dateto, IEnumerable<Order> OrdersData)
        {

            var data= OrderServices.GetRecords(datefrom, dateto, OrdersData);

            OrderData = new ObservableCollection<Record>(data);

            var genericData = new List<Order>(OrdersData);
            OrderGenerric = new OrderGenericData()
            {
                TotalOrder = genericData.Count(),
                TotalCompleted = genericData.Where(o => o.OrderStatus == OrderStatus.Completed && (o.OrderDate >= datefrom || o.OrderDate<= dateto)).Count(),
                TotalNotCompleted = genericData.Where(o => o.OrderStatus == OrderStatus.Pending && (o.OrderDate >= datefrom || o.OrderDate <= dateto)).Count(),

            };
            ChartTitle="Orders Completed Day";

            //DateTimeCategoryAxis yaxis = new DateTimeCategoryAxis();
            //yaxis.Title.Text = "Date";

            //NumericalAxis xaxis = new NumericalAxis();
            //xaxis.Title.Text = "Value";

             
            ColumnSeries columchart = new ColumnSeries()
            {
                Color = Color.Green,
                 EnableTooltip=true,
                  ItemsSource=OrderData,
                   XBindingPath= "RecordDate",
                   YBindingPath= "RecordValue",
                 
            };

            columchart.DataMarker = new ChartDataMarker()
            {
                ShowLabel = true
            };
            DataGraph = new ChartSeriesCollection();
            DataGraph.Add(columchart);
        }
        void OrdersCompletedWeek(DateTime datefrom, DateTime dateto, IEnumerable<Order> OrdersData)
        {
            var data = OrderServices.GetRecordByWeek(datefrom, dateto, OrdersData);

            OrderData = new ObservableCollection<Record>(data);
            ChartTitle = "Orders Completed Week";
            var genericData = new List<Order>(OrdersData);
            OrderGenerric = new OrderGenericData()
            {
                TotalOrder = genericData.Count(),
                TotalCompleted = genericData.Where(o => o.OrderStatus == OrderStatus.Completed && (o.OrderDate >= datefrom || o.OrderDate <= dateto)).Count(),
                TotalNotCompleted = genericData.Where(o => o.OrderStatus == OrderStatus.Pending && (o.OrderDate >= datefrom || o.OrderDate <= dateto)).Count(),

            };

            ColumnSeries columchart = new ColumnSeries()
            {
                Color = Color.Green,
                EnableTooltip = true,
                ItemsSource = OrderData,
                XBindingPath = "RecordDate",
                YBindingPath = "RecordValue",

            };

            columchart.DataMarker = new ChartDataMarker()
            {
                ShowLabel = true
            };
            DataGraph = new ChartSeriesCollection();
            DataGraph.Add(columchart);
        }
        void OrdersCompletedMonth(DateTime datefrom, DateTime dateto, IEnumerable<Order> OrdersData)
        {

            var data = OrderServices.GetRecordByMonths(datefrom, dateto, OrdersData);

            OrderData = new ObservableCollection<Record>(data);
            ChartTitle = "Orders Completed Month";
            var genericData = new List<Order>(OrdersData);
            OrderGenerric = new OrderGenericData()
            {
                TotalOrder = genericData.Count(),
                TotalCompleted = genericData.Where(o => o.OrderStatus == OrderStatus.Completed && (o.OrderDate >= datefrom || o.OrderDate <= dateto)).Count(),
                TotalNotCompleted = genericData.Where(o => o.OrderStatus == OrderStatus.Pending && (o.OrderDate >= datefrom || o.OrderDate <= dateto)).Count(),

            };

            ColumnSeries columchart = new ColumnSeries()
            {
                Color = Color.Green,
                EnableTooltip = true,
                ItemsSource = OrderData,
                XBindingPath = "RecordDate",
                YBindingPath = "RecordValue",

            };

            columchart.DataMarker = new ChartDataMarker()
            {
                ShowLabel = true
            };
            DataGraph = new ChartSeriesCollection();
            DataGraph.Add(columchart);
        }
        void OrdersCompletedYear(DateTime datefrom, DateTime dateto, IEnumerable<Order> OrdersData)
        {

            var data = OrderServices.GetRecordByYear(datefrom, dateto, OrdersData);

            OrderData = new ObservableCollection<Record>(data);
            ChartTitle = "Orders Completed Year";
            var genericData = new List<Order>(OrdersData);
            OrderGenerric = new OrderGenericData()
            {
                TotalOrder = genericData.Count(),
                TotalCompleted = genericData.Where(o => o.OrderStatus == OrderStatus.Completed && (o.OrderDate >= datefrom || o.OrderDate <= dateto)).Count(),
                TotalNotCompleted = genericData.Where(o => o.OrderStatus == OrderStatus.Pending && (o.OrderDate >= datefrom || o.OrderDate <= dateto)).Count(),

            };

            ColumnSeries columchart = new ColumnSeries()
            {
                Color = Color.Green,
                EnableTooltip = true,
                ItemsSource = OrderData,
                XBindingPath = "RecordDate",
                YBindingPath = "RecordValue",

            };

            columchart.DataMarker = new ChartDataMarker()
            {
                ShowLabel = true
            };
            DataGraph = new ChartSeriesCollection();
            DataGraph.Add(columchart);
        }
        #endregion

        #region Inventory Methods
        void LowQuantity(IEnumerable<ProductItem> products, int quantity,string type)
        {
            if(!string.IsNullOrEmpty(type))
            {
                var data = new ObservableCollection<ProductItem>(products);
                InventoryData = new ObservableCollection<ProductItem>(data.Where(item => item.Quantity <= quantity && item.Category == type));
                Inventory_YData = "Quantity";
                Inventory_XData = "Name";

                XTitle = "Names";
                YTitle = "Quantity";

                SelectedType = string.Empty;
                SelectedInventoryAction = string.Empty;

                InventoryGeneric = new InventoryInformation()
                {
                    TotalItems = InventoryData.Count,
                    InventoryValue = InventoryData.Sum(i => i.Price * i.Quantity),
                    Title="Low Quantity"

                };

            }
            else
            {
                var data = new ObservableCollection<ProductItem>(products);
                InventoryData = new ObservableCollection<ProductItem>(data.Where(item => item.Quantity <= quantity));
                Inventory_YData = "Quantity";
                Inventory_XData = "Name";

                XTitle = "Names";
                YTitle = "Quantity";

                SelectedInventoryAction = string.Empty;
                InventoryGeneric = new InventoryInformation()
                {
                    TotalItems = InventoryData.Count,
                    InventoryValue = InventoryData.Sum(i => i.Price * i.Quantity),
                    Title = "Low Quantity"

                };
            }
        }
        void HighQuantity(IEnumerable<ProductItem> products, int quantity, string type)
        {
            if (!string.IsNullOrEmpty(type))
            {
                var data = new ObservableCollection<ProductItem>(products);
                InventoryData = new ObservableCollection<ProductItem>(data.Where(item => item.Quantity > quantity && item.Category == type));
                Inventory_YData = "Quantity";
                Inventory_XData = "Name";

                XTitle = "Names";
                YTitle = "Quantity";

                SelectedType = string.Empty;
                SelectedInventoryAction = string.Empty;

                InventoryGeneric = new InventoryInformation()
                {
                    TotalItems = InventoryData.Count,
                    InventoryValue = InventoryData.Sum(i => i.Price * i.Quantity),
                    Title = "High Quantity"

                };
            }
            else
            {
                var data = new ObservableCollection<ProductItem>(products);
                InventoryData = new ObservableCollection<ProductItem>(data.Where(item => item.Quantity > quantity));
                Inventory_YData = "Quantity";
                Inventory_XData = "Name";

                XTitle = "Names";
                YTitle = "Quantity";

                SelectedInventoryAction = string.Empty;

                InventoryGeneric = new InventoryInformation()
                {
                    TotalItems = InventoryData.Count,
                    InventoryValue = InventoryData.Sum(i => i.Price * i.Quantity),
                    Title = "High Quantity"

                };
            }
        }

        #endregion

        #region Goal Methods

        void ProgessDay(DateTime datefrom, DateTime dateto, IEnumerable<Order> OrderData)
        {
            //int monthfrom = DateTime.ParseExact(From[0].ToString(), "MMM", CultureInfo.InvariantCulture).Month; ;
            //int dayfrom = Convert.ToInt32(From[1]);
            //int yearfrom = Convert.ToInt32(From[2]);
            //DateTime datefrom = new DateTime(yearfrom, monthfrom, dayfrom);

            //int monthto = DateTime.ParseExact(To[0].ToString(), "MMM", CultureInfo.InvariantCulture).Month; ;
            //int dayto = Convert.ToInt32(To[1]);
            //int yearto = Convert.ToInt32(To[2]);
            //DateTime dateto = new DateTime(yearto, monthto, dayto);

            OrderServices.GetRecords(datefrom, dateto, OrderData);
            //GoalToReach= Calculator.GetTotalToReached(50, UnitTimeFilter.GetDaysFromTo(datefrom, dateto));
            GoalToReach = 050;

        }

        #endregion

        void AddActions()
        {
            //Inicializa la lista de typo Action
            OrderActions = new List<Action<DateTime, DateTime, IEnumerable<Order>>>();
            InventoryActionsList = new List<Action<IEnumerable<ProductItem>, int, string>>();

            //Obtinen los nombres de las Actions
            ActionsOrders = new List<string>(GetOrdersActions());
            InventoryActions = new List<string>(GetInventoryActions());

            //Agrega un los metodo a una lista de tipo Action  para luego ser llamado
            OrderActions.Add(OrdersCompletedDay);
            OrderActions.Add(OrdersCompletedWeek);
            OrderActions.Add(OrdersCompletedMonth);
            OrderActions.Add(OrdersCompletedYear);

            //==Inventory Actions==
            InventoryActionsList.Add(LowQuantity);
            InventoryActionsList.Add(HighQuantity);
        }

    }
}
