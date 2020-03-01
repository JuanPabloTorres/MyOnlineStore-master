using Microcharts;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyOnlineStore.Dashboard.DashBoardModel
{
    public interface IDashboard
    {
        Chart Dashboard { get; set; }
        List<Entry> Entries { get; set; }

         string Title { get; set; }

        string OptionSelected { get; set; }

         DateTime From { get; set; }
         DateTime To { get; set; }
    }
}
