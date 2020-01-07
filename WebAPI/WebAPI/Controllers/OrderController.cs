using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Models;
using WebAPI.Repository;
using WebAPI.ViewModel;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize("Bearer")]
    public class OrderController : CoreController<Order, OrderRepository>
    {
        private readonly OrderRepository _orderRepository;

        public OrderController(OrderRepository orderRepository) : base(orderRepository)
        {
            _orderRepository = orderRepository;
        }

        // GET: api/Order
        [HttpGet]
        public async Task<IEnumerable<OrderViewModel>> GetOrder()
        {
            try
            {
                return await _orderRepository.GetOrder();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // GET: api/Order/5
        [HttpGet("{id:long}")]
        public async Task<ActionResult<OrderViewModel>> GetOrder(long id)
        {
            try
            {
                return await _orderRepository.GetOrder(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // PUT: api/Order/5
        [HttpPut]
        public async Task<IActionResult> PutOrder(Order order)
        {
            try
            {
                await _orderRepository.PutOrder(order);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return NoContent();
        }

        // POST: api/Order
        [HttpPost]
        public async Task PostOrder(Order order)
        {
            try
            {
                await _orderRepository.PostOrder(order);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // DELETE: api/Order/5
        [HttpDelete("{id:long}")]
        public async Task DeleteOrder(long id)
        {
            try
            {
                await _orderRepository.DeleteOrder(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private bool OrderExists(double id)
        {
            return _orderRepository.OrderExists(id);
        }

    }
}