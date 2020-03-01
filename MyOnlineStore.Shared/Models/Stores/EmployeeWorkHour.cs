using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MyOnlineStore.Shared.Models.Stores
{
    [Table("EmployeeWorkHour")]
    public class EmployeeWorkHour
    {

        [Key]
        public Guid WorkHourId { get; set; }

        public Guid EmployeeId { get; set; }
        public Guid StoreId { get; set; }

        public TimeSpan Start { get; set; }

        public TimeSpan End { get; set; }

        public string Day { get; set; }

        public EmployeeWorkHour()
        {

        }
    }
}
