using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using System.Security.Claims;
using Wallet.DataAccess.Context;
using System.Net.Http.Headers;

namespace Wallet.Web.ViewComponents
{
    public class UserBalanceViewComponent: ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IDistributedCache _cache;

        public UserBalanceViewComponent(IHttpClientFactory httpClientFactory,
                                        IDistributedCache cache)
        {
            _httpClientFactory = httpClientFactory;
            _cache = cache;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var token = await _cache.GetStringAsync("AccessToken");

            if (string.IsNullOrEmpty(token))
            {
                return View(-1m);
            }

            var client = _httpClientFactory.CreateClient("WalletAPI");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await client.GetAsync("api/User/GetBalance");

            if (response.IsSuccessStatusCode) 
            {
                var balanceStr = await response.Content.ReadAsStringAsync();
                decimal balance = decimal.TryParse(balanceStr, out var value) ? value : 0m;
                return View(balance);
            }

            ModelState.AddModelError("", "Failed");
            return View(-1m);
        }
    }
}
