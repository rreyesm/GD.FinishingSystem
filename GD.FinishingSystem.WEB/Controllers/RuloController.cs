using GD.FinishingSystem.Bussines;
using GD.FinishingSystem.Entities;
using GD.FinishingSystem.Entities.ViewModels;
using GD.FinishingSystem.WEB.Classes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
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

            var testCategoryList = await factory.TestCategories.GetTestCategoryList();
            var list = WebUtilities.Create<TestCategory>(testCategoryList, "TestCategoryID", "TestCode");
            list.Insert(0, new SelectListItem("All", "0"));
            ViewBag.TestCategorytList = list;
            ViewBag.ModalTestResultList = System.Text.Json.JsonSerializer.Serialize(list);

            ViewBag.numLote = ruloFilters.numLote;
            ViewBag.numBeam = ruloFilters.numBeam;
            ViewBag.numLoom = ruloFilters.numLoom;
            ViewBag.numPiece = ruloFilters.numPiece;
            ViewBag.txtStyle = ruloFilters.txtStyle;
            ViewBag.numTestCategory = ruloFilters.numTestCategory;

            //Style list
            var styleList = await factory.Rulos.GetRuloStyleList();
            ViewBag.StyleList = styleList;

        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = SystemStatics.DefaultScheme, Roles = "RuloAdd,RuloFull,AdminFull")]
        public async Task<IActionResult> Create()
        {
            ViewBag.Error = false;
            ViewBag.ErrorMessage = "";
            await SetViewBagsForCreateOrEdit();

            Rulo newRulo = new Rulo();
            return View("CreateOrUpdate", newRulo);
        }

        private async Task SetViewBagsForCreateOrEdit()
        {
            var originCategoryList = await factory.OriginCategories.GetOriginCategoryList();
            var list = WebUtilities.Create<OriginCategory>(originCategoryList, "OriginCategoryID", "OriginCode", true);
            ViewBag.OriginList = list;
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = SystemStatics.DefaultScheme, Roles = "RuloUp,RuloFull,AdminFull")]
        public async Task<IActionResult> Edit(int RuloID)
        {
            ViewBag.Error = false;
            ViewBag.ErrorMessage = "";
            await SetViewBagsForCreateOrEdit();

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
                foundRulo.OriginID = rulo.OriginID;
                foundRulo.Observations = rulo.Observations;

                await factory.Rulos.Update(foundRulo, int.Parse(User.Identity.Name));

            }
            return Redirect("Index");
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = SystemStatics.DefaultScheme, Roles = "RuloShow, AdminFull, RuloFull")]
        public async Task<IActionResult> Details(int ruloId)
        {
            var foundVMRulo = await factory.Rulos.GetVMRuloFromVMRuloID(ruloId);
            if (foundVMRulo == null)
            {
                return RedirectToAction("Error", "Home");
            }

            return View(foundVMRulo);
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = SystemStatics.DefaultScheme, Roles = "RuloDel, AdminFull, RuloFull")]
        public async Task<IActionResult> Delete(int ruloId)
        {
            var foundVMRulo = await factory.Rulos.GetVMRuloFromVMRuloID(ruloId);
            if (foundVMRulo == null)
            {
                return RedirectToAction("Error", "Home");
            }

            return View(foundVMRulo);
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
            // await RawPrinterHelper.PrintToZPLByIP("192.168.7.200", zpl);

            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> TestResultFinish(int RuloId, int TestCategoryId, string Description)
        {
            if (!User.IsInRole("Rulo", AuthType.Update)) return Unauthorized();

            var foundRulo = await factory.Rulos.GetRuloFromRuloID(RuloId);
            if (foundRulo == null) return NotFound();

            //Create or update test result
            TestResult testResult = null;
            if (foundRulo.TestResultID == null)
                testResult = new TestResult();
            else
                testResult = await factory.TestResults.GetTestResultFromTestResultID((int)foundRulo.TestResultID);

            if (TestCategoryId == 1 || TestCategoryId == 3) //OK √ or Fail √
            {
                testResult.Details = Description;
                testResult.CanContinue = true;
                testResult.TestCategoryID = TestCategoryId;
            }
            else
            {
                testResult.Details = Description;
                testResult.CanContinue = false;
                testResult.TestCategoryID = TestCategoryId;
            }

            if (foundRulo.TestResultID == null)
            {
                await factory.TestResults.Add(testResult, int.Parse(User.Identity.Name));

                bool isWaitingForTestResult = false;
                if (TestCategoryId == 5) //Waiting
                    isWaitingForTestResult = true;

                var intUser = int.Parse(User.Identity.Name);
                await factory.Rulos.SetTestResult(RuloId, testResult.TestResultID, isWaitingForTestResult, intUser, intUser);
            }
            else
            {
                if (TestCategoryId == 5) //Waiting
                {
                    foundRulo.IsWaitingAnswerFromTest = true;
                    await factory.Rulos.Update(foundRulo, int.Parse(User.Identity.Name));
                }
                else
                {
                    foundRulo.IsWaitingAnswerFromTest = false;
                    await factory.Rulos.Update(foundRulo, int.Parse(User.Identity.Name));
                }

                await factory.TestResults.Update(testResult, int.Parse(User.Identity.Name));
            }

            return Ok();
        }


        [HttpGet]
        [Authorize(AuthenticationSchemes = SystemStatics.DefaultScheme, Roles = "RuloProcessShow, RuloProcessFull, AdminFull")]
        public async Task<IActionResult> GetProcesses(int RuloId)
        {
            if (!User.IsInRole("RuloProcess", AuthType.Show)) return Unauthorized();

            var foundRulo = await factory.Rulos.GetRuloFromRuloID(RuloId);
            if (foundRulo == null) return NotFound();

            var processes = await factory.Rulos.GetRuloProcessesFromRuloID(RuloId);


            return PartialView(processes);


        }

    }
}
