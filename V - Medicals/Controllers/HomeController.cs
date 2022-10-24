﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace V___Medicals.Controllers
{
    public class HomeController : Controller
    {
        [Authorize()]
        public IActionResult Index()
        {
            return View();
        }
    }
}
