using ExpenseTracker.Interfaces;
using ExpenseTracker.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ExpenseTracker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionManager _transactionManager;

        public TransactionController(ITransactionManager transactionManager)
        {
            _transactionManager = transactionManager;
        }
        // GET: api/<TransactionController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserTransaction>>> GetAll()
        {
            return Ok(await _transactionManager.GetAllTransactions());
        }

        // GET api/<TransactionController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserTransaction>> Get(Guid id)
        {
            return Ok(await _transactionManager.GetTransactionById(id));
        }

        [HttpGet("user/{userId}")]
        public async Task<ActionResult<IEnumerable<UserTransaction>>> GetByUserId(Guid userId)
        {
            return Ok(await _transactionManager.GetTransactionsByUserId(userId));
        }
        [HttpGet("categoryId/{categoryId}")]
        public async Task<ActionResult<IEnumerable<UserTransaction>>> GetByCategoryId(Guid categoryId)
        {
            return Ok(await _transactionManager.GetTransactionsByCategoryId(categoryId));
        }
        [HttpGet("categoryName/{name}")]
        public async Task<ActionResult<IEnumerable<UserTransaction>>> GetByCategoryName(string name)
        {
            return Ok(await _transactionManager.GetTransactionsByName(name));
        }

        // POST api/<TransactionController>
        //[HttpPost]
        //public void Post([FromBody] string value)
        //{
        //}

        //// PUT api/<TransactionController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE api/<TransactionController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
