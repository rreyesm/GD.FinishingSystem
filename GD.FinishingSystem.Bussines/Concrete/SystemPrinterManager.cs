using GD.FinishingSystem.Bussines.Abstract;
using GD.FinishingSystem.DAL.Abstract;
using GD.FinishingSystem.DAL.Concrete;
using GD.FinishingSystem.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GD.FinishingSystem.Bussines.Concrete
{
    public class SystemPrinterManager : AbstractSystemPrinterService
    {
        IAsyncRepository<SystemPrinter> repository = null;

        public SystemPrinterManager(DbContext context)
        {
            this.repository = new GenericRepository<SystemPrinter>(context);
        }
        public override async Task Add(SystemPrinter systemPrinter, int adderRef)
        {
            await repository.Add(systemPrinter, adderRef);
        }

        public override async Task<int> CountByFileName(string printerName)
        {
            return await repository.CountWhere(x => !x.IsDeleted && x.Name.Equals(printerName));
        }

        public override async Task Delete(SystemPrinter systemPrinter, int deleterRef)
        {
            await repository.Remove(systemPrinter.SystemPrinterID, deleterRef);
        }

        public override async Task<SystemPrinter> GetSystemPrinterFromSystemPrinterID(int systemPrinterID)
        {
            return await repository.GetByPrimaryKey(systemPrinterID);
        }

        public override async Task<IEnumerable<SystemPrinter>> GetSystemPrinterList()
        {
            return await repository.GetWhere(x => !x.IsDeleted);
        }

        public override async Task Update(SystemPrinter systemPrinter, int updaterRef)
        {
            await repository.Update(systemPrinter, updaterRef);
        }
    }
}
