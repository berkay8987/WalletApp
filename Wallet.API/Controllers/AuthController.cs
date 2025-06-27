using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Wallet.Core.Entitites.Models;
using Wallet.Core.Entitites.ViewModels;

namespace Wallet.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : Controller
    {
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;

        public AuthController(SignInManager<User> signInManager, UserManager<User> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginViewModel model)
        {
            var result = await _signInManager.PasswordSignInAsync(model.Username!, model.Password!, model.RememberMe, false);

            if (result.Succeeded)
            {
                return Ok(model);
            }

            return BadRequest(model);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterViewModel model)
        {
            // Check if user exists.
            var userExist = await _userManager.FindByNameAsync(model.Username);
            if (userExist != null)
            {
                return StatusCode(StatusCodes.Status403Forbidden, model);
            }

            User user = new User()
            {
                Firstname = model.Firstname,
                Lastname = model.Lastname,
                UserName = model.Username,
            };

            var result = await _userManager.CreateAsync(user, model.Password);
            
            return result.Succeeded ? Ok(model) : BadRequest(model);
        }

    }
}
