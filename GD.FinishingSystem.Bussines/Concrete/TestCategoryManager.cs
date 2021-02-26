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
    public class TestCategoryManager : AbstractTestCategoryService
    {
        private IAsyncRepository<TestCategory> repository = null;
        public TestCategoryManager(DbContext context)
        {
            this.repository = new GenericRepository<TestCategory>(context);
        }
        public override async Task Add(TestCategory testCategory, int adderRef)
        {
            await this.repository.Add(testCategory, adderRef);
        }

        public override async Task Delete(TestCategory testCategory, int deleterRef)
        {
            await repository.Remove(testCategory.TestCategoryID, deleterRef);
        }

        public override async Task<TestCategory> GetTestCategoryFromTestCategoryID(int testCategoryID)
        {
            var result = await repository.GetByPrimaryKey(testCategoryID);
            return result;
        }

        public override async Task<IEnumerable<TestCategory>> GetTestCategoryList()
        {
            var result = await repository.GetWhere(x=> !x.IsDeleted);
            return result;
        }

        public override async Task Update(TestCategory testCategory, int updaterRef)
        {
            await repository.Update(testCategory, updaterRef);
        }

        public override async Task<TestCategory> GetTestCategoryFromTestCategoryCode(string testCategoryCode)
        {
            var result = await repository.GetWhere(x => !x.IsDeleted && x.TestCode.Equals(testCategoryCode));

            return result.ToList().FirstOrDefault();
        }
    }
}
