//using FastReport.Data;
//using FastReport.Web;
using GD.FinishingSystem.Bussines;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GD.FinishingSystem.WEB.Controllers
{
    [Authorize(AuthenticationSchemes = SystemStatics.DefaultScheme)]
    public class PDFReportController : Controller
    {
        private readonly IWebHostEnvironment webHostEnvironment = null;
        FinishingSystemFactory factory = null;
        public PDFReportController(IWebHostEnvironment webHostEnvironment)
        {
            this.webHostEnvironment = webHostEnvironment;
            factory = new FinishingSystemFactory();
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet, Authorize(AuthenticationSchemes = SystemStatics.DefaultScheme, Roles = "PDFReportShow,PDFReportFull,AdminFull")]
        [HttpGet]
        public async Task<IActionResult> Report()
        {
            DateTime dtBegin = DateTime.Today.AddMonths(-1);
            DateTime dtEnd = DateTime.Today;

            //var webReport = new WebReport();
            //webReport.Report.Load(System.IO.Path.Combine(webHostEnvironment.WebRootPath, "..\\Reports\\RulosPDFReport.frx"));

            //dtEnd = dtEnd.AddDays(1).AddMilliseconds(-1);

            //var rulos = await factory.Rulos.GetRuloListFromBetweenDate(dtBegin, dtEnd);
            //rulos = rulos.Where(x => x.RuloID == 8 || x.RuloID == 9).ToList();

            //webReport.Report.RegisterData(rulos, "VMRulo");
            //webReport.Report.GetDataSource("VMRulo").Enabled = true;

            ////TableDataSource table = webReport.Report.GetDataSource("tblRulos") as TableDataSource;
            ////table.SelectCommand = $"select * from tblRulos where IsDeleted != 1 and RuloId in (7,8)";

            //return View(webReport);
            return View();
        }
    }
}
