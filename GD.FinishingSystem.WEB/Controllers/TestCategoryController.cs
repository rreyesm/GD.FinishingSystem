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
    public class TestCategoryController : Controller
    {
        FinishingSystemFactory factory;
        public TestCategoryController()
        {
            factory = new FinishingSystemFactory();
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = SystemStatics.DefaultScheme, Roles = "TestCategoryShow, TestCategoryFull, AdminFull")]
        public async Task<IActionResult> Index()
        {
            var result = await factory.TestCategories.GetTestCategoryList();
            return View(result);
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = SystemStatics.DefaultScheme, Roles = "TestCategoryAdd, TestCategoryFull, AdminFull")]
        public IActionResult Create()
        {
            ViewBag.Error = false;
            ViewBag.ErrorMessage = "";
            TestCategory newTestCategory = new TestCategory();

            return View("CreateOrUpdate", newTestCategory);
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = SystemStatics.DefaultScheme, Roles = "TestCategoryUp, TestCategoryFull, AdminFull")]
        public async Task<IActionResult> Edit(int TestCategoryID)
        {
            ViewBag.Error = false;
            ViewBag.ErrorMessage = "";

            TestCategory testCategory = await factory.TestCategories.GetTestCategoryFromTestCategoryID(TestCategoryID);
            if (testCategory == null)
                return RedirectToAction("Error", "Home");
            return View("CreateOrUpdate", testCategory);
        }


        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Save(TestCategory testCategory)
        {
            ViewBag.Error = true;

            if (testCategory.TestCategoryID == 0)
            {
                if (!(User.IsInRole("TestCategoryAdd") || User.IsInRole("TestCategoryFull") || User.IsInRole("AdminFull")))
                    return Unauthorized();

                //Validate code
                var foundTestCategory = await factory.TestCategories.GetTestCategoryFromTestCategoryCode(testCategory.TestCode);

                if (foundTestCategory != null)
                {
                    ViewBag.ErrorMessage = "Test code already exist";
                    return View("CreateOrUpdate", testCategory);
                }

                await factory.TestCategories.Add(testCategory, int.Parse(User.Identity.Name));

            }
            else
            {
                if (!(User.IsInRole("TestCategoryUp") || User.IsInRole("TestCategoryFull") || User.IsInRole("AdminFull")))
                    return Unauthorized();

                var foundTestCategory = await factory.TestCategories.GetTestCategoryFromTestCategoryID(testCategory.TestCategoryID);

                foundTestCategory.TestCode = testCategory.TestCode;
                foundTestCategory.Name = testCategory.Name;

                await factory.TestCategories.Update(foundTestCategory, int.Parse(User.Identity.Name));

            }
            return Redirect("Index");
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = SystemStatics.DefaultScheme, Roles = "TestCategoryShow, TestCategoryFull, AdminFull")]
        public async Task<IActionResult> Details(int testCategoryID)
        {
            var testCategory = await factory.TestCategories.GetTestCategoryFromTestCategoryID(testCategoryID);
            if (testCategory == null)
            {
                return RedirectToAction("Error", "Home");
            }

            return View(testCategory);
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = SystemStatics.DefaultScheme, Roles = "TestCategoryDel, TestCategoryFull, AdminFull")]
        public async Task<IActionResult> Delete(int testCategoryID)
        {
            var originCategory = await factory.TestCategories.GetTestCategoryFromTestCategoryID(testCategoryID);
            if (originCategory == null)
            {
                return RedirectToAction("Error", "Home");
            }

            return View(originCategory);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int testCategoryID)
        {
            if (!(User.IsInRole("TestCategoryDel") || User.IsInRole("TestCategoryFull") || User.IsInRole("AdminFull")))
                return Unauthorized();

            var originCategory = await factory.TestCategories.GetTestCategoryFromTestCategoryID(testCategoryID);

            await factory.TestCategories.Delete(originCategory, int.Parse(User.Identity.Name));

            return Redirect("Index");
        }

    }
}
