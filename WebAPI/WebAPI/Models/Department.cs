using WebAPI.Interfaces;

namespace WebAPI.Models
{
    public class Department : IEntity
    {
        public int DepartmentID { get; set; }
        public string DepartmentName { get; set; }
    }
}
