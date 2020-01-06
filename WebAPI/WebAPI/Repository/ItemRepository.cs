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
    public class ItemRepository : CoreRepository<Item, DbSettings>
    {
        private readonly DbSettings _context;

        public ItemRepository(DbSettings context) : base(context)
        {
            _context = context;
        }


        // GET: api/Item
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Item>>> GetItem()
        {
            return await _context.Item.ToListAsync();
        }

        // GET: api/Item/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Item>> GetItem(int id)
        {
            try
            {
                var item = await _context.Item.FindAsync(id);

                return item;
            }
            catch (Exception ex)
            {
                throw ex;
            } 
        }

        // PUT: api/Item/5
        [HttpPut("{id}")]
        public async Task<bool> PutItem(int id, Item item)
        {
            if (id != item.ItemID)
            {
                return false;
            }

            try
            {
                _context.Entry(item).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ItemExists(id))
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

        // POST: api/Item
        [HttpPost]
        public async Task<bool> PostItem(Item item)
        {
            try
            {
                _context.Item.Add(item);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        // DELETE: api/Item/5
        [HttpDelete("{id}")]
        public async Task<Item> DeleteItem(int id)
        {
            try
            {
                var item = await _context.Item.FindAsync(id);
    
                _context.Item.Remove(item);
                await _context.SaveChangesAsync();

                return item;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool ItemExists(int id)
        {
            return _context.Item.Any(e => e.ItemID == id);
        }

    }
}
