using GD.FinishingSystem.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GD.FinishingSystem.Bussines.Abstract
{
    public abstract class AbstractOriginCategoryService
    {
        public abstract Task<IEnumerable<OriginCategory>> GetOriginCategoryList();
        public abstract Task<OriginCategory> GetOriginCategoryFromOriginCategoryID(int originCategoryID);
        public abstract Task Add(OriginCategory originCategory, int adderRef);
        public abstract Task Update(OriginCategory originCategory, int updaterRef);
        public abstract Task Delete(OriginCategory originCategory, int deleterRef);
        public abstract Task<OriginCategory> GetOriginCategoryFromOriginCategoryCode(string originCode);
    }
}
