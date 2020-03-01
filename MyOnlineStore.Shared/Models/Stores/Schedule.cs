using MyOnlineStore.Entities.Models.Interfaces;
using MyOnlineStore.Entities.Models.Stores;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyOnlineStore.Shared.Models.Stores
{
    public class Schedule : IBaseEntity
    {
        public Guid Id { get; set; }

        public bool IsAlive { get; set; }

        public Guid StoreId { get; set; }
        public Store? Store { get; set; }

        public List<WorkingHour> WorkingHours { get; set; }
    }
}
