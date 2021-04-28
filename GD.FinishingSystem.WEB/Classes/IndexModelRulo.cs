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
        private readonly IConfiguration Configuration;
        FinishingSystemFactory factory;

        public IndexModelRulo(FinishingSystemFactory factory, IConfiguration configuration)
        {
            Configuration = configuration;
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

            var pageSize = Configuration.GetValue("PageSize", 4);
            VMRuloList = PaginatedList<VMRulo>.CreatePaginated(result, pageIndex ?? 1, pageSize);
        }
    }

}
