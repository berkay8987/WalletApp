using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StackExchange.Redis;
using System.Security.Claims;
using System.Threading.Tasks;
using Wallet.Business.Abstract;
using Wallet.DataAccess.Abstract;

namespace Wallet.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class TransactionController : Controller
    {
        private readonly ITransactionService _transactionService;

        public TransactionController(ITransactionService transactionService)
        {
            _transactionService = transactionService;
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
        public async Task<IActionResult> GetAllTransactions()
        {
            var userId = User.Claims.FirstOrDefault(u => u.Type == ClaimTypes.NameIdentifier)?.Value;
            var transactions = await _transactionService.GetAllForUserAsync(userId);
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
