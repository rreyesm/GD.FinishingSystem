using GD.FinishingSystem.Bussines;
using GD.FinishingSystem.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace GD.FinishingSystem.WEB.Controllers
{
    [Authorize(AuthenticationSchemes = SystemStatics.DefaultScheme)]
    public class TestResultController : Controller
    {
        FinishingSystemFactory factory;
        public TestResultController()
        {
            factory = new FinishingSystemFactory();
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = SystemStatics.DefaultScheme, Roles = "TestResultShow, TestResultFull, AdminFull")]
        public async Task<IActionResult> Index()
        {
            DateTime dtBegin = DateTime.Today.AddMonths(-1);
            DateTime dtEnd = DateTime.Today.AddDays(1).AddMilliseconds(-1);

            var result = await factory.TestResults.GetTestResultListFromBetweenDate(dtBegin, dtEnd);
            SetViewBagsForDates(dtBegin, dtEnd);

            return View(result);
        }

        private void SetViewBagsForDates(DateTime dtBegin, DateTime dtEnd)
        {
            ViewBag.dtBegin = dtBegin.ToString("yyyy-MM-dd");
            ViewBag.dtEnd = dtEnd.ToString("yyyy-MM-dd");
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = SystemStatics.DefaultScheme, Roles = "TestResultShow, TestResultFull, AdminFull")]
        public async Task<IActionResult> Index(DateTime dtBegin, DateTime dtEnd)
        {
            dtEnd = dtEnd.AddDays(1).AddMilliseconds(-1);

            var result = await factory.TestResults.GetTestResultListFromBetweenDate(dtBegin, dtEnd);
            SetViewBagsForDates(dtBegin, dtEnd);

            return View(result);
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = SystemStatics.DefaultScheme, Roles = "TestResultAdd, TestResultFull, AdminFull")]
        public IActionResult Create(int relRuloId)
        {
            ViewBag.Error = false;
            ViewBag.ErrorMessage = "";
            TestResult newTestResult = new TestResult();

            ViewBag.RuloId = relRuloId;

            return View("CreateOrUpdate", newTestResult);
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = SystemStatics.DefaultScheme, Roles = "TestResultUp, TestResultFull, AdminFull")]
        public async Task<IActionResult> Edit(int testResultId, int relRuloId)
        {
            if (relRuloId != 0)
            {
                var relRulo = await factory.Rulos.GetRuloFromRuloID(relRuloId);
                if (relRulo == null)
                    return RedirectToAction("Error", "Home");

                if (relRulo.TestResultID == null)
                    return RedirectToAction("Error", "Home");

                testResultId = (int)relRulo.TestResultID;
                

            }

            ViewBag.Error = false;
            ViewBag.ErrorMessage = "";

            ViewBag.RuloId = relRuloId;
            TestResult testResult = await factory.TestResults.GetTestResultFromTestResultID(testResultId);
            if (testResult == null)
                return RedirectToAction("Error", "Home");
            return View("CreateOrUpdate", testResult);
        }

        //[HttpPost, ValidateAntiForgeryToken]
        //public async Task<IActionResult> Save(TestResult testResult)
        //{
        //    //ViewBag.Error = false;
        //    //ViewBag.ErrorMessage = string.Empty;
        //    //ViewBag.RuloId = testResult.RelRuloId;

        //    //if (testResult.TestResultID == 0)
        //    //{
        //    //    if (!(User.IsInRole("TestResultAdd") || User.IsInRole("TestResultFull") || User.IsInRole("AdminFull")))
        //    //        return Unauthorized();

        //    //    //Validation rule exist
        //    //    var rulo = await factory.Rulos.GetRuloFromRuloID(testResult.RelRuloId);
        //    //    if (rulo != null && rulo.TestResultID != null)
        //    //    {
        //    //        ViewBag.Error = true;
        //    //        ViewBag.ErrorMessage = "There is already a related test for this rulo. Edit the existing test.";

        //    //        return View("CreateOrUpdate", testResult);
        //    //    }

        //    //    await factory.TestResults.Add(testResult, int.Parse(User.Identity.Name));
        //    //}
        //    //else
        //    //{
        //    //    if (!(User.IsInRole("TestResultUp") || User.IsInRole("TestResultFull") || User.IsInRole("AdminFull")))
        //    //        return Unauthorized();

        //    //    var foundTestResult = await factory.TestResults.GetTestResultFromTestResultID(testResult.TestResultID);

        //    //    foundTestResult.Details = testResult.Details;
        //    //    foundTestResult.CanContinue = testResult.CanContinue;

        //    //    await factory.TestResults.Update(foundTestResult, int.Parse(User.Identity.Name));
        //    //}

        //    //var intUser = int.Parse(User.Identity.Name);
        //    //if (testResult.CanContinue)
        //    //    await factory.Rulos.SetTestResult(testResult.RelRuloId, testResult.TestResultID, intUser, intUser);
        //    //else
        //    //    await factory.Rulos.SetTestResult(testResult.RelRuloId, testResult.TestResultID, null, intUser);


        //    return RedirectToAction("Index", "Rulo");
        //}

        [HttpGet]
        [Authorize(AuthenticationSchemes = SystemStatics.DefaultScheme, Roles = "TestResultShow, TestResultFull, AdminFull")]
        public async Task<IActionResult> Details(int testResultId)
        {
            var testResult = await factory.TestResults.GetTestResultFromTestResultID(testResultId);
            if (testResult == null)
            {
                return RedirectToAction("Error", "Home");
            }

            return View(testResult);
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = SystemStatics.DefaultScheme, Roles = "TestResultDel, TestResultFull, AdminFull")]
        public async Task<IActionResult> Delete(int testResultId)
        {
            var testResult = await factory.TestResults.GetTestResultFromTestResultID(testResultId);
            if (testResult == null)
            {
                return RedirectToAction("Error", "Home");
            }

            return View(testResult);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int testResultId)
        {
            if (!(User.IsInRole("TestResultDel") || User.IsInRole("TestResultFull") || User.IsInRole("AdminFull")))
                return Unauthorized();

            var testResult = await factory.TestResults.GetTestResultFromTestResultID(testResultId);

            await factory.TestResults.Delete(testResult, int.Parse(User.Identity.Name));

            return Redirect("Index");
        }

    }
}
