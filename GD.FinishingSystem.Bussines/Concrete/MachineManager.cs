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
    public class MachineManager : AbstractMachineService
    {
        private IAsyncRepository<Machine> repository = null;
        private IAsyncRepository<DefinationProcess> procesrepository = null;
        public MachineManager(DbContext context)
        {
            this.repository = new GenericRepository<Machine>(context);
            this.procesrepository = new GenericRepository<DefinationProcess>(context);

        }
        public override async Task Add(Machine MachineInformation, int adderRef)
        {
            await repository.Add(MachineInformation, adderRef);
        }

        public override async Task Delete(Machine MachineInformation, int deleterRef)
        {
            await repository.Remove(MachineInformation.MachineID, deleterRef);
        }

        public override async Task<IEnumerable<VMMachine>> GetMachineList()
        {
            var machines = await repository.GetWhere(o => !o.IsDeleted);
            var processes = await procesrepository.GetWhere(o => !o.IsDeleted);

            var res = (
                from a in machines.ToList()
                join b in processes.ToList() on a.DefinationProcessID equals b.DefinationProcessID
                select new VMMachine
                {
                    ID = a.MachineID,
                    processName = b.Name,
                    MachineCode = a.MachineCode,
                    MachineName = a.MachineName
                }).ToList();


            return res;
        }

        public override async Task<Machine> GetMachineFromMachineID(int MachineID)
        {
            var result = await repository.GetByPrimaryKey(MachineID);
            return result;
        }

        public override async Task<IEnumerable<VMMachine>> GetMachineListFromBetweenDate(DateTime begin, DateTime end)
        {
            var machines = await repository.GetWhere(o => !o.IsDeleted && (o.CreatedDate <= end && o.CreatedDate >= begin) || (o.CreatedDate <= begin && o.CreatedDate >= end));
            var processes = await procesrepository.GetWhere(o => !o.IsDeleted);

            var res = (
                from a in machines.ToList()
                join b in processes.ToList() on a.DefinationProcessID equals b.DefinationProcessID
                select new VMMachine
                {
                    ID = a.MachineID,
                    processName = b.Name,
                    MachineCode = a.MachineCode,
                    MachineName = a.MachineName
                }).ToList();
            return res;
        }

        public override async Task<IEnumerable<VMMachine>> GetMachinesFromDefinationProcessID(int DefinationProcess)
        {
            var machines = await repository.GetWhere(o => !o.IsDeleted && o.DefinationProcessID == DefinationProcess);
            var processes = await procesrepository.GetWhere(o => !o.IsDeleted);

            var res = (
                from a in machines.ToList()
                join b in processes.ToList() on a.DefinationProcessID equals b.DefinationProcessID
                select new VMMachine
                {
                    ID = a.MachineID,
                    processName = b.Name,
                    MachineCode = a.MachineCode,
                    MachineName = a.MachineName
                }).ToList();
            return res;
        }

        public override async Task Update(Machine MachineInformation, int updaterRef)
        {
            await repository.Update(MachineInformation, updaterRef);
        }
    }
}
