using GD.FinishingSystem.Bussines;
using GD.FinishingSystem.Entities;
using GD.FinishingSystem.Entities.ViewModels;
using GD.FinishingSystem.WEB.Classes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Options;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace GD.FinishingSystem.WEB.Controllers
{
    [Authorize(AuthenticationSchemes = SystemStatics.DefaultScheme)]
    public class ReprocessController : Controller
    {
        FinishingSystemFactory factory;
        private AppSettings _appSettings;

        IndexModelReprocess indexModelReprocess = null;

        public ReprocessController(IOptions<AppSettings> options)
        {
            _appSettings = options.Value;
        }

        public async Task<IActionResult> Index(int positionReprocessID = 0, string currentFilter = null, int? pageIndex = null)
        {
            if (!User.IsInRole("Reprocess", AuthType.Show)) return Unauthorized();

            VMRuloFilters rulofilters = null;
            if (currentFilter != null)
            {
                rulofilters = new VMRuloFilters();
                rulofilters.dtEnd = DateTime.Today.AddDays(-15);
                rulofilters.dtEnd = DateTime.Today.RealDateEndDate();
            }
            else
            {
                rulofilters = System.Text.Json.JsonSerializer.Deserialize<VMRuloFilters>(currentFilter);
            }

            await SetViewBagsForDates(rulofilters, positionReprocessID);

            await indexModelReprocess.OnGetAsync("", pageIndex, rulofilters);

            return View(indexModelReprocess);
        }

        [HttpPost]
        public async Task<IActionResult> Index(VMRuloFilters ruloFilters)
        {
            if (!User.IsInRole("Reprocess", AuthType.Show)) return Unauthorized();

            ruloFilters.dtEnd = ruloFilters.dtEnd.RealDateEndDate();
            await SetViewBagsForDates(ruloFilters);

            await indexModelReprocess.OnGetAsync("", 1, ruloFilters);

            return View(indexModelReprocess);
        }

        private async Task SetViewBagsForDates(VMRuloFilters ruloFilters, int positionReprocessID = 0)
        {
            ViewBag.Error = false;
            ViewBag.ErrorMessage = string.Empty;

            ViewBag.dtBegin = ruloFilters.dtBegin.ToString("yyyy-MM-ddTHH:mm:ss");
            ViewBag.dtEnd = ruloFilters.dtEnd.ToString("yyyy-MM-ddTHH:mm:ss");

            ViewBag.numLote = ruloFilters.numLote;
            ViewBag.numBeam = ruloFilters.numBeam;
            ViewBag.numLoom = ruloFilters.numLoom;
            ViewBag.txtStyle = ruloFilters.txtStyle;
            ViewBag.numReprocessID = ruloFilters.numRuloMigrationID;

            var definitionProcessess = await factory.RuloMigrations.GetDefinitionProcessList();
            var definitionProcessessList = WebUtilities.Create<DefinationProcess>(definitionProcessess, "DefinationProcessID", "Name", true);

            var originCategories = await factory.RuloMigrations.GetOriginCategoryList();
            originCategories = originCategories.Where(x => x.OriginCategoryID == 1 || x.OriginCategoryID == 7); //1=PP00, 7=DES0
            var originCategoryList = WebUtilities.Create<OriginCategory>(originCategories, "OriginCategoryID", "OriginCode", true);

            ViewBag.DefinitionProcessList = definitionProcessessList;
            ViewBag.OriginCategoryList = originCategoryList;

            //Style list
            var styleDataList = await factory.Rulos.GetRuloStyleStringForProductionLoteList();
            ViewBag.StyleList = styleDataList.ToList();


            ViewBag.PositionReprocessID = positionReprocessID;

        }

        [HttpGet]
        public async Task<IActionResult> ValidateCreateRuloReprocess(int reprocessID, string lote)
        {
            if (!User.IsInRole("Rulo", AuthType.Add) || !User.IsInRole("Rulo", AuthType.Full) || !User.IsInRole("Admin", AuthType.Full)) return Unauthorized();

            string errorMessage = string.Empty;

            var reprocess = await factory.Reprocesses.GetReprocess(reprocessID);
            if (reprocess == null)
                errorMessage = "El reproceso no existe.";
            else if (reprocess != null && reprocess.RuloID != null)
                errorMessage = "El reproceso ya tiene un número de Rulo asignado.";

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
        public async Task<IActionResult> CreateRulo(int reprocessID)
        {
            if (!User.IsInRole("Rulo", AuthType.Add) || !User.IsInRole("Rulo", AuthType.Full) || !User.IsInRole("Admin", AuthType.Full)) return Unauthorized();

            var reprocess = await factory.Reprocesses.GetReprocess(reprocessID);

            var totalMeters = await factory.Reprocesses.GetTotalMetersByReprocess(reprocess.Lote, reprocess.Beam);

            //TempData["ruloMigrationId1"] = processID;

            Rulo newRulo = new Rulo();
            newRulo.Lote = reprocess.Lote.ToString();
            newRulo.Beam = reprocess.Beam;
            newRulo.Loom = reprocess.Loom;
            newRulo.Style = reprocess.Style;
            newRulo.StyleName = reprocess.StyleName;

            var systemPrinter = await WebUtilities.GetSystemPrinter(factory, this.HttpContext);
            var currentPeriod = await factory.Periods.GetCurrentPeriod(systemPrinter.SystemPrinterID);

            if (newRulo.SentAuthorizerID == 0) newRulo.SentAuthorizerID = null;
            newRulo.PeriodID = currentPeriod.PeriodID;

            //newRulo.WeavingLength = totalMeters;
            newRulo.EntranceLength = totalMeters;

            newRulo.Shift = GetTurno(DateTime.Now); //This Shift is from Loom
            newRulo.MainOriginID = reprocess.MainOriginID;
            newRulo.OriginID = reprocess.OriginID;
            newRulo.Observations = $"Obs Roll: {reprocess.RollObs}. Obs Pallet{reprocess.PalletObs}";

            return RedirectToAction("Index", "Rulo", newRulo);
        }

        private int GetTurno(DateTime date)
        {
            if (date.Hour < 7 || date.Hour == 23) return 3;
            else if (date.Hour < 15) return 1;
            else return 2;
        }

    }
}
