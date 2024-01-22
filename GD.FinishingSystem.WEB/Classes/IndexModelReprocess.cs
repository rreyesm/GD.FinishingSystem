using GD.FinishingSystem.Bussines;
using GD.FinishingSystem.Entities.ViewModels;
using GD.FinishingSystem.Entities;
using System.Threading.Tasks;
using System;

namespace GD.FinishingSystem.WEB.Classes
{
    public class IndexModelReprocess
    {
        FinishingSystemFactory factory;
        AppSettings appSettings;
        public IndexModelReprocess(FinishingSystemFactory factory, AppSettings appSettings)
        {
            this.factory = factory;
            this.appSettings = appSettings;
        }

        public string NameSort { get; set; }
        public string DateSort { get; set; }
        public string CurrentFilter { get; set; }
        public VMRuloFilters CurrentVMRuloFilters { get; set; }
        public string CurrentSort { get; set; }

        public PaginatedList<Reprocess> ReprocessList { get; set; }

        public async Task OnGetAsync(string sortOrder, int? pageIndex, VMRuloFilters ruloFilters)
        {
            CurrentSort = sortOrder;
            NameSort = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            DateSort = sortOrder == "Date" ? "date_desc" : "Date";

            if (ruloFilters.IsAccountingDate)
            {
                ruloFilters.dtEnd = ruloFilters.dtEnd.AccountEndDate();
            }

            CurrentVMRuloFilters = ruloFilters;

            var result = await factory.Reprocesses.GetReprocessListFromFilters(ruloFilters);

            var pageSize = appSettings.PageSize != 0 ? appSettings.PageSize : 5;
            ReprocessList = PaginatedList<Reprocess>.CreatePaginated(result, pageIndex ?? 1, pageSize);
        }
    }

}
