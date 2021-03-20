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
    public class SampleController : Controller
    {
        FinishingSystemFactory factory;
        public SampleController()
        {
            factory = new FinishingSystemFactory();
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = SystemStatics.DefaultScheme, Roles = "SampleShow, SampleFull, AdminFull")]
        public async Task<IActionResult> Index(int ruloProcessId)
        {
            if (ruloProcessId <= 0) return NotFound();
            
            var res = await factory.Rulos.GetRuloProcessFromRuloProcessID(ruloProcessId);
            if (res == null) NotFound();
            ViewBag.RuloProcessId = res.RuloProcessID;
            ViewBag.RuloId = res.RuloID;

            var result = await factory.Samples.GetSamplesByRuloProcessID(ruloProcessId);

            return View(result);
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = SystemStatics.DefaultScheme, Roles = "SampleAdd,SampleFull,AdminFull")]
        public async Task<IActionResult> Create(int ruloProcessId)
        {
            ViewBag.Error = false;
            ViewBag.ErrorMessage = "";
            if (ruloProcessId <= 0) NotFound();

            var ruloProcess = await factory.Rulos.GetRuloProcessFromRuloProcessID(ruloProcessId);

            if (ruloProcess == null) NotFound();

            Sample newSample = new Sample();
            newSample.RuloProcessID = ruloProcessId;

            return View("CreateOrUpdate", newSample);
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = SystemStatics.DefaultScheme, Roles = "SampleUp,SampleFull,AdminFull")]
        public async Task<IActionResult> Edit(int sampleId)
        {
            ViewBag.Error = false;
            ViewBag.ErrorMessage = "";

            Sample editSample = await factory.Samples.GetSampleFromSampleID(sampleId);

            if (editSample == null)
                return NotFound();

            return View("CreateOrUpdate", editSample);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Save(Sample sample)
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

            if (sample.SampleID == 0)
            {
                if (!(User.IsInRole("Sample", AuthType.Add)))
                    return Unauthorized();

                sample.CutterID = int.Parse(User.Identity.Name);

                await factory.Samples.Add(sample, int.Parse(User.Identity.Name));
            }
            else
            {
                if (!User.IsInRole("Sample", AuthType.Update))
                    return Unauthorized();

                var foundSample  = await factory.Samples.GetSampleFromSampleID(sample.SampleID);

                foundSample.Meter = sample.Meter;
                foundSample.CutterID = int.Parse(User.Identity.Name);
                foundSample.Details = sample.Details;

                await factory.Samples.Update(foundSample, int.Parse(User.Identity.Name));
            }

            return RedirectToAction("Index", new { sample.RuloProcessID });
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = SystemStatics.DefaultScheme, Roles =  "SampleShow, SampleFull, AdminFull")]
        public async Task<IActionResult> Details(int sampleId)
        {
            Sample foundSample = await factory.Samples.GetSampleFromSampleID(sampleId);

            if (foundSample == null)
                return NotFound();

            return View(foundSample);
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = SystemStatics.DefaultScheme, Roles = "SampleDel, SampleFull, AdminFull")]
        public async Task<IActionResult> Delete(int sampleId)
        {
            var foundSample = await factory.Samples.GetSampleFromSampleID(sampleId);

            if (foundSample == null)
                return NotFound();

            return View(foundSample);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int sampleId)
        {
            if (!User.IsInRole("Sample", AuthType.Delete))
                return Unauthorized();

            var sample = await factory.Samples.GetSampleFromSampleID(sampleId);

            await factory.Samples.Delete(sample, int.Parse(User.Identity.Name));

            return RedirectToAction("Index", new { sample.RuloProcessID });
        }

        [HttpGet]
        public async Task<IActionResult> GetRuloProcess(int ruloProcessId)
        {
            if (!User.IsInRole("Sample", AuthType.Show)) return Unauthorized();

            var foundRulo = await factory.Rulos.GetRuloProcessFromRuloProcessID(ruloProcessId);
            if (foundRulo == null) NotFound();

            return PartialView(foundRulo);
        }

    }
}
