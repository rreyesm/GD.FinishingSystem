using DocumentFormat.OpenXml.Drawing.Diagrams;
using DocumentFormat.OpenXml.Office2010.Drawing;
using FastReport;
using FastReport.Export.PdfSimple;
using FastReport.Export.PdfSimple.PdfCore;
using GD.FinishingSystem.Bussines;
using GD.FinishingSystem.DAL.EFdbPlanta;
using GD.FinishingSystem.Entities;
using GD.FinishingSystem.Entities.ViewModels;
using GD.FinishingSystem.WEB.Classes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Options;
using Microsoft.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;

namespace GD.FinishingSystem.WEB.Controllers
{
    [Authorize(AuthenticationSchemes = SystemStatics.DefaultScheme)]
    public class MigrationController : Controller
    {
        FinishingSystemFactory factory;
        private readonly IFileProvider fileProvider;
        private AppSettings _appSettings;
        private readonly IWebHostEnvironment webHostEnvironment;

        IndexModelMigration IndexModelMigration = null;
        public MigrationController(IWebHostEnvironment webHostEnvironment, IFileProvider fileProvider, IConfiguration configuration)
        {
            this.webHostEnvironment = webHostEnvironment;
            factory = new FinishingSystemFactory();
            this.fileProvider = fileProvider;

            this._appSettings = (AppSettings)configuration.GetSection("AppSettings").Get<AppSettings>();
            IndexModelMigration = new IndexModelMigration(factory, _appSettings);
        }
        // GET: RuloMigration
        [Authorize(AuthenticationSchemes = SystemStatics.DefaultScheme, Roles = "RuloMigrationShow,RuloMigrationFull,AdminFull")]
        public async Task<ActionResult> Index(int positionRuloMigrationId = 0, string currentFilter = null, int? pageIndex = null)
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

            await SetViewBagsForDates(ruloFilters, positionRuloMigrationId);

            await IndexModelMigration.OnGetAsync("", pageIndex, ruloFilters);

            return View(IndexModelMigration);
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = SystemStatics.DefaultScheme, Roles = "RuloMigrationShow,RuloMigrationFull,AdminFull")]
        public async Task<IActionResult> Index(VMRuloFilters ruloFilters)
        {
            ruloFilters.dtEnd = ruloFilters.dtEnd.RealDateEndDate();
            await SetViewBagsForDates(ruloFilters);

            await IndexModelMigration.OnGetAsync("", 1, ruloFilters);

            return View(IndexModelMigration);
        }

        private async Task SetViewBagsForDates(VMRuloFilters ruloFilters, int positionRuloMigrationId = 0)
        {
            ViewBag.Error = false;
            ViewBag.ErrorMessage = string.Empty;
            ViewBag.Ok = false;
            ViewBag.OkMessage = string.Empty;

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

            var migrationCategories = await factory.RuloMigrations.GetMigrationCategoryList();
            var migrationsCategoryList = WebUtilities.Create<MigrationCategory>(migrationCategories, "MigrationCategoryID", "Name", true, "All");

            var definitionProcessess = await factory.RuloMigrations.GetDefinitionProcessList();
            var definitionProcessessList = WebUtilities.Create<DefinationProcess>(definitionProcessess, "DefinationProcessID", "Name", true);

            var originCategories = await factory.RuloMigrations.GetOriginCategoryList();
            originCategories = originCategories.Where(x => x.OriginCategoryID == 1 || x.OriginCategoryID == 7); //1=PP00, 7=DES0
            var originCategoryList = WebUtilities.Create<OriginCategory>(originCategories, "OriginCategoryID", "OriginCode", true);

            ViewBag.MigrationCategoryList = migrationsCategoryList;
            ViewBag.DefinitionProcessList = definitionProcessessList;
            ViewBag.OriginCategoryList = originCategoryList;

            ViewBag.numLote = ruloFilters.numLote;
            ViewBag.numBeam = ruloFilters.numBeam;
            ViewBag.numLoom = ruloFilters.numLoom;
            ViewBag.numPiece = ruloFilters.numPiece;
            ViewBag.txtStyle = ruloFilters.txtStyle;
            ViewBag.numMigrationCategory = ruloFilters.numMigrationCategory;
            ViewBag.txtLocation = ruloFilters.txtLocation;

            //Style list
            var styleList = await factory.RuloMigrations.GetRuloMigrationStyleList();
            ViewBag.StyleList = styleList;

            var locations = await factory.RuloMigrations.GetLocationList();
            ViewBag.LocationList = locations.Select(x => x.Name);

            ViewBag.PositionRuloMigrationId = positionRuloMigrationId;

            var user = await factory.Users.GetByUserID(int.Parse(User.Identity.Name));
            ViewBag.AreaID = user.AreaID;
        }

        [HttpPost, ValidateAntiForgeryToken,
        Authorize(AuthenticationSchemes = SystemStatics.DefaultScheme, Roles = "RuloMigrationShow,RuloMigrationFull,AdminFull")]
        public async Task<IActionResult> UploadFile(IFormFile formFile)
        {
            VMRuloFilters ruloFilters = new VMRuloFilters();
            ruloFilters.dtBegin = DateTime.Today.AddDays(-15);
            ruloFilters.dtEnd = DateTime.Today.AddDays(1).AddMilliseconds(-1);

            ////////////////////////
            //var ruloMigrationList = await factory.RuloMigrations.GetRuloMigrationListFromBetweenDates(ruloFilters.dtBegin, ruloFilters.dtEnd);
            //await SetViewBagsForDates(ruloFilters);
            ruloFilters.dtEnd = ruloFilters.dtEnd.RealDateEndDate();
            await SetViewBagsForDates(ruloFilters);

            await IndexModelMigration.OnGetAsync("", 1, ruloFilters);
            ////////////////////////

            if (formFile == null)
            {
                ViewBag.Error = true;
                ViewBag.ErrorMessage = "File not found!";

                //return View(nameof(Index), ruloMigrationList);
                return View(nameof(Index), IndexModelMigration);
            }

            if (formFile.Length > _appSettings.FileSizeLimit)
            {
                ViewBag.Error = true;
                ViewBag.ErrorMessage = $"File too large. File size limit is {(_appSettings.FileSizeLimit / 1024) / 1024} megabytes!";
                //return View(nameof(Index), ruloMigrationList);
                return View(nameof(Index), IndexModelMigration);
            }

            string extensionFile = Path.GetExtension(formFile.FileName).ToLower();
            List<string> extensionList = new List<string>() { ".xls", ".xlsx" };

            if (!extensionList.Contains(extensionFile))
            {
                ViewBag.Error = true;
                ViewBag.ErrorMessage = "File type invalid! The file must be Excel";
                //return View(nameof(Index), ruloMigrationList);
                return View(nameof(Index), IndexModelMigration);
            }

            int rowsByFileName = await factory.RuloMigrations.CountByFileName(formFile.FileName);
            if (rowsByFileName > 0)
            {
                ViewBag.Error = true;
                ViewBag.ErrorMessage = $"{rowsByFileName} records with the same file name were found. Perhaps this file has already been uploaded, if not, rename the file.";
                //return View(nameof(Index), ruloMigrationList);
                return View(nameof(Index), IndexModelMigration);
            }

            if (formFile.Length > 0)
            {
                MemoryStream filestream = new MemoryStream();
                await formFile.CopyToAsync(filestream);

                ValidateDataMigration validate = new ValidateDataMigration();

                var result = await validate.ValidateDataAndExport(factory, filestream, formFile.FileName, int.Parse(User.Identity.Name));

                if (result.isOk)
                {
                    ViewBag.Ok = true;
                    ViewBag.OkMessage = result.message;

                    //ruloMigrationList = await factory.RuloMigrations.GetRuloMigrationListFromBetweenDates(ruloFilters.dtBegin, ruloFilters.dtEnd);
                }
                else
                {
                    StringBuilder sbError = new StringBuilder();

                    sbError.Append(result.message);
                    sbError.Append("<br>");

                    foreach (var itemList in result.errorByRowList)
                    {
                        foreach (var item in itemList)
                        {
                            sbError.Append(item);
                            sbError.Append("<br>");
                        }
                    }

                    ViewBag.Error = true;
                    ViewBag.ErrorMessage = sbError.ToString();

                    //return View(nameof(Index), ruloMigrationList);
                    return View(nameof(Index), IndexModelMigration);
                }


            }

            // Process uploaded files
            // Don't rely on or trust the FileName property without validation.

            //return View(nameof(Index), ruloMigrationList);
            return View(nameof(Index), IndexModelMigration);
        }

        // GET: RuloMigration/Details/5
        [HttpGet]
        [Authorize(AuthenticationSchemes = SystemStatics.DefaultScheme, Roles = "RuloMigrationShow,RuloMigrationFull,AdminFull")]
        public async Task<IActionResult> Details(int ruloMigrationId)
        {
            var foundRuloMigration = await factory.RuloMigrations.GetRuloMigrationFromRuloMigrationID(ruloMigrationId);

            if (foundRuloMigration == null) return NotFound();

            return View(foundRuloMigration);
        }

        // GET: RuloMigration/Create
        public async Task<IActionResult> Create()
        {
            await SetViewBagsForCreateOrEdit();

            RuloMigration newRuloMigration = new RuloMigration();
            newRuloMigration.Date = DateTime.Now;

            return View("CreateOrUpdate", newRuloMigration);
        }

        private async Task SetViewBagsForCreateOrEdit()
        {
            ViewBag.Error = false;
            ViewBag.ErrorMessage = string.Empty;

            var migrationCategories = await factory.RuloMigrations.GetMigrationCategoryList();
            var migrationCategorylist = WebUtilities.Create<MigrationCategory>(migrationCategories, "MigrationCategoryID", "Name", true);

            var definitionProcessess = await factory.RuloMigrations.GetDefinitionProcessList();
            var definitionProcessList = WebUtilities.Create<DefinationProcess>(definitionProcessess.ToList(), "DefinationProcessID", "Name", true);

            var originCategories = await factory.RuloMigrations.GetOriginCategoryList();
            originCategories = originCategories.Where(x => x.OriginCategoryID == 1 || x.OriginCategoryID == 7); //1=PP00, 7=DES0
            var originCategoryList = WebUtilities.Create<OriginCategory>(originCategories.ToList(), "OriginCategoryID", "Name", true);

            var locations = await factory.RuloMigrations.GetLocationList();
            var floors = await factory.Floors.GetFloorList();

            var list = from l in locations
                       join f in floors on l.Floor?.FloorID equals f.FloorID
                       into ljL
                       from subL in ljL.DefaultIfEmpty()
                       select new Location
                       {
                           LocationID = l.LocationID,
                           Name = $"{l.Name} ({subL?.FloorName})"
                       };

            var locationList = WebUtilities.Create<Location>(list.ToList(), "LocationID", "Name", true);

            ViewBag.MigrationCategoryList = migrationCategorylist;
            ViewBag.DefinitionProcessList = definitionProcessList;
            ViewBag.OriginCategoryList = originCategoryList;
            ViewBag.LocationList = locationList;

            var user = await factory.Users.GetByUserID(int.Parse(User.Identity.Name));
            ViewBag.AreaID = user.AreaID;
        }

        // GET: RuloMigration/Edit/5
        public async Task<IActionResult> Edit(int ruloMigrationId)
        {
            await SetViewBagsForCreateOrEdit();

            RuloMigration ruloMigration = await factory.RuloMigrations.GetRuloMigrationFromRuloMigrationID(ruloMigrationId);

            if (ruloMigration == null) return NotFound();

            return View("CreateOrUpdate", ruloMigration);
        }

        // POST: RuloMigration/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Save(RuloMigration ruloMigration)
        {
            ViewBag.Error = false;
            await SetViewBagsForCreateOrEdit();

            try
            {

                if (ruloMigration.RuloMigrationID == 0)
                {
                    if (!User.IsInRole("RuloMigration", AuthType.Add)) return Unauthorized();

                    //Validate if this rulo don't exists
                    if (await factory.RuloMigrations.Exists(ruloMigration))
                    {
                        ViewBag.Error = true;
                        ViewBag.ErrorMessage = $"Ya existe un registro en la base de datos con la informción lote: {ruloMigration.Lote}, julio: {ruloMigration.Beam}, telar: {ruloMigration.Loom}, pieza: {ruloMigration.PieceNo}, parada: {ruloMigration.BeamStop}.";
                        return View("CreateOrUpdate", ruloMigration);
                    }

                    ruloMigration.Date = DateTime.Now;
                    ruloMigration.WarehouseCategoryID = 1; //Default 1=RAW

                    if (ViewBag.AreaID == (int)AreaType.Tejido)
                    {
                        ruloMigration.OriginID = 1; //Default: 1=PP00
                        ruloMigration.WarehouseCategoryID = 8; //Deafult 8 = W1
                        ruloMigration.MigrationCategoryID = ruloMigration.Meters < 1000 ? 3 : 4; //3=Crudo, 4=Crudo-G
                    }

                    if (!SetStyleAndStyleName(ref ruloMigration))
                    {
                        return View("CreateOrUpdate", ruloMigration);
                    }

                    await factory.RuloMigrations.Add(ruloMigration, int.Parse(User.Identity.Name));
                }
                else
                {
                    if (!User.IsInRole("RuloMigration", AuthType.Update)) return Unauthorized();

                    //Validate if this rulo don't exists
                    if (await factory.RuloMigrations.Exists(ruloMigration))
                    {
                        ViewBag.Error = true;
                        ViewBag.ErrorMessage = $"Ya existe un registro en la base de datos con la informción lote: {ruloMigration.Lote}, julio: {ruloMigration.Beam}, telar: {ruloMigration.Loom}, pieza: {ruloMigration.PieceNo}, parada: {ruloMigration.BeamStop}.";
                        return View("CreateOrUpdate", ruloMigration);
                    }

                    var foundRuloMigration = await factory.RuloMigrations.GetRuloMigrationFromRuloMigrationID(ruloMigration.RuloMigrationID);

                    if (ViewBag.AreaID == (int)AreaType.Acabado)
                    {
                        foundRuloMigration.MigrationCategoryID = ruloMigration.MigrationCategoryID != 0 ? ruloMigration.MigrationCategoryID : 3; //Default 3-Crudo
                        foundRuloMigration.WeavingShift = ruloMigration.WeavingShift;
                        foundRuloMigration.OriginID = ruloMigration.OriginID;
                        foundRuloMigration.LocationID = ruloMigration.LocationID;
                    }
                    else
                    {
                        //foundRuloMigration.Date = ruloMigration.Date;
                        if (!SetStyleAndStyleName(ref ruloMigration))
                        {
                            return View("CreateOrUpdate", ruloMigration);
                        }

                        foundRuloMigration.Style = ruloMigration.Style;
                        foundRuloMigration.StyleName = ruloMigration.StyleName;
                        foundRuloMigration.NextMachine = ruloMigration.NextMachine;
                        foundRuloMigration.Lote = ruloMigration.Lote;
                        foundRuloMigration.Beam = ruloMigration.Beam;
                        foundRuloMigration.BeamStop = ruloMigration.BeamStop;
                        foundRuloMigration.IsToyotaText = ruloMigration.IsToyotaText;
                        foundRuloMigration.Loom = ruloMigration.Loom;
                        foundRuloMigration.PieceNo = ruloMigration.PieceNo;
                        foundRuloMigration.PieceBetilla = ruloMigration.PieceBetilla;
                        foundRuloMigration.Meters = ruloMigration.Meters;
                        foundRuloMigration.SizingMeters = ruloMigration.SizingMeters;
                        foundRuloMigration.MigrationCategoryID = ruloMigration.MigrationCategoryID != 0 ? ruloMigration.MigrationCategoryID : 3; //Default 3-Crudo
                        foundRuloMigration.Observations = ruloMigration.Observations;
                        foundRuloMigration.WeavingShift = ruloMigration.WeavingShift;
                        foundRuloMigration.DefinitionProcessID = ruloMigration.DefinitionProcessID;
                        foundRuloMigration.IsToyotaMigration = ruloMigration.IsToyotaMigration;
                        foundRuloMigration.OriginID = ruloMigration.OriginID;
                        foundRuloMigration.LocationID = ruloMigration.LocationID;
                    }

                    await factory.RuloMigrations.Update(foundRuloMigration, int.Parse(User.Identity.Name));
                }

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = true;
                ViewBag.ErrorMessage = ex.Message;
                return View("CreateOrUpdate", ruloMigration);
            }
        }

        private bool SetStyleAndStyleName(ref RuloMigration ruloMigration)
        {
            //Update the style and the name of the style since they are locked in the form, the value is not passed.
            VMStyleData styleData = null;
            //Validation if it is a test, the style is not updated in the DB
            if (!ruloMigration.IsTestStyle)
            {
                styleData = factory.Rulos.GetRuloStyle(ruloMigration.Lote.ToString()).Result;
                if (styleData == null)
                {
                    ViewBag.Error = true;
                    ViewBag.ErrorMessage = "The lote number is not registered in the Programming!";

                    return false;
                    //return View("CreateOrUpdate", ruloMigration);
                }

                ruloMigration.Style = styleData.Style;
                ruloMigration.StyleName = styleData.StyleName;
            }

            return true;
        }

        // GET: RuloMigration/Delete/5
        [HttpGet]
        [Authorize(AuthenticationSchemes = SystemStatics.DefaultScheme, Roles = "RuloMigrationShow,RuloMigrationFull,AdminFull")]
        public async Task<IActionResult> Delete(int ruloMigrationId)
        {
            var user = await factory.Users.GetByUserID(int.Parse(User.Identity.Name));
            ViewBag.AreaID = user.AreaID;

            var foundRuloMigration = await factory.RuloMigrations.GetRuloMigrationFromRuloMigrationID(ruloMigrationId);

            if (foundRuloMigration == null) return NotFound();

            return View(foundRuloMigration);
        }

        // POST: RuloMigration/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int ruloMigrationId)
        {
            if (!User.IsInRole("RuloMigration", AuthType.Delete)) return Unauthorized();

            var foundRuloMigration = await factory.RuloMigrations.GetRuloMigrationFromRuloMigrationID(ruloMigrationId);

            try
            {
                await factory.RuloMigrations.Delete(foundRuloMigration, int.Parse(User.Identity.Name));

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        [HttpGet]
        public async Task<IActionResult> ValidateCreateRulo(int ruloMigrationID, string lote)
        {
            if (!User.IsInRole("Rulo", AuthType.Add)) return Unauthorized();

            string errorMessage = string.Empty;
            var ruloMigration = await factory.RuloMigrations.GetRuloMigrationFromRuloMigrationID(ruloMigrationID);
            if (ruloMigration == null)
                errorMessage = "Raw is not exists.";
            else if (ruloMigration != null && ruloMigration.RuloID != null)
                errorMessage = "This Raw already has a Rulo number assigned.";

            var styleData = await factory.Rulos.GetRuloStyle(lote);

            var systemPrinter = await WebUtilities.GetSystemPrinter(factory, this.HttpContext);

            //Validate if period exists
            var currentPeriod = await factory.Periods.GetCurrentPeriod(systemPrinter.SystemPrinterID);
            if (currentPeriod == null)
            {
                errorMessage = "Cannot create a new roll. You must open a period.";
            }

            //Comparation period-rulo style
            if (currentPeriod != null)
            {
                if (currentPeriod.Style != styleData.Style)
                {
                    errorMessage = "The style entered does not correspond to the style of the period. First create the period for the style.";
                }
            }
            else
            {
                errorMessage = "Period style not found!";
            }

            return new JsonResult(new { errorMessage = errorMessage });
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = SystemStatics.DefaultScheme, Roles = "RuloAdd,RuloFull,AdminFull")]
        public async Task<IActionResult> CreateRulo(int ruloMigrationId)
        {
            if (!User.IsInRole("Rulo", AuthType.Add)) return Unauthorized();

            var foundRuloMigration = await factory.RuloMigrations.GetRuloMigrationFromRuloMigrationID(ruloMigrationId);
            var totalMeters = await factory.RuloMigrations.GetTotalMetersByRuloMigration(foundRuloMigration.Lote, foundRuloMigration.Beam);

            TempData["ruloMigrationId1"] = ruloMigrationId;

            Rulo newRulo = new Rulo();
            newRulo.Lote = foundRuloMigration.Lote.ToString();
            newRulo.Beam = foundRuloMigration.Beam;
            newRulo.BeamStop = foundRuloMigration.BeamStop;
            newRulo.Loom = foundRuloMigration.Loom;
            newRulo.IsToyota = foundRuloMigration.IsToyotaMigration; //foundRuloMigration.IsToyotaText != null && foundRuloMigration.IsToyotaText.Equals("T", StringComparison.InvariantCultureIgnoreCase) ? true : false;
            newRulo.Style = foundRuloMigration.Style;
            newRulo.StyleName = foundRuloMigration.StyleName;

            var systemPrinter = await WebUtilities.GetSystemPrinter(factory, this.HttpContext);
            var currentPeriod = await factory.Periods.GetCurrentPeriod(systemPrinter.SystemPrinterID);

            if (newRulo.SentAuthorizerID == 0) newRulo.SentAuthorizerID = null;
            newRulo.PeriodID = currentPeriod.PeriodID;

            newRulo.WeavingLength = totalMeters; //foundRuloMigration.Meters;
            newRulo.EntranceLength = 0;

            newRulo.Shift = GetTurno(DateTime.Now); //This Shift is from Loom
            newRulo.MainOriginID = foundRuloMigration.OriginID.HasValue ? (int)foundRuloMigration.OriginID : 1; //1 = PP00;
            newRulo.OriginID = foundRuloMigration.OriginID.HasValue ? (int)foundRuloMigration.OriginID : 1; //1 = PP00;
            newRulo.Observations = foundRuloMigration.Observations;

            return RedirectToAction("CreateFromRuloMigration", "Rulo", newRulo);
        }

        private int GetTurno(DateTime date)
        {
            if (date.Hour < 7 || date.Hour == 23) return 3;
            else if (date.Hour < 15) return 1;
            else return 2;
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = SystemStatics.DefaultScheme, Roles = "RuloMigrationShow,RuloMigrationFull,AdminFull")]
        public async Task<IActionResult> ExportToExcel(VMRuloFilters ruloFilters)
        {

            ruloFilters.dtEnd = ruloFilters.dtEnd.AddDays(1).AddMilliseconds(-1);
            var result = await factory.RuloMigrations.GetRawFabricStocktFromFilters(ruloFilters);

            ExportToExcel export = new ExportToExcel();
            string reportName = "Raw Fabric Stock";
            string fileName = $"Raw Fabric Stock_{DateTime.Today.Year}_{DateTime.Today.Month.ToString().PadLeft(2, '0')}_{DateTime.Today.Day.ToString().PadLeft(2, '0')}.xlsx";

            var fileResult = await export.ExportWithDisplayName<VMRuloMigrationReport>("Global Denim S.A. de C.V.", "Finishing", reportName, fileName, result.ToList());

            if (!fileResult.Item1) return NotFound();

            return fileResult.Item2;
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = SystemStatics.DefaultScheme, Roles = "RuloMigrationShow,RuloMigrationFull,AdminFull")]
        public async Task<IActionResult> ExportToExcelAllStock(VMRuloFilters ruloFilters)
        {
            ruloFilters.dtEnd = ruloFilters.dtEnd.AddDays(1).AddMilliseconds(-1);
            var result = await factory.RuloMigrations.GetAllTheInformationFromRawFabric(ruloFilters.dtEnd);

            ExportToExcel export = new ExportToExcel();
            string reportName = "Finishing Report Rulo Raw All";
            string fileName = $"Finishing Report Rulo Raw All_{DateTime.Today.Year}_{DateTime.Today.Month.ToString().PadLeft(2, '0')}_{DateTime.Today.Day.ToString().PadLeft(2, '0')}.xlsx";

            var fileResult = await export.ExportWithDisplayName<VMRuloMigrationReport>("Global Denim S.A. de C.V.", "Finishing", reportName, fileName, result.ToList());

            if (!fileResult.Item1) return NotFound();

            return fileResult.Item2;
        }

        [HttpPost]
        public async Task<IActionResult> GetStyleData(string lote)
        {
            string errorMessage = string.Empty;
            string style = string.Empty;
            string styleName = string.Empty;

            if (!(User.IsInRole("RuloMigration", AuthType.Add) || User.IsInRole("RuloMigration", AuthType.Update)))
                return Unauthorized();

            var styleData = await factory.Rulos.GetRuloStyle(lote);

            ////NO SERÍA NECESARIO VALIDAR EL PERIODO PORQUE AQUÍ SOLO SE VA A METER LA INFORMACIÓN DEL CRUDO
            //var systemPrinter = await WebUtilities.GetSystemPrinter(factory, this.HttpContext);

            ////Comparation period-rulo style
            //Period period = await factory.Periods.GetCurrentPeriod(systemPrinter.SystemPrinterID);
            //if (period != null)
            //{
            //    if (styleData != null)
            //    {
            //        if (period.Style != styleData.Style)
            //        {
            //            errorMessage = "The style entered does not correspond to the style of the period. First create the period for the style.";
            //        }
            //    }
            //}
            //else
            //{
            //    errorMessage = "Period style not found!";
            //}

            if (styleData == null)
                errorMessage = "The lote number is not registered in the Programming!";
            else
            {
                style = styleData.Style;
                styleName = styleData.StyleName;
            }

            return new JsonResult(new { errorMessage = errorMessage, style = style, styleName = styleName });
        }

        [HttpPost]
        public async Task<IActionResult> CreateFactoryAdvance(int ruloMigrationID, decimal meter)
        {
            string errorMessage = "";
            string lote = "0";
            int beam = 0;
            try
            {
                var ruloMigrationOrigin = await factory.RuloMigrations.GetRuloMigrationFromRuloMigrationID(ruloMigrationID);
                lote = ruloMigrationOrigin.Lote;
                beam = ruloMigrationOrigin.Beam;

                RuloMigration ruloMigrationDestination = new RuloMigration();

                GD.FinishingSystem.WEB.Classes.Extensions.CopyProperties(ruloMigrationOrigin, ruloMigrationDestination);

                //Change Rulo Migration ID
                ruloMigrationDestination.RuloMigrationID = 0;
                ruloMigrationDestination.Meters = meter;
                ruloMigrationDestination.FabricAdvance = true;

                //Update meters
                ruloMigrationOrigin.Meters = ruloMigrationOrigin.Meters - meter;

                await factory.RuloMigrations.Add(ruloMigrationDestination, int.Parse(User.Identity.Name));
                await factory.RuloMigrations.Update(ruloMigrationOrigin, int.Parse(User.Identity.Name));

            }
            catch (Exception)
            {
                errorMessage = $"Error al crear el avance de tela en el Crudo: {ruloMigrationID}";
            }

            return new JsonResult(new { errorMessage = errorMessage, lote = lote, beam = beam });
        }

        [Authorize(AuthenticationSchemes = SystemStatics.DefaultScheme, Roles = "RuloMigrationShow,RuloMigrationFull,AdminFull")]
        public IActionResult ShowPrintLabel(int areaId, int ruloMigrationId)
        {
            ViewBag.AreaId = areaId;
            ViewBag.RMId = ruloMigrationId;

            return PartialView("ShowPDF");
        }

        [Authorize(AuthenticationSchemes = SystemStatics.DefaultScheme, Roles = "RuloMigrationShow,RuloMigrationFull,AdminFull")]
        public async Task<IActionResult> ShowPDF(int areaId, int ruloMigrationId) //areId era para manejar diferente etiqueta para el área de acabado, por el momento no se manejará
        {
            Report report = null;
            PDFSimpleExport export = null;
            MemoryStream memoryStream = null;
            string contentType = "application/pdf";
            byte[] bytes = null;
            string pathReport = string.Empty;

            try
            {
                pathReport = System.IO.Path.Combine(webHostEnvironment.WebRootPath, @"..\Reports\WeavingLabel.frx");
                report = new Report();
                report.Load(pathReport);

                var ruloMigrations = await factory.RuloMigrations.GetRuloMigrationFromRuloMigrationID(ruloMigrationId);

                report.SetParameterValue("@StyleName", ruloMigrations.StyleName);
                report.SetParameterValue("@Style", ruloMigrations.Style);
                report.SetParameterValue("@Lote", ruloMigrations.Lote);
                report.SetParameterValue("@Beam", ruloMigrations.Beam);
                report.SetParameterValue("@Loom", ruloMigrations.Loom);
                report.SetParameterValue("@CreationDate", ruloMigrations.Date);
                report.SetParameterValue("@BeamStop", ruloMigrations.BeamStop);
                report.SetParameterValue("@Piece", ruloMigrations.PieceNo);
                report.SetParameterValue("@IsToyota", ruloMigrations.IsToyotaText);
                report.SetParameterValue("@SizingMeters", ruloMigrations.SizingMeters);
                report.SetParameterValue("@Meters", ruloMigrations.Meters);
                report.SetParameterValue("@RawID", ruloMigrations.RuloMigrationID);

                report.Prepare(true);

                memoryStream = new MemoryStream();
                export = new PDFSimpleExport();
                export.Export(report, memoryStream);
                memoryStream.Seek(0, SeekOrigin.Begin);
                bytes = memoryStream.ToArray();

            }
            catch (Exception ex)
            {
                BadRequest(ex);
            }
            finally
            {
                if (memoryStream != null)
                    memoryStream.Flush();
            }

            return File(bytes, contentType);
        }

        public async Task<IActionResult> TransferFromWeavingToFinishing(int ruloMigrationId)
        {
            var ruloMigration = await factory.RuloMigrations.GetRuloMigrationFromRuloMigrationID(ruloMigrationId);
            if (ruloMigration == null) return NotFound();

            ruloMigration.WarehouseCategoryID = 1; //RAW1
            await factory.RuloMigrations.Update(ruloMigration, int.Parse(User.Identity.Name));

            return Ok();

        }

    }
}
