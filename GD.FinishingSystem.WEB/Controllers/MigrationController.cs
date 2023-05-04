using GD.FinishingSystem.Bussines;
using GD.FinishingSystem.Entities;
using GD.FinishingSystem.Entities.ViewModels;
using GD.FinishingSystem.WEB.Classes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Options;
using Microsoft.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

        IndexModelMigration IndexModelMigration = null;
        public MigrationController(IFileProvider fileProvider, IConfiguration configuration)
        {
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
                ruloFilters.dtEnd = DateTime.Today.AddDays(1).AddMilliseconds(-1);
            }
            else
            {
                ruloFilters = System.Text.Json.JsonSerializer.Deserialize<VMRuloFilters>(currentFilter);
            }
            //var ruloMigrationList = await factory.RuloMigrations.GetRuloMigrationListFromBetweenDates(ruloFilters.dtBegin, ruloFilters.dtEnd);
            await SetViewBagsForDates(ruloFilters, positionRuloMigrationId);

            await IndexModelMigration.OnGetAsync("", pageIndex, ruloFilters);

            return View(IndexModelMigration);
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = SystemStatics.DefaultScheme, Roles = "RuloMigrationShow,RuloMigrationFull,AdminFull")]
        public async Task<IActionResult> Index(VMRuloFilters ruloFilters)
        {
            ruloFilters.dtEnd = ruloFilters.dtEnd.AddDays(1).AddMilliseconds(-1);
            //var result = await factory.RuloMigrations.GetRuloMigrationListFromFilters(ruloFilters);
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

            ViewBag.dtBegin = ruloFilters.dtBegin.ToString("yyyy-MM-dd");
            ViewBag.dtEnd = ruloFilters.dtEnd.ToString("yyyy-MM-dd");

            var migrationCategories = await factory.RuloMigrations.GetMigrationCategoryList();
            var migrationsCategoryList = WebUtilities.Create<MigrationCategory>(migrationCategories, "MigrationCategoryID", "Name", true, "All");

            ViewBag.MigrationCategoryList = migrationsCategoryList;

            ViewBag.numLote = ruloFilters.numLote;
            ViewBag.numBeam = ruloFilters.numBeam;
            ViewBag.numLoom = ruloFilters.numLoom;
            ViewBag.numPiece = ruloFilters.numPiece;
            ViewBag.txtStyle = ruloFilters.txtStyle;
            ViewBag.numMigrationCategory = ruloFilters.numMigrationCategory;

            //Style list
            var styleList = await factory.RuloMigrations.GetRuloMigrationStyleList();
            ViewBag.StyleList = styleList;

            ViewBag.PositionRuloMigrationId = positionRuloMigrationId;
        }

        [HttpPost, ValidateAntiForgeryToken,
        Authorize(AuthenticationSchemes = SystemStatics.DefaultScheme, Roles = "RuloMigrationShow,RuloMigrationFull,AdminFull")]
        public async Task<IActionResult> UploadFile(IFormFile formFile)
        {
            VMRuloFilters ruloFilters = new VMRuloFilters();
            ruloFilters.dtBegin = DateTime.Today.AddDays(-15);
            ruloFilters.dtEnd = DateTime.Today.AddDays(1).AddMilliseconds(-1);

            var ruloMigrationList = await factory.RuloMigrations.GetRuloMigrationListFromBetweenDates(ruloFilters.dtBegin, ruloFilters.dtEnd);
            await SetViewBagsForDates (ruloFilters);

            if (formFile == null)
            {
                ViewBag.Error = true;
                ViewBag.ErrorMessage = "File not found!";

                return View(nameof(Index), ruloMigrationList);
            }

            if (formFile.Length > _appSettings.FileSizeLimit)
            {
                ViewBag.Error = true;
                ViewBag.ErrorMessage = $"File too large. File size limit is {(_appSettings.FileSizeLimit / 1024) / 1024} megabytes!";
                return View(nameof(Index), ruloMigrationList);
            }

            string extensionFile = Path.GetExtension(formFile.FileName).ToLower();
            List<string> extensionList = new List<string>() { ".xls", ".xlsx" };

            if (!extensionList.Contains(extensionFile))
            {
                ViewBag.Error = true;
                ViewBag.ErrorMessage = "File type invalid! The file must be Excel";
                return View(nameof(Index), ruloMigrationList);
            }

            int rowsByFileName = await factory.RuloMigrations.CountByFileName(formFile.FileName);
            if (rowsByFileName > 0)
            {
                ViewBag.Error = true;
                ViewBag.ErrorMessage = $"{rowsByFileName} records with the same file name were found. Perhaps this file has already been uploaded, if not, rename the file.";
                return View(nameof(Index), ruloMigrationList);
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

                    ruloMigrationList = await factory.RuloMigrations.GetRuloMigrationListFromBetweenDates(ruloFilters.dtBegin, ruloFilters.dtEnd);
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

                    return View(nameof(Index), ruloMigrationList);
                }


            }

            // Process uploaded files
            // Don't rely on or trust the FileName property without validation.

            return View(nameof(Index), ruloMigrationList);
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
            await SetViewBagForCreateOrEdit();

            RuloMigration newRuloMigration = new RuloMigration();
            newRuloMigration.Date = DateTime.Now;

            return View("CreateOrUpdate", newRuloMigration);
        }

        private async Task SetViewBagForCreateOrEdit()
        {
            ViewBag.Error = false;
            ViewBag.ErrorMessage = string.Empty;

            var migrationCategoryList = await factory.RuloMigrations.GetMigrationCategoryList();
            var list = WebUtilities.Create<MigrationCategory>(migrationCategoryList, "MigrationCategoryID", "Name", true);

            ViewBag.MigrationCategoryList = list;
        }

        // GET: RuloMigration/Edit/5
        public async Task<IActionResult> Edit(int ruloMigrationId)
        {
            await SetViewBagForCreateOrEdit();

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

            try
            {
                if (ruloMigration.RuloMigrationID == 0)
                {
                    if (!User.IsInRole("RuloMigration", AuthType.Add)) return Unauthorized();

                    await factory.RuloMigrations.Add(ruloMigration, int.Parse(User.Identity.Name));
                }
                else
                {
                    if (!User.IsInRole("RuloMigration", AuthType.Update)) return Unauthorized();

                    var foundRuloMigration = await factory.RuloMigrations.GetRuloMigrationFromRuloMigrationID(ruloMigration.RuloMigrationID);

                    foundRuloMigration.Date = ruloMigration.Date;
                    foundRuloMigration.Style = ruloMigration.Style;
                    foundRuloMigration.StyleName = ruloMigration.StyleName;
                    foundRuloMigration.NextMachine = ruloMigration.NextMachine;
                    foundRuloMigration.Lote = ruloMigration.Lote;
                    foundRuloMigration.Stop = ruloMigration.Stop;
                    foundRuloMigration.Beam = ruloMigration.Beam;
                    foundRuloMigration.IsToyota = ruloMigration.IsToyota;
                    foundRuloMigration.Loom = ruloMigration.Loom;
                    foundRuloMigration.PieceNo = ruloMigration.PieceNo;
                    foundRuloMigration.PieceBetilla = ruloMigration.PieceBetilla;
                    foundRuloMigration.Meters = ruloMigration.Meters;
                    foundRuloMigration.GummedMeters = ruloMigration.GummedMeters;
                    foundRuloMigration.MigrationCategoryID = ruloMigration.MigrationCategoryID;
                    foundRuloMigration.Observations = ruloMigration.Observations;
                    foundRuloMigration.Shift = ruloMigration.Shift;

                    await factory.RuloMigrations.Update(foundRuloMigration, int.Parse(User.Identity.Name));
                }

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View("CreateOrUpdate", ruloMigration);
            }
        }

        // GET: RuloMigration/Delete/5
        [HttpGet]
        [Authorize(AuthenticationSchemes = SystemStatics.DefaultScheme, Roles = "RuloMigrationShow,RuloMigrationFull,AdminFull")]
        public async Task<IActionResult> Delete(int ruloMigrationId)
        {
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
        public async Task<IActionResult> ValidateCreateRulo(string lote)
        {
            if (!User.IsInRole("Rulo", AuthType.Add)) return Unauthorized();

            string errorMessage = string.Empty;
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

            return new JsonResult(new { errorMessage = errorMessage });
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = SystemStatics.DefaultScheme, Roles = "RuloAdd,RuloFull,AdminFull")]
        public async Task<IActionResult> CreateRulo(int ruloMigrationId)
        {
            if (!User.IsInRole("Rulo", AuthType.Add)) return Unauthorized();

            var foundRuloMigration = await factory.RuloMigrations.GetRuloMigrationFromRuloMigrationID(ruloMigrationId);

            TempData["ruloMigrationId1"] = ruloMigrationId;

            Rulo newRulo = new Rulo();
            newRulo.Lote = foundRuloMigration.Lote.ToString();
            newRulo.Beam = foundRuloMigration.Beam;
            newRulo.BeamStop = foundRuloMigration.Stop;
            newRulo.Loom = foundRuloMigration.Loom;
            newRulo.IsToyota = foundRuloMigration.IsToyota != null && foundRuloMigration.IsToyota.Equals("T", StringComparison.InvariantCultureIgnoreCase) ? true : false;
            newRulo.Style = foundRuloMigration.Style;
            newRulo.StyleName = foundRuloMigration.StyleName;

            newRulo.WeavingLength = foundRuloMigration.Meters;
            newRulo.EntranceLength = foundRuloMigration.Meters;

            //newRulo.Shift = foundRuloMigration.Shift; //This Shift is from Loom
            newRulo.OriginID = (int)OriginType.PP00;
            newRulo.Observations = foundRuloMigration.Observations;

            return RedirectToAction("CreateFromRuloMigration", "Rulo", newRulo);
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = SystemStatics.DefaultScheme, Roles = "RuloMigrationShow,RuloMigrationFull,AdminFull")]
        public async Task<IActionResult> ExportToExcel(VMRuloFilters ruloFilters)
        {

            ruloFilters.dtEnd = ruloFilters.dtEnd.AddDays(1).AddMilliseconds(-1);
            var result = await factory.RuloMigrations.GetRuloMigrationReportListFromFilters(ruloFilters);

            ExportToExcel export = new ExportToExcel();
            string reportName = "Finishing Report Rulo Raw";
            string fileName = $"Finishing Report Rulo Raw_{DateTime.Today.Year}_{DateTime.Today.Month.ToString().PadLeft(2, '0')}_{DateTime.Today.Day.ToString().PadLeft(2, '0')}.xlsx";

            var fileResult = await export.ExportWithDisplayName<VMRuloMigrationReport>("Global Denim S.A. de C.V.", "Finishing", reportName, fileName, result.ToList());

            if (!fileResult.Item1) return NotFound();

            return fileResult.Item2;
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = SystemStatics.DefaultScheme, Roles = "RuloMigrationShow,RuloMigrationFull,AdminFull")]
        public async Task<IActionResult> ExportToExcelAllStock(VMRuloFilters ruloFilters)
        {
            ruloFilters.dtEnd = ruloFilters.dtEnd.AddDays(1).AddMilliseconds(-1);
            var result = await factory.RuloMigrations.GetAllVMRuloReportList(ruloFilters.dtEnd);

            ExportToExcel export = new ExportToExcel();
            string reportName = "Finishing Report Rulo Raw All";
            string fileName = $"Finishing Report Rulo Raw All_{DateTime.Today.Year}_{DateTime.Today.Month.ToString().PadLeft(2, '0')}_{DateTime.Today.Day.ToString().PadLeft(2, '0')}.xlsx";

            var fileResult = await export.ExportWithDisplayName<VMRuloMigrationReport>("Global Denim S.A. de C.V.", "Finishing", reportName, fileName, result.ToList());

            if (!fileResult.Item1) return NotFound();

            return fileResult.Item2;
        }

    }
}
