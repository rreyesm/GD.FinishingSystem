using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using FastReport.Data;
//using FastReport.Data;
//using FastReport.Web;
using GD.FinishingSystem.Bussines;
using GD.FinishingSystem.Entities.ViewModels;
using GD.FinishingSystem.WEB.Classes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace GD.FinishingSystem.WEB.Controllers
{
    [Authorize(AuthenticationSchemes = SystemStatics.DefaultScheme)]
    public class ReportController : Controller
    {
        private readonly IWebHostEnvironment webHostEnvironment = null;
        private AppSettings appSettings;
        FinishingSystemFactory factory;

        public ReportController(IWebHostEnvironment webHostEnvironment, IOptions<AppSettings> appSettings)
        {
            this.webHostEnvironment = webHostEnvironment;
            this.appSettings = appSettings.Value;
            factory = new FinishingSystemFactory();
        }
        [HttpGet]
        [Authorize(AuthenticationSchemes = SystemStatics.DefaultScheme, Roles = "ReportShow,ReportFull,AdminFull")]
        public async Task<IActionResult> Index()
        {
            VMReportFilter reportFilter = new VMReportFilter();
            reportFilter.dtBegin = DateTime.Now.AddDays(-1).AccountStartDate();
            reportFilter.dtEnd = DateTime.Now.AccountEndDate();

            await SetViewBagsForDates(reportFilter);

            return View();
        }


        [HttpPost]
        [Authorize(AuthenticationSchemes = SystemStatics.DefaultScheme, Roles = "ReportShow,ReportFull,AdminFull")]
        public async Task<IActionResult> Index(VMReportFilter reportFilter)
        {
            await SetViewBagsForDates(reportFilter);

            return View();
        }

        private async Task SetViewBagsForDates(VMReportFilter reportFilter)
        {
            ViewBag.dtBegin = reportFilter.dtBegin.ToString("yyyy-MM-ddTHH:mm:ss");
            ViewBag.dtEnd = reportFilter.dtEnd.ToString("yyyy-MM-ddTHH:mm:ss");

            ViewBag.numLote = reportFilter.numLote;
            ViewBag.numBeam = reportFilter.numBeam;
            ViewBag.numLoom = reportFilter.numLoom;
            ViewBag.txtStyle = reportFilter.txtStyle;
            ViewBag.numShift = reportFilter.shift;
            ViewBag.withBatches = reportFilter.withBatches;

            //Style list
            var styleDataList = await factory.Rulos.GetRuloStyleStringForProductionLoteList();
            ViewBag.StyleList = styleDataList.ToList();

        }


        [HttpPost]
        [Authorize(AuthenticationSchemes = SystemStatics.DefaultScheme, Roles = "ReportShow,ReportFull,AdminFull")]
        public async Task<IActionResult> ExportToExcel(VMReportFilter reportFilter)
        {
            string reportName = string.Empty;
            string fileName = string.Empty;
            List<string> exclude = null;
            FileStreamResult fileStreamResult = null;
            List<string> totales = null;

            try
            {
                ExportToExcel export = new ExportToExcel();

                switch (reportFilter.typeReport)
                {
                    case 1:
                        fileName = $"Finished Fabric Report_{DateTime.Today.Year}_{DateTime.Today.Month.ToString().PadLeft(2, '0')}_{DateTime.Today.Day.ToString().PadLeft(2, '0')}.xlsx";
                        reportName = "Finished Fabric Report From " + reportFilter.dtBegin.ToString("dd-MM-yyyy HH:mm") + " To " + reportFilter.dtEnd.ToString("dd-MM-yyyy HH:mm");

                        IEnumerable<TblCustomReport> customReports = await factory.Rulos.GetFinishedFabricReport(reportFilter);

                        exclude = new List<string>() { "FinishMeterRP" };
                        totales = new List<string>() { "EntranceLength", "ExitRamaChamuscado", "ExitLength" };
                        var fileResult1 = await export.ExportWithDisplayName<TblCustomReport>("Global Denim S.A. de C.V.", "Finishing", reportName, fileName, customReports.ToList(), exclude, totaleList: totales);

                        if (!fileResult1.Item1) return NotFound();

                        fileStreamResult = fileResult1.Item2;

                        break;
                    case 2:
                        fileName = $"Finishing Fabric Detailed Report_{DateTime.Today.Year}_{DateTime.Today.Month.ToString().PadLeft(2, '0')}_{DateTime.Today.Day.ToString().PadLeft(2, '0')}.xlsx";
                        reportName = "Finishing Fabric Detailed Report From " + reportFilter.dtBegin.ToString("dd-MM-yyyy HH:mm") + " To " + reportFilter.dtEnd.ToString("dd-MM-yyyy HH:mm");

                        IEnumerable<VMRulo> vmRulos = await factory.Rulos.GetRuloReportListFromFilters(reportFilter as VMRuloFilters);

                        List<Tuple<int, string>> colorList = new List<Tuple<int, string>>();
                        vmRulos.ToList().ForEach(x =>
                        {
                            if (!string.IsNullOrWhiteSpace(x.BatchNumbers))
                                colorList.Add(new Tuple<int, string>(x.RuloID, "ffc300"));
                            else if (x.CanContinue == "Yes")
                                colorList.Add(new Tuple<int, string>(x.RuloID, "66ff66"));
                            else if (x.TestCategoryCode.Contains(" X", StringComparison.InvariantCultureIgnoreCase)) //Ok X or Fail X
                                colorList.Add(new Tuple<int, string>(x.RuloID, "ff6666"));

                        });

                        exclude = new List<string>() { "IsToyotaValue", "TestCategoryID", "IsWaitingAnswerFromTestValue", "CanContinueValue", "OriginID" };
                        var fileResult2 = await export.ExportWithDisplayName<VMRulo>("Global Denim S.A. de C.V.", "Finishing", reportName, fileName, vmRulos.ToList(), exclude, colorList, "RuloID");

                        if (!fileResult2.Item1) return NotFound();

                        fileStreamResult = fileResult2.Item2;
                        //return File(fileResult.Item2.FileStream, fileResult.Item2.ContentType, fileName = fileResult.Item2.FileDownloadName);

                        break;
                    default:
                        break;
                }

            }
            catch (Exception ex)
            {
            }

            return fileStreamResult;
        }

        [HttpPost, Authorize(AuthenticationSchemes = SystemStatics.DefaultScheme, Roles = "ReportShow,ReportFull,AdminFull")]
        public async Task<IActionResult> ExportToPDF(VMReportFilter reportFilter)
        {
            MemoryStream ms = null;
            string contentType = "application/pdf";
            string reportPDFFileName = "PDF_Report.pdf";

            try
            {

                string titleNameReport = string.Empty;

                switch (reportFilter.typeReport)
                {
                    case 1:
                        reportPDFFileName = "Processes Completed Report.pdf";
                        titleNameReport = "Processes Completed Report From " + reportFilter.dtBegin.ToString("dd-MM-yyyy HH:mm") + " To " + reportFilter.dtEnd.ToString("dd-MM-yyyy HH:mm");
                        break;
                    case 2:
                        reportPDFFileName = "All Processes Report.pdf";
                        titleNameReport = "All Processes Report From " + reportFilter.dtBegin.ToString("dd-MM-yyyy HH:mm") + " To " + reportFilter.dtEnd.ToString("dd-MM-yyyy HH:mm");
                        break;
                    default:
                        break;
                }

                //var webReport = new WebReport();
                var webReport = new FastReport.Report();
                webReport.Report.Load(System.IO.Path.Combine(webHostEnvironment.WebRootPath, "..\\Reports\\CustomReport.frx"));
                FastReport.Export.PdfSimple.PDFSimpleExport export = null;

                var result = await factory.Rulos.GetFinishedFabricReport(reportFilter);
                //var result1 = await factory.Rulos.GetRuloFromRuloID(8);
                //var result2 = await factory.Rulos.GetRuloFromRuloID(9);
                //var result3 = await factory.Rulos.GetRuloFromRuloID(10);

                //List<Entities.Rulo> ruloList = new List<Entities.Rulo>()
                //{
                //    result1, result2, result3
                //};

                MsSqlDataConnection sqlConnection = new MsSqlDataConnection();
                sqlConnection.ConnectionString = "Server=.;Database=dbFinishingSystem;User Id=SA;Password=0545696sS*;";
                sqlConnection.CreateAllTables(true);
                FastReport.Utils.RegisteredObjects.AddConnection(typeof(FastReport.Data.MsSqlDataConnection));
                webReport.Dictionary.Connections.Add(sqlConnection);

                TableDataSource table = webReport.GetDataSource("tblCustomReport") as TableDataSource;
                table.SelectCommand = $"select * from tblCustomReport";

                //webReport.RegisterData(result, "CustomReports");
                webReport.Dictionary.Report.GetDataSource("tblCustomReport").Enabled = true;
                webReport.Dictionary.Report.SetParameterValue("TitleName", titleNameReport);

                //var a = webReport.GetDataSource("CustomReports");

                //webReport.Dictionary.Report.RegisterData(result, "TblCustomReports");
                //webReport.Dictionary.Report.GetDataSource("TblCustomReports").Enabled = true;
                //webReport.Dictionary.Report.SetParameterValue("TitleName", titleNameReport);

                webReport.Prepare(true);

                //TableDataSource table = webReport.Report.GetDataSource("tblRulos") as TableDataSource;
                //table.SelectCommand = $"select * from tblRulos where IsDeleted != 1 and RuloId in (7,8)";

                //string filePath = FastReport.Utils.Config.ApplicationFolder + reportPDFFileName;

                //byte[] fileBytes = null;

                //using (MemoryStream ms = new MemoryStream())
                //{
                ms = new MemoryStream();
                export = new FastReport.Export.PdfSimple.PDFSimpleExport();
                export.Export(webReport, ms);
                ms.Seek(0, SeekOrigin.Begin);

                //fileBytes = ms.ToArray();
                //}

                //export = new FastReport.Export.PdfSimple.PDFSimpleExport();
                //export.Export(webReport.Report, reportPDFFileName);

                //return View(webReport);
                return new FileStreamResult(ms, contentType) { FileDownloadName = reportPDFFileName };
            }
            catch (Exception ex)
            {
                return RedirectToAction(nameof(Index));
            }
            finally
            {
                if (ms != null)
                    ms.Flush();
            }

        }


    }
}
