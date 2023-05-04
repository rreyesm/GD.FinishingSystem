using GD.FinishingSystem.Bussines;
using GD.FinishingSystem.Entities.ViewModels;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GD.FinishingSystem.WEB.Classes
{
    public class IndexModelRulo
    {
        private AppSettings _appSettings;
        FinishingSystemFactory factory;

        public IndexModelRulo(FinishingSystemFactory factory, AppSettings appSettings)
        {
            this._appSettings = appSettings;
            this.factory = factory;
        }

        public string NameSort { get; set; }
        public string DateSort { get; set; }
        public string CurrentFilter { get; set; }
        public VMRuloFilters CurrentVMRuloFilters { get; set; }
        public string CurrentSort { get; set; }

        public PaginatedList<VMRulo> VMRuloList { get; set; }

        public async Task OnGetAsync(string sortOrder, int? pageIndex, VMRuloFilters ruloFilters)
        {
            CurrentSort = sortOrder;
            NameSort = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            DateSort = sortOrder == "Date" ? "date_desc" : "Date";

            CurrentVMRuloFilters = ruloFilters;

            var result = await factory.Rulos.GetRuloListFromFilters(ruloFilters);

            var pageSize = _appSettings.PageSize != 0 ? _appSettings.PageSize : 5;
            VMRuloList = PaginatedList<VMRulo>.CreatePaginated(result, pageIndex ?? 1, int.Parse(pageSize.ToString()));

            var guvenInformation = await factory.Rulos.GetGuvenInformation(VMRuloList.Select(x => x.RuloID));
            VMRuloList.ForEach(x =>
            {
                x.BatchNumbers = string.Join(",", guvenInformation.Where(y => y.RuloID == x.RuloID).Select(y => y.BatchNumbers));
                x.InspectionLength = guvenInformation.Where(y => y.RuloID == x.RuloID).Sum(y => y.Inspectionlength);
            });
        }
    }

}
