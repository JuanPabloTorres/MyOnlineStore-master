using System;

namespace MyOnlineStore.Shared.Models.Stores
{
    public class DayOfTheWeek
    {
        #region Fields

        public static DayOfTheWeek Monday = new DayOfTheWeek 
            { DayName = nameof(DayOfWeek.Monday) };

        public static DayOfTheWeek Tuesday = new DayOfTheWeek 
            { DayName = nameof(DayOfWeek.Tuesday) };

        public static DayOfTheWeek Wednesday = new DayOfTheWeek 
            { DayName = nameof(DayOfWeek.Wednesday) };

        public static DayOfTheWeek Thursday = new DayOfTheWeek 
            { DayName = nameof(DayOfWeek.Thursday) };

        public static DayOfTheWeek Friday = new DayOfTheWeek 
            { DayName = nameof(DayOfWeek.Friday) };

        public static DayOfTheWeek Saturday = new DayOfTheWeek 
            { DayName = nameof(DayOfWeek.Saturday) };

        public static DayOfTheWeek Sunday = new DayOfTheWeek 
            { DayName = nameof(DayOfWeek.Sunday) };

        #endregion Fields

        #region Constructors

        private DayOfTheWeek()
        {
        }

        #endregion Constructors

        #region Properties

        public Guid Id { get; set; }
        public string DayName { get; private set; } = string.Empty;

        #endregion Properties
    }
}