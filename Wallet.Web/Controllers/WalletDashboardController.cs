using Microsoft.AspNetCore.Mvc;

namespace Wallet.Web.Controllers
{
    public class WalletDashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
