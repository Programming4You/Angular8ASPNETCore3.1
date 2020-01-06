using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Data;
using WebAPI.Models;
using WebAPI.ViewModel;


namespace WebAPI.Repository
{
    public class OrderRepository : CoreRepository<Order, DbSettings>
    {
        private readonly DbSettings _context;

        public OrderRepository(DbSettings context) : base(context)
        {
            _context = context;
        }


        // GET: api/Order
        [HttpGet]
        public async Task<IEnumerable<OrderViewModel>> GetOrder()
        {
            var result = await (from o in _context.Order
                                join c in _context.Customer on o.CustomerID equals c.CustomerID
                                select new OrderViewModel
                                {
                                    OrderID = o.OrderID,
                                    OrderNo = o.OrderNo,
                                    CustomerName = c.Name,
                                    PMethod = o.PMethod,
                                    GTotal = o.GTotal
                                }).ToListAsync();

            return result;
        }

        // GET: api/Order/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderViewModel>> GetOrder(long id)
        {
            var order = await (from o in _context.Order
                               where o.OrderID == id
                               select new Order
                               {
                                   OrderID = o.OrderID,
                                   OrderNo = o.OrderNo,
                                   CustomerID = o.CustomerID,
                                   PMethod = o.PMethod,
                                   GTotal = o.GTotal,
                                   DeletedOrderItemIDs = ""
                               }).FirstOrDefaultAsync();

            var orderDetails = await (from oi in _context.OrderItem
                                      join i in _context.Item on oi.ItemID equals i.ItemID
                                      where oi.OrderID == id
                                      select new OrderItemViewModel
                                      {
                                          OrderID = oi.OrderID,
                                          OrderItemID = oi.OrderItemID,
                                          ItemID = oi.ItemID,
                                          ItemName = i.Name,
                                          Price = i.Price,
                                          Quantity = oi.Quantity,
                                          Total = oi.Quantity * i.Price
                                      }).ToListAsync();

            OrderViewModel model = new OrderViewModel();
            model.Order = order;
            model.OrderDetails = orderDetails;

            return model;
        }

        // PUT: api/Order/5
        [HttpPut("{id}")]
        public async Task<bool> PutOrder(Order order)
        {
            try
            {
                _context.Entry(order).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
               throw ex;
            }

            return true;
        }

        // POST: api/Order
        [HttpPost]
        public async Task<bool> PostOrder(Order order)
        {
            try
            {
                //Order table
                if (order.OrderID == null)
                {
                    Order ord = new Order();
                    ord.OrderNo = order.OrderNo;
                    ord.CustomerID = order.CustomerID;
                    ord.PMethod = order.PMethod;
                    ord.GTotal = order.GTotal;

                    _context.Order.Add(ord);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    _context.Entry(order).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                }

                //OrderItems table
                var maxOrderID = _context.Order.Max(x => x.OrderID);

                var maxOrderItemID = _context.OrderItem.Max(x => x.OrderItemID);
                var orderItemID = maxOrderItemID == null ? 1 : maxOrderItemID + 1;

                foreach (var item in order.OrderItems) 
                {
                    if (item.OrderItemID == null)
                    {
                        item.OrderID = maxOrderID;
                        _context.OrderItem.Add(item);
                        await _context.SaveChangesAsync();
                    }
                    else
                    {
                        _context.Entry(item).State = EntityState.Modified;
                        await _context.SaveChangesAsync();
                    }
                }

                //delete OrderItems
                foreach (var id in order.DeletedOrderItemIDs.Split(',').Where(x => x != ""))
                {
                    OrderItem oi = await _context.OrderItem.FindAsync(Convert.ToInt64(id));
                    _context.OrderItem.Remove(oi);
                    await _context.SaveChangesAsync();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return true;
        }

        // DELETE: api/Order/5
        [HttpDelete("{id:long}")]
        public async Task<ActionResult<Order>> DeleteOrder(long id)
        {
            var order = await _context.Order.Include(y => y.OrderItems).
                        FirstOrDefaultAsync(x => x.OrderID == id);

            foreach (var item in order.OrderItems.ToList())
            {
                _context.OrderItem.Remove(item);
            }

            _context.Order.Remove(order);
            await _context.SaveChangesAsync();

            return order;
        }


        public bool OrderExists(double id)
        {
            return _context.Order.Any(e => e.OrderID == id);
        }

    }
}
