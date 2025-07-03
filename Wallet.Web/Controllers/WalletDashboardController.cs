using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;
using Wallet.Core.Entitites.Models;
using Wallet.Core.Entitites.ViewModels;

namespace Wallet.Web.Controllers
{
    [Authorize]
    public class WalletDashboardController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IDistributedCache _cache;

        public WalletDashboardController(IHttpClientFactory httpClientFactory,
                                         IDistributedCache cache)
        {
            _httpClientFactory = httpClientFactory;
            _cache = cache;
        }


        public async Task<IActionResult> Index()
        {
            var token = await _cache.GetStringAsync("AccessToken");
            if (token == null)
            {
                return BadRequest("Token not found.");
            }

            var client = _httpClientFactory.CreateClient("WalletAPI");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var responseUser = await client.GetAsync("api/User/GetUserInfo");
            if (!responseUser.IsSuccessStatusCode)
            {
                return BadRequest("User not found.");
            }

            var responseTransaction = await client.GetAsync("api/Transaction/GetAllTransactions");
            if (!responseTransaction.IsSuccessStatusCode)
            {
                return BadRequest("Transaction not found.");
            }

            var userString = await responseUser.Content.ReadAsStringAsync();
            var transactionString = await responseTransaction.Content.ReadAsStringAsync();

            var user = JsonSerializer.Deserialize<User>(userString, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            var transactions = JsonSerializer.Deserialize<List<Transaction>>(transactionString, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            WalletDashboardViewModel model = new WalletDashboardViewModel
            {
                User = user,
                Transactions = transactions
            };

            return View(model);
        }
    }
}
