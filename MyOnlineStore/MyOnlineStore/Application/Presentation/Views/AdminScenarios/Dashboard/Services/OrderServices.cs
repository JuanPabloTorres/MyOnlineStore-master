using Microcharts;
using MyOnlineStore.Dashboard.DashBoardModel;
using MyOnlineStore.Entities.Models.Purchases;
using MyOnlineStore.Shared.Models.Purchases;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace MyOnlineStore.Dashboard.Services
{
    public static class OrderServices
    {
        public static List<Record> GetRecordByWeek(DateTime from, DateTime to, IEnumerable<Order> orders)
        {
            List<Record> records = new List<Record>();
            List<Order> orderbyweek = new List<Order>(orders);

            for (var day = from.Date; day.Date <= to.Date; day = day.AddDays(1))
            {
                Record record = new Record();

                record.RecordValue = orderbyweek.Where(c => c.OrderDate == day && c.OrderStatus == OrderStatus.Completed).Sum(i => i.OrderItems.Sum(s => s.ProductItem.Price));

                record.RecordDate = day;

                records.Add(record);



            }

            double totalweek = 0;

            List<Record> recordsweeks = new List<Record>();

            for (var day = from.Date; day.Date <= to.Date; day = day.AddDays(1))
            {


                if (day.DayOfWeek > DayOfWeek.Sunday)
                {
                    totalweek += records.Where(d => d.RecordDate == day).Sum(i => i.RecordValue);



                }
                if (day.DayOfWeek == DayOfWeek.Sunday)
                {
                    recordsweeks.Add(new Record()
                    {
                        RecordValue = totalweek,
                        RecordDate = day

                    });
                    totalweek = 0;
                }
                if (day == to.Date)
                {
                    recordsweeks.Add(new Record()
                    {
                        RecordValue = totalweek,
                        RecordDate = day

                    });
                    totalweek = 0;
                }





            }

            return recordsweeks;
        }


        public static List<Record> GetRecordByYear( DateTime from, DateTime to, IEnumerable<Order> orders)
        {
            List<Record> records = new List<Record>();
            List<Order> orderbyweek = new List<Order>(orders);

            for (var day = from.Date; day.Date <= to.Date; day = day.AddDays(1))
            {
                Record record = new Record();

                record.RecordValue = orderbyweek.Where(c => c.OrderDate == day && c.OrderStatus == OrderStatus.Completed).Sum(i => i.OrderItems.Sum(s => s.ProductItem.Price));

                record.RecordDate = day;

                records.Add(record);



            }

            double totalweek = 0;

            List<Record> recordyears = new List<Record>();

            for (var day = from.Date; day.Date.Year <= to.Date.Year; day = day.AddYears(1))
            {


                totalweek += records.Where(d => d.RecordDate.Year == day.Year).Sum(i => i.RecordValue);

                recordyears.Add(new Record()
                {
                    RecordValue = totalweek,
                    RecordTitle = day.Year.ToString()

                });
                totalweek = 0;

            }

            return recordyears;
        }

        public static List<Record> GetRecordByMonths(DateTime from, DateTime to, IEnumerable<Order> orders)
        {
            List<Record> records = new List<Record>();
            List<Order> orderbyweek = new List<Order>(orders);



            for (var day = from.Date; day.Date <= to.Date; day = day.AddDays(1))
            {
                Record record = new Record();

                record.RecordValue = orderbyweek.Where(c => c.OrderDate == day && c.OrderStatus == OrderStatus.Completed).Sum(i => i.OrderItems.Sum(s => s.ProductItem.Price));

                record.RecordDate = day;

                records.Add(record);



            }

            double totalweek = 0;

            List<Record> recordyears = new List<Record>();

            for (var day = from.Date; day.Date.Month <= to.Date.Month; day = day.AddMonths(1))
            {


                totalweek += records.Where(d => d.RecordDate.Month == day.Month).Sum(i => i.RecordValue);

                recordyears.Add(new Record()
                {
                    RecordValue = totalweek,
                    RecordTitle = day.Month.ToString()

                });
                totalweek = 0;

            }

            return recordyears;
        }

        //public static List<Entry> GetComplete(IEnumerable<Record> records, double ValueReached)
        //{
        //    List<Record> recordData = new List<Record>(records);
        //    List<Entry> entries = new List<Entry>();

        //    if (ValueReached != 0)
        //    {
        //        ValueReached = 0;
        //    }


        //    foreach (var item in recordData)
        //    {



        //        if (item.RecordValue >= 50)
        //        {
        //            ValueReached += item.RecordValue;
        //            entries.Add(new Entry((float)item.RecordValue)
        //            {
        //                Label = item.RecordDate.ToString("MM / dd / yyyy"),
        //                ValueLabel = item.RecordValue.ToString(),
        //                Color = SKColor.Parse("#ffcc66"),
        //                TextColor = SKColor.Parse("#ffcc66"),
        //            });
        //        }
        //    }

        //    return entries;

        //}
        //public static List<Entry> GetOrdersCompletedByUnitTime(IEnumerable<Record> records)
        //{
        //    List<Record> completed = new List<Record>(records);
        //    List<Entry> entries = new List<Entry>();

        //    foreach (var item in completed)
        //    {

        //        entries.Add(new Entry((float)item.RecordValue)
        //        {
        //            ValueLabel = "$" + item.RecordValue,
        //            Label = item.RecordTitle,
        //            Color = SKColor.Parse("#00DC7D"),
        //            TextColor = SKColor.Parse("#00DC7D"),




        //        });

        //    }

        //    return entries;
        //}
        //public static List<Entry> GetOrdersCompleted(IEnumerable<Order> orders)
        //{
        //    List<Order> completed = new List<Order>(orders);
        //    List<Entry> entries = new List<Entry>();

        //    foreach (var item in completed)
        //    {

        //        if (item.IsCompleted)
        //        {

        //            entries.Add(new Entry((float)item.OrderItems.Sum(x => x.Price))
        //            {
        //                ValueLabel = "$" + item.OrderItems.Sum(x => x.Price).ToString(),
        //                Label = item.OrderDate.ToString("MM/dd/yyyy"),
        //                Color = SKColor.Parse("#00DC7D"),
        //                TextColor = SKColor.Parse("#00DC7D"),



        //            });
        //        }
        //    }

        //    return entries;
        //}
        //public static List<Entry> GetOrdersNotCompleted(IEnumerable<Order> orders)
        //{
        //    List<Order> completed = new List<Order>(orders);
        //    List<Entry> entries = new List<Entry>();

        //    foreach (var item in completed)
        //    {

        //        if (!item.IsCompleted)
        //        {

        //            entries.Add(new Entry((float)item.OrderItems.Sum(x => x.Price))
        //            {
        //                ValueLabel = "$" + item.OrderItems.Sum(x => x.Price).ToString(),
        //                Label = item.OrderDate.ToString("MM/dd/yyyy"),
        //                Color = SKColor.Parse("#F6522E"),
        //                TextColor = SKColor.Parse("#F6522E"),



        //            });
        //        }
        //    }

        //    return entries;
        //}
        //public static List<Entry> GetOrdersNotCompletedByUnitTime(IEnumerable<Record> orders)
        //{
        //    List<Record> completed = new List<Record>(orders);
        //    List<Entry> entries = new List<Entry>();

        //    foreach (var item in completed)
        //    {



        //        entries.Add(new Entry((float)item.RecordValue)
        //        {
        //            ValueLabel = "$" + item.RecordValue,

        //            Color = SKColor.Parse("#F6522E"),
        //            TextColor = SKColor.Parse("#F6522E"),
        //        });





        //    }

        //    return entries;
        //}

        static public IEnumerable<Order> GetOrdersByDate(DateTime date, IEnumerable<Order> orders)
        {
            return orders.Where(x => x.OrderDate == date);
        }

        static public int GetByCompleted(DateTime date, IEnumerable<Order> orders)
        {
            var completed = orders.Where(x => x.OrderStatus == OrderStatus.Completed && x.OrderDate == date).ToList();

            return completed.Count();
        }

        //static public double GetValueOfItemsPurchaseWithOrderofDate(DateTime date, IEnumerable<Order> orders)
        //{
        //    throw Exception
        //    //return orders.Where(s => s.OrderDate == date).Sum(x => x.OrderItems.Sum(z => z.ProductItem.Price * z.));
        //}

        //static public double GetValueOfItemsPurchaseWithOrder(DateTime date, IEnumerable<Order> orders)
        //{
        //    var selectedOrders = new List<Order>(orders.Where(s => s.OrderDate == date));

        //    double progress = selectedOrders.Sum(x => x.OrderItems.Sum(s => s.Price));

        //    return progress;
        //}

        static public IEnumerable<Record> GetRecords(DateTime from, DateTime to, IEnumerable<Order> orders)
        {
            List<Record> records = new List<Record>();

            for (var day = from.Date; day.Date <= to.Date; day = day.AddDays(1))
            {
                Record record = new Record();

                record.Orders = orders.Where(x => x.OrderDate == day && x.OrderStatus == OrderStatus.Completed).ToList();

                record.RecordValue = record.Orders.Sum(x => x.OrderItems.Sum(s => s.ProductItem.Price));

                record.RecordDate = day;

                records.Add(record);



            }

            return records;

        }

        static public IEnumerable<Record> GetRecordsNotCompleted(DateTime from, DateTime to, IEnumerable<Order> orders)
        {
            List<Record> records = new List<Record>();

            for (var day = from.Date; day.Date <= to.Date; day = day.AddDays(1))
            {
                Record record = new Record();

                record.Orders = orders.Where(x => x.OrderDate == day && x.OrderStatus == OrderStatus.Pending).ToList();

                record.RecordValue = record.Orders.Sum(x => x.OrderItems.Sum(s => s.ProductItem.Price));

                record.RecordDate = day;

                records.Add(record);



            }

            return records;

        }

        static public IEnumerable<Order> GetProgressFromDateTo(DateTime from, DateTime to, IEnumerable<Order> orders)
        {
            Record record = new Record();

            record.Orders = orders.Where(x => x.OrderDate >= from || x.OrderDate <= to).ToList();

            return record.Orders;

        }

        static public IEnumerable<Order> GetOrdersFromDateToDate(DateTime from, DateTime to, IEnumerable<Order> orders)
        {
            var result = new ObservableCollection<Order>(orders.Where(x => x.OrderDate >= from || x.OrderDate <= to));

            return result;
        }
    }
}
