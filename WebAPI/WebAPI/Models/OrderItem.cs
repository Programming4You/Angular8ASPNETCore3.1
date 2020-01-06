using System.ComponentModel.DataAnnotations;

namespace WebAPI.Models
{
    public class OrderItem
    {
        [Key]
        public long? OrderItemID { get; set; }
        public long? OrderID { get; set; }
        public int? ItemID { get; set; }
        public int? Quantity { get; set; }
    }
}
