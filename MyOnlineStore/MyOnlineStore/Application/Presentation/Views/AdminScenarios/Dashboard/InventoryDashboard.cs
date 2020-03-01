using Microcharts;
using MyOnlineStore.Shared.Models.Purchases;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyOnlineStore.Dashboard.DashBoardModel
{
    public class InventoryDashboard:IDashboard
    {

        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public Chart Dashboard { get; set; }
        public List<Entry> Entries { get; set; }
        public string OptionSelected { get; set; }
        public string Title { get ; set ; }

        public const int WidthRequest = 10 * 50;

        public Unit SelectedUnit;

        public InventoryDashboard()
        {

        }

        void InventoryChartChanged(List<Entry> entries)
        {
            Dashboard = new BarChart()
            {
                Entries = entries,
                LabelTextSize = 30f,


            };
        }
       



    }

   public enum Unit
    {
        Days,
        Weeks,
        Month,
        Year,
    
    }


}
