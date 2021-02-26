using GD.FinishingSystem.Bussines;
using GD.FinishingSystem.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GD.FinishingSystem.WEB.Controllers
{
    [Authorize(AuthenticationSchemes = SystemStatics.DefaultScheme)]
    public class FloorController : Controller
    {
        FinishingSystemFactory factory;
        public FloorController()
        {
            factory = new FinishingSystemFactory();
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = SystemStatics.DefaultScheme, Roles = "FloorShow, FloorFull, AdminFull")]
        public async Task<IActionResult> Index()
        {
            var result = await factory.Floors.GetFloorList();

            return View(result);
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = SystemStatics.DefaultScheme, Roles = "FloorAdd, FloorFull, AdminFull")]
        public IActionResult Create()
        {
            ViewBag.Error = false;
            ViewBag.ErrorMessage = "";
            Floor newFloor = new Floor();

            return View("CreateOrUpdate", newFloor);
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = SystemStatics.DefaultScheme, Roles = "FloorUp, FloorFull, AdminFull")]
        public async Task<IActionResult> Edit(int floorID)
        {
            ViewBag.Error = false;
            ViewBag.ErrorMessage = "";

            Floor floor = await factory.Floors.GetFloorFromFloorID(floorID);
            if (floor == null)
                return RedirectToAction("Error", "Home");
            return View("CreateOrUpdate", floor);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Save(Floor floor)
        {
            ViewBag.Error = true;

            if (floor.FloorID == 0)
            {
                if (!(User.IsInRole("FloorAdd") || User.IsInRole("FloorFull") || User.IsInRole("AdminFull")))
                    return Unauthorized();

                await factory.Floors.Add(floor, int.Parse(User.Identity.Name));

            }
            else
            {
                if (!(User.IsInRole("FloorUp") || User.IsInRole("FloorFull") || User.IsInRole("AdminFull")))
                    return Unauthorized();




                await factory.Floors.Update(floor, int.Parse(User.Identity.Name));

            }
            return Redirect("Index");
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = SystemStatics.DefaultScheme, Roles = "FloorShow, FloorFull, AdminFull")]
        public async Task<IActionResult> Details(int floorID)
        {
            var floor = await factory.Floors.GetFloorFromFloorID(floorID);
            if (floor == null)
            {
                return RedirectToAction("Error", "Home");
            }

            return View(floor);
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = SystemStatics.DefaultScheme, Roles = "FloorDel, FloorFull, AdminFull")]
        public async Task<IActionResult> Delete(int floorID)
        {
            var floor = await factory.Floors.GetFloorFromFloorID(floorID);
            if (floor == null)
            {
                return RedirectToAction("Error", "Home");
            }

            return View(floor);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int floorID)
        {
            if (!(User.IsInRole("FloorDel") || User.IsInRole("FloorFull") || User.IsInRole("AdminFull")))
                return Unauthorized();

            var floor = await factory.Floors.GetFloorFromFloorID(floorID);

            await factory.Floors.Delete(floor, int.Parse(User.Identity.Name));

            return Redirect("Index");
        }

    }
}
