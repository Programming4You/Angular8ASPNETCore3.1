using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Data;
using WebAPI.Models;

namespace WebAPI.Repository
{
    public class EmployeeRepository : CoreRepository<Employee, DbSettings>
    {
        private readonly DbSettings _context;
        public EmployeeRepository(DbSettings context) : base(context)
        {
            _context = context;
        }


        public bool ExistsEmployee(int id)
        {
            return _context.Employee.Any(e => e.EmployeeID == id);
        }

        public async Task<Employee> GetEmployee(int id)
        {
            return await _context.Employee.FirstOrDefaultAsync(e => e.EmployeeID == id);
        }

        [HttpGet]
        public async Task<IEnumerable<Employee>> GetAllEmployees()
        {
            try
            {
                return await _context.Employee.Include(x => x.Department).ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> PostEmployee(Employee model)
        {
            try
            {
                var employee = new Employee()
                {
                    FullName = model.FullName,
                    Email = model.Email,
                    Mobile = model.Mobile,
                    City = model.City,
                    Gender = model.Gender,
                    DepartmentID = model.DepartmentID,
                    HireDate = model.HireDate,
                    IsPermanent = model.IsPermanent
                };

                _context.Employee.Add(employee);
                await _context.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return true;
        }

        public async Task<bool> PutEmployee(int id, Employee employee)
        {
            var emp = _context.Employee.Find(id);
            var employeeModel = new Employee()
            {
                EmployeeID = emp.EmployeeID,
                FullName = employee.FullName,
                Email = employee.Email,
                Mobile = employee.Mobile,
                City = employee.City,
                Gender = employee.Gender,
                DepartmentID = employee.DepartmentID,
                HireDate = employee.HireDate,
                IsPermanent = employee.IsPermanent
            };

            try
            {
                _context.Entry(emp).CurrentValues.SetValues(employeeModel);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return true;
        }

        public async Task<object> DeleteEmployee(int id)
        {
            try
            {
                var employee = await _context.Employee.FindAsync(id);
                _context.Employee.Remove(employee);
                await _context.SaveChangesAsync();

                return employee;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
