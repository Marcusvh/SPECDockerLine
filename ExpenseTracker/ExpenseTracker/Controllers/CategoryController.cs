using ExpenseTracker.Interfaces;
using ExpenseTracker.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ExpenseTracker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryManager _categoryManager;
        public CategoryController(ICategoryManager categoryManager)
        {
            _categoryManager = categoryManager;
        }
        // GET: api/<CategoryController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Category>>> GetAll()
        {
            return Ok(await _categoryManager.GetAllCategories());
        }

        // GET api/<CategoryController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Category>> Get(Guid id)
        {
            return Ok(await _categoryManager.GetCategoryById(id));
        }

        // POST api/<CategoryController>
        [HttpGet("{name}")]
        public async Task<ActionResult<Category>> GetByName(string name)
        {
            return Ok(await _categoryManager.GetCategoryByName(name));
        }

        // PUT api/<CategoryController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE api/<CategoryController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
