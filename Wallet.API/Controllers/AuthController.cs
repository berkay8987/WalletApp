using Azure.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Wallet.Core.Entitites.Models;
using Wallet.Core.Entitites.ViewModels;
using Wallet.API.Tools;

namespace Wallet.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : Controller
    {
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _configuration;

        public AuthController(SignInManager<User> signInManager, UserManager<User> userManager, IConfiguration configuration)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _configuration = configuration;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginViewModel model)
        {
            var user = await _userManager.FindByNameAsync(model.Username);
            if (user == null)
            {
                return Unauthorized("User not found");
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);
            if (!result.Succeeded)
            {
                return Unauthorized("Password wrong");
            }

            JwtTokenTool tokenGen = new JwtTokenTool();
            var jwtToken = GenerateJwtToken(user);

            return Ok(jwtToken);
        }

        [AllowAnonymous]
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

        [Authorize]
        [HttpPost("test")]
        public IActionResult Test()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Unauthorized("User not authenticated.");
            }

            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            
            if (userIdClaim == null)
            {
                return BadRequest("UserId claim not found.");
            }

            return Ok(userIdClaim.Value);
        }
    }
}
