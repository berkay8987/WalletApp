﻿using Microsoft.AspNetCore.Mvc;

namespace Wallet.Web.Controllers
{
    public class ProfileController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
