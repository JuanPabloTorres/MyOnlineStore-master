using MyOnlineStore.Entities.Models.Purchases;
using System;
using System.Collections.Generic;

namespace MyOnlineStore.Dashboard.DashBoardModel
{
    public class Record
    {
        public Record()
        {

        }

        private DateTime recorddate;

        public List<Order> Orders { get; set; }

        public DateTime RecordDate
        {
            get { return recorddate; }
            set { recorddate = value; }
        }

        private double recordvalue;

        public double RecordValue
        {
            get { return recordvalue; }
            set { recordvalue = value; }
        }

        private string recordtitle;

        public string RecordTitle
        {
            get { return recordtitle; }
            set { recordtitle = value; }
        }





    }
}
