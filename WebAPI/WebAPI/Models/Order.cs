using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using WebAPI.Interfaces;

namespace WebAPI.Models
{
    public class Order : IEntity
    {
        public long? OrderID { get; set; }
        public string OrderNo { get; set; }
        public int? CustomerID { get; set; }
        public string PMethod { get; set; }
        public decimal? GTotal { get; set; }

        [NotMapped]
        public string DeletedOrderItemIDs { get; set; }

        public List<OrderItem> OrderItems { get; set; }
    }
}
