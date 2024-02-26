using DocumentFormat.OpenXml.Wordprocessing;
using GD.FinishingSystem.Bussines;
using GD.FinishingSystem.DAL.EFdbPerformanceStandards;
using GD.FinishingSystem.Entities;
using GD.FinishingSystem.Entities.ViewModels;
using GD.FinishingSystem.WEB.Classes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
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
        private AppSettings appSettings;
        FinishingSystemFactory factory;

        IndexModelRulo IndexModelRulo = null;

        public RuloController(IWebHostEnvironment webHostEnvironment, IConfiguration configuration)
        {
            this.webHostEnvironment = webHostEnvironment;
            factory = new FinishingSystemFactory();
            this.appSettings = configuration.GetSection("AppSettings").Get<AppSettings>();

            IndexModelRulo = new IndexModelRulo(factory, this.appSettings);
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = SystemStatics.DefaultScheme, Roles = "RuloShow,RuloFull,AdminFull")]
        public async Task<IActionResult> Index(int positionRuloId = 0, string currentFilter = null, int? pageIndex = null)
        {
            VMRuloFilters ruloFilters = null;
            if (currentFilter == null)
            {
                ruloFilters = new VMRuloFilters();
                ruloFilters.dtBegin = DateTime.Today.AddDays(-15);
                ruloFilters.dtEnd = DateTime.Today.RealDateEndDate();
            }
            else
            {
                ruloFilters = System.Text.Json.JsonSerializer.Deserialize<VMRuloFilters>(currentFilter);
            }

            await SetViewBagsForDates(ruloFilters, positionRuloId);

            await IndexModelRulo.OnGetAsync("", pageIndex, ruloFilters);

            return View(IndexModelRulo);
        }


        [HttpPost]
        [Authorize(AuthenticationSchemes = SystemStatics.DefaultScheme, Roles = "RuloShow,RuloFull,AdminFull")]
        public async Task<IActionResult> Index(VMRuloFilters ruloFilters)
        {
            ruloFilters.dtEnd = ruloFilters.dtEnd.RealDateEndDate();
            await SetViewBagsForDates(ruloFilters);

            await IndexModelRulo.OnGetAsync("", 1, ruloFilters);

            return View(IndexModelRulo);
        }

        private async Task SetViewBagsForDates(VMRuloFilters ruloFilters, int positionRuloId = 0)
        {
            ViewBag.dtBegin = ruloFilters.dtBegin.ToString("yyyy-MM-ddTHH:mm:ss");
            ViewBag.dtEnd = ruloFilters.dtEnd.ToString("yyyy-MM-ddTHH:mm:ss");

            if (ruloFilters.IsAccountingDate)
            {
                ruloFilters.dtBegin = ruloFilters.dtBegin.AccountStartDate();
                ruloFilters.dtEnd = ruloFilters.dtEnd.AccountEndDate();
            }

            ViewBag.isAccountingDate = ruloFilters.IsAccountingDate;
            ViewBag.realBeginDate = DateTime.Today.AddDays(-15).RealDateStartDate().ToString("yyyy-MM-ddTHH:mm:ss");
            ViewBag.realDate = DateTime.Today.RealDateEndDate().ToString("yyyy-MM-ddTHH:mm:ss");
            ViewBag.accountingBeginDate = DateTime.Now.AddDays(-15).AccountStartDate().ToString("yyyy-MM-ddTHH:mm:ss");
            ViewBag.accountingDate = DateTime.Now.AccountEndDate().ToString("yyyy-MM-ddTHH:mm:ss");

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

            ViewBag.withBatches = ruloFilters.withBatches;
            ViewBag.numRuloID = ruloFilters.numRuloID;

            //Style list
            var styleDataList = await factory.Rulos.GetRuloStyleStringForProductionLoteList();
            ViewBag.StyleList = styleDataList.ToList();
            await GetInfoTitle();

            ViewBag.PositionRuloId = positionRuloId;

        }


        public async Task<IActionResult> ValidateCreateRulo()
        {
            if (!User.IsInRole("Rulo", AuthType.Add)) return Unauthorized();

            string errorMessage = string.Empty;

            var systemPrinter = await WebUtilities.GetSystemPrinter(factory, this.HttpContext);

            if (systemPrinter == null)
            {
                errorMessage = "This PC is not register for capture. Add this PC in the system printers window.";
            }

            return new JsonResult(new { errorMessage = errorMessage });
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = SystemStatics.DefaultScheme, Roles = "RuloAdd,RuloFull,AdminFull")]
        public async Task<IActionResult> Create()
        {
            ViewBag.Error = false;
            ViewBag.ErrorMessage = "";

            await SetViewBagsForCreateOrEdit(false);

            var systemPrinter = await WebUtilities.GetSystemPrinter(factory, this.HttpContext);

            Rulo newRulo = new Rulo();
            var currentPeriod = await factory.Periods.GetCurrentPeriod(systemPrinter.SystemPrinterID);
            if (currentPeriod == null)
            {
                ViewBag.Error = true;
                ViewBag.ErrorMessage = "Cannot create a new roll. You must open a period.";
                return View("CreateOrUpdate", newRulo);
            }

            return View("CreateOrUpdate", newRulo);
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = SystemStatics.DefaultScheme, Roles = "RuloAdd,RuloFull,AdminFull")]
        public async Task<IActionResult> CreateFromRuloMigration(Rulo newRulo)
        {
            ViewBag.Error = false;
            ViewBag.ErrorMessage = "";

            //Aquí no se valida porque RuloID es igual a 0 y por lo tanto siempre da falso
            await SetViewBagsForCreateOrEdit(true);

            if (TempData["rawsIDs1"] != null)
            {
                string rawIDs1 = TempData["rawsIDs1"].ToString();
                TempData["rawsIDs2"] = rawIDs1;
            }

            var systemPrinter = await WebUtilities.GetSystemPrinter(factory, this.HttpContext);
            var currentPeriod = await factory.Periods.GetCurrentPeriod(systemPrinter.SystemPrinterID);

            if (currentPeriod == null)
            {
                ViewBag.Error = true;
                ViewBag.ErrorMessage = "Cannot create a new roll. You must open a period.";
                if (newRulo.RuloID == 0) newRulo.RuloID = -1;
                View("CreateOrUpdate", newRulo);
            }

            return View("CreateOrUpdate", newRulo);
        }

        [HttpPost]
        public async Task<IActionResult> ValidateCreateFromRuloMigration(Rulo rulo)
        {
            if (!User.IsInRole("Rulo", AuthType.Add)) return Unauthorized();

            string errorMessage = string.Empty;

            if (!ModelState.IsValid)
            {
                foreach (var modelStateKey in ModelState.Keys)
                {
                    var modelStateVal = ModelState[modelStateKey];
                    foreach (var error in modelStateVal.Errors)
                    {
                        var key = modelStateKey;
                        errorMessage += error.ErrorMessage + "\r\n";
                    }
                }

            }

            return new JsonResult(new { errorMessage = errorMessage });
        }

        private async Task SetViewBagsForCreateOrEdit(bool isFromRuloMigration)
        {
            //var list = WebUtilities.Create<OriginType>();
            var mainOriginList = await factory.OriginCategories.GetOriginCategoryList();
            IEnumerable<OriginCategory> originList = mainOriginList;
            //If rulo is not from Raw it's not show 1-PP00 and 7-DES0
            if (!isFromRuloMigration)
                originList = mainOriginList.Where(x => x.OriginCategoryID != 1 && x.OriginCategoryID != 7);
            ViewBag.MainOriginList = WebUtilities.Create<OriginCategory>(mainOriginList, "OriginCategoryID", "Name", false);
            ViewBag.OriginList = WebUtilities.Create<OriginCategory>(originList, "OriginCategoryID", "Name", false);

            var sentAuthorizerList = await factory.Users.GetAll();
            var sentAuthorizerListItem = WebUtilities.Create<User>(sentAuthorizerList, "UserID", "Name", true);
            ViewBag.SentAuthorizer = sentAuthorizerListItem;

            ViewBag.FromRuloMigration = !isFromRuloMigration;
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
                if (styleData != null)
                {
                    if (period.Style != styleData.Style)
                    {
                        errorMessage = "The style entered does not correspond to the style of the period. First create the period for the style.";
                    }
                }
            }
            else
            {
                errorMessage = "Period style not found!";
            }

            if (styleData == null)
                errorMessage = "The lote number is not registered in the Programming!";
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

            Rulo existRulo = await factory.Rulos.GetRuloFromRuloID(RuloID);
            //Validate if rulo is from Rulo Migration
            var isFromRuloMigration = await factory.RuloMigrations.ExistRuloInRuloMigration(RuloID);
            await SetViewBagsForCreateOrEdit(isFromRuloMigration);

            if (existRulo == null)
                return RedirectToAction("Error", "Home");
            return View("CreateOrUpdate", existRulo);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Save(Rulo rulo)
        {
            ViewBag.Error = true;
            await SetViewBagsForCreateOrEdit(false);

            if (!ModelState.IsValid)
            {
                string errorMessage = string.Empty;
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
                if (rulo.RuloID == 0) rulo.RuloID = -1; //This is a trick since when happen an error the view is load like Reprocess and this avoid that behavior
                return View("CreateOrUpdate", rulo);
            }

            //Update the style and the name of the style since they are locked in the form, the value is not passed.
            VMStyleData styleData = null;
            //Validation if it is a test, the style is not updated in the DB
            if (!rulo.IsTestStyle)
            {
                styleData = await factory.Rulos.GetRuloStyle(rulo.Lote);
                if (styleData == null)
                {
                    ViewBag.Error = true;
                    ViewBag.ErrorMessage = "The lote number is not registered in the Programming!";

                    if (rulo.RuloID == 0) rulo.RuloID = -1;
                    return View("CreateOrUpdate", rulo);
                }

                rulo.Style = styleData.Style;
                rulo.StyleName = styleData.StyleName;
            }

            var systemPrinter = await WebUtilities.GetSystemPrinter(factory, this.HttpContext);

            if (systemPrinter == null)
            {
                ViewBag.Error = true;
                ViewBag.ErrorMessage = "This PC is not assigned to any finishing machine. Use an assigned machine to update data.";

                if (rulo.RuloID == 0) rulo.RuloID = -1;
                return View("CreateOrUpdate", rulo);
            }

            //Comparation perido-rulo style
            Period period = await factory.Periods.GetCurrentPeriod(systemPrinter.SystemPrinterID);
            if (period != null)
            {
                if (!rulo.IsTestStyle)
                {
                    if (period.Style != styleData.Style) //TODO: Las modificaciones se realizan en el momento, si se realizan cuando ya no está el mismo estilo marcará este error
                    {
                        ViewBag.Error = true;
                        ViewBag.ErrorMessage = "The style entered does not correspond to the style of the period. First create the period for the style.";

                        if (rulo.RuloID == 0) rulo.RuloID = -1;
                        return View("CreateOrUpdate", rulo);
                    }
                }
            }
            else
            {
                ViewBag.Error = true;
                ViewBag.ErrorMessage = "Period style not found!";

                if (rulo.RuloID == 0) rulo.RuloID = -1;
                return View("CreateOrUpdate", rulo);
            }

            if (rulo.RuloID == 0 || rulo.RuloID == -1)
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
                if (TempData["rawsIDs2"] != null && (rulo.RuloID == 0 || rulo.RuloID == -1)) //Si hubo algún error se pierde los metros de tejido al actualizar los datos, aquí se actulizan para asegurar que el dato se guarde correctamente.
                {
                    string rawsIDs = TempData["rawsIDs2"].ToString();
                    List<int> rawsIDsInt = rawsIDs.Split(',').ToList().Select(int.Parse).ToList();
                    var foundRuloMigrations = await factory.RuloMigrations.GetRuloMigrationFromIDs(rawsIDsInt);
                    var totalMeters = foundRuloMigrations.Sum(x => x.Meters);
                    rulo.WeavingLength = totalMeters;
                }

                await factory.Rulos.Add(rulo, int.Parse(User.Identity.Name));

                if (TempData["rawsIDs2"] != null)
                {
                    string rawsIDs = TempData["rawsIDs2"].ToString();
                    List<int> rawsIDsInt = rawsIDs.Split(',').ToList().Select(int.Parse).ToList();
                    var result = await factory.RuloMigrations.UpdateRuloMigrationsFromRuloMigrationIDs(rawsIDsInt, rulo.RuloID, int.Parse(User.Identity.Name));

                    if (!result)
                    {
                        ViewBag.Error = true;
                        ViewBag.ErrorMessage = $"An error occurred while trying to update the system. You must inform to system departament and show rulo: {rulo.RuloID}, and ruloMigrations: {rawsIDs}";
                        //If it happen an error here the information of the view will be load like reprocess because rulo is not 0 or -1. In this case is necesary change value in BD.
                        return View("CreateOrUpdate", rulo);
                    }
                    else
                        TempData["rawsIDs2"] = null;
                }

            }
            else
            {
                if (!(User.IsInRole("RuloUp") || User.IsInRole("AdminFull") || User.IsInRole("RuloFull")))
                    return Unauthorized();

                //Validate if rulo is from Rulo Migration
                var isFromRuloMigration = await factory.RuloMigrations.ExistRuloInRuloMigration(rulo.RuloID);
                await SetViewBagsForCreateOrEdit(isFromRuloMigration);

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
                foundRulo.WeavingLength = rulo.WeavingLength;
                foundRulo.EntranceLength = rulo.EntranceLength;
                foundRulo.Shift = rulo.Shift;
                foundRulo.MainOriginID = rulo.MainOriginID;
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
            ViewBag.Error = false;

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
            ViewBag.Error = false;

            if (!(User.IsInRole("RuloDel") || User.IsInRole("AdminFull") || User.IsInRole("RuloFull")))
                return Unauthorized();

            //TODO: Validar si existe un RuloMigrations relacionado, mostrar un error de que no se puede elimimnar
            if (await factory.RuloMigrations.Exists(ruloId))
            {
                ViewBag.Error = true;
                ViewBag.ErrorMessage = "El rulo no se puede eliminar porque ya se encuenta relacionado con un registro de Crudo";
                var foundVMRulo = await factory.Rulos.GetVMRuloFromRuloID(ruloId);
                return View("Delete", foundVMRulo);
            }

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

            string machine = await factory.Rulos.GetMachineByRuloId(ruloId);

            if (print.CheckConnection())
            {
                print.Connect();

                string ZPLString = System.IO.File.ReadAllText(System.IO.Path.Combine(webHostEnvironment.WebRootPath, "..\\Reports\\RuloLabel.prn"));

                int offset = 672; //Before: 733, 512
                int lengthId = rulo.RuloID.ToString().Length;
                if (lengthId > 1)
                    offset += (lengthId - 1) * 8; //Before: 11

                Dictionary<string, string> replaceVaues = new Dictionary<string, string>();
                replaceVaues.Add("^XA", "^XA^CI28"); //This is for working latin characters
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

                //VMOriginType origin = VMOriginType.ToList().Where(x => x.Value == rulo.OriginID).FirstOrDefault();
                var originList = await factory.OriginCategories.GetOriginCategoryList();
                var origin = originList.ToList().Where(x => x.OriginCategoryID == rulo.OriginID).FirstOrDefault();
                if (origin != null)
                    replaceVaues.Add("ReplaceOrigin", origin.Name);
                else
                    replaceVaues.Add("ReplaceOrigin", originList.ToList().Where(x => x.OriginCategoryID == 1).FirstOrDefault().Name);

                replaceVaues.Add("ReplacePiece", piece.PieceNo.ToString());
                replaceVaues.Add("ReplaceMachine", machine);

                print.PrintFromZPL(ZPLString, replaceVaues);
            }
            else
            {
                return new JsonResult(new { errorMessage = "Cannot connect to printer!" });
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
        public async Task<IActionResult> TestResultFinish(int RuloId, int TestCategoryId, string TestCategoryText, string Description, int PerformanceId)
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
                testResult.PerformanceID = PerformanceId;
            }
            else
            {
                testResult.Details = Description;
                testResult.CanContinue = false;
                testResult.TestCategoryID = TestCategoryId;
                testResult.PerformanceID = PerformanceId;
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
        public async Task<IActionResult> ExportToWarehouseStock(VMRuloFilters ruloFilters)
        {
            try
            {
                var result = await factory.Rulos.GetWarehouseStock(ruloFilters);

                ExportToExcel export = new ExportToExcel();
                string reportName = "Finishing Report Warehouses Stock";
                string fileName = $"Finishing Report Warehouses Stock_{DateTime.Today.Year}_{DateTime.Today.Month.ToString().PadLeft(2, '0')}_{DateTime.Today.Day.ToString().PadLeft(2, '0')}.xlsx";
                string setDateRange = $"From {ruloFilters.dtBegin.ToString("dd/MM/yyyy HH:mm")} to {ruloFilters.dtEnd.ToString("dd/MM/yyyy HH:mm")}";

                var fileResult = await export.ExportWithDisplayName<WarehouseStock>("Global Denim S.A. de C.V.", "Finishing", reportName, fileName, result.ToList(), setDateRange: setDateRange);

                if (!fileResult.Item1) return NotFound();

                //return fileResult.Item2;
                return File(fileResult.Item2.FileStream, fileResult.Item2.ContentType, fileName = fileResult.Item2.FileDownloadName);
            }
            catch (Exception ex)
            {
            }
            return null;
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = SystemStatics.DefaultScheme, Roles = "RuloShow,RuloFull,AdminFull")]
        public async Task<IActionResult> ExportToExcel(VMRuloFilters ruloFilters)
        {
            try
            {
                var result = await factory.Rulos.GetRuloReportListFromFilters(ruloFilters);

                ExportToExcel export = new ExportToExcel();
                string reportName = "Finishing Report";
                string fileName = $"Finishing Report_{DateTime.Today.Year}_{DateTime.Today.Month.ToString().PadLeft(2, '0')}_{DateTime.Today.Day.ToString().PadLeft(2, '0')}.xlsx";

                List<Tuple<int, string>> colorList = new List<Tuple<int, string>>();
                result.ToList().ForEach(x =>
                {
                    if (!string.IsNullOrWhiteSpace(x.BatchNumbers))
                        colorList.Add(new Tuple<int, string>(x.RuloID, "ffc300"));
                    else if (x.CanContinue == "Yes")
                        colorList.Add(new Tuple<int, string>(x.RuloID, "66ff66"));
                    else if (x.TestCategoryCode.Contains(" X", StringComparison.InvariantCultureIgnoreCase)) //Ok X or Fail X
                        colorList.Add(new Tuple<int, string>(x.RuloID, "ff6666"));

                });

                var exclude = new List<string>() { "IsToyotaValue", "TestCategoryID", "IsWaitingAnswerFromTestValue", "CanContinueValue", "OriginID" };
                var fileResult = await export.ExportWithDisplayName<VMRulo>("Global Denim S.A. de C.V.", "Finishing", reportName, fileName, result.ToList(), exclude, colorList, "RuloID");

                if (!fileResult.Item1) return NotFound();

                //return fileResult.Item2;
                return File(fileResult.Item2.FileStream, fileResult.Item2.ContentType, fileName = fileResult.Item2.FileDownloadName);
            }
            catch (Exception ex)
            {
            }
            return null;
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = SystemStatics.DefaultScheme, Roles = "RuloShow,RuloFull,AdminFull")]
        public async Task<IActionResult> ExportToExcelAllStock(VMRuloFilters ruloFilters)
        {
            //ruloFilters.dtEnd = ruloFilters.dtEnd.AddDays(1).AddMilliseconds(-1);
            var result = await factory.Rulos.GetAllVMRuloReportList("spGetAllStock @p0", new[] { ruloFilters.dtEnd.ToString("yyyy-MM-ddTHH:mm:ss") });

            ExportToExcel export = new ExportToExcel();
            string reportName = "Finishing Report All";
            string fileName = $"Finishing Report All_{DateTime.Today.Year}_{DateTime.Today.Month.ToString().PadLeft(2, '0')}_{DateTime.Today.Day.ToString().PadLeft(2, '0')}.xlsx";

            var exclude = new List<string>() { "TestCategoryID" };
            var fileResult = await export.ExportWithDisplayName<VMRulo>("Global Denim S.A. de C.V.", "Finishing", reportName, fileName, result.ToList(), exclude);

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

                //TODO: Comment because the machine is shown differently when because the user captures in another PC not assigned to the machine
                //if (!string.IsNullOrWhiteSpace(machineName))
                //{
                //    if (string.IsNullOrWhiteSpace(style))
                //        style += machineName.ToUpper() + ": " + period.Style;
                //    else
                //        style += ", " + machineName.ToUpper() + ": " + period.Style;
                //}
                //else
                //{
                if (string.IsNullOrWhiteSpace(style))
                    style += period.Style;
                else
                    style += ", " + period.Style;
                //}
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

            //Validate if rulo is from Rulo Migration
            var isFromRuloMigration = await factory.RuloMigrations.ExistRuloInRuloMigration(ruloId);
            await SetViewBagsForCreateOrEdit(isFromRuloMigration);

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

            //var foundRulo = await factory.Rulos.GetRuloFromFolio(folioNumber);
            //if (foundRulo != null)
            //{
            //    ViewBag.Error = true;
            //    ViewBag.ErrorMessage = "Folio number already exist!";
            //    return PartialView("GetFolioNumber", ruloTemp);
            //}

            var foundRulo = await factory.Rulos.GetRuloFromRuloID(ruloId);

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

        //[HttpGet]
        //public async Task<IActionResult> GetReportStock()
        //{
        //    VMRuloFilters ruloFilters = new VMRuloFilters();
        //    //ruloFilters.dtEnd = ruloFilters.dtEnd.AddDays(1).AddMilliseconds(-1);
        //    var result = await factory.Rulos.GetAllVMRuloReportList("spGetAllStock @p0", new[] { ruloFilters.dtEnd.ToString("yyyy-MM-ddTHH:mm:ss") });

        //    ExportToExcel export = new ExportToExcel();
        //    string reportName = "Finishing Report Stock";
        //    string fileName = $"Finishing Report Stock_{DateTime.Today.Year}_{DateTime.Today.Month.ToString().PadLeft(2, '0')}_{DateTime.Today.Day.ToString().PadLeft(2, '0')}.xlsx";

        //    var exclude = new List<string>() { "TestCategoryID" };
        //    var fileResult = await export.ExportWithDisplayName<VMRulo>("Global Denim S.A. de C.V.", "Finishing", reportName, fileName, result.ToList(), exclude);

        //    if (!fileResult.Item1) return NotFound();

        //    return fileResult.Item2;
        //}

        [HttpGet]
        public async Task<IActionResult> GetPerformanceTestResult(int ruloId)
        {
            if (!User.IsInRole("Rulo", AuthType.Show)) return Unauthorized();

            //Rulo ID = 50278, Bem: 327 //TODO: For test
            IEnumerable<TblCustomPerformanceForFinishing> performanceTestResultList = new List<TblCustomPerformanceForFinishing>();

            var ruloTemp = await factory.Rulos.GetRuloFromRuloID(ruloId);
            if (ruloTemp.TestResultID != null)
            {
                var testResultTemp = await factory.TestResults.GetTestResultFromTestResultID((int)ruloTemp.TestResultID);
                if (testResultTemp != null)
                {
                    if (testResultTemp.PerformanceID != null)
                    {
                        performanceTestResultList = await factory.Rulos.GetPerformanceTestResultById((int)testResultTemp.PerformanceID);
                    }

                }
            }

            if (performanceTestResultList == null) return NotFound();

            return PartialView(performanceTestResultList);
        }

        [HttpGet]
        public async Task<IActionResult> GetTestResultWithPerformance(int ruloId, int performanceId)
        {
            if (!User.IsInRole("Rulo", AuthType.Show)) return Unauthorized();

            //tblMaster.ID = 2352; //TODO: For test
            IEnumerable<TblCustomPerformanceForFinishing> performanceTestResultList = null;
            if (performanceId == 0)
                performanceTestResultList = await factory.Rulos.GetPerformanceTestResultByRuloId(ruloId);
            else
                performanceTestResultList = await factory.Rulos.GetPerformanceTestResultById(performanceId);

            if (performanceTestResultList == null) return NotFound();

            var testCategoryList = await factory.TestCategories.GetTestCategoryList();
            var testCategorylist2 = WebUtilities.Create<TestCategory>(testCategoryList, "TestCategoryID", "TestCode", true, "All");
            ViewBag.TestCategorytList = testCategorylist2;
            ViewBag.RuloId = ruloId;

            return PartialView(performanceTestResultList);
        }

        [HttpPost]
        public async Task<IActionResult> ExportPerformanceTestResult(int ruloIdPerformance)
        {
            if (!User.IsInRole("Rulo", AuthType.Show)) return Unauthorized();

            IEnumerable<TblCustomPerformanceForFinishing> performanceTestResultList = new List<TblCustomPerformanceForFinishing>();

            var ruloTemp = await factory.Rulos.GetRuloFromRuloID(ruloIdPerformance);

            if (ruloTemp == null)
                return NotFound();

            if (ruloTemp.TestResultID != null)
            {
                var testResultTemp = await factory.TestResults.GetTestResultFromTestResultID((int)ruloTemp.TestResultID);
                if (testResultTemp != null)
                {
                    if (testResultTemp.PerformanceID != null)
                        performanceTestResultList = await factory.Rulos.GetPerformanceTestResultById((int)testResultTemp.PerformanceID);
                }
            }

            if (performanceTestResultList == null || performanceTestResultList.Count() == 0)
                return NotFound();

            ExportToExcel export = new ExportToExcel();
            string reportName = "Performance Test Result Report";
            string fileName = $"Performance Test Result Report_{DateTime.Today.Year}_{DateTime.Today.Month.ToString().PadLeft(2, '0')}_{DateTime.Today.Day.ToString().PadLeft(2, '0')}.xlsx";

            var exclude = new List<string>() { "TestCategoryID" };
            var fileResult = await export.ExportWithDisplayName<TblCustomPerformanceForFinishing>("Global Denim S.A. de C.V.", $"Finishing - Rulo: {ruloIdPerformance}, Lote: {ruloTemp.Lote}, Beam: {ruloTemp.Beam}", reportName, fileName, performanceTestResultList.ToList(), exclude);

            if (!fileResult.Item1) return NotFound();

            return fileResult.Item2;
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = SystemStatics.DefaultScheme, Roles = "PerformanceTestReportShow,PerformanceTestReportFull,AdminFull")]
        public async Task<ActionResult> ExportToExcelPerformanceTestReport(VMRuloFilters ruloFilters)
        {
            //ruloFilters.dtEnd = ruloFilters.dtEnd.AddDays(1).AddMilliseconds(-1);
            var rulos = await factory.Rulos.GetRuloListFromFilters(ruloFilters, 0, 0);

            if (rulos == null || rulos.Count() == 0)
                return NotFound();

            //Get only records that contain test result
            List<int> testResultIDs = rulos.Where(x => x.TestResultID != null).Select(x => x.TestResultID.Value).ToList();

            if (testResultIDs == null || testResultIDs.Count() == 0)
                return NotFound();

            var testResultList = await factory.TestResults.GetTestResultFromTestResultIDs(testResultIDs);
            var testResultList2 = testResultList.Where(x => x.PerformanceID != null).Select(x => x.PerformanceID.Value).Distinct().ToList();

            if (testResultList2 == null || testResultList2.Count() == 0)
                return NotFound();

            var customePerformanceTestResultList = await factory.Rulos.GetPerformanceTestResultMasive(testResultList2);

            List<VMPerformanceTestReport> performanceTestReportList = new List<VMPerformanceTestReport>();
            performanceTestReportList = (from customPT in customePerformanceTestResultList
                                         join tr in testResultList on customPT.TestMasterId equals tr.PerformanceID
                                         join r in rulos on tr.TestResultID equals r.TestResultID
                                         select new VMPerformanceTestReport
                                         {
                                             RuloID = r.RuloID,
                                             Lote = r.Lote,
                                             Beam = r.Beam,
                                             Loom = r.Loom,
                                             ParameterName = customPT.ParameterName,
                                             MethodName = customPT.MethodName,
                                             Value = customPT.Value,
                                             Success = customPT.Success,
                                             Category = customPT.Category,
                                             TestBeam = customPT.Beam
                                         }).OrderBy(x => x.RuloID).ToList();

            ExportToExcel export = new ExportToExcel();
            string reportName = "Performance Test Report";
            string fileName = $"Performance Test Report_{DateTime.Today.Year}_{DateTime.Today.Month.ToString().PadLeft(2, '0')}_{DateTime.Today.Day.ToString().PadLeft(2, '0')}.xlsx";

            var fileResult = await export.ExportWithDisplayName<VMPerformanceTestReport>("Global Denim S.A. de C.V.", $"Finishing", reportName, fileName, performanceTestReportList.ToList());

            if (!fileResult.Item1) return NotFound();

            return fileResult.Item2;
        }

        [HttpGet]
        public async Task<ActionResult> GetShrinkage(int ruloId)
        {
            ViewBag.Error = false;
            ViewBag.ErrorMessage = "";

            if (!User.IsInRole("Rulo", AuthType.Update)) return Unauthorized();

            //Validate if rulo is from Rulo Migration
            var isFromRuloMigration = await factory.RuloMigrations.ExistRuloInRuloMigration(ruloId);
            await SetViewBagsForCreateOrEdit(isFromRuloMigration);

            var foundRulo = await factory.Rulos.GetRuloFromRuloID(ruloId);

            if (foundRulo == null) return NotFound();

            return PartialView(foundRulo);
        }

        [HttpPost]
        public async Task<IActionResult> SaveShrinkage(int ruloId, decimal shrinkage)
        {
            ViewBag.Error = false;
            ViewBag.ErrorMessage = "";
            string errorMessage = string.Empty;

            if (!User.IsInRole("Rulo", AuthType.Update)) return Unauthorized();

            var ruloTemp = new Rulo();
            ruloTemp.RuloID = ruloId;
            ruloTemp.Shrinkage = shrinkage;

            if (ruloId <= 0)
            {
                ViewBag.Error = true;
                ViewBag.ErrorMessage = "Rulo not found!";
                return PartialView("GetShrinkage", ruloTemp);
            }

            if (shrinkage <= 0)
            {
                ViewBag.Error = true;
                errorMessage += "Shrinkage no valid!" + "<br>";
            }

            if ((bool)ViewBag.Error)
            {
                ViewBag.ErrorMessage = errorMessage;
                return PartialView("GetShrinkage", ruloTemp);
            }

            var foundRulo = await factory.Rulos.GetRuloFromRuloID(ruloId);

            //Validate rulo process
            var ruloProcesses = await factory.Rulos.GetVMRuloProcessesFromRuloID(ruloId);

            if (ruloProcesses == null || ruloProcesses.Count() == 0)
            {
                ViewBag.Error = true;
                ViewBag.ErrorMessage = "You cannot assign Shrinkage if there are no assigned processes.";
                return PartialView("GetShrinkage", foundRulo);
            }

            //Validate pieces
            var pieces = await factory.Pieces.GetPiecesFromRuloID(ruloId);
            if (pieces == null || pieces.Count() == 0)
            {
                ViewBag.Error = true;
                ViewBag.ErrorMessage = "You cannot assign folio and terminate if there are no created pieces";
                return PartialView("GetShrinkage", foundRulo);
            }

            foundRulo.Shrinkage = shrinkage;

            await factory.Rulos.Update(foundRulo, int.Parse(User.Identity.Name));

            return Ok();
        }

    }
}
