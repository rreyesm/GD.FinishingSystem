using GD.FinishingSystem.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GD.FinishingSystem.Bussines.Abstract
{
    public abstract class AbstractTestResultService
    {
        public abstract Task<IEnumerable<TestResult>> GetTestResultList();
        public abstract Task<IEnumerable<TestResult>> GetTestResultListFromBetweenDate(DateTime begin, DateTime end);
        public abstract Task<TestResult> GetTestResultFromTestResultID(int testResultID);
        public abstract Task<IEnumerable<TestResult>> GetTestResultFromTestResultIDs(List<int> testResultIDs);
        public abstract Task Add(TestResult TestResultInformation, int adderRef);
        public abstract Task Update(TestResult TestResultInformation, int updaterRef);
        public abstract Task Delete(TestResult TestResultInformation, int deleterRef);
    }
}
