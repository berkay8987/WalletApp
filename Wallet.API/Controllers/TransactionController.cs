using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Wallet.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class TransactionController : Controller
    {
        [HttpPost("CreateTransaction")]
        public IActionResult CreateTransaction()
        {
            return BadRequest("Not implemented");
        }

        [HttpGet("GetAllTransactions")]
        public IActionResult GetAllTransactions()
        {
            return BadRequest("Not implemented");
        }

        [HttpPost("UpdateTransactionById")]
        public IActionResult UpdateTransactionById()
        {
            return BadRequest("Not implemented");
        }

        [HttpPost("DeleteTransactionById")]
        public IActionResult DeleteTransactionById()
        {
            return BadRequest("Not implemented");
        }
    }
}
