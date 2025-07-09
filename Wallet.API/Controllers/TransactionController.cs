using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StackExchange.Redis;
using System.Security.Claims;
using Wallet.DataAccess.Abstract;

namespace Wallet.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class TransactionController : Controller
    {
        private readonly ITransactionDal _transactionDal;

        public TransactionController(ITransactionDal transactionDal)
        {
            _transactionDal = transactionDal;
        }

        [HttpPost("CreateTransaction")]
        public IActionResult CreateTransaction()
        {
            return BadRequest("Not implemented");
        }

        /// <summary>
        ///     Returns all transactions associated with the logged-in user.
        /// </summary>
        /// <returns>List<Transaction></returns>
        [HttpGet("GetAllTransactions")]
        public IActionResult GetAllTransactions()
        {
            var userId = User.Claims.FirstOrDefault(u => u.Type == ClaimTypes.NameIdentifier)?.Value;
            var transactions = _transactionDal.GetAllTransactions(userId);
            return Ok(transactions);
        }

        [HttpPut("UpdateTransactionById")]
        public IActionResult UpdateTransactionById()
        {
            return BadRequest("Not implemented");
        }

        [HttpDelete("DeleteTransactionById")]
        public IActionResult DeleteTransactionById()
        {
            return BadRequest("Not implemented");
        }
    }
}
