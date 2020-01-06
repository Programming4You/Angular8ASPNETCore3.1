using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Models;
using WebAPI.Repository;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : CoreController<Department, DepartmentRepository>
    {
        private readonly DepartmentRepository _departmentRepository;

        public DepartmentController(DepartmentRepository departmentRepository) : base(departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }

        // GET: api/Department
        [HttpGet]
        public async Task<IEnumerable<Department>> GetDepartment()
        {
            try
            {
                return await _departmentRepository.GetDepartment();
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
                return await _departmentRepository.GetDepartment(id);
            }
            catch (Exception)
            {
                throw;
            }
        }

        // PUT: api/Department/5
        [HttpPut("{id}")]
        public async Task<bool> PutDepartment(int id, Department department)
        {
            try
            {
                return await _departmentRepository.PutDepartment(id, department);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // POST: api/Department
        [HttpPost]
        public async Task<ActionResult<Department>> PostDepartment(Department department)
        {
            try
            {
                await _departmentRepository.PostDepartment(department);

                return CreatedAtAction("GetDepartment", new { id = department.DepartmentID }, department);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // DELETE: api/Department/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Department>> DeleteDepartment(int id)
        {
            try
            {
                var department = await _departmentRepository.DeleteDepartment(id);

                if (department == null)
                {
                    return NotFound();
                }

                return department;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool DepartmentExists(int id)
        {
            return _departmentRepository.DepartmentExists(id);
        }

    }
}