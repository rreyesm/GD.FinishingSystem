using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GD.FinishingSystem.Bussines;
using GD.FinishingSystem.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GD.FinishingSystem.WEB.Controllers
{
    [Authorize(AuthenticationSchemes = SystemStatics.DefaultScheme)]
    public class SystemPrinterController : Controller
    {
        FinishingSystemFactory factory;
        public SystemPrinterController()
        {
            factory = new FinishingSystemFactory();
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = SystemStatics.DefaultScheme, Roles = "SystemPrinterShow, SystemPrinterFull, AdminFull")]
        public async Task<IActionResult> Index()
        {
            var result = await factory.SystemPrinters.GetSystemPrinterList();

            return View();
        }


        [HttpGet]
        [Authorize(AuthenticationSchemes = SystemStatics.DefaultScheme, Roles = "SystemPrinterAdd, SystemPrinterFull, AdminFull")]
        public IActionResult Create()
        {
            ViewBag.Error = false;
            ViewBag.ErrorMessage = string.Empty;

            SystemPrinter systemPrinter = new SystemPrinter();

            return View("CreateOrUpdate", systemPrinter);
        }

        [Authorize(AuthenticationSchemes = SystemStatics.DefaultScheme, Roles = "SystemPrinterUp, SystemPrinterFull, AdminFull")]
        public async Task<IActionResult> Edit(int systemPrinterId)
        {
            ViewBag.Error = false;
            ViewBag.ErrorMessage = string.Empty;

            SystemPrinter systemPrinter = await factory.SystemPrinters.GetSystemPrinterFromSystemPrinterID(systemPrinterId);

            if (systemPrinter == null) return NotFound();

            return View("CreateOrUpdate", systemPrinter);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Save(SystemPrinter systemPrinter)
        {
            ViewBag.Error = false;

            if (systemPrinter.SystemPrinterID == 0)
            {
                if (!User.IsInRole("SystemPrinter", AuthType.Add)) return Unauthorized();

                await factory.SystemPrinters.Add(systemPrinter, int.Parse(User.Identity.Name));
            }
            else
            {
                if (!User.IsInRole("SystemPrinter", AuthType.Update)) return Unauthorized();

                await factory.SystemPrinters.Update(systemPrinter, int.Parse(User.Identity.Name));
            }

            return Redirect("Index");
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = SystemStatics.DefaultScheme, Roles = "SystemPrinterShow, SystemPrinterFull, AdminFull")]
        public async Task<IActionResult> Details(int systemPrinterId)
        {
            var systemPrinter = await factory.SystemPrinters.GetSystemPrinterFromSystemPrinterID(systemPrinterId);
            if (systemPrinter == null) return NotFound();

            return View(systemPrinter);
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = SystemStatics.DefaultScheme, Roles = "SystemPrinterDel, SystemPrinterFull, AdminFull")]
        public async Task<IActionResult> Delete(int systemPrinterId)
        {
            var systemPrinter = await factory.SystemPrinters.GetSystemPrinterFromSystemPrinterID(systemPrinterId);
            if (systemPrinter == null) return NotFound();

            return View(systemPrinter);
        }

        [HttpPost, ActionName("Delete"), ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int systemPrinterId)
        {
            if (!User.IsInRole("SystemPrinter", AuthType.Delete)) return Unauthorized();

            var systemPrinter = await factory.SystemPrinters.GetSystemPrinterFromSystemPrinterID(systemPrinterId);

            await factory.SystemPrinters.Delete(systemPrinter, int.Parse(User.Identity.Name));

            return Redirect("Index");
        }

    }
}
