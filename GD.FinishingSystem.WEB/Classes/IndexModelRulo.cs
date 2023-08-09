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

        public int TotalPages { get; set; }

        public async Task OnGetAsync(string sortOrder, int? pageIndex, VMRuloFilters ruloFilters)
        {
            CurrentSort = sortOrder;
            NameSort = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            DateSort = sortOrder == "Date" ? "date_desc" : "Date";

            int currentPageIngex = pageIndex ?? 1;
            int pageSize = _appSettings.PageSize != 0 ? _appSettings.PageSize : 5;
            int totalRecords = await factory.Rulos.GetRuloTotalRecords(ruloFilters);
            var result = await factory.Rulos.GetRuloListFromFilters(ruloFilters, currentPageIngex, pageSize);

            if (ruloFilters.IsAccountingDate)
            {
                ruloFilters.dtEnd = ruloFilters.dtEnd.AccountEndDate();
            }
            CurrentVMRuloFilters = ruloFilters;

            VMRuloList = PaginatedList<VMRulo>.CreatePaginated(result, totalRecords, currentPageIngex, pageSize);

            ////Ya no sería necesario obtener por separado la información de Guven
            //var guvenInformation = await factory.Rulos.GetGuvenInformation(VMRuloList.Select(x => x.RuloID));
            //VMRuloList.ForEach(x =>
            //{
            //    x.BatchNumbers = string.Join(",", guvenInformation.Where(y => y.RuloID == x.RuloID).Select(y => y.BatchNumbers));
            //    x.InspectionLength = guvenInformation.Where(y => y.RuloID == x.RuloID).Sum(y => y.Inspectionlength);
            //    x.InspectionCuttingLength = guvenInformation.Where(y => y.RuloID == x.RuloID).Sum(y => y.CuttingLenght);
            //    x.ExitLengthMinusSamples = x.ExitLength - factory.Rulos.GetSumSamples(x.RuloID);
            //});
        }
    }

}
