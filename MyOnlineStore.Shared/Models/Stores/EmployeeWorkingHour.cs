using System;
using System.Collections.Generic;
using System.Text;

namespace MyOnlineStore.Shared.Models.Stores
{
    class EmployeeWorkingHour
    {
        public int Id { get; set; }

        public DayOfTheWeek Day { get; set; }

        public TimeSpan From { get; set; }

        public TimeSpan To { get; set; }

        public bool AllDay { get; set; }

        public bool NoWork { get; set; }

        public EmployeeWorkingHour()
        {
            Day = DayOfTheWeek.Monday;
        }

        public static IEnumerable<WorkingHour> AllWeekDaysInit()
        {
            return new List<WorkingHour>
            {
                new WorkingHour
                {
                    Day = DayOfTheWeek.Monday,
                    From = TimeSpan.FromHours(8),
                    To = TimeSpan.FromHours(18),
                    AllDay = false,
                    NoWork = false
                },
                new WorkingHour
                {
                    Day = DayOfTheWeek.Tuesday,
                    From = TimeSpan.FromHours(8),
                    To = TimeSpan.FromHours(18),
                    AllDay = false,
                    NoWork = false
                },
                new WorkingHour
                {
                    Day = DayOfTheWeek.Wednesday,
                    From = TimeSpan.FromHours(8),
                    To = TimeSpan.FromHours(18),
                    AllDay = false,
                    NoWork = false
                },
                new WorkingHour
                {
                    Day = DayOfTheWeek.Thursday,
                    From = TimeSpan.FromHours(8),
                    To = TimeSpan.FromHours(18),
                    AllDay = false,
                    NoWork = false
                },
                new WorkingHour
                {
                    Day = DayOfTheWeek.Friday,
                    From = TimeSpan.FromHours(8),
                    To = TimeSpan.FromHours(18),
                    AllDay = false,
                    NoWork = false
                },
                new WorkingHour
                {
                    Day = DayOfTheWeek.Saturday,
                    From = TimeSpan.FromHours(8),
                    To = TimeSpan.FromHours(18),
                    AllDay = false,
                    NoWork = true
                },
                new WorkingHour
                {
                    Day = DayOfTheWeek.Sunday,
                    From = TimeSpan.FromHours(8),
                    To = TimeSpan.FromHours(18),
                    AllDay = false,
                    NoWork = true
                }
            };
        }
    }
}
