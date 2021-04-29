using GD.FinishingSystem.Bussines.Abstract;
using GD.FinishingSystem.DAL.Abstract;
using GD.FinishingSystem.DAL.Concrete;
using GD.FinishingSystem.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GD.FinishingSystem.Bussines.Concrete
{
    public class PeriodManager : AbstractPeriodService
    {
        private IAsyncRepository<Period> repository = null;
        public PeriodManager(DbContext context)
        {
            this.repository = new GenericRepository<Period>(context);
        }
        public async override Task Add(Period period, int adderRef)
        {
            await repository.Add(period, adderRef);
        }

        public async override Task Delete(Period period, int deleterRef)
        {
            await repository.Remove(period.PeriodID, deleterRef);
        }

        public async override Task<Period> GetCurrentPeriod(int? systemPrinterId)
        {
            var result = await repository.FirstOrDefault(o => !o.IsDeleted && o.FinishDate == null && o.SystemPrinterID == systemPrinterId);
            return result;
        }

        public async override Task<IEnumerable<Period>> GetCurrentPeriods()
        {
            var result = await repository.GetWhere(x => !x.IsDeleted && x.FinishDate == null);
            return result;
        }

        public async override Task<Period> GetPeriodFromPeriodID(int periodID)
        {
            return await repository.GetByPrimaryKey(periodID);
        }

        public async override Task<IEnumerable<Period>> GetPeriodList()
        {
            var periods = await repository.GetWhere(x => !x.IsDeleted);

            return periods.OrderByDescending(x => x.PeriodID);
        }

        public async override Task Update(Period period, int updaterRef)
        {
            await repository.Update(period, updaterRef);
        }
    }
}
