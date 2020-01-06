using System;
using WebAPI.Interfaces;

namespace WebAPI.Models
{
    public class Employee : IEntity
    {
        public int EmployeeID { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string City { get; set; }
        public int Gender { get; set; }
        public Department Department { get; set; }
        public int DepartmentID { get; set; }
        public DateTime HireDate { get; set; }
        public bool IsPermanent { get; set; } = false;
    }
}
