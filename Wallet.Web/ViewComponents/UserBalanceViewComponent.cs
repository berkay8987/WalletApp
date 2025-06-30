using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Wallet.DataAccess.Context;

namespace Wallet.Web.ViewComponents
{
    public class UserBalanceViewComponent: ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public UserBalanceViewComponent(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var client = _httpClientFactory.CreateClient("WalletAPI");
            var response = await client.GetAsync("api/User/getbalance");

            if (!response.IsSuccessStatusCode) 
            {
                ModelState.AddModelError("", "Failed");
                return View(0m);
            }

            var balanceStr = await response.Content.ReadAsStringAsync();
            decimal balance = decimal.TryParse(balanceStr, out var value) ? value : 0m;

            return View(balance);
        }

    }
}
