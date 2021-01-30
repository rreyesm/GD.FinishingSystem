using GD.FinishingSystem.Bussines;
using GD.FinishingSystem.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GD.FinishingSystem.WEB.Controllers
{

    [Authorize(AuthenticationSchemes = SystemStatics.DefaultScheme)]
    public class RuloController : Controller
    {
        FinishingSystemFactory factory;
        public RuloController()
        {
            factory = new FinishingSystemFactory();
        }
        [HttpGet]
        [Authorize(AuthenticationSchemes = SystemStatics.DefaultScheme, Roles = "RuloShow,RuloFull,AdminFull")]
        public async Task<IActionResult> Index()
        {
            DateTime dtBegin = DateTime.Today.AddMonths(-1);
            DateTime dtEnd = DateTime.Today.AddDays(1).AddMilliseconds(-1);


            var result = await factory.Rulos.GetRuloListFromBetweenDate(dtBegin, dtEnd);
            SetViewBagsForDates(dtBegin, dtEnd);



            return View(result);
        }


        [HttpPost]
        [Authorize(AuthenticationSchemes = SystemStatics.DefaultScheme, Roles = "RuloShow,RuloFull,AdminFull")]
        public async Task<IActionResult> Index(DateTime dtBegin, DateTime dtEnd)
        {
            dtEnd = dtEnd.AddDays(1).AddMilliseconds(-1);
            var result = await factory.Rulos.GetRuloListFromBetweenDate(dtBegin, dtEnd);
            SetViewBagsForDates(dtBegin, dtEnd);
            return View(result);
        }

        private void SetViewBagsForDates(DateTime dtBegin, DateTime dtEnd)
        {
            ViewBag.dtBegin = dtBegin.ToString("yyyy-MM-dd");
            ViewBag.dtEnd = dtEnd.ToString("yyyy-MM-dd");
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = SystemStatics.DefaultScheme, Roles = "RuloAdd,RuloFull,AdminFull")]
        public IActionResult Create()
        {
            ViewBag.Error = false;
            ViewBag.ErrorMessage = "";
            Rulo newRulo = new Rulo();
            return View("CreateOrUpdate", newRulo);
        }


        [HttpGet]
        [Authorize(AuthenticationSchemes = SystemStatics.DefaultScheme, Roles = "RuloUp,RuloFull,AdminFull")]
        public async Task<IActionResult> Edit(int RuloID)
        {
            ViewBag.Error = false;
            ViewBag.ErrorMessage = "";
            Rulo existRulo = await factory.Rulos.GetRuloFromRuloID(RuloID);
            if (existRulo == null)
                return NotFound();
            return View("CreateOrUpdate", existRulo);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Save(Rulo rulo)
        {
            ViewBag.Error = true;
            if (rulo.RuloID == 0)
            {
                if (!(User.IsInRole("RuloAdd") || User.IsInRole("AdminFull") || User.IsInRole("RuloAll")))
                    return Unauthorized();


                await factory.Rulos.Add(rulo, int.Parse(User.Identity.Name));

            }
            else
            {
                if (!(User.IsInRole("RuloUp") || User.IsInRole("AdminFull") || User.IsInRole("RuloAll")))
                    return Unauthorized();

                var foundRulo = await factory.Rulos.GetRuloFromRuloID(rulo.RuloID);

                foundRulo.Style = rulo.Style;
                foundRulo.StyleName = rulo.StyleName;
                foundRulo.Width = rulo.Width;
                foundRulo.EntranceLength = rulo.EntranceLength;
                foundRulo.ExitLength = rulo.ExitLength;


                await factory.Rulos.Update(foundRulo, int.Parse(User.Identity.Name));

            }
            return Redirect("Index");
        }

    }
}
