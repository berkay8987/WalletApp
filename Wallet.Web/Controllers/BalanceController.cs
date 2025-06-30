using Microsoft.AspNetCore.Mvc;
using Wallet.Core.Entitites.ViewModels;

namespace Wallet.Web.Controllers
{
    public class BalanceController : Controller
    {
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

            return RedirectToAction("Index", "Home");
        }
    }
}
