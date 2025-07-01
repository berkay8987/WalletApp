using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Wallet.Web.Controllers
{
    [Authorize]
    public class WalletDashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
