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
    public class OriginCategoryController : Controller
    {
        FinishingSystemFactory factory;
        public OriginCategoryController()
        {
            factory = new FinishingSystemFactory();
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = SystemStatics.DefaultScheme, Roles = "OriginCategoryShow, OriginCategoryFull, AdminFull")]
        public async Task<IActionResult> Index()
        {
            var result = await factory.OriginCategories.GetOriginCategoryList();
            return View(result);
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = SystemStatics.DefaultScheme, Roles = "OriginCategoryAdd, OriginCategoryFull, AdminFull")]
        public IActionResult Create()
        {
            ViewBag.Error = false;
            ViewBag.ErrorMessage = "";
            OriginCategory newOriginCategory = new OriginCategory();

            return View("CreateOrUpdate", newOriginCategory);
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = SystemStatics.DefaultScheme, Roles = "OriginCategoryUp, OriginCategoryFull, AdminFull")]
        public async Task<IActionResult> Edit(int originCategoryID)
        {
            ViewBag.Error = false;
            ViewBag.ErrorMessage = "";

            OriginCategory originCategory = await factory.OriginCategories.GetOriginCategoryFromOriginCategoryID(originCategoryID);
            if (originCategory == null)
                return RedirectToAction("Error", "Home");
            return View("CreateOrUpdate", originCategory);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Save(OriginCategory originCategory)
        {
            ViewBag.Error = true;

            if (originCategory.OriginCategoryID == 0)
            {
                if (!(User.IsInRole("OriginCategoryAdd") || User.IsInRole("OriginCategoryFull") || User.IsInRole("AdminFull")))
                    return Unauthorized();

                //Validate code
                var foundOriginCategory = await factory.OriginCategories.GetOriginCategoryFromOriginCategoryCode(originCategory.OriginCode);

                if (foundOriginCategory != null)
                {
                    ViewBag.ErrorMessage = "Origin code already exist";
                    return View("CreateOrUpdate", originCategory);
                }

                await factory.OriginCategories.Add(originCategory, int.Parse(User.Identity.Name));

            }
            else
            {
                if (!(User.IsInRole("OriginCategoryUp") || User.IsInRole("OriginCategoryFull") || User.IsInRole("AdminFull")))
                    return Unauthorized();

                var foundOriginCategory = await factory.OriginCategories.GetOriginCategoryFromOriginCategoryID(originCategory.OriginCategoryID);

                foundOriginCategory.OriginCode = originCategory.OriginCode;
                foundOriginCategory.Name = originCategory.Name;

                await factory.OriginCategories.Update(foundOriginCategory, int.Parse(User.Identity.Name));

            }
            return Redirect("Index");
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = SystemStatics.DefaultScheme, Roles = "OriginCategoryShow, OriginCategoryFull, AdminFull")]
        public async Task<IActionResult> Details(int originCategoryID)
        {
            var originCategory = await factory.OriginCategories.GetOriginCategoryFromOriginCategoryID(originCategoryID);
            if (originCategory == null)
            {
                return RedirectToAction("Error", "Home");
            }

            return View(originCategory);
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = SystemStatics.DefaultScheme, Roles = "OriginCategoryDel, OriginCategoryFull, AdminFull")]
        public async Task<IActionResult> Delete(int originCategoryID)
        {
            var originCategory = await factory.OriginCategories.GetOriginCategoryFromOriginCategoryID(originCategoryID);
            if (originCategory == null)
            {
                return RedirectToAction("Error", "Home");
            }

            return View(originCategory);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int originCategoryID)
        {
            if (!(User.IsInRole("OriginCategoryDel") || User.IsInRole("OriginCategoryFull") || User.IsInRole("AdminFull")))
                return Unauthorized();

            var originCategory = await factory.OriginCategories.GetOriginCategoryFromOriginCategoryID(originCategoryID);

            await factory.OriginCategories.Delete(originCategory, int.Parse(User.Identity.Name));

            return Redirect("Index");
        }

    }
}
