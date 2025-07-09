using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Wallet.Business.Abstract;
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
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        ///     Returns currently logged-in user's balance using token information
        /// </summary>
        /// <returns>decimal balance</returns>
        [HttpGet("GetBalance")]
        public async Task<IActionResult> GetBalance()
        {
            var userId = User.Claims.FirstOrDefault(u => u.Type == ClaimTypes.NameIdentifier)?.Value;
            var balance = await _userService.GetBalanceAsync(userId);
            return Ok(balance);
        }

        /// <summary>
        ///     Increases the balance by the value and returns the new balance of the currently logged-in user
        /// </summary>
        /// <param name="value">Amount to increase the balance</param>
        /// <returns></returns>
        [HttpPost("AddBalance")]
        public async Task<IActionResult> AddBalance([FromBody] decimal value)
        {
            var userId = User.Claims.FirstOrDefault(u => u.Type == ClaimTypes.NameIdentifier)?.Value;
            var newBalance = await _userService.AddBalanceAsync(userId, value);
            return Ok(newBalance);
        }


        /// <summary>
        ///     Deacreses the balance by the value and returns the new balance of the currently logged-in user
        /// </summary>
        /// <param name="value">Amount to decrease the balance</param>
        /// <returns></returns>
        [HttpPost("RemoveBalance")]
        public async Task<IActionResult> RemoveBalance([FromBody] decimal value)
        {
            var userId = User.Claims.FirstOrDefault(u => u.Type == ClaimTypes.NameIdentifier).Value;
            var newBalance = await _userService.RemoveBalanceAsync(userId, value);
            return Ok(newBalance);
        }

        /// <summary>
        ///     Returns the last time that the balance of the currently logged-in user changed.
        /// </summary>
        /// <returns>DateTime LastUpdated</returns>
        [HttpGet("GetLastUpdated")]
        public async Task<IActionResult> GetLastUpdated()
        {
            var userId = User.Claims.FirstOrDefault(u => u.Type == ClaimTypes.NameIdentifier)?.Value;
            var lastUpdated = await _userService.GetLastUpdatedAsync(userId);
            return Ok(lastUpdated);
        }

        /// <summary>
        ///     Return the user associated with the user id.
        /// </summary>
        /// <returns>User user</returns>
        [HttpGet("GetUserInfo")]
        public async Task<IActionResult> GetUserInfo()
        {
            var userId = User.Claims.FirstOrDefault(u => u.Type == ClaimTypes.NameIdentifier)?.Value;
            var user = await _userService.GetUserAsync(userId);
            return Ok(user);
        }
    }
}
