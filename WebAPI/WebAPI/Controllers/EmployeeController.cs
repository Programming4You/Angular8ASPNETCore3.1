using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Models;
using WebAPI.Repository;


namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize("Bearer")]
    public class EmployeeController : CoreController<Employee, EmployeeRepository>
    {
        private readonly EmployeeRepository _repository;

        public EmployeeController(EmployeeRepository repository) : base(repository)
        {
            _repository = repository;
        }


        // GET: api/Employee
        [HttpGet]
        public async Task<IEnumerable<Employee>> GetEmployees() 
        {
            try
            {
                return await _repository.GetAllEmployees();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //GET: api/Employee/5
        [HttpGet("{id:int}")]
        public async Task<Employee> GetEmployee(int id)
        {
            return await _repository.Get(id);
        }

        // POST: api/Employee
        [HttpPost]
        public async Task PostEmployee(Employee model)
        {
            try
            {
                await _repository.Add(model);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // PUT: api/Employee/5
        [HttpPut("{id:int}")]
        public async Task PutEmployee(int id, Employee employee) 
        {
            try
            {
                await _repository.PutEmployee(id, employee);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // DELETE: api/Employee/5
        [HttpDelete]
        public async Task DeleteEmployee(int id)
        {
            try
            {
                await _repository.DeleteEmployee(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

    }
}