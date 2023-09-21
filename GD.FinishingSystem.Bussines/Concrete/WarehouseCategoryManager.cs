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
    public class WarehouseCategoryManager : AbstractWarehouseCategoryService
    {
        private IAsyncRepository<WarehouseCategory> repository = null;
        public WarehouseCategoryManager(DbContext context)
        {
            this.repository = new GenericRepository<WarehouseCategory>(context);
        }
        public override async Task Add(WarehouseCategory stockCategory, int adderRef)
        {
            await repository.Add(stockCategory, adderRef);
        }

        public override async Task Delete(WarehouseCategory stockCategory, int deleterRef)
        {
            await repository.Remove(stockCategory.WarehouseCategoryID, deleterRef);
        }

        public override async Task<WarehouseCategory> GetStockCategoryFromStockCategoryID(int stockCategoryID)
        {
            var result = await repository.GetByPrimaryKey(stockCategoryID);

            return result;
        }

        public override async Task<IEnumerable<WarehouseCategory>> GetStockCategoryList()
        {
            var result = await repository.GetWhere(x => !x.IsDeleted);

            return result;
        }

        public override async Task Update(WarehouseCategory stockCategory, int updaterRef)
        {
            await repository.Update(stockCategory, updaterRef);
        }

        public override async Task<WarehouseCategory> GetStockCategoryFromStockCategoryCode(string stockCode)
        {
            var result = await repository.GetWhere(x => !x.IsDeleted && x.WarehouseCode.Equals(stockCode));

            return result.ToList().FirstOrDefault();
        }
    }
}
