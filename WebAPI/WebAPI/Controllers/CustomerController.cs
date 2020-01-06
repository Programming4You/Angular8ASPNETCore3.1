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
    public class CustomerController : CoreController<Customer, CustomerRepository>
    {
        private readonly CustomerRepository _customerRepository;

        public CustomerController(CustomerRepository customerRepository) : base(customerRepository)
        {
            _customerRepository = customerRepository;
        }

        // GET: api/Customer
        [HttpGet]
        public async Task<IEnumerable<Customer>> GetCustomer()
        {
            try
            {
                return await _customerRepository.GetAllCustomers();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // GET: api/Customer/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Customer>> GetCustomer(int id)
        {
            try
            {
                return await _customerRepository.GetCustomer(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // PUT: api/Customer/5
        [HttpPut("{id}")]
        public async Task<bool> PutCustomer(int id, Customer customer)
        {
            try
            {
                return await _customerRepository.PutCustomer(id, customer);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // POST: api/Customer
        [HttpPost]
        public async Task<ActionResult<Customer>> PostCustomer(Customer customer)
        {
            try
            {
                await _customerRepository.PostCustomer(customer);

                return CreatedAtAction("GetCustomer", new { id = customer.CustomerID }, customer);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // DELETE: api/Customer/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Customer>> DeleteCustomer(int id)
        {
            try
            {
                var customer = await _customerRepository.DeleteCustomer(id);

                if (customer == null)
                {
                    return NotFound();
                }

                return customer;
            }
            catch (Exception ex)
            {
                return BadRequest(new { ex.Message });
            }
        }

        public bool CustomerExists(int id)
        {
            return _customerRepository.CustomerExists(id);
        }
    }
}