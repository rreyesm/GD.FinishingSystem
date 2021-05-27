using GD.FinishingSystem.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GD.FinishingSystem.Bussines.Abstract
{
    public abstract class AbstractDefinationProcessService
    {
            
        public abstract Task<IEnumerable<DefinationProcess>> GetDefinationProcessList();
        public abstract Task<IEnumerable<DefinationProcess>> GetDefinationProcessListFromBetweenDate();
        public abstract Task<DefinationProcess> GetDefinationProcessFromDefinationProcessID(int DefinationProcessID);
        public abstract Task Add(DefinationProcess DefinationProcessInformation, int adderRef);
        public abstract Task Update(DefinationProcess DefinationProcessInformation, int updaterRef);
        public abstract Task Delete(DefinationProcess DefinationProcessInformation, int deleterRef);
        public abstract Task<DefinationProcess> GetDefinitionProcessFromDefinitionProcessCode(string processCode);
    }
}
