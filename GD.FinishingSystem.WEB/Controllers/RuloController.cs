using GD.FinishingSystem.Bussines;
using GD.FinishingSystem.Entities;
using GD.FinishingSystem.Entities.ViewModels;
using GD.FinishingSystem.WEB.Classes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
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

        IConfiguration Configuration;
        IndexModelRulo IndexModelRulo = null;

        public RuloController(IWebHostEnvironment webHostEnvironment, IOptions<AppSettings> appSettings, IConfiguration configuration)
        {
            this.webHostEnvironment = webHostEnvironment;
            this.appSettings = appSettings.Value;
            factory = new FinishingSystemFactory();

            Configuration = configuration;
            IndexModelRulo = new IndexModelRulo(factory, configuration);
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = SystemStatics.DefaultScheme, Roles = "RuloShow,RuloFull,AdminFull")]
        public async Task<IActionResult> Index(int positionRuloId = 0, string currentFilter = null, int? pageIndex = null)
        {
            VMRuloFilters ruloFilters = null;
            if (currentFilter == null)
            {
                ruloFilters = new VMRuloFilters();
                ruloFilters.dtBegin = DateTime.Today.AddMonths(-1);
                ruloFilters.dtEnd = DateTime.Today.AddDays(1).AddMilliseconds(-1);
            }
            else
            {
                ruloFilters = System.Text.Json.JsonSerializer.Deserialize<VMRuloFilters>(currentFilter);
            }

            //var result = await factory.Rulos.GetRuloListFromBetweenDate(ruloFilters.dtBegin, ruloFilters.dtEnd);
            await SetViewBagsForDates(ruloFilters, positionRuloId);

            await IndexModelRulo.OnGetAsync("", pageIndex, ruloFilters);

            return View(IndexModelRulo);
        }


        [HttpPost]
        [Authorize(AuthenticationSchemes = SystemStatics.DefaultScheme, Roles = "RuloShow,RuloFull,AdminFull")]
        public async Task<IActionResult> Index(VMRuloFilters ruloFilters)
        {
            ruloFilters.dtEnd = ruloFilters.dtEnd.AddDays(1).AddMilliseconds(-1);
            //var result = await factory.Rulos.GetRuloListFromFilters(ruloFilters);
            await SetViewBagsForDates(ruloFilters);

            await IndexModelRulo.OnGetAsync("", 1, ruloFilters);

            return View(IndexModelRulo);
        }

        private async Task SetViewBagsForDates(VMRuloFilters ruloFilters, int positionRuloId = 0)
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
            var styleDataList = await factory.Rulos.GetRuloStyleStringForProductionLoteList();
            ViewBag.StyleList = styleDataList.ToList();
            await GetInfoTitle();

            ViewBag.PositionRuloId = positionRuloId;

        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = SystemStatics.DefaultScheme, Roles = "RuloAdd,RuloFull,AdminFull")]
        public async Task<IActionResult> Create()
        {
            ViewBag.Error = false;
            ViewBag.ErrorMessage = "";
            await SetViewBagsForCreateOrEdit();

            var systemPrinter = await WebUtilities.GetSystemPrinter(factory, this.HttpContext);

            Rulo newRulo = new Rulo();
            var currentPeriod = await factory.Periods.GetCurrentPeriod(systemPrinter.SystemPrinterID);
            if (currentPeriod == null)
            {
                ViewBag.Error = true;
                ViewBag.ErrorMessage = "Cannot create a new roll. You must open a period.";
                View("CreateOrUpdate", newRulo);
            }

            return View("CreateOrUpdate", newRulo);
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = SystemStatics.DefaultScheme, Roles = "RuloAdd,RuloFull,AdminFull")]
        public async Task<IActionResult> CreateFromRuloMigration(Rulo newRulo)
        {
            ViewBag.Error = false;
            ViewBag.ErrorMessage = "";

            await SetViewBagsForCreateOrEdit();

            int ruloMigrationId1 = Convert.ToInt32(TempData["ruloMigrationId1"]);
            TempData["ruloMigrationId2"] = ruloMigrationId1;

            var systemPrinter = await WebUtilities.GetSystemPrinter(factory, this.HttpContext);
            var currentPeriod = await factory.Periods.GetCurrentPeriod(systemPrinter.SystemPrinterID);

            if (currentPeriod == null)
            {
                ViewBag.Error = true;
                ViewBag.ErrorMessage = "Cannot create a new roll. You must open a period.";
                View("CreateOrUpdate", newRulo);
            }

            return View("CreateOrUpdate", newRulo);
        }

        private async Task SetViewBagsForCreateOrEdit()
        {
            var list = WebUtilities.Create<OriginType>();
            ViewBag.OriginList = list;

            var sentAuthorizerList = await factory.Users.GetAll();
            var sentAuthorizerListItem = WebUtilities.Create<User>(sentAuthorizerList, "UserID", "Name", true);
            ViewBag.SentAuthorizer = sentAuthorizerListItem;
        }

        [HttpPost]
        public async Task<IActionResult> GetStyleData(string lote)
        {
            string errorMessage = string.Empty;
            string style = string.Empty;
            string styleName = string.Empty;

            if (!(User.IsInRole("Rulo", AuthType.Add) || User.IsInRole("Rulo", AuthType.Update)))
                return Unauthorized();

            var styleData = await factory.Rulos.GetRuloStyle(lote);

            var systemPrinter = await WebUtilities.GetSystemPrinter(factory, this.HttpContext);
            //Comparation period-rulo style
            Period period = await factory.Periods.GetCurrentPeriod(systemPrinter.SystemPrinterID);
            if (period != null)
            {
                if (period.Style != styleData.Style)
                {
                    errorMessage = "The style entered does not correspond to the style of the period. First create the period for the style.";
                }
            }
            else
            {
                errorMessage = "Period style not found!";
            }

            if (style == null)
                errorMessage = "Lote not found in Control de Rollos!";
            else
            {
                style = styleData.Style;
                styleName = styleData.StyleName;
            }

            return new JsonResult(new { errorMessage = errorMessage, style = style, styleName = styleName });
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
            await SetViewBagsForCreateOrEdit();

            //Update style and style name to avoid update user
            var styleData = await factory.Rulos.GetRuloStyle(rulo.Lote);
            rulo.Style = styleData.Style;
            rulo.StyleName = styleData.StyleName;

            var systemPrinter = await WebUtilities.GetSystemPrinter(factory, this.HttpContext);
            //Comparation period-rulo style
            Period period = await factory.Periods.GetCurrentPeriod(systemPrinter.SystemPrinterID);
            if (period != null)
            {
                if (period.Style != styleData.Style)
                {
                    ViewBag.Error = true;
                    ViewBag.ErrorMessage = "The style entered does not correspond to the style of the period. First create the period for the style.";

                    return View("CreateOrUpdate", rulo);
                }
            }
            else
            {
                ViewBag.Error = true;
                ViewBag.ErrorMessage = "Period style not found!";

                return View("CreateOrUpdate", rulo);
            }

            if (rulo.RuloID == 0)
            {
                if (!(User.IsInRole("RuloAdd") || User.IsInRole("AdminFull") || User.IsInRole("RuloFull")))
                    return Unauthorized();

                //Period newPeriod = new Period();
                //newPeriod.StartDate = DateTime.Now;
                //newPeriod.Style = rulo.Style;
                //await factory.Periods.Add(newPeriod, int.Parse(User.Identity.Name));

                var currentPeriod = await factory.Periods.GetCurrentPeriod(systemPrinter.SystemPrinterID);

                if (rulo.SentAuthorizerID == 0) rulo.SentAuthorizerID = null;
                rulo.PeriodID = currentPeriod.PeriodID;
                await factory.Rulos.Add(rulo, int.Parse(User.Identity.Name));

                if (TempData["ruloMigrationId2"] != null)
                {
                    int ruloMigrationId2 = Convert.ToInt32(TempData["ruloMigrationId2"]);

                    RuloMigration ruloMigration = await factory.RuloMigrations.GetRuloMigrationFromRuloMigrationID(ruloMigrationId2);

                    if (ruloMigration != null)
                    {
                        ruloMigration.RuloID = rulo.RuloID;
                        await factory.RuloMigrations.Update(ruloMigration, int.Parse(User.Identity.Name));
                    }
                }

            }
            else
            {
                if (!(User.IsInRole("RuloUp") || User.IsInRole("AdminFull") || User.IsInRole("RuloFull")))
                    return Unauthorized();

                var foundRulo = await factory.Rulos.GetRuloFromRuloID(rulo.RuloID);
                //var period = await factory.Periods.GetPeriodFromPeriodID(rulo.PeriodID);

                foundRulo.Lote = rulo.Lote;
                foundRulo.Beam = rulo.Beam;
                foundRulo.BeamStop = rulo.BeamStop;
                foundRulo.Loom = rulo.Loom;
                foundRulo.IsToyota = rulo.IsToyota;
                foundRulo.Style = rulo.Style;
                ////Validate the style change to update the period
                //if (period != null && period.Style != foundRulo.Style)
                //{
                //    period.Style = foundRulo.Style;
                //    await factory.Periods.Update(period, int.Parse(User.Identity.Name));
                //}
                foundRulo.StyleName = rulo.StyleName;
                foundRulo.Width = rulo.Width;
                foundRulo.EntranceLength = rulo.EntranceLength;
                foundRulo.Shift = rulo.Shift;
                foundRulo.OriginID = rulo.OriginID;
                foundRulo.Observations = rulo.Observations;

                foundRulo.FolioNumber = rulo.FolioNumber;

                await factory.Rulos.Update(foundRulo, int.Parse(User.Identity.Name));

            }
            return Redirect("Index");
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = SystemStatics.DefaultScheme, Roles = "RuloShow, AdminFull, RuloFull")]
        public async Task<IActionResult> Details(int ruloId)
        {
            var foundVMRulo = await factory.Rulos.GetVMRuloFromRuloID(ruloId);
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
            var foundVMRulo = await factory.Rulos.GetVMRuloFromRuloID(ruloId);
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
        public async Task<IActionResult> GetPieces(int ruloId)
        {
            string errorMessage = string.Empty;

            if (!User.IsInRole("Rulo", AuthType.Update)) return Unauthorized();

            var rulo = await factory.Rulos.GetRuloFromRuloID(ruloId);

            if (rulo == null)
            {
                errorMessage = "Rulo not found";
                return new JsonResult(new { errorMessage = errorMessage, pieces = new List<SelectListItem>() });
            }

            IEnumerable<Piece> pieceList = await factory.Pieces.GetPiecesFromRuloID(ruloId);
            List<SelectListItem> pieces = new List<SelectListItem>();
            if (pieceList == null || pieceList.Count() == 0)
            {
                pieceList = new List<Piece>() { new Piece() { PieceID = 1, PieceNo = 1 } };
                pieces = WebUtilities.Create<Piece>(pieceList, "PieceID", "PieceNo");
            }
            pieces = WebUtilities.Create<Piece>(pieceList, "PieceID", "PieceNo");

            return new JsonResult(new { errorMessage = errorMessage, pieces = pieces });
        }

        [HttpPost]
        public async Task<IActionResult> Print(int ruloId, string floor, int pieceId)
        {
            if (!User.IsInRole("Rulo", AuthType.Update)) return Unauthorized();

            var rulo = await factory.Rulos.GetRuloFromRuloID(ruloId);

            if (rulo == null)
                return new JsonResult(new { errorMessage = "Rulo not found!" });

            var piece = await factory.Pieces.GetPieceFromPieceID(pieceId);

            if (piece == null)
                return new JsonResult(new { errorMessage = "Piece not found" });

            var systemPrinterList = await factory.SystemPrinters.GetSystemPrinterList();

            //var printersByFloor = systemPrinterList.Where(x => x.Floor.FloorName.Equals(floor, StringComparison.InvariantCultureIgnoreCase));

            var resultIP = WebUtilities.GetMachineIP(this.HttpContext);
            if (!resultIP.IsOk)
            {
                return new JsonResult(new { errorMessage = "Error getting the ip from the PC" });
            }

            var printersByFloor = systemPrinterList.Where(x => x.Floor.FloorName.Equals(floor, StringComparison.InvariantCultureIgnoreCase) && x.PCIP == resultIP.IP);

            //Implement later by machine
            var printer = printersByFloor.FirstOrDefault();
            if (printer == null)
            {
                return new JsonResult(new { errorMessage = $"Error, Printer settings not found in {floor.ToUpper()}!" });
            }

            if (string.IsNullOrWhiteSpace(printer.Location))
                return new JsonResult(new { errorMessage = "Error, IP or Printer Location not found!" });

            IPrinterGD print;
            if (printer.IsPrinterIP)
            {
                print = new ZEBRA_IP_PRINTER(printer.Location, 9100);
            }
            else
            {
                print = new ZEBRA_PRINTER(printer.Location);
            }

            if (print.CheckConnection())
            {
                print.Connect();

                string ZPLString = System.IO.File.ReadAllText(System.IO.Path.Combine(webHostEnvironment.WebRootPath, "..\\Reports\\RuloLabel.prn"));

                int offset = 672; //Before: 733, 512
                int lengthId = rulo.RuloID.ToString().Length;
                if (lengthId > 1)
                    offset += (lengthId - 1) * 8; //Before: 11

                Dictionary<string, string> replaceVaues = new Dictionary<string, string>();
                replaceVaues.Add("ReplaceRuloID", rulo.RuloID.ToString());
                replaceVaues.Add("ReplaceName", rulo.StyleName);
                replaceVaues.Add("ReplaceStyle", rulo.Style);
                replaceVaues.Add("ReplaceLote", rulo.Lote);
                replaceVaues.Add("ReplaceBeam", rulo.Beam.ToString());
                replaceVaues.Add("ReplaceStop", rulo.BeamStop);
                replaceVaues.Add("ReplaceLoom", rulo.Loom.ToString());
                replaceVaues.Add("ReplaceShift", rulo.Shift.ToString());

                if (piece.PieceNo != 1)
                    replaceVaues.Add("ReplaceMeters", piece.Meter.ToString("#,##0.00"));
                else
                    replaceVaues.Add("ReplaceMeters", rulo.ExitLength.ToString("#,##0.00"));

                replaceVaues.Add("ReplaceDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm"));
                replaceVaues.Add("ReplaceOffset", offset.ToString());

                VMOriginType origin = VMOriginType.ToList().Where(x => x.Value == rulo.OriginID).FirstOrDefault();
                if (origin != null)
                    replaceVaues.Add("ReplaceOrigin", origin.Text.ToString());
                else
                    replaceVaues.Add("ReplaceOrigin", VMOriginType.ToList().Where(x => x.Value == 1).FirstOrDefault().Text);

                replaceVaues.Add("ReplacePiece", piece.PieceNo.ToString());

                print.PrintFromZPL(ZPLString, replaceVaues);
            }
            else
            {
                return new JsonResult(new { errorMessage = "Can not connect to printer!" });
            }

            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> TestResultValidateInfo(int ruloId)
        {
            string errorMessage = string.Empty;

            //Validate rulo process
            var ruloProcesses = await factory.Rulos.GetVMRuloProcessesFromRuloID(ruloId);

            if (ruloProcesses == null || ruloProcesses.Count() == 0)
            {
                errorMessage += "Cannot be approved if there are no assigned processes. ";
            }

            //Validate pieces
            var pieces = await factory.Pieces.GetPiecesFromRuloID(ruloId);
            if (pieces == null || pieces.Count() == 0)
            {
                errorMessage += "Cannot be approved if there are no pieces created";
            }

            int performanceId = await factory.Rulos.GetPerformanceRuloID(ruloId);

            return new JsonResult(new { errorMessage = errorMessage, performanceId = performanceId });
        }

        [HttpPost]
        public async Task<IActionResult> TestResultFinish(int RuloId, int TestCategoryId, string TestCategoryText, string Description)
        {
            if (!User.IsInRole("TestResult", AuthType.Update)) return Unauthorized();

            var foundRulo = await factory.Rulos.GetRuloFromRuloID(RuloId);
            if (foundRulo == null) return NotFound();

            //Create or update test result
            TestResult testResult = null;
            if (foundRulo.TestResultID == null)
                testResult = new TestResult();
            else
                testResult = await factory.TestResults.GetTestResultFromTestResultID((int)foundRulo.TestResultID);

            Description = Description ?? string.Empty;

            if (TestCategoryText.Contains("√")) //OK √ or Fail √ - Before: 1, 3
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
                if (TestCategoryText.Contains("Waiting", StringComparison.InvariantCultureIgnoreCase)) //Waiting, Before: 5
                    isWaitingForTestResult = true;

                var intUser = int.Parse(User.Identity.Name);
                await factory.Rulos.SetTestResult(RuloId, testResult.TestResultID, isWaitingForTestResult, intUser, intUser);
            }
            else
            {
                if (TestCategoryText.Contains("Waiting", StringComparison.InvariantCultureIgnoreCase)) //Waiting, Before: 5
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
            var result = await factory.Rulos.GetRuloReportListFromFilters(ruloFilters);

            ExportToExcel export = new ExportToExcel();
            string reportName = "Finishing Report";
            string fileName = $"Finishing Report_{DateTime.Today.Year}_{DateTime.Today.Month.ToString().PadLeft(2, '0')}_{DateTime.Today.Day.ToString().PadLeft(2, '0')}.xlsx";

            var exclude = new List<string>() { "TestCategoryID" };
            var fileResult = await export.ExportWithDisplayName<VMRuloReport>("Global Denim S.A. de C.V.", "Finishing", reportName, fileName, result.ToList(), exclude);

            if (!fileResult.Item1) return NotFound();

            return fileResult.Item2;
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = SystemStatics.DefaultScheme, Roles = "RuloShow,RuloFull,AdminFull")]
        public async Task<IActionResult> ExportToExcelAllStock(VMRuloFilters ruloFilters)
        {
            ruloFilters.dtEnd = ruloFilters.dtEnd.AddDays(1).AddMilliseconds(-1);
            var result = await factory.Rulos.GetAllVMRuloReportList("spGetAllStock @p0", new[] { ruloFilters.dtEnd.ToString("yyyy-MM-ddTHH:mm:ss") });

            ExportToExcel export = new ExportToExcel();
            string reportName = "Finishing Report All";
            string fileName = $"Finishing Report All_{DateTime.Today.Year}_{DateTime.Today.Month.ToString().PadLeft(2, '0')}_{DateTime.Today.Day.ToString().PadLeft(2, '0')}.xlsx";

            var exclude = new List<string>() { "TestCategoryID" };
            var fileResult = await export.ExportWithDisplayName<VMRuloReport>("Global Denim S.A. de C.V.", "Finishing", reportName, fileName, result.ToList(), exclude);

            if (!fileResult.Item1) return NotFound();

            return fileResult.Item2;
        }

        [HttpGet, Authorize(AuthenticationSchemes = SystemStatics.DefaultScheme, Roles = "RuloShow,RuloFull,AdminFull")]
        public async Task GetInfoTitle()
        {
            var periods = await factory.Periods.GetCurrentPeriods();
            var systemPrinterList = await factory.SystemPrinters.GetSystemPrinterList();
            var machineList = await factory.Machines.GetMachineList();

            string style = string.Empty;
            string machineName = string.Empty;
            foreach (var period in periods.ToList())
            {
                if (period.SystemPrinterID != null)
                {
                    var systemPrinter = systemPrinterList.Where(x => x.SystemPrinterID == period.SystemPrinterID).FirstOrDefault();
                    if (systemPrinter != null)
                    {
                        var machine = machineList.Where(x => x.MachineID == systemPrinter.MachineID).FirstOrDefault();
                        if (machine != null)
                            machineName = machine.MachineName;
                    }
                }

                if (!string.IsNullOrWhiteSpace(machineName))
                {
                    if (string.IsNullOrWhiteSpace(style))
                        style += machineName.ToUpper() + ": " + period.Style;
                    else
                        style += ", " + machineName.ToUpper() + ": " + period.Style;
                }
                else
                {
                    if (string.IsNullOrWhiteSpace(style))
                        style += period.Style;
                    else
                        style += ", " + period.Style;
                }
            }

            if (string.IsNullOrWhiteSpace(style))
                style = "There are no rulo in process";

            string title = $"Periods: {style}";
            ViewBag.CurrentPeriod = title;

        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = SystemStatics.DefaultScheme, Roles = "PieceShow, PieceUp, PieceDel, PieceFull, AdminFull")]
        public async Task<IActionResult> GetPieceIndex(int ruloId)
        {
            if (!User.IsInRole("Piece", AuthType.Show) || !User.IsInRole("Piece", AuthType.Add) || !User.IsInRole("Piece", AuthType.Delete))
                return Unauthorized();


            //Validate rulo exist
            var foundRulo = await factory.Rulos.GetRuloFromRuloID(ruloId);
            if (foundRulo == null) return NotFound();

            var pieces = await factory.Pieces.GetPiecesFromRuloID(ruloId);
            ViewBag.RuloId = ruloId;

            return PartialView(pieces);
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = SystemStatics.DefaultScheme, Roles = "PieceUp, PieceFull, AdminFull")]
        public async Task<IActionResult> GetCreateOrUpdatePiece(int ruloId, int pieceId)
        {
            ViewBag.Error = false;
            ViewBag.ErrorMessage = string.Empty;

            if (ruloId <= 0) return NotFound();

            var rulo = await factory.Rulos.GetRuloFromRuloID(ruloId);
            if (rulo == null) return NotFound();

            Piece piece = null;
            if (pieceId == 0)
            {
                piece = new Piece();
                piece.RuloID = ruloId;
            }
            else
            {
                piece = await factory.Pieces.GetPieceFromPieceID(pieceId);
                if (piece == null) return NotFound();
            }

            return PartialView(piece);
        }

        [HttpGet]
        public async Task<IActionResult> SavePiece(Piece piece)
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

                return PartialView("GetCreateOrUpdatePiece", piece);
            }

            var validatePiece = await factory.Pieces.GetPiece(piece.RuloID, piece.PieceNo);
            if (validatePiece != null && validatePiece.PieceID != piece.PieceID)
            {
                ViewBag.Error = true;
                ViewBag.ErrorMessage = "Piece already exist";

                return PartialView("GetCreateOrUpdatePiece", piece);
            }

            if (piece.PieceID == 0)
            {
                if (!User.IsInRole("Piece", AuthType.Add)) return Unauthorized();

                await factory.Pieces.Add(piece, int.Parse(User.Identity.Name));

                //Update rulo pieces
                var rulo = await factory.Rulos.GetRuloFromRuloID(piece.RuloID);
                rulo.PieceCount += 1;
                await factory.Rulos.Update(rulo, int.Parse(User.Identity.Name));
            }
            else
            {
                if (!User.IsInRole("Piece", AuthType.Update)) return Unauthorized();

                var foundPiece = await factory.Pieces.GetPieceFromPieceID(piece.PieceID);

                foundPiece.PieceNo = piece.PieceNo;
                foundPiece.Meter = piece.Meter;

                await factory.Pieces.Update(foundPiece, int.Parse(User.Identity.Name));
            }

            return RedirectToAction("GetPieceIndex", new { piece.RuloID });
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = SystemStatics.DefaultScheme, Roles = "PieceUp, PieceFull, AdminFull")]
        public async Task<IActionResult> GetDetailsPiece(int pieceId)
        {
            Piece foundPiece = await factory.Pieces.GetPieceFromPieceID(pieceId);

            if (foundPiece == null) return NotFound();

            return PartialView(foundPiece);
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = SystemStatics.DefaultScheme, Roles = "PieceUp, PieceFull, AdminFull")]
        public async Task<IActionResult> GetDeletePiece(int pieceId)
        {
            var foundPiece = await factory.Pieces.GetPieceFromPieceID(pieceId);

            if (foundPiece == null) return NotFound();

            return PartialView(foundPiece);
        }

        [HttpGet]
        public async Task<ActionResult> DeleteConfirmedPiece(int pieceId)
        {
            if (!User.IsInRole("Piece", AuthType.Delete)) return Unauthorized();

            var foundPiece = await factory.Pieces.GetPieceFromPieceID(pieceId);

            await factory.Pieces.Delete(foundPiece, int.Parse(User.Identity.Name));

            //Update rulo pieces
            var rulo = await factory.Rulos.GetRuloFromRuloID(foundPiece.RuloID);
            rulo.PieceCount -= 1;
            await factory.Rulos.Update(rulo, int.Parse(User.Identity.Name));

            return RedirectToAction("GetPieceIndex", new { foundPiece.RuloID });
        }

        [HttpPost, Authorize(AuthenticationSchemes = SystemStatics.DefaultScheme, Roles = "AuthorizeFull,AdminFull")]
        public async Task<IActionResult> AuthorizeRulo(int ruloId)
        {
            var rulo = await factory.Rulos.GetRuloFromRuloID(ruloId);
            if (rulo == null) return NotFound();

            rulo.SentAuthorizerID = int.Parse(User.Identity.Name);

            await factory.Rulos.Update(rulo, int.Parse(User.Identity.Name));

            return Ok();
        }

        [HttpGet]
        public async Task<ActionResult> GetFolioNumber(int ruloId)
        {
            ViewBag.Error = false;
            ViewBag.ErrorMessage = "";

            if (!User.IsInRole("Rulo", AuthType.Update)) return Unauthorized();

            await SetViewBagsForCreateOrEdit();

            var foundRulo = await factory.Rulos.GetRuloFromRuloID(ruloId);

            if (foundRulo == null) return NotFound();

            return PartialView(foundRulo);
        }

        [HttpPost]
        public async Task<IActionResult> SaveFolioNumber(int ruloId, int shift, int folioNumber)
        {
            ViewBag.Error = false;
            ViewBag.ErrorMessage = "";
            string errorMessage = string.Empty;

            if (!User.IsInRole("Rulo", AuthType.Update)) return Unauthorized();

            var ruloTemp = new Rulo();
            ruloTemp.RuloID = ruloId;
            ruloTemp.Shift = shift;
            ruloTemp.FolioNumber = folioNumber;

            if (ruloId <= 0)
            {
                ViewBag.Error = true;
                ViewBag.ErrorMessage = "Rulo not found!";
                return PartialView("GetFolioNumber", ruloTemp);
            }

            if (folioNumber <= 0)
            {
                ViewBag.Error = true;
                errorMessage += "Folio Number no valid!" + "<br>";
            }
            if (shift <= 0)
            {
                ViewBag.Error = true;
                errorMessage += "Shift no valid!" + "<br>";
            }

            if ((bool)ViewBag.Error)
            {
                ViewBag.ErrorMessage = errorMessage;
                return PartialView("GetFolioNumber", ruloTemp);
            }

            var foundRulo = await factory.Rulos.GetRuloFromFolio(folioNumber);
            if (foundRulo != null)
            {
                ViewBag.Error = true;
                ViewBag.ErrorMessage = "Folio number already exist!";
                return PartialView("GetFolioNumber", ruloTemp);
            }

            foundRulo = await factory.Rulos.GetRuloFromRuloID(ruloId);

            //Validate rulo process
            var ruloProcesses = await factory.Rulos.GetVMRuloProcessesFromRuloID(ruloId);

            if (ruloProcesses == null || ruloProcesses.Count() == 0)
            {
                ViewBag.Error = true;
                ViewBag.ErrorMessage = "You cannot assign folio and terminate if there are no assigned processes.";
                return PartialView("GetFolioNumber", foundRulo);
            }

            //Validate pieces
            var pieces = await factory.Pieces.GetPiecesFromRuloID(ruloId);
            if (pieces == null || pieces.Count() == 0)
            {
                ViewBag.Error = true;
                ViewBag.ErrorMessage = "You cannot assign folio and terminate if there are no created pieces";
                return PartialView("GetFolioNumber", foundRulo);
            }

            foundRulo.SentDate = DateTime.Now;

            //Period foundPeriod = await factory.Periods.GetPeriodFromPeriodID(foundRulo.PeriodID);
            //foundPeriod.FinishDate = DateTime.Now;
            //foundPeriod.ValidityLastPeriod = foundPeriod.FinishDate.Value.Subtract(foundPeriod.StartDate);

            //await factory.Periods.Update(foundPeriod, int.Parse(User.Identity.Name));

            foundRulo.FolioNumber = folioNumber;

            foundRulo.SenderID = int.Parse(User.Identity.Name);

            await factory.Rulos.Update(foundRulo, int.Parse(User.Identity.Name));

            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetTestResultObservations(int ruloId)
        {
            if (!User.IsInRole("TestResult", AuthType.Show) || !User.IsInRole("Rulo", AuthType.Show)) return Unauthorized();

            string details = string.Empty;
            var rulo = await factory.Rulos.GetRuloFromRuloID(ruloId);

            if (rulo != null)
            {
                var testResult = await factory.TestResults.GetTestResultFromTestResultID((int)rulo.TestResultID);

                details = !string.IsNullOrWhiteSpace(testResult.Details) ? testResult.Details : "No observations";
            }

            return new JsonResult(new { details = details });
        }

        [HttpGet]
        public async Task<IActionResult> GetReportStock()
        {
            VMRuloFilters ruloFilters = new VMRuloFilters();
            ruloFilters.dtEnd = ruloFilters.dtEnd.AddDays(1).AddMilliseconds(-1);
            var result = await factory.Rulos.GetAllVMRuloReportList("spGetAllStock @p0", new[] { ruloFilters.dtEnd.ToString("yyyy-MM-ddTHH:mm:ss") });

            ExportToExcel export = new ExportToExcel();
            string reportName = "Finishing Report Stock";
            string fileName = $"Finishing Report Stock_{DateTime.Today.Year}_{DateTime.Today.Month.ToString().PadLeft(2, '0')}_{DateTime.Today.Day.ToString().PadLeft(2, '0')}.xlsx";

            var exclude = new List<string>() { "TestCategoryID" };
            var fileResult = await export.ExportWithDisplayName<VMRuloReport>("Global Denim S.A. de C.V.", "Finishing", reportName, fileName, result.ToList(), exclude);

            if (!fileResult.Item1) return NotFound();

            return fileResult.Item2;
        }

        [HttpGet]
        public async Task<IActionResult> GetPerformanceTestResult(int ruloId)
        {
            if (!User.IsInRole("Rulo", AuthType.Show)) return Unauthorized();

            //tblMaster.ID = 2352; //TODO: For test
            var performanceTestResultList = await factory.Rulos.GetPerformanceTestResult(ruloId);

            if (performanceTestResultList == null) return NotFound();

            return PartialView(performanceTestResultList);
        }

    }
}
