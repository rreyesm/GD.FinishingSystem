using GD.FinishingSystem.Entities;
using GD.FinishingSystem.Entities.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GD.FinishingSystem.Bussines.Abstract
{
    public abstract class AbstractRuloMigrationService
    {
        public abstract Task<IEnumerable<RuloMigration>> GetRuloMigrationList();
        public abstract Task<RuloMigration> GetRuloMigrationFromRuloMigrationID(int ruloMigrationID);
        public abstract Task Add(RuloMigration ruloMigration, int adderRef);
        public abstract Task AddRange(IEnumerable<RuloMigration> ruloMigrationList, int adderRef);
        public abstract Task Update(RuloMigration ruloMigration, int updaterRef);
        public abstract Task Delete(RuloMigration ruloMigration, int deleterRef);

        public abstract Task<IEnumerable<RuloMigration>> GetRuloMigrationListFromBetweenDates(DateTime dtBegin, DateTime dtEnd);
        public abstract Task<IEnumerable<RuloMigration>> GetRuloMigrationListFromFilters(VMRuloFilters ruloFilters);
        public abstract Task<IEnumerable<MigrationCategory>> GetMigrationCategoryList();

        public abstract Task AddMigrationControl(MigrationControl migrationControl, int addRef);
        public abstract Task UpdateMigrationControls(MigrationControl migrationContol, int addRef);
        public abstract Task<int> CountByFileName(string fileName);
        public abstract Task<IEnumerable<String>> GetRuloMigrationStyleList();
        public abstract Task<IEnumerable<VMRuloMigrationReport>> GetRawFabricStocktFromFilters(VMRuloFilters ruloFilters);
        public abstract Task<IEnumerable<VMRuloMigrationReport>> GetAllVMRuloReportList(DateTime dtEnd);
        public abstract Task<IEnumerable<DefinationProcess>> GetDefinitionProcessList();
        public abstract Task<IEnumerable<OriginCategory>> GetOriginCategoryList();
        public abstract Task<IEnumerable<VMStyleData>> GetStylesFromProductionLoteList();
        public abstract Task<bool> ExistRuloInRuloMigration(int ruloID);
        public abstract Task<decimal> GetTotalMetersByRuloMigration(string lote, int beam);
        public abstract Task<bool> UpdateRuloMigrationsFromRuloMigrationID(int ruloMigrationID, int ruloID, int userID);
        public abstract Task<IEnumerable<Location>> GetLocationList();
        public abstract Task<bool> Exists(RuloMigration ruloMigration);
        public abstract Task<bool> Exists(int ruloId);
        public abstract Task<IEnumerable<TblFinishRawFabricEntrance>> GetFinishedRawFabricEntrance(VMReportFilter reportFilter);
    }
}
