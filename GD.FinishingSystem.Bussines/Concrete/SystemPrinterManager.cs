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
    public class SystemPrinterManager : AbstractSystemPrinterService
    {
        IAsyncRepository<SystemPrinter> repository = null;
        IAsyncRepository<Floor> repositoryFloor = null;
        IAsyncRepository<Machine> repositoryMachine = null;
        public SystemPrinterManager(DbContext context)
        {
            this.repository = new GenericRepository<SystemPrinter>(context);
            this.repositoryFloor = new GenericRepository<Floor>(context);
            this.repositoryMachine = new GenericRepository<Machine>(context);
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
            var result = await repository.GetByPrimaryKey(systemPrinterID);

            if (result != null)
            {
                var floor = await repositoryFloor.GetByPrimaryKey(result.FloorID);
                result.Floor = floor;
            }
            return result;
        }

        public override async Task<IEnumerable<SystemPrinter>> GetSystemPrinterList()
        {
            var systemPrinters = await repository.GetWhere(x => !x.IsDeleted);
            var floors = await repositoryFloor.GetWhere(x=> !x.IsDeleted);
            var machines = await repositoryMachine.GetWhere(x => !x.IsDeleted);

            var result = (from sp in systemPrinters
                          join f in floors on sp.FloorID equals f.FloorID
                          into ljf
                          join m in machines on sp.MachineID equals m.MachineID
                          into ljm from subM in ljm.DefaultIfEmpty() 
                          select new SystemPrinter
                          {
                              SystemPrinterID = sp.SystemPrinterID,
                              Name = sp.Name,
                              IsPrinterIP = sp.IsPrinterIP,
                              Location = sp.Location,
                              FloorID = sp.FloorID,
                              Floor = ljf.FirstOrDefault(),
                              PCIP = sp.PCIP,
                              MachineID = sp.MachineID,
                              Machine = subM
                          });
            return result;
        }

        public override async Task Update(SystemPrinter systemPrinter, int updaterRef)
        {
            await repository.Update(systemPrinter, updaterRef);
        }
    }
}
