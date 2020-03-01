using System;
using System.Collections.Generic;

namespace MyOnlineStore.Shared.Models.Stores
{
    public class WorkingHour
    {
        public int Id { get; set; }

        public Guid WorkingHourOwnerId { get; set; }

        public DayOfTheWeek Day { get; set; }

        public TimeSpan From { get; set; }

        public TimeSpan To { get; set; }

        public bool AllDay { get; set; }

        public bool NoWork { get; set; }

        public WorkingHour()
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