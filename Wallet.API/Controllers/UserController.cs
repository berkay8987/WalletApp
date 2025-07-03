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

        /// <summary>
        ///     Increases the balance by the value and returns the new balance of the currently logged-in user
        /// </summary>
        /// <param name="value">Amount to increase the balance</param>
        /// <returns></returns>
        [HttpPost("AddBalance")]
        public IActionResult AddBalance([FromBody] decimal value)
        {
            var userId = User.Claims.FirstOrDefault(u => u.Type == ClaimTypes.NameIdentifier).Value;
            var newBalance = _userDal.AddBalanceByUserId(userId, value);
            return Ok(newBalance);
        }


        /// <summary>
        ///     Deacreses the balance by the value and returns the new balance of the currently logged-in user
        /// </summary>
        /// <param name="value">Amount to decrease the balance</param>
        /// <returns></returns>
        [HttpPost("RemoveBalance")]
        public IActionResult RemoveBalance([FromBody] decimal value)
        {
            var userId = User.Claims.FirstOrDefault(u => u.Type == ClaimTypes.NameIdentifier).Value;
            var newBalance = _userDal.RemoveBalanceByUserId(userId, value);
            return Ok(newBalance);
        }

        /// <summary>
        ///     Returns the last time that the balance of the currently logged-in user changed.
        /// </summary>
        /// <returns>DateTime LastUpdated</returns>
        [HttpGet("GetLastUpdated")]
        public IActionResult GetLastUpdated()
        {
            var userId = User.Claims.FirstOrDefault(u => u.Type == ClaimTypes.NameIdentifier).Value;
            var lastUpdated = _userDal.GetLastUpdatedByUserId(userId);
            return Ok(lastUpdated);
        }

        /// <summary>
        ///     Return the user associated with the user id.
        /// </summary>
        /// <returns>User user</returns>
        [HttpGet("GetUserInfo")]
        public IActionResult GetUserInfo()
        {
            var userId = User.Claims.FirstOrDefault(u => u.Type == ClaimTypes.NameIdentifier).Value;
            var user = _userDal.GetUserbyUserId(userId);
            return Ok(user);
        }
    }
}
