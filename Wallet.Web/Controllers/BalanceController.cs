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

        [HttpPost("AddBalance")]
        public async Task<IActionResult> AddBalance(UpdateBalanceViewModel UpdateBalance) 
        { 
            if (!ModelState.IsValid)
            {
                return BadRequest("Model not valid.");
            }

            var token = await _cache.GetStringAsync("AccessToken");
            if (token == null)
            {
                ModelState.AddModelError("", "Failed to update balance.");
                return View(UpdateBalance);
            }

            var client = _httpClientFactory.CreateClient("WalletAPI");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await client.PostAsJsonAsync("api/User/AddBalance", UpdateBalance.Balance);

            if (!response.IsSuccessStatusCode)
            {
                ModelState.AddModelError("", "Failed to update balance.");
                return View(UpdateBalance);
            }

            return RedirectToAction("Index", "WalletDashboard");
        }

        [HttpPost("SubtractBalance")]
        public async Task<IActionResult> SubtractBalance(UpdateBalanceViewModel UpdateBalance)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Model not valid.");
            }

            var token = await _cache.GetStringAsync("AccessToken");
            if (token == null)
            {
                ModelState.AddModelError("", "Failed to update balance.");
                return View(UpdateBalance);
            }

            var client = _httpClientFactory.CreateClient("WalletAPI");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await client.PostAsJsonAsync("api/User/RemoveBalance", UpdateBalance.Balance);

            if (!response.IsSuccessStatusCode)
            {
                ModelState.AddModelError("", "Failed to update balance.");
                return View(UpdateBalance);
            }

            return RedirectToAction("Index", "WalletDashboard");
        }
    }
}
