using Microcharts;
using MyOnlineStore.Dashboard.DashBoardModel;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace MyOnlineStore.Dashboard.Services
{
   public class GoalServices
    {
      public static  List<Entry> GetgoalpastDays(IEnumerable<Record> records,out double ValueReached)
        {

            ValueReached = 0;

            List<Record> recordData = new List<Record>(records);
            List<Entry> entries = new List<Entry>();

            if (ValueReached != 0)
            {
                ValueReached = 0;
            }


            foreach (var item in recordData)
            {

                //Suma el total del valor alcanzado de todos los records. (Meta llegada)
                ValueReached += item.RecordValue;

                if (item.RecordValue < 50)
                {
                    entries.Add(new Entry((float)item.RecordValue)
                    {
                        Label = item.RecordDate.DayOfWeek.ToString() + item.RecordDate.Day.ToString(),
                        ValueLabel = item.RecordValue.ToString(),
                        Color = SKColor.Parse("#ff0000"),
                        TextColor = SKColor.Parse("#ff0000"),



                    });
                }
                else
                {
                    entries.Add(new Entry((float)item.RecordValue)
                    {
                        Label = item.RecordDate.DayOfWeek.ToString() + item.RecordDate.Day.ToString(),
                        ValueLabel = item.RecordValue.ToString(),
                        Color = SKColor.Parse("#ffcc66"),
                        TextColor = SKColor.Parse("#ffcc66"),



                    });
                }


            }

            return entries;

        }
        public static List<Entry> Getgoalweek(IEnumerable<Record> records,out double ValueReached)
        {
            List<Record> recordData = new List<Record>(records);
            List<Entry> entries = new List<Entry>();


            ValueReached = 0;
            foreach (var item in recordData)
            {

                ValueReached += item.RecordValue;
                if (item.RecordValue < 50 * 7)
                {
                    entries.Add(new Entry((float)item.RecordValue)
                    {
                        Label = item.RecordDate.ToString("MM / dd / yyyy"),
                        ValueLabel = item.RecordValue.ToString(),
                        Color = SKColor.Parse("#ff0000"),
                        TextColor = SKColor.Parse("#ff0000"),



                    });
                }

            }

            return entries;
        }

       public static List<Entry> Getgoalbymonths(IEnumerable<Record> records,out double val)
        {
            List<Record> recordData = new List<Record>(records);
            List<Entry> entries = new List<Entry>();

            val = 0;
            foreach (var item in recordData)
            {
                val += item.RecordValue;
                if (item.RecordValue < 50 * DateTime.DaysInMonth(item.RecordDate.Year, item.RecordDate.Month))
                {
                    entries.Add(new Entry((float)item.RecordValue)
                    {
                        Label = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(item.RecordDate.Month),
                        ValueLabel = item.RecordValue.ToString(),
                        Color = SKColor.Parse("#ff0000"),
                        TextColor = SKColor.Parse("#ff0000"),



                    });
                }
                else
                {
                    entries.Add(new Entry((float)item.RecordValue)
                    {
                        Label = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(item.RecordDate.Month),
                        ValueLabel = item.RecordValue.ToString(),
                        Color = SKColor.Parse("#ffcc66"),
                        TextColor = SKColor.Parse("#ffcc66"),
                    });
                }

            }

            return entries;

        }

       public static List<Entry> Getgoalbyyear(IEnumerable<Record> records, out double val)
        {
            List<Record> recordData = new List<Record>(records);
            List<Entry> entries = new List<Entry>();

            val = 0;

            foreach (var item in recordData)
            {
                DateTime date = new DateTime();

                date = item.RecordDate;

                val += item.RecordValue;

                if (item.RecordValue < 50 * date.DayOfYear)
                {
                    entries.Add(new Entry((float)item.RecordValue)
                    {
                        Label = item.RecordTitle,
                        ValueLabel = item.RecordValue.ToString(),
                        Color = SKColor.Parse("#ff0000"),
                        TextColor = SKColor.Parse("#ff0000"),




                    });
                }
                else
                {
                    entries.Add(new Entry((float)item.RecordValue)
                    {
                        Label = item.RecordTitle,
                        ValueLabel = item.RecordValue.ToString(),
                        Color = SKColor.Parse("#00DC7D"),
                        TextColor = SKColor.Parse("#00DC7D"),




                    });
                }

            }

            return entries;
        }


    }
}
