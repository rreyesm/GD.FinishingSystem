using GD.FinishingSystem.Bussines;
using GD.FinishingSystem.Entities;
using GD.FinishingSystem.Entities.ViewModels;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GD.FinishingSystem.WEB.Classes
{
    public class IndexModelRuloMigration
    {
        FinishingSystemFactory factory;
        AppSettings appSettings;
        public IndexModelRuloMigration(FinishingSystemFactory factory, AppSettings appSettings)
        {
            this.factory = factory;
            this.appSettings = appSettings;
        }

        public string NameSort { get; set; }
        public string DateSort { get; set; }
        public string CurrentFilter { get; set; }
        public VMRuloFilters CurrentVMRuloFilters { get; set; }
        public string CurrentSort { get; set; }

        public PaginatedList<RuloMigration> RuloMigrationList { get; set; }

        public async Task OnGetAsync(string sortOrder, int? pageIndex, VMRuloFilters ruloFilters)
        {
            CurrentSort = sortOrder;
            NameSort = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            DateSort = sortOrder == "Date" ? "date_desc" : "Date";

            CurrentVMRuloFilters = ruloFilters;

            var result = await factory.RuloMigrations.GetRuloMigrationListFromFilters(ruloFilters);

            var pageSize = appSettings.PageSize != 0 ? appSettings.PageSize : 5;
            RuloMigrationList = PaginatedList<RuloMigration>.CreatePaginated(result, pageIndex ?? 1, pageSize);
        }
    }
}
