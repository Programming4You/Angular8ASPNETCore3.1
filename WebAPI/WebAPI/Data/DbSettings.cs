using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebAPI.Models;

namespace WebAPI.Data
{
    public class DbSettings : IdentityDbContext<AspNetUsers>  //DbContext
    {
        public DbSettings(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Customer> Customer { get; set; }
        public DbSet<Item> Item { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<OrderItem> OrderItem { get; set; }
        public DbSet<AspNetUsers> AspNetUsers { get; set; }
        public DbSet<Employee> Employee { get; set; }
        public DbSet<Department> Department { get; set; }
    }
}
