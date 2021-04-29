using GD.FinishingSystem.Bussines;
using GD.FinishingSystem.Entities;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GD.FinishingSystem.WEB.Classes
{
    public class IndexModelPeriod
    {
        private readonly IConfiguration Configuration;
        FinishingSystemFactory factory;

        public IndexModelPeriod(FinishingSystemFactory factory, IConfiguration configuration)
        {
            Configuration = configuration;
            this.factory = factory;
        }

        public string NameSort { get; set; }
        public string DateSort { get; set; }
        public string CurrentFilter { get; set; }
        public string CurrentSort { get; set; }

        public PaginatedList<Period> PeriodList { get; set; }

        public async Task OnGetAsync(string sortOrder, int? pageIndex)
        {
            CurrentSort = sortOrder;

            var result = await factory.Periods.GetPeriodList();

            var pageSize = Configuration.GetValue("PageSize", 4);
            PeriodList = PaginatedList<Period>.CreatePaginated(result, pageIndex ?? 1, pageSize);
        }
    }
}
