using WebAPI.Interfaces;

namespace WebAPI.Models
{
    public class Item : IEntity
    {
        public int ItemID { get; set; }
        public string Name { get; set; }
        public decimal? Price { get; set; }
    }
}
