using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace GD.Finishing.WebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class FinishingController : ControllerBase
    {
        IFinishingService finishingService;
        public FinishingController(IFinishingService finishingService)
        {
            this.finishingService = finishingService;
        }

        // GET: FinishingController
        [HttpGet]
        public async Task<IActionResult> GetPackingList(int packingListID)
        {
            PackingList packingList = await finishingService.GetPackingList(packingListID);

            if (packingList == null)
                return NotFound();

            return Ok(packingList);
        }

        [HttpGet]
        public async Task<IActionResult> GetRuloMigration(int ruloMigrationID)
        {
            RuloMigrationModel packingListModel = await finishingService.GetRuloMigration(ruloMigrationID);

            if (packingListModel == null)
                return NotFound();

            return Ok(packingListModel);
        }

        [HttpGet]
        public async Task<IActionResult> GetRuloMigrationList(int packingListID)
        {
            IEnumerable<RuloMigrationModel> packingList = await finishingService.GetRuloMigrationList(packingListID);

            if (packingList == null)
                return NotFound();

            return Ok(packingList);
        }

        [HttpPut]
        public async Task<IActionResult> UpdatePackingListFinishing(int packingListID, List<RuloMigrationModel> ruloMigrationList, int userID)
        {
            int packinglistNo = await finishingService.UpdatePackingListFinishing(packingListID, ruloMigrationList, userID);

            return Ok(packinglistNo);
        }

    }
}
