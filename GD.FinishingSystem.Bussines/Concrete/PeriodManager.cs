using GD.FinishingSystem.Bussines.Abstract;
using GD.FinishingSystem.DAL.Abstract;
using GD.FinishingSystem.DAL.Concrete;
using GD.FinishingSystem.Entities;
using GD.FinishingSystem.Entities.ViewModels;
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
        private IAsyncRepository<SystemPrinter> repositorySystemPrinter = null;
        private IAsyncRepository<Machine> repositoryMachine = null;
        public PeriodManager(DbContext context)
        {
            this.repository = new GenericRepository<Period>(context);
            this.repositorySystemPrinter = new GenericRepository<SystemPrinter>(context);
            this.repositoryMachine = new GenericRepository<Machine>(context);
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
            //Fix bug for when there is more than one PC assigned to a finishing machine.
            var systemPrinter = await repositorySystemPrinter.FirstOrDefault(x => x.SystemPrinterID == systemPrinterId);

            //Get all of ID SystemPrinter that it is assigned to finishing machine
            var machineList = await repositorySystemPrinter.GetWhere(x=> x.MachineID == systemPrinter.MachineID);
            List<int> machines = machineList.Select(x => x.SystemPrinterID).ToList();

            //var result = await repository.FirstOrDefault(o => !o.IsDeleted && o.FinishDate == null && o.SystemPrinterID == systemPrinterId);

            //Search all PCs relationship with machine
            var result = await repository.FirstOrDefault(o => !o.IsDeleted && o.FinishDate == null && machines.Contains((int)o.SystemPrinterID));

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

        public async override Task<IEnumerable<VMPeriodReport>> GetVMPeriodList()
        {
            var result = repository.GetQueryable(x => !x.IsDeleted);

            var vmResult = await (from p in result
                           select new VMPeriodReport
                           {
                               ID = p.PeriodID,
                               Style = p.Style,
                               StartDate = p.StartDate,
                               FinishDate = p.FinishDate,
                               LastPeriod = p.LastPeriod
                           }).OrderByDescending(x=> x.ID).ToListAsync();

            return vmResult;
        }
    }
}
