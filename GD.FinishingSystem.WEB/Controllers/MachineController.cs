using GD.FinishingSystem.Bussines;
using GD.FinishingSystem.Entities;
using GD.FinishingSystem.Entities.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GD.FinishingSystem.WEB.Controllers
{
    [Authorize(AuthenticationSchemes = SystemStatics.DefaultScheme)]
    public class MachineController : Controller
    {
        FinishingSystemFactory factory;
        public MachineController()
        {
            factory = new FinishingSystemFactory();
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = SystemStatics.DefaultScheme, Roles = "MachineShow, MachineFull, AdminFull")]
        public async Task<IActionResult> Index(int relDefinationProcessId)
        {

            ViewBag.RelDefinationProcessId = relDefinationProcessId;

            IEnumerable<VMMachine> result = null;
            if (relDefinationProcessId != 0)
                result = await factory.Machines.GetMachinesFromDefinationProcessID(relDefinationProcessId);

            return View(result);
        }


        [HttpGet]
        [Authorize(AuthenticationSchemes = SystemStatics.DefaultScheme, Roles = "MachineAdd, MachineFull, AdminFull")]
        public IActionResult Create(int relDefinationProcessId)
        {
            ViewBag.Error = false;
            ViewBag.ErrorMessage = "";
            Machine newMachine = new Machine();
            newMachine.DefinationProcessID = relDefinationProcessId;

            return View("CreateOrUpdate", newMachine);
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = SystemStatics.DefaultScheme, Roles = "MachineUp, MachineFull, AdminFull")]
        public async Task<IActionResult> Edit(int machineId)
        {
            ViewBag.Error = false;
            ViewBag.ErrorMessage = "";
            Machine machine = await factory.Machines.GetMachineFromMachineID(machineId);
            if (machine == null)
                return RedirectToAction("Error", "Home");
            return View("CreateOrUpdate", machine);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Save(Machine machine)
        {
            ViewBag.Error = true;

            if (machine.MachineID == 0)
            {
                if (!(User.IsInRole("MachineAdd") || User.IsInRole("MachineFull") || User.IsInRole("AdminFull")))
                    return Unauthorized();

                await factory.Machines.Add(machine, int.Parse(User.Identity.Name));

            }
            else
            {
                if (!(User.IsInRole("MachineUp") || User.IsInRole("MachineFull") || User.IsInRole("AdminFull")))
                    return Unauthorized();

                var foundMachine = await factory.Machines.GetMachineFromMachineID(machine.MachineID);

                foundMachine.MachineCode = machine.MachineCode;
                foundMachine.MachineName = machine.MachineName;

                await factory.Machines.Update(foundMachine, int.Parse(User.Identity.Name));

            }
            return Redirect("Index");
            //return RedirectToAction("Index", new { relDefinationProcessId = machine.DefinationProcessID });
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = SystemStatics.DefaultScheme, Roles = "MachineShow, MachineFull, AdminFull")]
        public async Task<IActionResult> Details(int machineId)
        {
            var machine = await factory.Machines.GetMachineFromMachineID(machineId);
            if (machine == null)
            {
                return RedirectToAction("Error", "Home");
            }

            return View(machine);
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = SystemStatics.DefaultScheme, Roles = "MachineDel, MachineFull, AdminFull")]
        public async Task<IActionResult> Delete(int machineId)
        {
            var machine = await factory.Machines.GetMachineFromMachineID(machineId);
            if (machine == null)
            {
                return RedirectToAction("Error", "Home");
            }

            return View(machine);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int machineId)
        {
            if (!(User.IsInRole("TestResultDel") || User.IsInRole("TestResultFull") || User.IsInRole("AdminFull")))
                return Unauthorized();

            var machine = await factory.Machines.GetMachineFromMachineID(machineId);

            await factory.Machines.Delete(machine, int.Parse(User.Identity.Name));

            return Redirect("Index");
        }
    }
}
