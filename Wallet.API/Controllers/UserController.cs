using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Wallet.DataAccess.Abstract;

namespace Wallet.API.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        IUserDal _userDal;

        public UserController(IUserDal userDal)
        {
            _userDal = userDal;
        }

        [HttpGet("getbalance")]
        public async Task<IActionResult> GetBalance()
        {
            var userId = User.Claims.FirstOrDefault(u => u.Type == ClaimTypes.NameIdentifier).Value;
            var balance = _userDal.GetBalanceByUserId(userId);
            return Ok(balance);
        }
    }
}
