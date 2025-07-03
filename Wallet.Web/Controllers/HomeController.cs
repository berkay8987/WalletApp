using System.Diagnostics;
using System.Text.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Wallet.Core.Entitites.Models;
using Wallet.Web.Models;

namespace Wallet.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHttpClientFactory _httpClientFactory;

        public HomeController(ILogger<HomeController> logger, 
                              IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient("WalletAPI");
            var response = await client.GetAsync("api/Product/GetAllProducts");
            if (response.IsSuccessStatusCode)
            {
                var productsString = await response.Content.ReadAsStringAsync();

                var products = JsonSerializer.Deserialize<List<Product>>(productsString, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                return View(products);
            }
            return View();
        }

        [Authorize]
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
