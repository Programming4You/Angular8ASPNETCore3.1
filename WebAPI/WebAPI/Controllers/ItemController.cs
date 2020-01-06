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
    public class ItemController : CoreController<Item, ItemRepository>
    {
        private readonly ItemRepository _itemRepository;

        public ItemController(ItemRepository itemRepository) : base(itemRepository)
        {
            _itemRepository = itemRepository;
        }

        // GET: api/Item
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Item>>> GetItem()
        {
            try
            {
                return await _itemRepository.GetItem();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // GET: api/Item/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Item>> GetItem(int id)
        {
            try
            {
                return await _itemRepository.GetItem(id);
            }
            catch (Exception)
            {
                throw;
            }
        }

        // PUT: api/Item/5
        [HttpPut("{id}")]
        public async Task<bool> PutItem(int id, Item item)
        {
            try
            {
                return await _itemRepository.PutItem(id, item);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // POST: api/Item
        [HttpPost]
        public async Task<ActionResult<Item>> PostItem(Item item)
        {
            try
            {
                await _itemRepository.PostItem(item);

                return CreatedAtAction("GetItem", new { id = item.ItemID }, item);
            }
            catch (Exception ex)
            {
                throw ex;
            }
  
        }

        // DELETE: api/Item/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Item>> DeleteItem(int id)
        {
            try
            {
                var item = await _itemRepository.DeleteItem(id);

                if (item == null)
                {
                    return NotFound();
                }

                return item;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool ItemExists(int id)
        {
            return _itemRepository.ItemExists(id);
        }

    }
}