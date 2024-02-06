using GD.FinishingSystem.DAL.EFdbPerformanceStandards;
using GD.FinishingSystem.Entities;
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
        public abstract Task<Rulo> GetRuloFromFolio(int folioNumber);
        public abstract Task<VMRulo> GetVMRuloFromRuloID(int RuloID);
        public abstract Task Add(Rulo RuloInformation, int adderRef);
        public abstract Task Update(Rulo RuloInformation, int updaterRef);
        public abstract Task Delete(Rulo RuloInformation, int deleterRef);
        public abstract Task AddRuloProcess(RuloProcess ruloProcess, int adderRef);
        public abstract Task UpdateRuloProcess(RuloProcess ruloProcess, int updaterRef);
        public abstract Task DeleteRuloProcess(RuloProcess ruloProcess, int deleterRef);
        public abstract Task<IEnumerable<VMRuloProcess>> GetVMRuloProcessesFromRuloID(int RuloID);
        public abstract Task<IEnumerable<RuloProcess>> GetRuloProcessListFromBetweenDate(DateTime begin, DateTime end);
        public abstract Task<RuloProcess> GetRuloProcessFromRuloProcessID(int RuloProcessID);
        public abstract Task<bool> ExistsRuloProcess(int ruloID, int definitionProcessID);
        public abstract Task SetTestResult(int RuloID, int TestResultID, bool isWaitingForTestResult, int? authorizer, int setter);

        public abstract Task<IEnumerable<string>> GetRuloStyleStringForProductionLoteList();
        public abstract Task<VMStyleData> GetRuloStyle(string lote);
        public abstract Task<int> GetRuloTotalRecords(VMRuloFilters ruloFilters);
        public abstract Task<IEnumerable<VMRulo>> GetRuloListFromFilters(VMRuloFilters ruloFilters, int currentPaindex, int pageSize);
        public abstract Task<IEnumerable<VMRuloBatch>> GetGuvenInformation(IEnumerable<int> ruloIDs);
        public abstract decimal GetSumSamples(int ruloID);
        public abstract Task<IEnumerable<VMRulo>> GetRuloReportListFromFilters(VMRuloFilters ruloFilters);
        public abstract Task<IEnumerable<WarehouseStock>> GetWarehouseStock(VMRuloFilters ruloFilters);
        public abstract Task<IEnumerable<VMRulo>> GetAllVMRuloReportList(string query, params object[] parameters);

        public abstract Task DeleteRuloProcessFromRuloProcessID(int ruloProcessID, int deleterRef);

        public abstract Task<int> GetPerformanceRuloID(int ruloId);
        public abstract Task<IEnumerable<TblCustomPerformanceForFinishing>> GetPerformanceTestResultByRuloId(int ruloId);
        public abstract Task<IEnumerable<TblCustomPerformanceForFinishing>> GetPerformanceTestResultById(int perfomanceId);

        public abstract Task<IEnumerable<TblCustomPerformanceMasiveForFinishing>> GetPerformanceTestResultMasive(List<int> testMasterList);

        public abstract Task<IEnumerable<TblCustomReport>> GetFinishedFabricReport(VMReportFilter reportFilter);

        public abstract Task<string> GetMachineByRuloId(int ruloId);
    }
}
