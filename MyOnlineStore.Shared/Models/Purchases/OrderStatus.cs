using System.ComponentModel.DataAnnotations;

namespace MyOnlineStore.Shared.Models.Purchases
{
    public class OrderStatus
    {
        [Key]
        public string NameOfStatus { get; set; } = string.Empty;

        public static readonly OrderStatus Pending = new OrderStatus { NameOfStatus = nameof(Pending) };
        public static readonly OrderStatus Completed = new OrderStatus { NameOfStatus = nameof(Completed) };

        public OrderStatus()
        {
        }
    }
}