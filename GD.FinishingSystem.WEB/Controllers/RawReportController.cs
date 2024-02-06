using GD.FinishingSystem.Bussines;
using GD.FinishingSystem.Entities.ViewModels;
using GD.FinishingSystem.WEB.Classes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using SQLitePCL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GD.FinishingSystem.WEB.Controllers
{
    public class RawReportController : Controller
    {
        private readonly IWebHostEnvironment webHostEnvironment = null;
        private AppSettings appSettings;
        FinishingSystemFactory factory;

        public RawReportController(IWebHostEnvironment webHostEnvironment, IOptions<AppSettings> appSettings)
        {
            this.webHostEnvironment = webHostEnvironment;
            this.appSettings = appSettings.Value;
            factory = new FinishingSystemFactory();
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = SystemStatics.DefaultScheme, Roles = "RawReportShow,RawReportFull,AdminFull")]
        public async Task<ActionResult> Index()
        {
            VMRuloFilters reportFilter = new VMRuloFilters();
            //reportFilter.dtBegin = DateTime.Now.AddDays(-1).AccountStartDate();
            //reportFilter.dtEnd = DateTime.Now.AccountEndDate();
            reportFilter.dtBegin = DateTime.Now.AddDays(-1).AccountStartDate();
            reportFilter.dtEnd = DateTime.Now.AddDays(-1).AccountEndDate();

            await SetViewBagsForDates(reportFilter);

            return View();
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = SystemStatics.DefaultScheme, Roles = "RawReportShow,RawReportFull,AdminFull")]
        public async Task<IActionResult> Index(VMRuloFilters reportFilter)
        {
            await SetViewBagsForDates(reportFilter);

            return View();
        }

        private async Task SetViewBagsForDates(VMRuloFilters reportFilter)
        {
            //ViewBag.dtBegin = reportFilter.dtBegin.ToString("yyyy-MM-ddTHH:mm");
            //ViewBag.dtEnd = reportFilter.dtEnd.ToString("yyyy-MM-ddTHH:mm");
            ViewBag.dtBegin = reportFilter.dtBegin.ToString("yyyy-MM-dd");
            ViewBag.dtEnd = reportFilter.dtEnd.ToString("yyyy-MM-dd");

            ViewBag.numLote = reportFilter.numLote;
            ViewBag.numBeam = reportFilter.numBeam;
            //ViewBag.txtStop = reportFilter.stop;
            ViewBag.numLoom = reportFilter.numLoom;
            ViewBag.txtStyle = reportFilter.txtStyle;
            //ViewBag.numShift = reportFilter.shift;

            //Style list
            var styleDataList = await factory.Rulos.GetRuloStyleStringForProductionLoteList();
            ViewBag.StyleList = styleDataList.ToList();

            ViewBag.isFinishingDetailed = reportFilter.isFinishingDetailed = true;

        }


        [HttpPost]
        [Authorize(AuthenticationSchemes = SystemStatics.DefaultScheme, Roles = "RawReportShow,RawReportFull,AdminFull")]
        public async Task<IActionResult> ExportToExcel(VMReportFilter reportFilter)
        {
            ExportToExcel export = new ExportToExcel();
            string reportName = string.Empty;
            string fileName = string.Empty;
            List<string> exclude = null;
            FileStreamResult fileStreamResult = null;

            switch (reportFilter.typeReport)
            {
                case 1: //Eliminado por indicación de Alfredo 26-01-2024
                    reportName = "Finished Raw Fabric Entrance From " + reportFilter.dtBegin.ToString("dd-MM-yyyy") + " To " + reportFilter.dtEnd.ToString("dd-MM-yyyy");
                    fileName = $"Finished Raw Fabric Entrance_{DateTime.Today.Year}_{DateTime.Today.Month.ToString().PadLeft(2, '0')}_{DateTime.Today.Day.ToString().PadLeft(2, '0')}.xlsx";

                    IEnumerable<TblFinishRawFabricEntrance> result = await factory.RuloMigrations.GetFinishedRawFabricEntrance(reportFilter); //Este se usa en Rulos
                    List<string> totales = new List<string>() { "Salon1", "Salon2", "Salon3", "Salon4", "TotalGeneral" };
                    var fileResult = await export.ExportWithDisplayName<TblFinishRawFabricEntrance>("Global Denim S.A. de C.V.", "Finishing", reportName, fileName, result.ToList(), totaleList: totales);

                    if (!fileResult.Item1) return NotFound();

                    fileStreamResult = fileResult.Item2;
                    break;
                case 2: //Fecha contable
                    reportName = "Raw Fabric Stock From " + reportFilter.dtBegin.ToString("dd-MM-yyyy") + " To " + reportFilter.dtEnd.ToString("dd-MM-yyyy");
                    fileName = $"Raw Fabric Stock_{DateTime.Today.Year}_{DateTime.Today.Month.ToString().PadLeft(2, '0')}_{DateTime.Today.Day.ToString().PadLeft(2, '0')}.xlsx";

                    var ruloMigrations = await factory.RuloMigrations.GetRawFabricStocktFromFilters(reportFilter);

                    var fileResult2 = await export.ExportWithDisplayName<VMRuloMigrationReport>("Global Denim S.A. de C.V.", "Finishing", reportName, fileName, ruloMigrations.ToList());

                    if (!fileResult2.Item1) return NotFound();

                    fileStreamResult = fileResult2.Item2;
                    break;
                case 3: //Fecha dinámica
                    var result3 = await factory.RuloMigrations.GetFinishedProcessRawFabric(reportFilter);

                    reportName = "Finished Process Raw Fabric From " + reportFilter.dtBegin.ToString("dd-MM-yyyy") + " To " + reportFilter.dtEnd.ToString("dd-MM-yyyy");
                    fileName = $"Finished Process Raw Fabric_{DateTime.Today.Year}_{DateTime.Today.Month.ToString().PadLeft(2, '0')}_{DateTime.Today.Day.ToString().PadLeft(2, '0')}.xlsx";

                    exclude = new List<string>() { "IsToyota", "WarehouseCategoryID", "Partiality" };
                    var fileResult3 = await export.ExportWithDisplayName<VMRuloMigrationReport>("Global Denim S.A. de C.V.", "Finishing", reportName, fileName, result3.ToList(), excludeFieldList: exclude);

                    if (!fileResult3.Item1) return NotFound();

                    fileStreamResult = fileResult3.Item2;
                    break;
                case 4: //Fecha contable
                    reportName = "Finished Raw Fabric Entrance Detailed From " + reportFilter.dtBegin.ToString("dd-MM-yyyy") + " To " + reportFilter.dtEnd.ToString("dd-MM-yyyy");
                    fileName = $"Finished Raw Fabric Entrance Detailed_{DateTime.Today.Year}_{DateTime.Today.Month.ToString().PadLeft(2, '0')}_{DateTime.Today.Day.ToString().PadLeft(2, '0')}.xlsx";

                    IEnumerable<VMRuloMigrationReport> result4 = await factory.RuloMigrations.GetFinishedRawFabricEntranceDetailed(reportFilter); //Este se usa en Rulos
                    var fileResult4 = await export.ExportWithDisplayName<VMRuloMigrationReport>("Global Denim S.A. de C.V.", "Finishing", reportName, fileName, result4.ToList());

                    if (!fileResult4.Item1) return NotFound();

                    fileStreamResult = fileResult4.Item2;
                    break;
                case 5: //Fecha dinámica TODO: CONTINUAR
                    reportName = "Monthly Stock Report From " + reportFilter.dtBegin.ToString("dd-MM-yyyy") + " To " + reportFilter.dtEnd.ToString("dd-MM-yyyy");
                    fileName = $"Monthly Stock Report From_{DateTime.Today.Year}_{DateTime.Today.Month.ToString().PadLeft(2, '0')}_{DateTime.Today.Day.ToString().PadLeft(2, '0')}.xlsx";

                    IEnumerable<WarehouseStock> result5 = await factory.RuloMigrations.GetMonthlyFinishingStockReport(reportFilter);
                    var fileResult5 = await export.ExportWithDisplayName<WarehouseStock>("Global Denim S.A. de C.V.", "Finishing", reportName, fileName, result5.ToList());

                    if (!fileResult5.Item1) return NotFound();

                    fileStreamResult = fileResult5.Item2;
                    break;
                default:
                    break;
            }

            return fileStreamResult;
        }



    }
}
