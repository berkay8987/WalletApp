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
using Microsoft.Extensions.Caching.Distributed;

namespace Wallet.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : Controller
    {
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _configuration;
        private readonly IDistributedCache _cache;

        public AuthController(SignInManager<User> signInManager, 
                              UserManager<User> userManager, 
                              IConfiguration configuration,
                              IDistributedCache cache)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _configuration = configuration;
            _cache = cache;
        }


        /// <summary>
        ///     Logs in a user with username and password.
        /// </summary>
        /// <param name="model">User model</param>
        /// <returns>Returns an access token</returns>
        [AllowAnonymous]
        [HttpPost("Login")]
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

            var jwtToken = JwtTokenGenerator.GenerateJwtToken(user);

            var cacheOptions = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(1)
            };

            await _cache.SetStringAsync("AccessToken", jwtToken, cacheOptions);

            return Ok(jwtToken);
        }

        /// <summary>
        ///     Registers a user to identity user db.
        /// </summary>
        /// <param name="model">User to register</param>
        /// <returns>Http.Success or Http.BadRequest</returns>
        [AllowAnonymous]
        [HttpPost("Register")]
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

        /// <summary>
        ///     This is an test API for authorization
        /// </summary>
        /// <returns>Returns the authorized user's UserId</returns>
        [Authorize]
        [HttpPost("GetUserId")]
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
