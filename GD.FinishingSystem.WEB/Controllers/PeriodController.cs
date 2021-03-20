using GD.FinishingSystem.Bussines;
using GD.FinishingSystem.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GD.FinishingSystem.WEB.Controllers
{
    [Authorize(AuthenticationSchemes = SystemStatics.DefaultScheme)]
    public class PeriodController : Controller
    {
        FinishingSystemFactory factory;
        public PeriodController()
        {
            factory = new FinishingSystemFactory();
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = SystemStatics.DefaultScheme, Roles = "PeriodShow, PeriodFull, AdminFull")]
        public async Task<ActionResult> Index()
        {
            var result = await factory.Periods.GetPeriodList();

            return View(result);
        }

        [HttpPost, Authorize(AuthenticationSchemes = SystemStatics.DefaultScheme, Roles = "PeriodAdd,PeriodFull,AdminFull")]
        public async Task<IActionResult> CreatePeriod(string style)
        {
            var currentPeriod = await factory.Periods.GetCurrentPeriod();

            //Validate current period and update
            if (currentPeriod != null)
            {
                currentPeriod.FinishDate = DateTime.Now;
                currentPeriod.ValidityLastPeriod = currentPeriod.FinishDate.Value.Subtract(currentPeriod.StartDate);

                await factory.Periods.Update(currentPeriod, int.Parse(User.Identity.Name));
            }

            Period newPeriod = new Period();
            newPeriod.StartDate = DateTime.Now;
            newPeriod.Style = style;

            await factory.Periods.Add(newPeriod, int.Parse(User.Identity.Name));

            return Ok();
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = SystemStatics.DefaultScheme, Roles = "PeriodAdd,PeriodFull,AdminFull")]
        public async Task<ActionResult> Create(int periodId)
        {
            ViewBag.Error = false;
            ViewBag.ErrorMessage = "";
            if (periodId <= 0) NotFound();

            var rulo = await factory.Rulos.GetRuloFromRuloID(periodId);
            if (rulo == null) return NotFound();

            Period newPeriod = new Period();

            return View("CreateOrUpdate", newPeriod);
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = SystemStatics.DefaultScheme, Roles = "PeriodUp,PeriodFull,AdminFull")]
        public async Task<ActionResult> Edit(int periodId)
        {
            Period editPeriod = await factory.Periods.GetPeriodFromPeriodID(periodId);

            return View("CreateOrUpdate", editPeriod);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Save(Period period)
        {
            ViewBag.Error = true;
            string errorMessage = string.Empty;
            if (!ModelState.IsValid)
            {
                foreach (var key in ModelState.Keys)
                {
                    var modelStateVal = ModelState[key];
                    foreach (var error in modelStateVal.Errors)
                    {
                        errorMessage += error.ErrorMessage + "<br>";
                    }
                }

                ViewBag.ErrorMessage = errorMessage;

                return View("CreateOrUpdate");
            }

            if (period.PeriodID == 0)
            {
                if (!User.IsInRole("Period", AuthType.Add)) return Unauthorized();

                await factory.Periods.Add(period, int.Parse(User.Identity.Name));
            }

            return RedirectToAction(nameof(Index), new { period.PeriodID });
        }

        // GET: PeriodController/Details/5
        public ActionResult Details(int periodId)
        {
            return View();
        }





        // POST: PeriodController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: PeriodController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: PeriodController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
