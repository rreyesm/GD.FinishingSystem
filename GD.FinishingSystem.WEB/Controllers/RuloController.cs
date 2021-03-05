using GD.FinishingSystem.Bussines;
using GD.FinishingSystem.Entities;
using GD.FinishingSystem.Entities.ViewModels;
using GD.FinishingSystem.WEB.Classes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Options;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace GD.FinishingSystem.WEB.Controllers
{

    [Authorize(AuthenticationSchemes = SystemStatics.DefaultScheme)]
    public class RuloController : Controller
    {
        private readonly IWebHostEnvironment webHostEnvironment = null;
        private AppSettings appSettings;
        FinishingSystemFactory factory;
        public RuloController(IWebHostEnvironment webHostEnvironment, IOptions<AppSettings> appSettings)
        {
            this.webHostEnvironment = webHostEnvironment;
            this.appSettings = appSettings.Value;
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
            var testCategorylist2 = WebUtilities.Create<TestCategory>(testCategoryList, "TestCategoryID", "TestCode", true, "All");

            var definitionProcessList = await factory.DefinationProcesses.GetDefinationProcessList();
            var definitionProcessList2 = WebUtilities.Create<DefinationProcess>(definitionProcessList, "DefinationProcessID", "Name", true, "All");

            ViewBag.TestCategorytList = testCategorylist2;
            ViewBag.DefinitionProcessList = definitionProcessList2;

            ViewBag.ModalTestResultList = System.Text.Json.JsonSerializer.Serialize(testCategorylist2);

            ViewBag.numLote = ruloFilters.numLote;
            ViewBag.numBeam = ruloFilters.numBeam;
            ViewBag.numLoom = ruloFilters.numLoom;
            ViewBag.numPiece = ruloFilters.numPiece;
            ViewBag.txtStyle = ruloFilters.txtStyle;
            ViewBag.numTestCategory = ruloFilters.numTestCategory;
            ViewBag.numDefinitionProcess = ruloFilters.numDefinitionProcess;
            ViewBag.folioNumber = ruloFilters.FolioNumber;

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
            var list = WebUtilities.Create<OriginType>();
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

                foundRulo.Lote = rulo.Lote;
                foundRulo.Beam = rulo.Beam;
                foundRulo.BeamStop = rulo.BeamStop;
                foundRulo.Loom = rulo.Loom;
                foundRulo.LoomLetter = rulo.LoomLetter;
                foundRulo.Piece = rulo.Piece;
                foundRulo.PieceLetter = rulo.PieceLetter;
                foundRulo.Style = rulo.Style;
                foundRulo.StyleName = rulo.StyleName;
                foundRulo.Width = rulo.Width;
                foundRulo.EntranceLength = rulo.EntranceLength;
                foundRulo.Shift = rulo.Shift;
                foundRulo.OriginID = rulo.OriginID;
                foundRulo.Observations = rulo.Observations;
                if (foundRulo.FolioNumber == 0)
                    foundRulo.DeliveryDate = DateTime.Now;
                foundRulo.FolioNumber = rulo.FolioNumber;

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

            string ZPLString = string.Empty;
            ZPLString = System.IO.File.ReadAllText(System.IO.Path.Combine(webHostEnvironment.WebRootPath, "..\\Reports\\RuloLabel.prn"));
            ZPLString = ZPLString.Replace("ReplaceRuloID", rulo.RuloID.ToString());
            ZPLString = ZPLString.Replace("ReplaceStyle", rulo.Style);
            ZPLString = ZPLString.Replace("ReplaceLote", rulo.Lote);
            ZPLString = ZPLString.Replace("ReplaceBeam", rulo.Beam.ToString());
            ZPLString = ZPLString.Replace("ReplaceLoom", rulo.Loom.ToString());
            ZPLString = ZPLString.Replace("ReplaceMeters", rulo.ExitLength.ToString("#,##0.00"));
            ZPLString = ZPLString.Replace("ReplaceDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm"));

            var result = await RawPrinterHelper.PrintToZPLByIP(appSettings.PrinterIP, ZPLString);

            if (!result)
                return new JsonResult(new { errorMessage = "Error to the print label!" });
            
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

            var processes = await factory.Rulos.GetVMRuloProcessesFromRuloID(RuloId);


            return PartialView(processes);


        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = SystemStatics.DefaultScheme, Roles = "RuloShow,RuloFull,AdminFull")]
        public async Task<IActionResult> ExportToExcel(VMRuloFilters ruloFilters)
        {

            ruloFilters.dtEnd = ruloFilters.dtEnd.AddDays(1).AddMilliseconds(-1);
            var result = await factory.Rulos.GetRuloListFromFilters(ruloFilters);

            ExportToExcel export = new ExportToExcel();
            string reportName = "Finishing Report";
            string fileName = $"Finishing Report_{DateTime.Today.Year}_{DateTime.Today.Month.ToString().PadLeft(2, '0')}_{DateTime.Today.Day.ToString().PadLeft(2, '0')}.xlsx";
            var fileResult = await export.Export<VMRulo>("Global Denim S.A. de C.V.", "Finishing", reportName, fileName, result.ToList());

            if (!fileResult.Item1) return NotFound();

            return fileResult.Item2;
        }

    }
}
