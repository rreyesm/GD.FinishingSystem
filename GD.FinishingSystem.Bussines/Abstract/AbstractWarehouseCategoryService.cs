using GD.FinishingSystem.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GD.FinishingSystem.Bussines.Abstract
{
    public abstract class AbstractWarehouseCategoryService
    {
        public abstract Task<IEnumerable<WarehouseCategory>> GetStockCategoryList();
        public abstract Task<WarehouseCategory> GetStockCategoryFromStockCategoryID(int originCategoryID);
        public abstract Task Add(WarehouseCategory originCategory, int adderRef);
        public abstract Task Update(WarehouseCategory originCategory, int updaterRef);
        public abstract Task Delete(WarehouseCategory originCategory, int deleterRef);
        public abstract Task<WarehouseCategory> GetStockCategoryFromStockCategoryCode(string originCode);
    }
}
