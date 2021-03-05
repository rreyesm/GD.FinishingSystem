using GD.FinishingSystem.Bussines;
using GD.FinishingSystem.Entities;
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
        [Authorize(AuthenticationSchemes = SystemStatics.DefaultScheme, Roles = "DefinitionProcessShow, DefinitionProcessFull, AdminFull")]
        public async Task<IActionResult> Index()
        {
            DateTime dtBegin = DateTime.Today.AddMonths(-1);
            DateTime dtEnd = DateTime.Today.AddDays(1).AddMilliseconds(-1);

            var result = await factory.DefinationProcesses.GetDefinationProcessListFromBetweenDate(dtBegin, dtEnd);

            SetViewBagsForDates(dtBegin, dtEnd);
            return View(result);
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = SystemStatics.DefaultScheme, Roles = "DefinitionProcessShow, DefinitionProcessFull,AdminFull")]
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
        [Authorize(AuthenticationSchemes = SystemStatics.DefaultScheme, Roles = "DefinitionProcessAdd, DefinitionProcessFull, AdminFull")]
        public IActionResult Create()
        {
            ViewBag.Error = false;
            ViewBag.ErrorMessage = "";
            DefinationProcess newDefinationProcess = new DefinationProcess();
            return View("CreateOrUpdate", newDefinationProcess);
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = SystemStatics.DefaultScheme, Roles = "DefinitionProcessUp, DefinitionProcessFull, AdminFull")]
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
                if (!(User.IsInRole("DefinitionProcessAdd") || User.IsInRole("AdminFull") || User.IsInRole("DefinitionProcessFull")))
                    return Unauthorized();

                //Validate code
                var foundDefinationProcess = await factory.DefinationProcesses.GetDefinitionProcessFromDefinitionProcessCode(definationProcess.ProcessCode);

                if (foundDefinationProcess != null)
                {
                    ViewBag.ErrorMessage = "Definition code already exist";
                    return View("CreateOrUpdate", foundDefinationProcess);
                }

                await factory.DefinationProcesses.Add(definationProcess, int.Parse(User.Identity.Name));
            }
            else
            {
                if (!(User.IsInRole("DefinitionProcessUp") || User.IsInRole("AdminFull") || User.IsInRole("DefinitionProcessFull")))
                    return Unauthorized();

                var foundDefinationProcess = await factory.DefinationProcesses.GetDefinationProcessFromDefinationProcessID(definationProcess.DefinationProcessID);

                foundDefinationProcess.ProcessCode = definationProcess.ProcessCode;
                foundDefinationProcess.Name = definationProcess.Name;

                await factory.DefinationProcesses.Update(foundDefinationProcess, int.Parse(User.Identity.Name));

            }
            return Redirect("Index");
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = SystemStatics.DefaultScheme, Roles = "DefinitionProcessShow, AdminFull, DefinitionProcessFull")]
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
        [Authorize(AuthenticationSchemes = SystemStatics.DefaultScheme, Roles = "DefinitionProcessDel, AdminFull, DefinitionProcessFull")]
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
            if (!(User.IsInRole("DefinitionProcessDel") || User.IsInRole("AdminFull") || User.IsInRole("DefinitionProcessFull")))
                return Unauthorized();

            var definationProcess = await factory.DefinationProcesses.GetDefinationProcessFromDefinationProcessID(definationProcessId);

            await factory.DefinationProcesses.Delete(definationProcess, int.Parse(User.Identity.Name));

            return Redirect("Index");
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = SystemStatics.DefaultScheme, Roles = "MachineShow, MachineFull, AdminFull")]
        public async Task<IActionResult> GetMachines(int DefinationProcessId)
        {
            if (!User.IsInRole("RuloProcess", AuthType.Show)) return Unauthorized();

            var foundDefProcess = await factory.DefinationProcesses.GetDefinationProcessFromDefinationProcessID(DefinationProcessId);
            if (foundDefProcess == null) return NotFound();

            var machines = await factory.Machines.GetVMMachinesFromDefinationProcessID(DefinationProcessId);

            return PartialView(machines);


        }

    }
}
