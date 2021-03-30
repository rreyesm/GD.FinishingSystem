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
    public class RuloMigrationsManager : AbstractRuloMigrationService
    {
        IAsyncRepository<RuloMigration> repository = null;
        IAsyncRepository<MigrationCategory> repositoryMigrationCategory = null;
        IAsyncRepository<MigrationControl> repositoryMigrationControl = null;
        public RuloMigrationsManager(DbContext context)
        {
            this.repository = new GenericRepository<RuloMigration>(context);
            this.repositoryMigrationCategory = new GenericRepository<MigrationCategory>(context);
            this.repositoryMigrationControl = new GenericRepository<MigrationControl>(context);
        }
        public override async Task Add(RuloMigration ruloMigration, int adderRef)
        {
            await repository.Add(ruloMigration, adderRef);
        }

        public override async Task AddRange(IEnumerable<RuloMigration> ruloMigrationList, int adderRef)
        {
            await repository.AddRange(ruloMigrationList, adderRef);
        }

        public override async Task AddMigrationControl(MigrationControl migrationControl, int addRef)
        {
            await repositoryMigrationControl.Add(migrationControl, addRef);
        }

        public override async Task Delete(RuloMigration ruloMigration, int deleterRef)
        {
            await repository.Remove(ruloMigration.RuloMigrationID, deleterRef);
        }

        public override async Task<IEnumerable<MigrationCategory>> GetMigrationCategoryList()
        {
            return await repositoryMigrationCategory.GetWhere(x => !x.IsDeleted);
        }

        public override async Task<RuloMigration> GetRuloMigrationFromRuloMigrationID(int ruloMigrationID)
        {
            var result = await repository.GetByPrimaryKey(ruloMigrationID);

            if (result != null)
            {
                var migrationCategory = await repositoryMigrationCategory.GetByPrimaryKey(result.MigrationCategoryID);
                result.MigrationCategory = migrationCategory;
            }

            return result;
        }

        public override async Task<IEnumerable<RuloMigration>> GetRuloMigrationList()
        {
            return await repository.GetWhere(x => !x.IsDeleted);
        }

        public override async Task<IEnumerable<RuloMigration>> GetRuloMigrationListFromBetweenDates(DateTime dtBegin, DateTime dtEnd)
        {
            var ruloMigrationList = await repository.GetWhere(x => !x.IsDeleted && ((x.Date <= dtEnd && x.Date >= dtBegin) || (x.Date <= dtBegin && x.Date >= dtEnd)));
            var migrationCategoryList = await repositoryMigrationCategory.GetWhere(x => !x.IsDeleted);

            ruloMigrationList.ToList().ForEach(x =>
            {
                x.MigrationCategory = migrationCategoryList.Where(y => y.MigrationCategoryID == x.MigrationCategoryID).FirstOrDefault();
            });

            return ruloMigrationList;
        }

        public override async Task UpdateMigrationControls(MigrationControl migrationContol, int addRef)
        {
            await repositoryMigrationControl.Update(migrationContol, addRef);
        }

        public override async Task Update(RuloMigration ruloMigration, int updaterRef)
        {
            await repository.Update(ruloMigration, updaterRef);
        }

        public override async Task<int> CountByFileName(string fileName)
        {
            return await repository.CountWhere(x => !x.IsDeleted && x.ExcelFileName.Equals(fileName));
        }
    }
}
