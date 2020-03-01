using MyOnlineStore.Entities.Models.Interfaces;
using MyOnlineStore.Shared.Models.Purchases;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyOnlineStore.Entities.Models.Purchases
{
    [Table("Orders")]
    public class Order : IBaseEntity
    {
        #region Fields

        private OrderStatus? status;

        #endregion Fields

        #region Constructors

        public Order()
        {
            //MyStore = new Store();
        }

        public Order(Order order)
        {
            OrderItems = order.OrderItems;
            Id = order.Id;
            MyStoreId = order.MyStoreId;
            OrderDate = order.OrderDate;
            PayedWithCash = order.PayedWithCash;

            if(OrderStatus is object)
            {
                Status = OrderStatus.NameOfStatus;
            }
        }

        #endregion Constructors

        #region Properties

        public Guid Id { get; set; }
        public List<OrdersProductItems>? OrderItems { get; set; }

        //public Store MyStore { get; set; }
        public Guid MyStoreId { get; set; }

        public DateTime OrderDate { get; set; }

        public Guid? CardId { get; set; }

        public bool PayedWithCash { get; set; }

        public Guid MyClientId { get; set; }

        public virtual OrderStatus? OrderStatus
        {
            get => status;
            set
            {
                status = value;
                if(status is object)
                {
                    Status = status.NameOfStatus;
                }
            }
        }

        public string Status { get; private set; } = string.Empty;

        public bool IsAlive { get; set; }

        #endregion Properties
    }
}