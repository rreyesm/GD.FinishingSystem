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
    public class OriginCategoryManager : AbstractOriginCategoryService
    {
        private IAsyncRepository<OriginCategory> repository = null;
        public OriginCategoryManager(DbContext context)
        {
            this.repository = new GenericRepository<OriginCategory>(context);
        }
        public override async Task Add(OriginCategory originCategory, int adderRef)
        {
            await repository.Add(originCategory, adderRef);
        }

        public override async Task Delete(OriginCategory originCategory, int deleterRef)
        {
            await repository.Remove(originCategory.OriginCategoryID, deleterRef);
        }

        public override async Task<OriginCategory> GetOriginCategoryFromOriginCategoryID(int originCategoryID)
        {
            var result = await repository.GetByPrimaryKey(originCategoryID);

            return result;
        }

        public override async Task<IEnumerable<OriginCategory>> GetOriginCategoryList()
        {
            var result = await repository.GetWhere(x => !x.IsDeleted);

            return result;
        }

        public override async Task Update(OriginCategory originCategory, int updaterRef)
        {
            await repository.Update(originCategory, updaterRef);
        }

        public override async Task<OriginCategory> GetOriginCategoryFromOriginCategoryCode(string originCode)
        {
            var result = await repository.GetWhere(x => !x.IsDeleted && x.OriginCode.Equals(originCode));

            return result.ToList().FirstOrDefault();
        }
    }
}
