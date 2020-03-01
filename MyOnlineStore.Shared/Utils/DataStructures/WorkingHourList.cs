using MyOnlineStore.Shared.Models.Stores;
using System.Collections.Generic;

namespace MyOnlineStore.Shared.Utils.DataStructures
{
    /**
     * Will always be order by week days
     */

    public class WorkingHourList : BaseList<WorkingHour>
    {
        public WorkingHourList()
        {
        }

        public WorkingHourList(IEnumerable<WorkingHour> collection) : base(collection)
        {
        }

        public WorkingHourList(int capacity) : base(capacity)
        {
        }

        #region Services

        //public bool ReplaceWorkingHour(WorkingHour workingHour)
        //{
        //    Sort()
        //}

        #endregion Services
    }
}