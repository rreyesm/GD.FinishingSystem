using GD.FinishingSystem.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GD.FinishingSystem.Bussines.Abstract
{
    public abstract class AbstractRuloService
    {
        public abstract Task<IEnumerable<Rulo>> GetRuloList();
        public abstract Task<IEnumerable<Rulo>> GetRuloListFromBetweenDate(DateTime begin, DateTime end);
        public abstract Task<Rulo> GetRuloFromRuloID(int RuloID);
        public abstract Task Add(Rulo RuloInformation, int adderRef);
        public abstract Task Update(Rulo RuloInformation, int updaterRef);
        public abstract Task Delete(Rulo RuloInformation, int deleterRef);
        public abstract Task AddRuloProcess(RuloProcess ruloProcess, int adderRef);
        public abstract Task UpdateRuloProcess(RuloProcess ruloProcess, int updaterRef);
        public abstract Task DeleteRuloProcess(RuloProcess ruloProcess, int deleterRef);
        public abstract Task<IEnumerable<RuloProcess>> GetRuloProcessesFromRuloID(int RuloID);
        public abstract Task<RuloProcess> GetRuloProcessFromRuloProcessID(int RuloProcessID);
        public abstract Task SetTestResult(int RuloID, int TestResultID, int setter);
        

    }
}
