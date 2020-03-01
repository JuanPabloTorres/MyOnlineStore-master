using Microcharts;
using MyOnlineStore.Reports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Microsoft.Extensions.DependencyInjection;
using Entry = Microcharts.Entry;

namespace MyOnlineStore.Views.Administrator
{
    [Xamarin.Forms.Xaml.XamlCompilation(Xamarin.Forms.Xaml.XamlCompilationOptions.Compile)]
    public partial class ReportsPage : ContentPage
    {
        List<Analityc> dashboardData;
        public ReportsPage()
        {
            InitializeComponent();


            chartView.Chart = new LineChart()
            {
                Entries =DaySales(),
                LineMode = LineMode.Spline,
                LineSize = 3,
                PointMode = PointMode.Circle,
                PointSize = 4,
                 LabelTextSize= 30, 
                
            };
           
            BindingContext = this;

        }

        private void ChartView_Touch(object sender, SkiaSharp.Views.Forms.SKTouchEventArgs e)
        {

            var l = e.Location;
            chartView.HeightRequest += 50;
           
            
        }

        List<Entry> DaySales()
        {
            List<Analityc> dashboardData = new List<Analityc>()
            {
                new Analityc()
                {
                     Date=DateTime.Now.AddDays(1),
                      Total=20,
                },
                 new Analityc()
                {
                     Date=DateTime.Now.AddDays(2),
                      Total=25,
                },
                  new Analityc()
                {
                     Date=DateTime.Now.AddDays(3),
                      Total=50,
                },
                   new Analityc()
                {
                     Date=DateTime.Now.AddDays(4),
                      Total=60,
                },
            };

            List<Entry> dataentries = new List<Entry>();


            foreach (var item in dashboardData)
            {

                Entry entry = new Entry(item.Total)
                {
                    Color = SkiaSharp.SKColor.Parse("#313d4e"),
                    Label = item.Date.ToString(),
                    ValueLabel = item.Total.ToString(),
                };
                chartView.WidthRequest += 50;
                dataentries.Add(entry);

            }

            return dataentries;
        }
        List<Entry> Inventory()
        {
            List<Analityc> dashboardData = new List<Analityc>()
            {
                new Analityc()
                {
                     Date=DateTime.Now.AddDays(1),
                      Total=20,
                },
                 new Analityc()
                {
                     Date=DateTime.Now.AddDays(2),
                      Total=25,
                },
                  new Analityc()
                {
                     Date=DateTime.Now.AddDays(3),
                      Total=50,
                },
                   new Analityc()
                {
                     Date=DateTime.Now.AddDays(4),
                      Total=60,
                },
            };

            List<Entry> dataentries = new List<Entry>();


         

                float totalItems = dashboardData.Count;

                float totalavailable = (totalItems - 2) / totalItems;
               

                Entry Total = new Entry(totalItems)
                {
                    Color = SkiaSharp.SKColor.Parse("#313d4e"),
                    Label = "Total:"+totalItems.ToString(),
                    ValueLabel = totalItems.ToString()
                };

            Entry Available = new Entry(totalavailable)
            {
                Color = SkiaSharp.SKColor.Parse("#49D19D"),
                Label = "Total Available:" + totalavailable.ToString(),
                ValueLabel = totalavailable.ToString()
            };

            dataentries.Add(Total);
            dataentries.Add(Available);

            return dataentries;

        }

      
    }
}