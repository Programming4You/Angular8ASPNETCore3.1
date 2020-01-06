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
    public class DepartmentRepository : CoreRepository<Department, DbSettings>
    {
        private readonly DbSettings _context;

        public DepartmentRepository(DbSettings context) : base(context)
        {
            _context = context;
        }


        // GET: api/Department
        [HttpGet]
        public async Task<IEnumerable<Department>> GetDepartment()
        {
            try
            {
                return await _context.Department.ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // GET: api/Department/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Department>> GetDepartment(int id)
        {
            try
            {
                var department = await _context.Department.FindAsync(id);

                return department;
            }
            catch (Exception ex)
            {
                throw ex;
            } 
        }

        // PUT: api/Department/5
        [HttpPut("{id}")]
        public async Task<bool> PutDepartment(int id, Department department)
        {
            if (id != department.DepartmentID)
            {
                return false;
            }

            try
            {
                _context.Entry(department).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!DepartmentExists(id))
                {
                    return false;
                }
                else
                {
                    throw ex;
                }
            }

            return true;
        }

        // POST: api/Department
        [HttpPost]
        public async Task<bool> PostDepartment(Department department)
        {
            try
            {
                _context.Department.Add(department);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // DELETE: api/Department/5
        [HttpDelete("{id}")]
        public async Task<Department> DeleteDepartment(int id)
        {
            try
            {
                var department = await _context.Department.FindAsync(id);

                _context.Department.Remove(department);
                await _context.SaveChangesAsync();
                return department;
            }
            catch (Exception)
            {
                throw;
            }
  
        }

        public bool DepartmentExists(int id)
        {
            return _context.Department.Any(e => e.DepartmentID == id);
        }

    }
}
