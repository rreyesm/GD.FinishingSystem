using GD.FinishingSystem.Bussines.Abstract;
using GD.FinishingSystem.DAL.Abstract;
using GD.FinishingSystem.DAL.Concrete;
using GD.FinishingSystem.Entities;
using GD.FinishingSystem.Entities.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GD.FinishingSystem.Bussines.Concrete
{
    public class RuloManager : AbstractRuloService
    {
        private IAsyncRepository<TestResult> testResultRepository = null;
        private IAsyncRepository<Rulo> repository = null;
        private IAsyncRepository<RuloProcess> ruloProcessRepository = null;
        private IAsyncRepository<DefinationProcess> definationProcessRepository = null;
        private IAsyncRepository<TestCategory> testCategoryRepository = null;
        private IAsyncRepository<User> userRepository = null;
        private IAsyncRepository<Sample> sampleRepository = null;

        public RuloManager(DbContext context)
        {
            this.repository = new GenericRepository<Rulo>(context);
            this.ruloProcessRepository = new GenericRepository<RuloProcess>(context);
            this.definationProcessRepository = new GenericRepository<DefinationProcess>(context);
            this.testResultRepository = new GenericRepository<TestResult>(context);
            this.testCategoryRepository = new GenericRepository<TestCategory>(context);
            this.userRepository = new GenericRepository<User>(context);
            this.sampleRepository = new GenericRepository<Sample>(context);
        }

        public override async Task Add(Rulo RuloInformation, int adderRef)
        {
            await repository.Add(RuloInformation, adderRef);
        }

        public override async Task AddRuloProcess(RuloProcess ruloProcess, int adderRef)
        {
            await ruloProcessRepository.Add(ruloProcess, adderRef);
        }

        public override async Task Delete(Rulo RuloInformation, int deleterRef)
        {
            await repository.Remove(RuloInformation.RuloID, deleterRef);
        }

        public override async Task DeleteRuloProcess(RuloProcess ruloProcess, int deleterRef)
        {
            await ruloProcessRepository.Remove(ruloProcess.RuloID, deleterRef);
        }

        public override async Task<Rulo> GetRuloFromRuloID(int RuloID)
        {
            var result = await repository.GetByPrimaryKey(RuloID);
            return result;
        }

        public override async Task<VMRulo> GetVMRuloFromVMRuloID(int RuloID)
        {
            var rulo = await repository.GetByPrimaryKey(RuloID);
            var testResults = await testResultRepository.GetWhere(x => !x.IsDeleted && x.TestResultID == rulo.TestResultID);
            var testResult = testResults.FirstOrDefault();
            TestCategory testCategory = null;
            if (testResult != null)
            {
                var testCategories = await testCategoryRepository.GetWhere(x => x.TestCategoryID == testResult.TestCategoryID);
                testCategory = testCategories.FirstOrDefault();
            }

            OriginType originType = OriginType.Process;
            string sOriginType = string.Empty;
            if (Enum.TryParse(rulo.OriginID.ToString(), true, out originType))
                sOriginType = originType.ToString();

            var user = await userRepository.GetWhere(x => x.UserID == rulo.TestResultAuthorizer);

            var vwRulo = new VMRulo()
            {
                RuloID = rulo.RuloID,
                Lote = rulo.Lote,
                Beam = rulo.Beam,
                BeamStop = rulo.BeamStop,
                Loom = rulo.Loom,
                LoomLetter = rulo.LoomLetter,
                Piece = rulo.Piece,
                PieceLetter = rulo.PieceLetter,
                Style = rulo.Style,
                StyleName = rulo.StyleName,
                Width = rulo.Width,
                EntranceLength = rulo.EntranceLength,
                ExitLength = rulo.ExitLength,
                Shift = rulo.Shift,
                FolioNumber = rulo.FolioNumber,
                DeliveryDate = rulo.DeliveryDate,
                IsWaitingAnswerFromTest = rulo.IsWaitingAnswerFromTest,
                TestResultID = rulo.TestResultID,
                CanContinue = testResults.FirstOrDefault()?.CanContinue ?? false,
                TestCategoryID = testCategory?.TestCategoryID ?? 0,
                TestCategoryCode = testCategory?.TestCode ?? string.Empty,
                OriginID = sOriginType,
                Observations = rulo.Observations,
                TestResultAuthorizer = user?.FirstOrDefault()?.UserName ?? string.Empty
            };

            return vwRulo;
        }

        public override async Task<IEnumerable<VMRulo>> GetRuloList()
        {
            var rulos = await repository.GetWhere(o => !o.IsDeleted);
            var testResults = await testResultRepository.GetWhere(x => !x.IsDeleted);
            var testCategories = await testCategoryRepository.GetWhere(x => x.TestCategoryID != 0);

            var result = (from r in rulos.ToList()
                          join tr in testResults.ToList() on r.TestResultID equals tr.TestResultID
                          into ljTR
                          from subTR in ljTR.DefaultIfEmpty()
                          join tc in testCategories.ToList() on subTR?.TestCategoryID equals tc.TestCategoryID
                          into ljTC
                          from subTC in ljTC.DefaultIfEmpty()
                          select new VMRulo
                          {
                              RuloID = r.RuloID,
                              Lote = r.Lote,
                              Beam = r.Beam,
                              Loom = r.Loom,
                              Piece = r.Piece,
                              Style = r.Style,
                              StyleName = r.StyleName,
                              Width = r.Width,
                              EntranceLength = r.EntranceLength,
                              ExitLength = r.ExitLength,
                              IsWaitingAnswerFromTest = r.IsWaitingAnswerFromTest,
                              TestResultID = r.TestResultID,
                              CanContinue = subTR?.CanContinue ?? false,
                              TestCategoryID = subTC?.TestCategoryID ?? 0,
                              TestCategoryCode = subTC?.TestCode ?? string.Empty
                          }).ToList();

            return result;
        }

        public override async Task<IEnumerable<VMRulo>> GetRuloListFromBetweenDate(DateTime begin, DateTime end)
        {
            var rulos = await repository.GetWhere(o => !o.IsDeleted && ((o.CreatedDate <= end && o.CreatedDate >= begin) || (o.CreatedDate <= begin && o.CreatedDate >= end)));
            var testResults = await testResultRepository.GetWhere(x => !x.IsDeleted);
            var testCategories = await testCategoryRepository.GetWhere(x => x.TestCategoryID != 0);

            var result = (from r in rulos.ToList()
                          join tr in testResults.ToList() on r.TestResultID equals tr.TestResultID
                          into ljTR
                          from subTR in ljTR.DefaultIfEmpty()
                          join tc in testCategories.ToList() on subTR?.TestCategoryID equals tc.TestCategoryID
                          into ljTC
                          from subTC in ljTC.DefaultIfEmpty()
                          select new VMRulo
                          {
                              RuloID = r.RuloID,
                              Lote = r.Lote,
                              Beam = r.Beam,
                              Loom = r.Loom,
                              Piece = r.Piece,
                              Style = r.Style,
                              StyleName = r.StyleName,
                              Width = r.Width,
                              EntranceLength = r.EntranceLength,
                              ExitLength = r.ExitLength,
                              IsWaitingAnswerFromTest = r.IsWaitingAnswerFromTest,
                              TestResultID = r.TestResultID,
                              CanContinue = subTR?.CanContinue ?? false,
                              TestCategoryID = subTC?.TestCategoryID ?? 0,
                              TestCategoryCode = subTC?.TestCode ?? string.Empty
                          }).ToList();

            return result;
        }

        public override async Task<IEnumerable<VMRuloProcess>> GetVMRuloProcessesFromRuloID(int RuloID)
        {
            var ruloProcess = await ruloProcessRepository.GetWhere(o => !o.IsDeleted && o.RuloID == RuloID);
            var definationResult = await definationProcessRepository.GetAll();
            List<Sample> sampleList = new List<Sample>();
            if (ruloProcess != null && ruloProcess.Count() != 0)
            {
                List<int> ruloProcessesList = ruloProcess.ToList().Select(x => x.RuloProcessID).ToList();
                var sampleResult = await sampleRepository.GetWhere(x => !x.IsDeleted && ruloProcessesList.Contains(x.RuloProcessID));
                sampleList = sampleResult.ToList();
            }

            var result = (from rp in ruloProcess.ToList()
                          join dp in definationResult.ToList() on rp.DefinationProcessID equals dp.DefinationProcessID
                          join s in sampleList on rp.SampleID equals s.SampleID
                          into ljS
                          from subS in ljS.DefaultIfEmpty()
                          select new VMRuloProcess
                          {
                              RuloProcessID = rp.RuloProcessID,
                              RuloID = rp.RuloID,
                              DefinationProcess = dp,
                              DefinationProcessID = rp.DefinationProcessID,
                              BeginningDate = rp.BeginningDate,
                              EndDate = rp.EndDate,
                              FinishMeter = rp.FinishMeter,
                              IsFinished = rp.IsFinished,
                              SampleID = rp.SampleID,
                              Sample = subS
                          }).ToList();



            return result;
        }

        public override async Task<IEnumerable<RuloProcess>> GetRuloProcessListFromBetweenDate(DateTime begin, DateTime end)
        {
            var result = await ruloProcessRepository.GetWhere(o => !o.IsDeleted && ((o.CreatedDate <= end && o.CreatedDate >= begin) || (o.CreatedDate <= begin && o.CreatedDate >= end)));

            var definationResult = await definationProcessRepository.GetAll();
            foreach (var item in result)
                item.DefinationProcess = definationResult.Where(x => x.DefinationProcessID == item.DefinationProcessID).FirstOrDefault();

            return result;
        }

        public override async Task<RuloProcess> GetRuloProcessFromRuloProcessID(int RuloProcessID)
        {
            var result = await ruloProcessRepository.GetByPrimaryKey(RuloProcessID);

            var definationResult = await definationProcessRepository.GetByPrimaryKey(result.DefinationProcessID);
            result.DefinationProcess = definationResult;

            return result;
        }

        public override async Task SetTestResult(int RuloID, int TestResultID, bool isWaitingForTestResult, int? authorizer, int setter)
        {
            var result = await repository.GetByPrimaryKey(RuloID);
            result.IsWaitingAnswerFromTest = isWaitingForTestResult;
            result.TestResultID = TestResultID;
            result.TestResultAuthorizer = authorizer;
            await repository.Update(result, setter);
        }

        public override async Task Update(Rulo RuloInformation, int updaterRef)
        {
            await repository.Update(RuloInformation, updaterRef);
        }

        public override async Task UpdateRuloProcess(RuloProcess ruloProcess, int updaterRef)
        {
            await ruloProcessRepository.Update(ruloProcess, updaterRef);
        }

        public override async Task<IEnumerable<String>> GetRuloStyleList()
        {
            var result = await repository.GetWhere(x => !x.IsDeleted);
            var styleList = result.Select(x => x.Style).Distinct();

            return styleList;
        }

        public override async Task<IEnumerable<VMRulo>> GetRuloListFromFilters(VMRuloFilters ruloFilters)
        {
            var query = repository.GetQueryable(x => !x.IsDeleted && ((x.CreatedDate <= ruloFilters.dtEnd && x.CreatedDate >= ruloFilters.dtBegin) || (x.CreatedDate <= ruloFilters.dtBegin && x.CreatedDate >= ruloFilters.dtEnd)));

            var rulos = (
                from r in query
                where
                (!(ruloFilters.numLote > 0) || r.Lote == ruloFilters.numLote.ToString()) &&
                (!(ruloFilters.numBeam > 0) || r.Beam == ruloFilters.numBeam) &&
                (!(ruloFilters.numLoom > 0) || r.Loom == ruloFilters.numLoom) &&
                (!(ruloFilters.numPiece > 0) || r.Piece == ruloFilters.numPiece) &&
                (!(ruloFilters.FolioNumber > 0) || r.FolioNumber == ruloFilters.FolioNumber) &&
                 ((string.IsNullOrWhiteSpace(ruloFilters.txtStyle)) || r.Style.Contains(ruloFilters.txtStyle))

                select r

                ).ToList();

            var testResults = await testResultRepository.GetWhere(x => !x.IsDeleted);
            var testCategories = await testCategoryRepository.GetWhere(x => x.TestCategoryID != 0);

            var result = (from r in rulos
                          join tr in testResults.ToList() on r.TestResultID equals tr.TestResultID
                          into ljTR
                          from subTR in ljTR.DefaultIfEmpty()
                          join tc in testCategories.ToList() on subTR?.TestCategoryID equals tc.TestCategoryID
                          into ljTC
                          from subTC in ljTC.DefaultIfEmpty()
                          select new VMRulo
                          {
                              RuloID = r.RuloID,
                              Lote = r.Lote,
                              Beam = r.Beam,
                              Loom = r.Loom,
                              Piece = r.Piece,
                              Style = r.Style,
                              StyleName = r.StyleName,
                              Width = r.Width,
                              EntranceLength = r.EntranceLength,
                              ExitLength = r.ExitLength,
                              IsWaitingAnswerFromTest = r.IsWaitingAnswerFromTest,
                              TestResultID = r.TestResultID,
                              CanContinue = subTR?.CanContinue ?? false,
                              TestCategoryID = subTC?.TestCategoryID ?? 0,
                              TestCategoryCode = subTC?.TestCode ?? string.Empty
                          }).ToList();

            if (ruloFilters.numDefinitionProcess != 0)
            {
                var ruloProcess = ruloProcessRepository.GetQueryable(x => !x.IsDeleted && ((x.BeginningDate <= ruloFilters.dtEnd && x.BeginningDate >= ruloFilters.dtBegin) || (x.BeginningDate <= ruloFilters.dtBegin && x.BeginningDate >= ruloFilters.dtEnd)) && x.EndDate == null && x.DefinationProcessID == ruloFilters.numDefinitionProcess).ToList();

                result = (from r in result
                          join rp in ruloProcess on r.RuloID equals rp.RuloID
                          where rp.DefinationProcessID == ruloFilters.numDefinitionProcess
                          select r).ToList();
            }

            if (ruloFilters.numTestCategory != 0)
                result = result.Where(x => x.TestCategoryID == ruloFilters.numTestCategory).ToList();

            var sql = query.ToQueryString();

            return result;
        }

        public override async Task DeleteRuloProcessFromRuloProcessID(int RuloProcessID, int deleterRef)
        {
            await ruloProcessRepository.Remove(RuloProcessID, deleterRef);
        }
    }
}
