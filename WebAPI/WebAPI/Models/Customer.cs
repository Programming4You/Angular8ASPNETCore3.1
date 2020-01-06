using WebAPI.Interfaces;

namespace WebAPI.Models
{
    public class Customer : IEntity
    {
        public int CustomerID { get; set; }
        public string Name { get; set; }
    }
}
