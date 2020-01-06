using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Data;
using WebAPI.Models;

namespace WebAPI.Repository
{
    public class CustomerRepository : CoreRepository<Customer, DbSettings>
    {
        private readonly DbSettings _context;

        public CustomerRepository(DbSettings context) : base(context)
        {
            _context = context;
        }

        public bool CustomerExists(int id)
        {
            return _context.Customer.Any(e => e.CustomerID == id);
        }

        public async Task<Customer> DeleteCustomer(int id)
        {
            try
            {
                var customer = await _context.Customer.FindAsync(id);

                _context.Customer.Remove(customer);
                await _context.SaveChangesAsync();

                return customer;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<Customer>> GetAllCustomers()
        {
            try
            {
                return await _context.Customer.ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Customer> GetCustomer(int id)
        {
            try
            {
                var customer = await _context.Customer.FindAsync(id);

                return customer;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> PostCustomer(Customer customer)
        {
            try
            {
                await _context.Customer.AddAsync(customer);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> PutCustomer(int id, Customer customer)
        {
            if (id != customer.CustomerID)
            {
                return false;
            }

            try
            {
                _context.Entry(customer).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerExists(id))
                {
                    return false;
                }
                else
                {
                    throw;
                }
            }

            return true;
        }
    }
}
