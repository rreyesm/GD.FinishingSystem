using GD.FinishingSystem.Bussines.Abstract;
using GD.FinishingSystem.DAL.Abstract;
using GD.FinishingSystem.DAL.Concrete;
using GD.FinishingSystem.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GD.FinishingSystem.Bussines.Concrete
{
    public class TestResultManager : AbstractTestResultService
    {
        private IAsyncRepository<TestResult> repository = null;
        public TestResultManager(DbContext context)
        {
            this.repository = new GenericRepository<TestResult>(context);
        }
        public override async Task Add(TestResult TestResultInformation, int adderRef)
        {
            await repository.Add(TestResultInformation, adderRef);
        }

        public override async Task Delete(TestResult TestResultInformation, int deleterRef)
        {
            await repository.Remove(TestResultInformation.TestResultID, deleterRef);
        }

        public override async Task<IEnumerable<TestResult>> GetTestResultList()
        {
            var result = await repository.GetWhere(o => !o.IsDeleted);
            return result;
        }

        public override async Task<TestResult> GetTestResultFromTestResultID(int TestResultID)
        {
            var result = await repository.GetByPrimaryKey(TestResultID);
            return result;
        }

        public override async Task<IEnumerable<TestResult>> GetTestResultListFromBetweenDate(DateTime begin, DateTime end)
        {
            var result = await repository.GetWhere(o => !o.IsDeleted && (o.CreatedDate <= end && o.CreatedDate >= begin) || (o.CreatedDate <= begin && o.CreatedDate >= end));

            return result;
        }

        public override async Task Update(TestResult TestResultInformation, int updaterRef)
        {
            await repository.Update(TestResultInformation, updaterRef);
        }
    }
}
