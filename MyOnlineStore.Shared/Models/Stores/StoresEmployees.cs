using MyOnlineStore.Entities.Models.Interfaces;
using MyOnlineStore.Shared.Models.Stores;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyOnlineStore.Entities.Models.Stores
{
    [Table("StoresEmployees")]
    public class StoresEmployees : IBaseEntity
    {
        #region Constructors

        public StoresEmployees()
        {
        }

        #endregion Constructors

        #region Properties

        public Guid Id { get; set; }

        public Guid UserEmployeeId { get; set; }

        public Guid StoreId { get; set; }
        public Store? Store { get; set; }

        public string FullName { get; set; }         

        public Position EmployeePosition { get; set; }


      
        public List<EmployeeWorkHour>? EmployeeWork { get; set; }

        public bool IsAlive { get; set; }

        #endregion Properties
    }

    public enum Status
    {
        NewEmployee,
        OnWork,
        Lunch,
        Free,
        
    }
    public enum Position
    {
        None,
        Preparer,
        Delivery,
        Receptionist,
        Manager,
        Supervisor,



    }

}