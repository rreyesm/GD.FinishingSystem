using GD.FinishingSystem.WEB.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace GD.FinishingSystem.WEB.Controllers
{
    public class HomeController : Controller
    {
        [Authorize(AuthenticationSchemes = SystemStatics.DefaultScheme)]
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Error()
        {
            return PartialView();
        }
    }
}
