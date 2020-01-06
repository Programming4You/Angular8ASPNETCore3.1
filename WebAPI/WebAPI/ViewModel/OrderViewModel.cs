using System.Collections.Generic;
using WebAPI.Models;

namespace WebAPI.ViewModel
{
    public class OrderViewModel
    {
        public long? OrderID { get; set; }
        public string OrderNo { get; set; }
        public int? CustomerID { get; set; }
        public string CustomerName { get; set; }
        public string PMethod { get; set; }
        public decimal? GTotal { get; set; }

        public List<OrderItem> OrderItems { get; set; }

        public Order Order { get; set; }
        public List<OrderItemViewModel> OrderDetails { get; set; }

    }
}
