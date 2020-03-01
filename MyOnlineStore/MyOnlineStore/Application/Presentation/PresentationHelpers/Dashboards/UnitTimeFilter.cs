using MyOnlineStore.Dashboard.DashBoardModel;
using MyOnlineStore.Entities.Models.Purchases;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyOnlineStore.Dashboard.Filters
{
    static class UnitTimeFilter
    {
     public  static List<Record> GetRecordByWeek(DateTime from, DateTime to,IEnumerable<Order> orders,bool status)
        {
            List<Record> records = new List<Record>();
            List<Order> orderbyweek = new List<Order>(orders);

            for (var day = from.Date; day.Date <= to.Date; day = day.AddDays(1))
            {
                Record record = new Record();

                record.RecordValue = orderbyweek.Where(c => c.OrderDate == day && c.IsCompleted == status).Sum(i => i.OrderItems.Sum(s => s.Price));

                record.RecordDate = day;

                records.Add(record);



            }

            double totalweek = 0;

            List<Record> recordsweeks = new List<Record>();

            int weektitle = 0;


            for (var day = from.Date; day.Date <= to.Date; day = day.AddDays(1))
            {

               


                if (day.DayOfWeek > DayOfWeek.Sunday)
                {
                    totalweek += records.Where(d => d.RecordDate == day).Sum(i => i.RecordValue);



                }
                if (day.DayOfWeek == DayOfWeek.Sunday)
                {

                    if (records.Where(r => r.RecordDate == day).FirstOrDefault() != null)
                    {
                        totalweek += records.Where(d => d.RecordDate == day).Sum(i => i.RecordValue);

                    }

                    weektitle++;
                    recordsweeks.Add(new Record()
                    {
                        RecordValue = totalweek,
                        RecordTitle = weektitle.ToString(),

                    });
                    totalweek = 0;
                }
                if (day == to.Date)
                {
                    weektitle++;
                    recordsweeks.Add(new Record()
                    {
                        RecordValue = totalweek,
                        RecordTitle = weektitle.ToString(),
                        RecordDate = day.Date

                    }) ;
                    totalweek = 0;
                }





            }

            return recordsweeks;
        }
        public static int NumberOfWeeks(DateTime dateFrom, DateTime dateTo)
        {
            TimeSpan Span = dateTo.Subtract(dateFrom);

            if (Span.Days <= 7)
            {
                if (dateFrom.DayOfWeek > dateTo.DayOfWeek)
                {
                    return 2;
                }

                return 1;
            }

            int Days = Span.Days - 7 + (int)dateFrom.DayOfWeek;
            int WeekCount = 1;
            int DayCount = 0;

            for (WeekCount = 1; DayCount < Days; WeekCount++)
            {
                DayCount += 7;
            }

            return WeekCount;
        }


        public static List<Record> GetRecordByYear(DateTime from,DateTime to , IEnumerable<Order> orders,bool status)
        {
            List<Record> records = new List<Record>();
            List<Order> orderbyweek = new List<Order>(orders);

            for (var day = from.Date; day.Date <= to.Date; day = day.AddDays(1))
            {
                Record record = new Record();

                record.RecordValue = orderbyweek.Where(c => c.OrderDate == day && c.IsCompleted == status).Sum(i => i.OrderItems.Sum(s => s.Price));

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
                    RecordTitle = day.Year.ToString(),
                    RecordDate=day

                });
                totalweek = 0;

            }

            return recordyears;
        }

     public static  List<Record> GetRecordByMonths(DateTime from, DateTime to,IEnumerable<Order> orders,bool status)
        {
            List<Record> records = new List<Record>();
            List<Order> orderbyweek = new List<Order>(orders);



            for (var day = from.Date; day.Date <= to.Date; day = day.AddDays(1))
            {
                Record record = new Record();

                record.RecordValue = orderbyweek.Where(c => c.OrderDate == day && c.IsCompleted == status).Sum(i => i.OrderItems.Sum(s => s.Price));

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
                    RecordTitle = day.ToString("MMMM"),
                    RecordDate= day

                }) ;
                totalweek = 0;

            }

            return recordyears;

        }

        public static int GetDaysFromTo(DateTime from, DateTime to)
        {

            int days = 0;
            for (var day = from.Date; day.Date <= to.Date; day = day.AddDays(1))
            {

                days++;


            }

            return days;

        }

        //New methods====================================================================================

        public static List<Record> GetRecordbyWeek(DateTime from, DateTime to, IEnumerable<Order> orders)
        {
            List<Record> records = new List<Record>();
            List<Order> orderbyweek = new List<Order>(orders);

            for (var day = from.Date; day.Date <= to.Date; day = day.AddDays(1))
            {
                Record record = new Record();

                record.RecordValue = orderbyweek.Where(c => c.OrderDate == day).Sum(i => i.OrderItems.Sum(s => s.Price));

                record.RecordDate = day;

                records.Add(record);



            }

            double totalweek = 0;

            List<Record> recordsweeks = new List<Record>();

            for (var day = from.Date; day.Date <= to.Date; day = day.AddDays(1))
            {


                if (day.DayOfWeek >= DayOfWeek.Monday && day.DayOfWeek <= DayOfWeek.Sunday)
                {
                    totalweek += records.Where(d => d.RecordDate == day).Sum(i => i.RecordValue);

                    if(day.DayOfWeek == DayOfWeek.Sunday)
                    {
                        recordsweeks.Add(new Record()
                        {
                            RecordValue = totalweek,
                            RecordDate = day

                        });
                        totalweek = 0;
                    }

                }

                //if (day.DayOfWeek > DayOfWeek.Sunday)
                //{
                //    totalweek += records.Where(d => d.RecordDate == day).Sum(i => i.RecordValue);



                //}
                //if (day.DayOfWeek == DayOfWeek.Sunday)
                //{

                //    if (records.Where(r => r.RecordDate == day).FirstOrDefault() != null)
                //    {
                //        totalweek += records.Where(d => d.RecordDate == day).Sum(i => i.RecordValue);

                //    }

                //    recordsweeks.Add(new Record()
                //    {
                //        RecordValue = totalweek,
                //        RecordDate = day

                //    });
                //    totalweek = 0;
                //}
                //if (day == to.Date)
                //{
                //    recordsweeks.Add(new Record()
                //    {
                //        RecordValue = totalweek,
                //        RecordDate = day

                //    });
                //    totalweek = 0;
                //}





            }

            return recordsweeks;
        }


        public static List<Record> GetRecordByYear(DateTime from, DateTime to, IEnumerable<Order> orders)
        {
            List<Record> records = new List<Record>();
            List<Order> orderbyweek = new List<Order>(orders);

            for (var day = from.Date; day.Date <= to.Date; day = day.AddDays(1))
            {
                Record record = new Record();

                record.RecordValue = orderbyweek.Where(c => c.OrderDate == day ).Sum(i => i.OrderItems.Sum(s => s.Price));

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

        public static List<Record> GetRecordByMonths(DateTime from, DateTime to, IEnumerable<Order> orders )
        {
            List<Record> records = new List<Record>();
            List<Order> orderbyweek = new List<Order>(orders);



            for (var day = from.Date; day.Date <= to.Date; day = day.AddDays(1))
            {
                Record record = new Record();

                record.RecordValue = orderbyweek.Where(c => c.OrderDate == day ).Sum(i => i.OrderItems.Sum(s => s.Price));

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
    }
}
