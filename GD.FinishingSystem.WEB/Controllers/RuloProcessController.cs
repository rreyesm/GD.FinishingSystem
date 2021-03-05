using GD.FinishingSystem.Bussines;
using GD.FinishingSystem.Entities;
using GD.FinishingSystem.Entities.ViewModels;
using GD.FinishingSystem.WEB.Classes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GD.FinishingSystem.WEB.Controllers
{
    [Authorize(AuthenticationSchemes = SystemStatics.DefaultScheme)]
    public class RuloProcessController : Controller
    {
        FinishingSystemFactory factory;
        public RuloProcessController()
        {
            factory = new FinishingSystemFactory();
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = SystemStatics.DefaultScheme, Roles = "RuloProcessShow, RuloProcessFull, AdminFull")]
        public async Task<IActionResult> Index(int? ruloid)
        {

            if (ruloid == null || ruloid.Value <= 0) return NotFound();

            IEnumerable<VMRuloProcess> result = null;

            result = await factory.Rulos.GetVMRuloProcessesFromRuloID(ruloid.Value);
            if (result == null) return NotFound();
            ViewBag.relRuloID = ruloid;

            return View(result);
        }


        [HttpGet]
        [Authorize(AuthenticationSchemes = SystemStatics.DefaultScheme, Roles = "RuloProcessAdd,RuloProcessFull,AdminFull")]
        public async Task<IActionResult> Create(int relRuloId)
        {
            ViewBag.Error = false;
            ViewBag.ErrorMessage = "";

            await SetViewBagForDefinationProcess();

            RuloProcess newRuloProcess = new RuloProcess();
            newRuloProcess.RuloID = relRuloId;
            newRuloProcess.BeginningDate = DateTime.Now;

            return View("CreateOrUpdate", newRuloProcess);
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = SystemStatics.DefaultScheme, Roles = "RuloProcessUp,RuloProcessFull,AdminFull")]
        public async Task<IActionResult> Edit(int ruloProcessID)
        {
            ViewBag.Error = false;
            ViewBag.ErrorMessage = "";

            await SetViewBagForDefinationProcess();

            RuloProcess existRuloProcess = await factory.Rulos.GetRuloProcessFromRuloProcessID(ruloProcessID);
            if (existRuloProcess == null)
                return RedirectToAction("Error", "Home");
            return View("CreateOrUpdate", existRuloProcess);
        }

        private async Task SetViewBagForDefinationProcess()
        {
            var definationProcessList = await factory.DefinationProcesses.GetDefinationProcessList();
            ViewBag.DefinationProcessList = WebUtilities.Create<DefinationProcess>(definationProcessList, "DefinationProcessID", "Name", true);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Save(RuloProcess ruloProcess)
        {
            ViewBag.Error = true;
            string errorMessage = string.Empty;
            await SetViewBagForDefinationProcess();

            if (!ModelState.IsValid)
            {
                foreach (var modelStateKey in ModelState.Keys)
                {
                    var modelStateVal = ModelState[modelStateKey];
                    foreach (var error in modelStateVal.Errors)
                    {
                        var key = modelStateKey;
                        errorMessage += error.ErrorMessage + "<br>";
                    }
                }

                ViewBag.ErrorMessage = errorMessage;

                if (ruloProcess.IsFinished == true && (ruloProcess.FinishMeter == null || ruloProcess.FinishMeter <= 0))
                {
                    ViewBag.ErrorMessage += "Finish Meter is not valid!" + "<br>";
                    return View("CreateOrUpdate", ruloProcess);
                }

                return View("CreateOrUpdate", ruloProcess);
            }



            if (ruloProcess.RuloProcessID == 0)
            {
                if (!(User.IsInRole("RuloProcess", AuthType.Add)))
                    return Unauthorized();


                await factory.Rulos.AddRuloProcess(ruloProcess, int.Parse(User.Identity.Name));

            }
            else
            {
                if (!(User.IsInRole("RuloProcessUp") || User.IsInRole("RuloProcessFull") || User.IsInRole("AdminFull")))
                    return Unauthorized();

                var foundRuloProcess = await factory.Rulos.GetRuloProcessFromRuloProcessID(ruloProcess.RuloID);

                foundRuloProcess.DefinationProcessID = ruloProcess.DefinationProcessID;
                foundRuloProcess.BeginningDate = ruloProcess.BeginningDate;

                await factory.Rulos.UpdateRuloProcess(foundRuloProcess, int.Parse(User.Identity.Name));

            }

            return RedirectToAction("Index", new { ruloid = ruloProcess.RuloID });
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = SystemStatics.DefaultScheme, Roles = "RuloProcessShow, RuloProcessFull, AdminFull")]
        public async Task<IActionResult> Details(int ruloProcessId)
        {
            var foundRuloProcess = await factory.Rulos.GetRuloProcessFromRuloProcessID(ruloProcessId);
            if (foundRuloProcess == null)
            {
                return RedirectToAction("Error", "Home");
            }

            return View(foundRuloProcess);
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = SystemStatics.DefaultScheme, Roles = "RuloProcessDel, RuloProcessFull, AdminFull")]
        public async Task<IActionResult> Delete(int ruloProcessId)
        {
            var ruloProcess = await factory.Rulos.GetRuloProcessFromRuloProcessID(ruloProcessId);
            if (ruloProcess == null)
            {
                return RedirectToAction("Error", "Home");
            }

            return View(ruloProcess);
        }


        [ValidateAntiForgeryToken, HttpPost, ActionName("Delete")]

        public async Task<IActionResult> DeleteConfirmed(int RuloProcessID)
        {
            if (!(User.IsInRole("RuloProcess", AuthType.Delete)))
                return Unauthorized();

            var ruloProcess = await factory.Rulos.GetRuloProcessFromRuloProcessID(RuloProcessID);
            if (ruloProcess == null) return NotFound();

            await factory.Rulos.DeleteRuloProcessFromRuloProcessID(RuloProcessID, int.Parse(User.Identity.Name));

            return RedirectToAction("Index", new { RuloID = ruloProcess.RuloID });
        }

        [HttpPost]
        public async Task<IActionResult> FinishProcess(int RuloProcessID, decimal Meter)
        {

            if (!User.IsInRole("RuloProcess", AuthType.Update)) return Unauthorized();
            var foundRuloProcess = await factory.Rulos.GetRuloProcessFromRuloProcessID(RuloProcessID);
            if (foundRuloProcess == null) return NotFound();

            foundRuloProcess.FinishMeter = Meter;
            foundRuloProcess.IsFinished = true;
            foundRuloProcess.EndDate = DateTime.Now;
            await factory.Rulos.UpdateRuloProcess(foundRuloProcess, int.Parse(User.Identity.Name));

            var rulo = await factory.Rulos.GetRuloFromRuloID(foundRuloProcess.RuloID);
            rulo.ExitLength = Meter;

            await factory.Rulos.Update(rulo, int.Parse(User.Identity.Name));
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> Sample(int SampleID, int RuloID, int RuloProcessID, decimal Meter)
        {
            if (!User.IsInRole("Sample", AuthType.Update)) return Unauthorized();

            var foundRuloProcess = await factory.Rulos.GetRuloProcessFromRuloProcessID(RuloProcessID);
            if (foundRuloProcess == null) return NotFound();

            if (SampleID == 0)
            {
                Sample sample = new Sample();
                sample.RuloID = RuloID;
                sample.RuloProcessID = RuloProcessID;
                sample.Meter = Meter;
                sample.DateTime = DateTime.Now;

                await factory.Samples.Add(sample, int.Parse(User.Identity.Name));

                foundRuloProcess.SampleID = sample.SampleID;
                await factory.Rulos.UpdateRuloProcess(foundRuloProcess, int.Parse(User.Identity.Name));
            }
            else
            {
                var sample = await factory.Samples.GetSampleFromSampleID(SampleID);
                sample.Meter = Meter;
                sample.DateTime = DateTime.Now;
                await factory.Samples.Update(sample, int.Parse(User.Identity.Name));
            }

            return Ok();
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = SystemStatics.DefaultScheme, Roles = "RuloProcessShow, RuloProcessFull, AdminFull")]
        public async Task<IActionResult> GetRulo(int ruloId)
        {
            if (!User.IsInRole("RuloProcess", AuthType.Show)) return Unauthorized();

            var foundRulo = await factory.Rulos.GetVMRuloFromVMRuloID(ruloId);
            if (foundRulo == null) NotFound();

            return PartialView(foundRulo);
        }

    }
}
