﻿using GD.FinishingSystem.Entities;
using GD.FinishingSystem.Entities.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GD.FinishingSystem.Bussines.Abstract
{
    public abstract class AbstractRuloService
    {
        public abstract Task<IEnumerable<VMRulo>> GetRuloList();
        public abstract Task<IEnumerable<VMRulo>> GetRuloListFromBetweenDate(DateTime begin, DateTime end);
        public abstract Task<Rulo> GetRuloFromRuloID(int RuloID);
        public abstract Task<VMRulo> GetVMRuloFromVMRuloID(int RuloID);
        public abstract Task Add(Rulo RuloInformation, int adderRef);
        public abstract Task Update(Rulo RuloInformation, int updaterRef);
        public abstract Task Delete(Rulo RuloInformation, int deleterRef);
        public abstract Task AddRuloProcess(RuloProcess ruloProcess, int adderRef);
        public abstract Task UpdateRuloProcess(RuloProcess ruloProcess, int updaterRef);
        public abstract Task DeleteRuloProcess(RuloProcess ruloProcess, int deleterRef);
        public abstract Task<IEnumerable<VMRuloProcess>> GetVMRuloProcessesFromRuloID(int RuloID);
        public abstract Task<IEnumerable<RuloProcess>> GetRuloProcessListFromBetweenDate(DateTime begin, DateTime end);
        public abstract Task<RuloProcess> GetRuloProcessFromRuloProcessID(int RuloProcessID);
        public abstract Task SetTestResult(int RuloID, int TestResultID, bool isWaitingForTestResult, int? authorizer, int setter);

        public abstract Task<IEnumerable<String>> GetRuloStyleList();
        public abstract Task<IEnumerable<VMRulo>> GetRuloListFromFilters(VMRuloFilters ruloFilters);

        public abstract Task DeleteRuloProcessFromRuloProcessID(int ruloProcessID, int deleterRef);
 
    }
}
