using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Interfaces;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public abstract class CoreController<TEntity, TRepository> : Controller
        where TEntity : class, IEntity
        where TRepository : IRepository<TEntity> 
    {

        private readonly TRepository _repository;

        public CoreController(TRepository repository)
        {
            _repository = repository;
        }

        // GET: api/[controller]
        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<TEntity>>> GetAll()
        //{
        //    return await _repository.GetAll();
        //}

        //GET: api/[controller]/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TEntity>> Get(int id)
        {
            var employee = await _repository.Get(id);
            if (employee == null)
            {
                return NotFound();
            }
            return employee;
        }

        // PUT: api/[controller]/5
        [HttpPut("{employee}")]
        public async Task<IActionResult> Put(TEntity employee)
        {
            await _repository.Update(employee);
            return NoContent();
        }

        // POST: api/[controller]
        [HttpPost("{employee}")]
        public async Task<ActionResult<TEntity>> Post(TEntity employee)
        {
            await _repository.Add(employee);
            return employee;
        }

        // DELETE: api/[controller]/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<TEntity>> Delete(int id)
        {
            var employee = await _repository.Delete(id);
            if (employee == null)
            {
                return NotFound();
            }
            return employee;
        }

    }
}