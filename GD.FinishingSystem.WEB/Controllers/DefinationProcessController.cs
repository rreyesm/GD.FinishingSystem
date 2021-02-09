using GD.FinishingSystem.Bussines;
using GD.FinishingSystem.Entities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace GD.FinishingSystem.WEB.Controllers
{
    [Authorize(AuthenticationSchemes = SystemStatics.DefaultScheme)]
    public class DefinationProcessController : Controller
    {
        FinishingSystemFactory factory;
        public DefinationProcessController()
        {
            factory = new FinishingSystemFactory();
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = SystemStatics.DefaultScheme, Roles = "DefinationProcessShow, DefinationProcessFull, AdminFull")]
        public async Task<IActionResult> Index()
        {
            DateTime dtBegin = DateTime.Today.AddMonths(-1);
            DateTime dtEnd = DateTime.Today.AddDays(1).AddMilliseconds(-1);

            var result = await factory.DefinationProcesses.GetDefinationProcessListFromBetweenDate(dtBegin, dtEnd);

            SetViewBagsForDates(dtBegin, dtEnd);
            return View(result);
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = SystemStatics.DefaultScheme, Roles = "DefinationProcessShow, DefinationProcessFull,AdminFull")]
        public async Task<IActionResult> Index(DateTime dtBegin, DateTime dtEnd)
        {
            dtEnd = dtEnd.AddDays(1).AddMilliseconds(-1);
            var result = await factory.DefinationProcesses.GetDefinationProcessListFromBetweenDate(dtBegin, dtEnd);
            SetViewBagsForDates(dtBegin, dtEnd);
            return View(result);
        }

        private void SetViewBagsForDates(DateTime dtBegin, DateTime dtEnd)
        {
            ViewBag.dtBegin = dtBegin.ToString("yyyy-MM-dd");
            ViewBag.dtEnd = dtEnd.ToString("yyyy-MM-dd");
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = SystemStatics.DefaultScheme, Roles = "DefinationProcessAdd, DefinationProcessFull, AdminFull")]
        public IActionResult Create()
        {
            ViewBag.Error = false;
            ViewBag.ErrorMessage = "";
            DefinationProcess newDefinationProcess = new DefinationProcess();
            return View("CreateOrUpdate", newDefinationProcess);
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = SystemStatics.DefaultScheme, Roles = "DefinationProcessUp, DefinationProcessFull, AdminFull")]
        public async Task<IActionResult> Edit(int DefinationProcessId)
        {
            ViewBag.Error = false;
            ViewBag.ErrorMessage = "";
            DefinationProcess existDefinationProcess = await factory.DefinationProcesses.GetDefinationProcessFromDefinationProcessID(DefinationProcessId);
            if (existDefinationProcess == null)
                return RedirectToAction("Error", "Home");
            return View("CreateOrUpdate", existDefinationProcess);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Save(DefinationProcess definationProcess)
        {
            ViewBag.Error = true;
            if (definationProcess.DefinationProcessID == 0)
            {
                if (!(User.IsInRole("DefinationProcessAdd") || User.IsInRole("AdminFull") || User.IsInRole("DefinationProcessFull")))
                    return Unauthorized();

                await factory.DefinationProcesses.Add(definationProcess, int.Parse(User.Identity.Name));
            }
            else
            {
                if (!(User.IsInRole("DefinationProcessUp") || User.IsInRole("AdminFull") || User.IsInRole("DefinationProcessFull")))
                    return Unauthorized();

                var foundDefinationProcess = await factory.DefinationProcesses.GetDefinationProcessFromDefinationProcessID(definationProcess.DefinationProcessID);

                foundDefinationProcess.ProcessCode = definationProcess.ProcessCode;
                foundDefinationProcess.Name = definationProcess.Name;

                await factory.DefinationProcesses.Update(foundDefinationProcess, int.Parse(User.Identity.Name));

            }
            return Redirect("Index");
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = SystemStatics.DefaultScheme, Roles = "DefinationProcessShow, AdminFull, DefinationProcessFull")]
        public async Task<IActionResult> Details(int definationProcessId)
        {
            var definationProcess = await factory.DefinationProcesses.GetDefinationProcessFromDefinationProcessID(definationProcessId);
            if (definationProcess == null)
            {
                return RedirectToAction("Error", "Home");
            }

            return View(definationProcess);
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = SystemStatics.DefaultScheme, Roles = "DefinationProcessDel, AdminFull, DefinationProcessFull")]
        public async Task<IActionResult> Delete(int definationProcessId)
        {
            var definationProcess = await factory.DefinationProcesses.GetDefinationProcessFromDefinationProcessID(definationProcessId);

            if (definationProcess == null)
            {
                return RedirectToAction("Error", "Home");
            }

            return View(definationProcess);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int definationProcessId)
        {
            if (!(User.IsInRole("DefinationProcessDel") || User.IsInRole("AdminFull") || User.IsInRole("DefinationProcessFull")))
                return Unauthorized();

            var definationProcess = await factory.DefinationProcesses.GetDefinationProcessFromDefinationProcessID(definationProcessId);

            await factory.DefinationProcesses.Delete(definationProcess, int.Parse(User.Identity.Name));

            return Redirect("Index");
        }


    }
}
