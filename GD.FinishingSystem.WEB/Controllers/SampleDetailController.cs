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
    public class SampleDetailController : Controller
    {
        FinishingSystemFactory factory;
        public SampleDetailController()
        {
            factory = new FinishingSystemFactory();
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = SystemStatics.DefaultScheme, Roles = "SampleShow, SampleFull, AdminFull")]
        //public async Task<IActionResult> Index([FromBody] SampleDetail sampleDetail)
        public async Task<IActionResult> Index(int ruloId, int sampleId)
        {
            if (sampleId == 0) return NotFound();

            var result = await factory.Samples.GetSampleDetailsFromSampleID(sampleId);
            ViewBag.RuloId = ruloId;
            ViewBag.SampleId = sampleId;

            return View(result);
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = SystemStatics.DefaultScheme, Roles = "SampleAdd,SampleFull,AdminFull")]
        public async Task<IActionResult> Create(int sampleId)
        {
            ViewBag.Error = false;
            ViewBag.ErrorMessage = "";

            var sample = await factory.Samples.GetSampleFromSampleID(sampleId);

            if (sample == null) NotFound();

            SampleDetail newSampleDetail = new SampleDetail();
            newSampleDetail.SampleID = sample.SampleID;
            newSampleDetail.RuloID = sample.RuloID;
            newSampleDetail.RuloProcessID = sample.RuloProcessID;
            newSampleDetail.DateTime = DateTime.Now;

            return View("CreateOrUpdate", newSampleDetail);
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = SystemStatics.DefaultScheme, Roles = "SampleUp,SampleFull,AdminFull")]
        public async Task<IActionResult> Edit(int sampleDetailId)
        {
            ViewBag.Error = false;
            ViewBag.ErrorMessage = "";

            SampleDetail editSampleDetail = await factory.Samples.GetSampleDetailFromSampleDetailID(sampleDetailId);

            if (editSampleDetail == null)
                return NotFound();

            return View("CreateOrUpdate", editSampleDetail);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Save(SampleDetail sampleDetail)
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

            if (sampleDetail.SampleDetailID == 0)
            {
                if (!(User.IsInRole("Sample", AuthType.Add)))
                    return Unauthorized();

                await factory.Samples.AddSampleDetail(sampleDetail, int.Parse(User.Identity.Name));
            }
            else
            {
                if (!(User.IsInRole("Sample", AuthType.Update)))
                    return Unauthorized();

                var foundSampleDetails  = await factory.Samples.GetSampleDetailFromSampleDetailID(sampleDetail.SampleDetailID);

                foundSampleDetails.Meter = sampleDetail.Meter;
                foundSampleDetails.DateTime = sampleDetail.DateTime;
                foundSampleDetails.Details = sampleDetail.Details;

                await factory.Samples.UpdateSampleDetail(foundSampleDetails, int.Parse(User.Identity.Name));
            }

            return RedirectToAction("Index", new { sampleDetail.RuloID, sampleDetail.SampleID });
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = SystemStatics.DefaultScheme, Roles =  "SampleShow, SampleFull, AdminFull")]
        public async Task<IActionResult> Details(int sampleDetailId)
        {
            SampleDetail foundSampleDetail = await factory.Samples.GetSampleDetailFromSampleDetailID(sampleDetailId);

            if (foundSampleDetail == null)
                return NotFound();

            return View(foundSampleDetail);
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = SystemStatics.DefaultScheme, Roles = "SampleDel, SampleFull, AdminFull")]
        public async Task<IActionResult> Delete(int sampleDetailId)
        {
            var foundSampleDetail = await factory.Samples.GetSampleDetailFromSampleDetailID(sampleDetailId);

            if (foundSampleDetail == null)
                return NotFound();

            return View(foundSampleDetail);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int sampleDetailId)
        {
            if (!User.IsInRole("Sample", AuthType.Delete))
                return Unauthorized();

            var sampleDetail = await factory.Samples.GetSampleDetailFromSampleDetailID(sampleDetailId);

            await factory.Samples.DeleteSampleDetail(sampleDetail, int.Parse(User.Identity.Name));

            return RedirectToAction("Index", new { SampleID = sampleDetail.SampleID});
        }

    }
}
