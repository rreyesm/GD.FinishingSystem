using GD.FinishingSystem.Entities;
using GD.FinishingSystem.Entities.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GD.FinishingSystem.Bussines.Abstract
{
    public abstract class AbstractMachineService
    {
        public abstract Task<IEnumerable<VMMachine>> GetMachineList();
        public abstract Task<IEnumerable<VMMachine>> GetMachineListFromBetweenDate(DateTime begin, DateTime end);
        public abstract Task<Machine> GetMachineFromMachineID(int MachineID);
        public abstract Task<VMMachine> GetVMMachineFromVMMachineID(int MachineID);
        public abstract Task<IEnumerable<VMMachine>> GetVMMachinesFromDefinationProcessID(int DefinationProcessID);
        public abstract Task Add(Machine MachineInformation, int adderRef);
        public abstract Task Update(Machine MachineInformation, int updaterRef);
        public abstract Task Delete(Machine MachineInformation, int deleterRef);
        public abstract Task<Machine> GetMachineFromMachineCode(string machineCode);

    }
}
