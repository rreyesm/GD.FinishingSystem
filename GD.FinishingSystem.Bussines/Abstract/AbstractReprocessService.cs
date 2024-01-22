using GD.FinishingSystem.Entities;
using GD.FinishingSystem.Entities.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GD.FinishingSystem.Bussines.Abstract
{
    public abstract class AbstractReprocessService
    {
        public abstract Task<IEnumerable<Reprocess>> GetReprocessListFromFilters(VMRuloFilters ruloFilters);
        public abstract Task<Reprocess> GetReprocess(int reprocessID);
        public abstract Task<decimal> GetTotalMetersByReprocess(string lote, int beam);
    }
}
