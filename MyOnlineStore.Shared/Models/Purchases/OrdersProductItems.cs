using MyOnlineStore.Entities.Models.Purchases;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyOnlineStore.Shared.Models.Purchases
{
    [Table("OrdersProductItems")]
    public class OrdersProductItems /*: IBaseEntity*/
    {
        //public Guid Id { get; set; }
        public Guid OrderId { get; set; }

        public Guid ProductItemId { get; set; }
        public ProductItem ProductItem { get; set; }
        public uint QuantityOfItem { get; set; } = uint.MinValue;
        //public string CreatedBy { get;  set; } = string.Empty;

        //public string UpdatedBy { get;  set; } = string.Empty;

        //public DateTime CreatedDateTime { get;  set; }

        //public DateTime UpdateDateTime { get;  set; }

        //public bool IsAlive { get;  set; }
        public OrdersProductItems()
        {
            ProductItem = new ProductItem();
        }

        public OrdersProductItems(OrdersProductItems ordersProduct)
        {
            OrderId = ordersProduct.OrderId;
            ProductItem = ordersProduct.ProductItem;
            QuantityOfItem = ordersProduct.QuantityOfItem;
        }
    }
}