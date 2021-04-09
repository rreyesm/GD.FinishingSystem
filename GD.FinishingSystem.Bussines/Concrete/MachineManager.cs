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
        private IAsyncRepository<DefinationProcess> processrepository = null;
        private IAsyncRepository<Floor> floorRepository = null;
        public MachineManager(DbContext context)
        {
            this.repository = new GenericRepository<Machine>(context);
            this.processrepository = new GenericRepository<DefinationProcess>(context);
            this.floorRepository = new GenericRepository<Floor>(context);

        }
        public override async Task Add(Machine MachineInformation, int adderRef)
        {
            await repository.Add(MachineInformation, adderRef);
        }

        public override async Task Delete(Machine MachineInformation, int deleterRef)
        {
            await repository.Remove(MachineInformation.MachineID, deleterRef);
        }

        public override async Task<IEnumerable<VMMachine>> GetVMMachineList()
        {
            var machines = await repository.GetWhere(o => !o.IsDeleted);
            var processes = await processrepository.GetWhere(o => !o.IsDeleted);
            var floors = await floorRepository.GetWhere(x => x.FloorID != 0);

            var res = (
                from a in machines.ToList()
                join b in processes.ToList() on a.DefinationProcessID equals b.DefinationProcessID
                join c in floors.ToList() on a.FloorID equals c.FloorID
                select new VMMachine
                {
                    ID = a.MachineID,
                    DefinationProcessID = b.DefinationProcessID,
                    processName = b.Name,
                    MachineCode = a.MachineCode,
                    MachineName = a.MachineName,
                    FloorName = c.FloorName,
                }).ToList();


            return res;
        }

        public override async Task<Machine> GetMachineFromMachineID(int MachineID)
        {
            var result = await repository.GetByPrimaryKey(MachineID);
            return result;
        }

        public override async Task<VMMachine> GetVMMachineFromVMMachineID(int MachineID)
        {
            var machine = await repository.GetByPrimaryKey(MachineID);
            var process = await processrepository.GetWhere(x => !x.IsDeleted && x.DefinationProcessID == machine.DefinationProcessID);
            var floor = await floorRepository.GetWhere(x => !x.IsDeleted && x.FloorID == machine.FloorID);

            VMMachine result = new VMMachine()
            {
                ID = machine.MachineID,
                DefinationProcessID = process?.FirstOrDefault()?.DefinationProcessID ?? 0,
                processName = process?.FirstOrDefault()?.Name ?? string.Empty,
                MachineCode = machine.MachineCode,
                MachineName = machine.MachineName,
                FloorName = floor?.FirstOrDefault()?.FloorName ?? string.Empty,
            };

            return result;
        }

        public override async Task<IEnumerable<VMMachine>> GetVMMachineListFromBetweenDate(DateTime begin, DateTime end)
        {
            var machines = await repository.GetWhere(o => !o.IsDeleted && (o.CreatedDate <= end && o.CreatedDate >= begin) || (o.CreatedDate <= begin && o.CreatedDate >= end));
            var processes = await processrepository.GetWhere(o => !o.IsDeleted);
            var floors = await floorRepository.GetWhere(x => x.FloorID != 0);

            var res = (
                from a in machines.ToList()
                join b in processes.ToList() on a.DefinationProcessID equals b.DefinationProcessID
                join c in floors.ToList() on a.FloorID equals c.FloorID
                select new VMMachine
                {
                    ID = a.MachineID,
                    DefinationProcessID = b.DefinationProcessID,
                    processName = b.Name,
                    MachineCode = a.MachineCode,
                    MachineName = a.MachineName,
                    FloorName = c.FloorName,
                }).ToList();
            return res;
        }

        public override async Task<IEnumerable<VMMachine>> GetVMMachinesFromDefinationProcessID(int DefinationProcess)
        {
            var machines = await repository.GetWhere(o => !o.IsDeleted && o.DefinationProcessID == DefinationProcess);
            var processes = await processrepository.GetWhere(o => !o.IsDeleted);
            var floors = await floorRepository.GetWhere(x => x.FloorID != 0);

            var res = (
                from a in machines.ToList()
                join b in processes.ToList() on a.DefinationProcessID equals b.DefinationProcessID
                join c in floors.ToList() on a.FloorID equals c.FloorID
                select new VMMachine
                {
                    ID = a.MachineID,
                    DefinationProcessID = b.DefinationProcessID,
                    processName = b.Name,
                    MachineCode = a.MachineCode,
                    MachineName = a.MachineName,
                    FloorName = c.FloorName,
                }).ToList();
            return res;
        }

        public override async Task Update(Machine MachineInformation, int updaterRef)
        {
            await repository.Update(MachineInformation, updaterRef);
        }

        public override async Task<Machine> GetMachineFromMachineCode(string machineCode)
        {
            var result = await repository.GetWhere(x => !x.IsDeleted && x.MachineCode.Equals(machineCode));

            return result.ToList().FirstOrDefault();
        }

        public override async Task<IEnumerable<Machine>> GetMachineList()
        {
            var result = await repository.GetWhere(x => !x.IsDeleted);

            return result;
        }
    }
}
