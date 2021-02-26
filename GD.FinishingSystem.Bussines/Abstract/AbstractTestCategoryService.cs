using GD.FinishingSystem.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GD.FinishingSystem.Bussines.Abstract
{
    public abstract class AbstractTestCategoryService
    {
        public abstract Task<IEnumerable<TestCategory>> GetTestCategoryList();
        public abstract Task<TestCategory> GetTestCategoryFromTestCategoryID(int testCategoryID);
        public abstract Task Add(TestCategory testCategory, int adderRef);
        public abstract Task Update(TestCategory testCategory, int updaterRef);
        public abstract Task Delete(TestCategory testCategory, int deleterRef);
        public abstract Task<TestCategory> GetTestCategoryFromTestCategoryCode(string testCategoryCode);
    }
}
