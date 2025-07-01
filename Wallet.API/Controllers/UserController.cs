using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Wallet.Core.Entitites.ViewModels;
using Wallet.DataAccess.Abstract;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Wallet.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly IUserDal _userDal;

        public UserController(IUserDal userDal)
        {
            _userDal = userDal;
        }

        /// <summary>
        ///     Returns currently logged-in user's balance using token information
        /// </summary>
        /// <returns>decimal balance</returns>
        [HttpGet("GetBalance")]
        public IActionResult GetBalance()
        {
            var userId = User.Claims.FirstOrDefault(u => u.Type == ClaimTypes.NameIdentifier).Value;
            var balance = _userDal.GetBalanceByUserId(userId);
            return Ok(balance);
        }

        [HttpPost("AddBalance")]
        public IActionResult AddBalance([FromBody] UpdateBalanceViewModel model)
        {
            var userId = User.Claims.FirstOrDefault(u => u.Type == ClaimTypes.NameIdentifier).Value;
            var newBalance = _userDal.AddBalanceByUserId(userId, model.Balance);
            return Ok(newBalance);
        }
    }
}
