using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Wallet.Core.Entitites.ViewModels;
using System.Net.Http.Headers;

namespace Wallet.Web.Controllers
{
    [Authorize]
    public class BalanceController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IDistributedCache _cache;

        public BalanceController(IHttpClientFactory httpClientFactory,
                                 IDistributedCache cache)
        {
            _httpClientFactory = httpClientFactory;
            _cache = cache;
        }

        public IActionResult Balance()
        {
            return View();
        }

        [HttpPost("UpdateBalance")]
        public async Task<IActionResult> UpdateBalance(UpdateBalanceViewModel model) 
        { 
            if (!ModelState.IsValid)
            {
                return BadRequest("Model not valid.");
            }

            var token = await _cache.GetStringAsync("AccessToken");
            if (token == null)
            {
                ModelState.AddModelError("", "Failed to update balance.");
                return View(model);
            }

            var client = _httpClientFactory.CreateClient("WalletAPI");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await client.PostAsJsonAsync("api/User/AddBalance", model.Balance);

            if (!response.IsSuccessStatusCode)
            {
                ModelState.AddModelError("", "Failed to update balance.");
                return View(model);
            }

            return RedirectToAction("Index", "Home");
        }
    }
}
