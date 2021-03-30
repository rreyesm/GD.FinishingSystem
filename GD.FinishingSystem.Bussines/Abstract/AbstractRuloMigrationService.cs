using GD.FinishingSystem.Entities;
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
        public abstract Task<IEnumerable<MigrationCategory>> GetMigrationCategoryList();

        public abstract Task AddMigrationControl(MigrationControl migrationControl, int addRef);
        public abstract Task UpdateMigrationControls(MigrationControl migrationContol, int addRef);
        public abstract Task<int> CountByFileName(string fileName);
    }
}
