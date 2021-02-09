using GD.FinishingSystem.Bussines;
using GD.FinishingSystem.Entities;
using GD.FinishingSystem.Entities.ViewModels;
using GD.FinishingSystem.WEB.Classes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace GD.FinishingSystem.WEB.Controllers
{

    [Authorize(AuthenticationSchemes = SystemStatics.DefaultScheme)]
    public class RuloController : Controller
    {
        private readonly IWebHostEnvironment webHostEnvironment = null;
        FinishingSystemFactory factory;
        public RuloController(IWebHostEnvironment webHostEnvironment)
        {
            this.webHostEnvironment = webHostEnvironment;
            factory = new FinishingSystemFactory();
        }
        [HttpGet]
        [Authorize(AuthenticationSchemes = SystemStatics.DefaultScheme, Roles = "RuloShow,RuloFull,AdminFull")]
        public async Task<IActionResult> Index()
        {
            VMRuloFilters ruloFilters = new VMRuloFilters();
            ruloFilters.dtBegin = DateTime.Today.AddMonths(-1);
            ruloFilters.dtEnd = DateTime.Today.AddDays(1).AddMilliseconds(-1);

            var result = await factory.Rulos.GetRuloListFromBetweenDate(ruloFilters.dtBegin, ruloFilters.dtEnd);
            await SetViewBagsForDates(ruloFilters);

            return View(result);
        }


        [HttpPost]
        [Authorize(AuthenticationSchemes = SystemStatics.DefaultScheme, Roles = "RuloShow,RuloFull,AdminFull")]
        public async Task<IActionResult> Index(VMRuloFilters ruloFilters)
        {

            ruloFilters.dtEnd = ruloFilters.dtEnd.AddDays(1).AddMilliseconds(-1);
            var result = await factory.Rulos.GetRuloListFromFilters(ruloFilters);
            await SetViewBagsForDates(ruloFilters);

            return View(result);
        }

        private async Task SetViewBagsForDates(VMRuloFilters ruloFilters)
        {
            ViewBag.dtBegin = ruloFilters.dtBegin.ToString("yyyy-MM-dd");
            ViewBag.dtEnd = ruloFilters.dtEnd.ToString("yyyy-MM-dd");

            ViewBag.TestResultList = new List<SelectListItem>() {
                new SelectListItem("Select", "0"),
                new SelectListItem("Prueba", "1")
            };

            ViewBag.numLote = ruloFilters.numLote;
            ViewBag.numBeam = ruloFilters.numBeam;
            ViewBag.numLoom = ruloFilters.numLoom;
            ViewBag.numPiece = ruloFilters.numPiece;
            ViewBag.txtStyle = ruloFilters.txtStyle;
            ViewBag.numTestRsult = ruloFilters.numTestResult;


            //Style list
            var styleList = await factory.Rulos.GetRuloStyleList();
            ViewBag.StyleList = styleList;

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
                return RedirectToAction("Error", "Home");
            return View("CreateOrUpdate", existRulo);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Save(Rulo rulo)
        {
            ViewBag.Error = true;
            if (rulo.RuloID == 0)
            {
                if (!(User.IsInRole("RuloAdd") || User.IsInRole("AdminFull") || User.IsInRole("RuloFull")))
                    return Unauthorized();


                await factory.Rulos.Add(rulo, int.Parse(User.Identity.Name));

            }
            else
            {
                if (!(User.IsInRole("RuloUp") || User.IsInRole("AdminFull") || User.IsInRole("RuloFull")))
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

        [HttpGet]
        [Authorize(AuthenticationSchemes = SystemStatics.DefaultScheme, Roles = "RuloShow, AdminFull, RuloFull")]
        public async Task<IActionResult> Details(int ruloId)
        {
            var foundRulo = await factory.Rulos.GetRuloFromRuloID(ruloId);
            if (foundRulo == null)
            {
                return RedirectToAction("Error", "Home");
            }

            return View(foundRulo);
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = SystemStatics.DefaultScheme, Roles = "RuloDel, AdminFull, RuloFull")]
        public async Task<IActionResult> Delete(int ruloId)
        {
            var rulo = await factory.Rulos.GetRuloFromRuloID(ruloId);
            if (rulo == null)
            {
                return RedirectToAction("Error", "Home");
            }

            return View(rulo);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int ruloId)
        {
            if (!(User.IsInRole("RuloDel") || User.IsInRole("AdminFull") || User.IsInRole("RuloFull")))
                return Unauthorized();

            var rulo = await factory.Rulos.GetRuloFromRuloID(ruloId);

            await factory.Rulos.Delete(rulo, int.Parse(User.Identity.Name));

            return Redirect("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Print(int ruloId)
        {
            if (!User.IsInRole("Rulo", AuthType.Update)) return Unauthorized();

            var rulo = await factory.Rulos.GetRuloFromRuloID(ruloId);

            string zpl = "";
            await RawPrinterHelper.PrintToZPLByIP("192.168.7.200", zpl);

            return Redirect("Index");
        }

        [HttpPost]
        public async Task<IActionResult> TestResultFinish(int RuloId, string description)
        {
            if (!User.IsInRole("Rulo", AuthType.Update)) return Unauthorized();

            var foundRulo = await factory.Rulos.GetRuloFromRuloID(RuloId);
            if (foundRulo == null) return NotFound();

            //Update test result
            TestResult testResult = new TestResult();
            testResult.Details = description;
            testResult.CanContinue = true;

            await factory.TestResults.Add(testResult, int.Parse(User.Identity.Name));

            var intUser = int.Parse(User.Identity.Name);
            await factory.Rulos.SetTestResult(RuloId, testResult.TestResultID, intUser, intUser);

            return Ok();
        }

    }
}
