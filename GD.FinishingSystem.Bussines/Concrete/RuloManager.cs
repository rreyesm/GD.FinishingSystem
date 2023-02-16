using GD.FinishingSystem.Bussines.Abstract;
using GD.FinishingSystem.DAL.Abstract;
using GD.FinishingSystem.DAL.Concrete;
using GD.FinishingSystem.DAL.EFdbPerformanceStandards;
using GD.FinishingSystem.DAL.EFdbPlanta;
using GD.FinishingSystem.Entities;
using GD.FinishingSystem.Entities.ViewModels;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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
        private IAsyncRepository<VMRuloReport> ruloReportRepository = null;
        private IAsyncRepository<TblCustomReport> customReportRepository = null;
        private IAsyncRepository<VMRuloBatch> ruloBatchRepository = null;

        public RuloManager(DbContext context)
        {
            this.repository = new GenericRepository<Rulo>(context);
            this.ruloProcessRepository = new GenericRepository<RuloProcess>(context);
            this.definationProcessRepository = new GenericRepository<DefinationProcess>(context);
            this.testResultRepository = new GenericRepository<TestResult>(context);
            this.testCategoryRepository = new GenericRepository<TestCategory>(context);
            this.userRepository = new GenericRepository<User>(context);
            this.sampleRepository = new GenericRepository<Sample>(context);
            this.ruloReportRepository = new GenericRepository<VMRuloReport>(context);
            this.customReportRepository = new GenericRepository<TblCustomReport>(context);
            this.ruloBatchRepository = new GenericRepository<VMRuloBatch>(context);
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

        public override async Task<VMRulo> GetVMRuloFromRuloID(int RuloID)
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

            OriginType originType = OriginType.PP00;
            string sOriginType = string.Empty;
            if (Enum.TryParse(rulo.OriginID.ToString(), true, out originType))
                sOriginType = originType.ToString(); //.SplitCamelCase();

            var userList = await userRepository.GetAll();
            var testResultAuthorizerUser = userList.Where(x => x.UserID == rulo.TestResultAuthorizer).FirstOrDefault();
            var senderUser = userList.Where(x => x.UserID == rulo.SenderID).FirstOrDefault();
            var sentAuthorizer = userList.Where(x => x.UserID == rulo.SentAuthorizerID).FirstOrDefault();

            var vwRulo = new VMRulo()
            {
                RuloID = rulo.RuloID,
                Lote = rulo.Lote,
                Beam = rulo.Beam,
                BeamStop = rulo.BeamStop,
                Loom = rulo.Loom,
                IsToyota = rulo.IsToyota,
                Style = rulo.Style,
                StyleName = rulo.StyleName,
                Width = rulo.Width,
                EntranceLength = rulo.EntranceLength,
                ExitLength = rulo.ExitLength,
                Shift = rulo.Shift,
                FolioNumber = rulo.FolioNumber,
                Observations = rulo.Observations,
                SentDate = rulo.SentDate,
                Sender = senderUser,
                SentAuthorizer = sentAuthorizer,
                SentAuthorizerID = rulo.SentAuthorizerID,
                IsWaitingAnswerFromTest = rulo.IsWaitingAnswerFromTest,
                TestResultID = rulo.TestResultID,
                CanContinue = testResults.FirstOrDefault()?.CanContinue ?? false,
                TestCategoryID = testCategory?.TestCategoryID ?? 0,
                TestCategoryCode = testCategory?.TestCode ?? string.Empty,
                OriginID = sOriginType,
                TestResultAuthorizer = testResultAuthorizerUser?.UserName ?? string.Empty,
            };

            return vwRulo;
        }

        public override async Task<IEnumerable<VMRulo>> GetRuloList()
        {
            var rulos = await repository.GetWhere(o => !o.IsDeleted);
            var testResults = await testResultRepository.GetWhere(x => !x.IsDeleted);
            var testCategories = await testCategoryRepository.GetWhere(x => x.TestCategoryID != 0);

            var originList = VMOriginType.ToList();

            var userList = await userRepository.GetWhere(x => !x.IsDeleted);

            var result = (from r in rulos.ToList()
                          join tr in testResults.ToList() on r.TestResultID equals tr.TestResultID
                          into ljTR
                          from subTR in ljTR.DefaultIfEmpty()
                          join tc in testCategories.ToList() on subTR?.TestCategoryID equals tc.TestCategoryID
                          into ljTC
                          from subTC in ljTC.DefaultIfEmpty()
                          join uSender in userList on r.SenderID equals uSender.UserID
                          into ljuSender
                          from subuSender in ljuSender.DefaultIfEmpty()
                          join uSentAutho in userList on r.SentAuthorizerID equals uSentAutho.UserID
                          into ljuSentAutho
                          from subuSentAutho in ljuSentAutho.DefaultIfEmpty()
                          join uResultAutho in userList on r.TestResultAuthorizer equals uResultAutho.UserID
                          into ljuResultAutho
                          from subuResultAutho in ljuResultAutho.DefaultIfEmpty()
                          join o in originList on r.OriginID equals o.Value
                          into ljO
                          from subO in ljO.DefaultIfEmpty()
                          select new VMRulo
                          {
                              RuloID = r.RuloID,
                              Lote = r.Lote,
                              Beam = r.Beam,
                              Loom = r.Loom,
                              PieceCount = r.PieceCount,
                              Style = r.Style,
                              StyleName = r.StyleName,
                              Width = r.Width,
                              EntranceLength = r.EntranceLength,
                              ExitLength = r.ExitLength,
                              Shift = r.Shift,
                              FolioNumber = r.FolioNumber,
                              Observations = r.Observations,
                              SentDate = r.SentDate,
                              Sender = subuSender,
                              SentAuthorizer = subuSentAutho,
                              SentAuthorizerID = r.SentAuthorizerID,
                              IsWaitingAnswerFromTest = r.IsWaitingAnswerFromTest,
                              TestResultID = r.TestResultID,
                              CanContinue = subTR?.CanContinue ?? false,
                              TestCategoryID = subTC?.TestCategoryID ?? 0,
                              TestCategoryCode = subTC?.TestCode ?? string.Empty,
                              OriginID = subO?.Text,
                              TestResultAuthorizer = subuResultAutho?.UserName ?? string.Empty
                          }).ToList();

            return result;
        }

        public override async Task<IEnumerable<VMRulo>> GetRuloListFromBetweenDate(DateTime begin, DateTime end)
        {
            var rulos = await repository.GetWhere(o => !o.IsDeleted && ((o.CreatedDate <= end && o.CreatedDate >= begin) || (o.CreatedDate <= begin && o.CreatedDate >= end)));
            var testResults = await testResultRepository.GetWhere(x => !x.IsDeleted);
            var testCategories = await testCategoryRepository.GetWhere(x => x.TestCategoryID != 0);

            var originList = VMOriginType.ToList();

            var userList = await userRepository.GetWhere(x => !x.IsDeleted);

            var result = (from r in rulos.ToList()
                          join tr in testResults.ToList() on r.TestResultID equals tr.TestResultID
                          into ljTR
                          from subTR in ljTR.DefaultIfEmpty()
                          join tc in testCategories.ToList() on subTR?.TestCategoryID equals tc.TestCategoryID
                          into ljTC
                          from subTC in ljTC.DefaultIfEmpty()
                          join uSender in userList on r.SenderID equals uSender.UserID
                          into ljuSender
                          from subuSender in ljuSender.DefaultIfEmpty()
                          join uSentAutho in userList on r.SentAuthorizerID equals uSentAutho.UserID
                          into ljuSentAutho
                          from subuSentAutho in ljuSentAutho.DefaultIfEmpty()
                          join uResultAutho in userList on r.TestResultAuthorizer equals uResultAutho.UserID
                          into ljuResultAutho
                          from subuResultAutho in ljuResultAutho.DefaultIfEmpty()
                          join o in originList on r.OriginID equals o.Value
                          into ljO
                          from subO in ljO.DefaultIfEmpty()
                          select new VMRulo
                          {
                              RuloID = r.RuloID,
                              Lote = r.Lote,
                              Beam = r.Beam,
                              BeamStop = r.BeamStop,
                              Loom = r.Loom,
                              IsToyota = r.IsToyota,
                              Style = r.Style,
                              StyleName = r.StyleName,
                              Width = r.Width,
                              EntranceLength = r.EntranceLength,
                              ExitLength = r.ExitLength,
                              Shift = r.Shift,
                              FolioNumber = r.FolioNumber,
                              Observations = r.Observations,
                              SentDate = r.SentDate,
                              Sender = subuSender,
                              SentAuthorizer = subuSentAutho,
                              SentAuthorizerID = r.SentAuthorizerID,
                              IsWaitingAnswerFromTest = r.IsWaitingAnswerFromTest,
                              TestResultID = r.TestResultID,
                              CanContinue = subTR?.CanContinue ?? false,
                              TestCategoryID = subTC?.TestCategoryID ?? 0,
                              TestCategoryCode = subTC?.TestCode ?? string.Empty,
                              OriginID = subO?.Text,
                              TestResultAuthorizer = subuResultAutho?.UserName ?? string.Empty,
                          }).ToList();

            return result;
        }

        public override async Task<IEnumerable<VMRuloProcess>> GetVMRuloProcessesFromRuloID(int RuloID)
        {
            var ruloProcess = await ruloProcessRepository.GetWhere(o => !o.IsDeleted && o.RuloID == RuloID);
            var definationResult = await definationProcessRepository.GetWhere(x => !x.IsDeleted);
            List<Sample> sampleList = new List<Sample>();
            if (ruloProcess != null && ruloProcess.Count() != 0)
            {
                List<int> ruloProcessesList = ruloProcess.ToList().Select(x => x.RuloProcessID).ToList();
                var sampleResult = await sampleRepository.GetWhere(x => !x.IsDeleted && ruloProcessesList.Contains(x.RuloProcessID));
                sampleList = sampleResult.ToList();
            }

            var result = (from rp in ruloProcess.ToList()
                          join dp in definationResult.ToList() on rp.DefinationProcessID equals dp.DefinationProcessID
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
                              IsMustSample = dp.IsMustSample
                          }).ToList();

            result.ForEach(x =>
            {
                x.ExistSample = sampleList.Where(y => y.RuloProcessID == x.RuloProcessID).Any();
            });


            return result;
        }

        public override async Task<IEnumerable<RuloProcess>> GetRuloProcessListFromBetweenDate(DateTime begin, DateTime end)
        {
            var result = await ruloProcessRepository.GetWhere(o => !o.IsDeleted && ((o.CreatedDate <= end && o.CreatedDate >= begin) || (o.CreatedDate <= begin && o.CreatedDate >= end)));

            var definationResult = await definationProcessRepository.GetWhere(x => !x.IsDeleted);
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

        public override async Task<IEnumerable<string>> GetRuloStyleStringForProductionLoteList()
        {
            IEnumerable<string> styleDataList = null;
            using (dbPlantaContext context = new dbPlantaContext())
            {
                styleDataList = await (from ftt in context.FichaTecnicaTelas
                                       join lp in context.LotesDeProduccions
                                       on ftt.CódigoTela equals lp.CódigoTela
                                       select ftt.CódigoTela).Distinct().ToListAsync();
            }

            return styleDataList;
        }

        public override async Task<IEnumerable<VMStyleData>> GetRuloStyleForProductionLoteList()
        {
            IEnumerable<VMStyleData> styleDataList = null;
            using (dbPlantaContext context = new dbPlantaContext())
            {
                styleDataList = await (from ftt in context.FichaTecnicaTelas
                                       join lp in context.LotesDeProduccions
                                       on ftt.CódigoTela equals lp.CódigoTela
                                       select new VMStyleData
                                       {
                                           Style = ftt.CódigoTela,
                                           StyleName = ftt.Nombre,
                                           Lote = lp.Lote
                                       }).ToListAsync();
            }

            return styleDataList;
        }

        public override async Task<VMStyleData> GetRuloStyle(string lote)
        {
            VMStyleData styleData = null;
            using (dbPlantaContext context = new dbPlantaContext())
            {
                styleData = await (from ftt in context.FichaTecnicaTelas
                                   join lp in context.LotesDeProduccions
                                   on ftt.CódigoTela equals lp.CódigoTela
                                   where lp.Lote == lote
                                   select new VMStyleData
                                   {
                                       Style = ftt.CódigoTela,
                                       StyleName = ftt.Nombre,
                                       Lote = lp.Lote
                                   }).FirstOrDefaultAsync();
            }


            return styleData;
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
                (!(ruloFilters.numPiece > 0) || r.PieceCount == ruloFilters.numPiece) &&
                (!(ruloFilters.FolioNumber > 0) || r.FolioNumber == ruloFilters.FolioNumber) &&
                 ((string.IsNullOrWhiteSpace(ruloFilters.txtStyle)) || r.Style.Contains(ruloFilters.txtStyle))

                select r

                ).ToList();

            var testResults = await testResultRepository.GetWhere(x => !x.IsDeleted);
            var testCategories = await testCategoryRepository.GetWhere(x => x.TestCategoryID != 0);

            //Add machine by rulo
            var definationsProcess = await definationProcessRepository.GetWhereWithNoTrack(x => !x.IsDeleted);

            var ruloProcessGroup = await ruloProcessRepository.GetWhereWithNoTrack(x => !x.IsDeleted && ((x.BeginningDate <= ruloFilters.dtEnd && x.BeginningDate >= ruloFilters.dtBegin) || (x.BeginningDate <= ruloFilters.dtBegin && x.BeginningDate >= ruloFilters.dtEnd)));
            List<RuloProcess> ruloProcessesList = new List<RuloProcess>();
            ruloProcessesList = ruloProcessGroup.Where(x => !x.IsDeleted).GroupBy(x => x.RuloID, (key, g) => g.OrderByDescending(e => e.RuloProcessID).FirstOrDefault()).ToList();

            var resulRPAndDP = (from rp in ruloProcessesList
                                join dp in definationsProcess.ToList() on rp.DefinationProcessID equals dp.DefinationProcessID
                                select new
                                {
                                    rp.RuloID,
                                    rp.RuloProcessID,
                                    dp.DefinationProcessID,
                                    dp.ProcessCode,
                                    dp.Name
                                }).ToList();
            //////////////////////////
            
            //Add Origin
            var originList = VMOriginType.ToList();

            var result = (from r in rulos
                          join tr in testResults.ToList() on r.TestResultID equals tr.TestResultID
                          into ljTR
                          from subTR in ljTR.DefaultIfEmpty()
                          join tc in testCategories.ToList() on subTR?.TestCategoryID equals tc.TestCategoryID
                          into ljTC
                          from subTC in ljTC.DefaultIfEmpty()

                          join rp in resulRPAndDP.ToList() on r.RuloID equals rp.RuloID
                          into ljRP
                          from subRP in ljRP.DefaultIfEmpty()

                          join o in originList on r.OriginID equals o.Value
                          into ljO
                          from subO in ljO.DefaultIfEmpty()
                          select new VMRulo
                          {
                              RuloID = r.RuloID,
                              Lote = r.Lote,
                              Beam = r.Beam,
                              BeamStop = r.BeamStop,
                              Loom = r.Loom,
                              IsToyota = r.IsToyota,
                              PieceCount = r.PieceCount,
                              Style = r.Style,
                              StyleName = r.StyleName,
                              Width = r.Width,
                              EntranceLength = r.EntranceLength,
                              ExitLength = r.ExitLength,
                              Shift = r.Shift,
                              FolioNumber = r.FolioNumber,
                              Observations = r.Observations,
                              SentDate = r.SentDate,
                              Sender = r.Sender,
                              SentAuthorizer = r.SentAuthorizer,
                              SentAuthorizerID = r.SentAuthorizerID,
                              IsWaitingAnswerFromTest = r.IsWaitingAnswerFromTest,
                              TestResultID = r.TestResultID,
                              CanContinue = subTR?.CanContinue ?? false,
                              DateTestResult = subTR?.CreatedDate,
                              BatchNumbers = null,
                              TestCategoryID = subTC?.TestCategoryID ?? 0,
                              TestCategoryCode = subTC?.TestCode ?? string.Empty,
                              Machine = subRP?.Name,
                              OriginID = subO?.Text
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

            //2023-02-15
            var ruloBatch = await ruloBatchRepository.GetWithRawSql("stpGetBatchesFromGuven @p0", new[] { string.Join(",", result.Select(x => x.RuloID)) });

            result.ForEach(x =>
            {
                x.BatchNumbers = string.Join(",", ruloBatch.Where(y => y.RuloID == x.RuloID).Select(y => y.BatchNumbers));
            });

            return result.OrderByDescending(x => x.RuloID);
        }

        public override async Task<IEnumerable<VMRuloReport>> GetRuloReportListFromFilters(VMRuloFilters ruloFilters)
        {
            var query = repository.GetQueryable(x => !x.IsDeleted && ((x.CreatedDate <= ruloFilters.dtEnd && x.CreatedDate >= ruloFilters.dtBegin) || (x.CreatedDate <= ruloFilters.dtBegin && x.CreatedDate >= ruloFilters.dtEnd)));

            var rulos = (
                from r in query
                where
                (!(ruloFilters.numLote > 0) || r.Lote == ruloFilters.numLote.ToString()) &&
                (!(ruloFilters.numBeam > 0) || r.Beam == ruloFilters.numBeam) &&
                (!(ruloFilters.numLoom > 0) || r.Loom == ruloFilters.numLoom) &&
                (!(ruloFilters.numPiece > 0) || r.PieceCount == ruloFilters.numPiece) &&
                (!(ruloFilters.FolioNumber > 0) || r.FolioNumber == ruloFilters.FolioNumber) &&
                 ((string.IsNullOrWhiteSpace(ruloFilters.txtStyle)) || r.Style.Contains(ruloFilters.txtStyle))

                select r

                ).AsNoTracking().ToList();

            var testResults = await testResultRepository.GetWhere(x => !x.IsDeleted);
            var testCategories = await testCategoryRepository.GetWhere(x => x.TestCategoryID != 0);
            var userList = await userRepository.GetWhere(x => !x.IsDeleted);
            var originList = VMOriginType.ToList();
            var definationsProcess = await definationProcessRepository.GetWhereWithNoTrack(x => !x.IsDeleted);

            var ruloProcessGroup = await ruloProcessRepository.GetWhereWithNoTrack(x => !x.IsDeleted && ((x.BeginningDate <= ruloFilters.dtEnd && x.BeginningDate >= ruloFilters.dtBegin) || (x.BeginningDate <= ruloFilters.dtBegin && x.BeginningDate >= ruloFilters.dtEnd)));
            IEnumerable<IGrouping<int, RuloProcess>> ruloProcessesListGroup = ruloProcessGroup.GroupBy(x => x.RuloID).ToList();
            List<RuloProcess> ruloProcessesList = new List<RuloProcess>();

            foreach (var key in ruloProcessesListGroup)
            {
                ruloProcessesList.Add(key.Where(x=> !x.IsDeleted).MaxBy(x => x.RuloProcessID));
            }

            //Get value rama
            List<RuloProcess> ruloProcessRamaList = new List<RuloProcess>();
            foreach (var key in ruloProcessesListGroup)
            {
                var temp = key.Where(x => x.DefinationProcessID == 6).FirstOrDefault();
                if (temp != null)
                    ruloProcessRamaList.Add(temp);
            }

            var resulRPAndDP = (from rp in ruloProcessesList
                                join dp in definationsProcess.ToList() on rp.DefinationProcessID equals dp.DefinationProcessID
                                select new
                                {
                                    rp.RuloID,
                                    rp.RuloProcessID,
                                    dp.DefinationProcessID,
                                    dp.ProcessCode,
                                    dp.Name
                                }).ToList();


            var result = (from r in rulos
                          join rp in resulRPAndDP.ToList() on r.RuloID equals rp.RuloID
                          into ljRP
                          from subRP in ljRP.DefaultIfEmpty()

                          join rprama in ruloProcessRamaList on r.RuloID equals rprama.RuloID
                          into ljRPRAMA
                          from subRPRAMA in ljRPRAMA.DefaultIfEmpty()

                          join tr in testResults.ToList() on r.TestResultID equals tr.TestResultID
                          into ljTR
                          from subTR in ljTR.DefaultIfEmpty()
                          join tc in testCategories.ToList() on subTR?.TestCategoryID equals tc.TestCategoryID
                          into ljTC
                          from subTC in ljTC.DefaultIfEmpty()
                          join uResultAutho in userList on r.TestResultAuthorizer equals uResultAutho.UserID
                          into ljuResultAutho
                          from subuResultAutho in ljuResultAutho.DefaultIfEmpty()
                          join uCreator in userList on r.CreatorID equals uCreator.UserID
                          into ljuCreator
                          from subuCreator in ljuCreator.DefaultIfEmpty()
                          join o in originList on r.OriginID equals o.Value
                          into ljO
                          from subO in ljO.DefaultIfEmpty()
                          select new VMRuloReport
                          {
                              RuloID = r.RuloID,
                              Lote = r.Lote,
                              Beam = r.Beam,
                              BeamStop = r.BeamStop,
                              Loom = r.Loom,
                              IsToyota = r.IsToyota ? "Yes" : "No",
                              PieceCount = r.PieceCount,
                              Style = r.Style,
                              StyleName = r.StyleName,
                              Width = r.Width,
                              EntranceLength = r.EntranceLength,
                              ExitLengthRama = subRPRAMA?.FinishMeter ?? 0,
                              ExitLength = r.ExitLength,
                              Shift = r.Shift,
                              FolioNumber = r.FolioNumber,
                              RuloObservations = r.Observations,
                              SentDate = r.SentDate,
                              SenderID = r.Sender?.ToString(),
                              SentAuthorizerID = r.SentAuthorizer?.ToString(),
                              IsWaitingAnswerFromTest = r.IsWaitingAnswerFromTest ? "Yes" : "No",
                              CanContinue = subTR?.CanContinue == null ? string.Empty : subTR?.CanContinue != null ? "Yes" : "No",
                              TestResultObservations = subTR?.Details,
                              TestCategoryID = subTC?.TestCategoryID ?? 0,
                              TestCategoryCode = subTC?.TestCode ?? string.Empty,
                              DateTestResult = subTR?.CreatedDate,
                              BatchNumbers = null,
                              OriginID = subO?.Text,
                              TestResultAuthorizer = subuResultAutho?.UserName ?? string.Empty,
                              CreatorID = subuCreator?.UserName ?? string.Empty,
                              CreatedDate = r.CreatedDate,
                              LastRuloProcess = subRP?.Name
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

            //2023-02-15
            var ruloBatch = await ruloBatchRepository.GetWithRawSql("stpGetBatchesFromGuven @p0", new[] { string.Join(",", result.Select(x => x.RuloID)) });

            result.ForEach(x =>
            {
                x.BatchNumbers = string.Join(",", ruloBatch.Where(x=> x.RuloID == x.RuloID).Select(x => x.BatchNumbers));
            });

            return result;
        }

        public override async Task DeleteRuloProcessFromRuloProcessID(int RuloProcessID, int deleterRef)
        {
            await ruloProcessRepository.Remove(RuloProcessID, deleterRef);
        }

        public async override Task<Rulo> GetRuloFromFolio(int folioNumber)
        {
            return await repository.FirstOrDefault(x => !x.IsDeleted && x.FolioNumber == folioNumber);
        }

        public async override Task<IEnumerable<VMRuloReport>> GetAllVMRuloReportList(string query, params object[] parameters)
        {

            var ruloReportList = await ruloReportRepository.GetWithRawSql(query, parameters);

            return ruloReportList;
        }

        public async override Task<int> GetPerformanceRuloID(int ruloId)
        {
            int id = 0;
            var rulo = await repository.GetByPrimaryKey(ruloId);

            using (dbPerformanceStandardsContext context = new dbPerformanceStandardsContext())
            {
                var testMaster = context.TblTestMasters.Where(x => x.Lote == int.Parse(rulo.Lote) && x.Beam == rulo.Beam).OrderByDescending(x => x.CreateDate).FirstOrDefault();

                if (testMaster != null)
                {
                    id = testMaster.Id;
                }
            }

            return id;
        }

        public async override Task<IEnumerable<TblCustomPerformanceForFinishing>> GetPerformanceTestResultByRuloId(int ruloId)
        {
            var rulo = await repository.GetByPrimaryKey(ruloId);

            List<TblCustomPerformanceForFinishing> tblCustomPerformanceForFinishingList = new List<TblCustomPerformanceForFinishing>();

            try
            {
                using (dbPerformanceStandardsContext context = new dbPerformanceStandardsContext())
                {
                    var testMaster = context.TblTestMasters.Where(x => x.Lote == int.Parse(rulo.Lote) && x.Beam == rulo.Beam).OrderByDescending(x => x.CreateDate).FirstOrDefault();

                    if (testMaster != null)
                    {
                        List<SqlParameter> sqlParameters = new List<SqlParameter>();
                        sqlParameters.Add(new SqlParameter("@p0", testMaster.Id));

                        tblCustomPerformanceForFinishingList = await context.TblCustomPerformanceForFinishings.FromSqlRaw("stpGetPerformanceForFinishing @p0", sqlParameters.ToArray()).ToListAsync();
                    }

                }
            }
            catch (Exception ex)
            {
            }

            return tblCustomPerformanceForFinishingList;
        }

        public async override Task<IEnumerable<TblCustomPerformanceForFinishing>> GetPerformanceTestResultById(int perfomanceId)
        {
            List<TblCustomPerformanceForFinishing> tblCustomPerformanceForFinishingList = new List<TblCustomPerformanceForFinishing>();

            try
            {
                using (dbPerformanceStandardsContext context = new dbPerformanceStandardsContext())
                {
                    List<SqlParameter> sqlParameters = new List<SqlParameter>();
                    sqlParameters.Add(new SqlParameter("@p0", perfomanceId));

                    tblCustomPerformanceForFinishingList = await context.TblCustomPerformanceForFinishings.FromSqlRaw("stpGetPerformanceForFinishing @p0", sqlParameters.ToArray()).ToListAsync();

                }
            }
            catch (Exception ex)
            {
            }

            return tblCustomPerformanceForFinishingList;
        }

        public async override Task<IEnumerable<TblCustomPerformanceMasiveForFinishing>> GetPerformanceTestResultMasive(List<int> testMasterList)
        {
            List<TblCustomPerformanceMasiveForFinishing> tblCustomPerformanceMasiveForFinishingList = new List<TblCustomPerformanceMasiveForFinishing>();
            try
            {
                using (dbPerformanceStandardsContext context = new dbPerformanceStandardsContext())
                {
                    List<SqlParameter> sqlParameters = new List<SqlParameter>();
                    sqlParameters.Add(new SqlParameter("@p0", string.Join(",", testMasterList)));

                    tblCustomPerformanceMasiveForFinishingList = await context.TblCustomPerformanceMasiveForFinishing.FromSqlRaw("stpGetPerformanceMasiveForFinishing @p0", sqlParameters.ToArray()).ToListAsync();
                }
            }
            catch (Exception ex)
            {
            }

            return tblCustomPerformanceMasiveForFinishingList;
        }

        public async override Task<IEnumerable<TblCustomReport>> GetCustomReportList(VMReportFilter reportFilter)
        {
            //reportFilter.dtEnd = reportFilter.dtEnd.AddDays(1).AddMilliseconds(-1);
            string query = string.Empty;

            //Order in stored procedure
            var txtStyle = string.IsNullOrWhiteSpace(reportFilter.txtStyle) ? reportFilter.txtStyle : null;
            var numLote = reportFilter.numLote != 0 ? (int?)reportFilter.numLote : null;
            var numBeam = reportFilter.numBeam != 0 ? (int?)reportFilter.numBeam : null;
            var stop = string.IsNullOrWhiteSpace(reportFilter.stop) ? reportFilter.stop : null;
            var numLoom = reportFilter.numLoom != 0 ? (int?)reportFilter.numLoom : null;
            var shift = reportFilter.shift != 0 ? (int?)reportFilter.shift : null;

            switch (reportFilter.typeReport)
            {
                case 1:
                    query = "spGetProcessesCompletedReport @p0,@p1,@p2,@p3,@p4,@p5,@p6,@p7";
                    break;
                case 2:
                    query = "spGetAllProcessesReport @p0,@p1,@p2,@p3,@p4,@p5,@p6,@p7";
                    break;
                default:
                    break;
            }

            object[] parameters = new object[] {
            reportFilter.dtBegin.ToString("yyyy-MM-dd HH:mm:ss"),
            reportFilter.dtEnd.ToString("yyyy-MM-dd HH:mm:ss"),
            txtStyle,
            numLote,
            numBeam,
            stop,
            numLoom,
            shift
            };

            var ruloReportList = await customReportRepository.GetWithRawSql(query, parameters);

            return ruloReportList;
        }

        public async override Task<string> GetMachineByRuloId(int ruloId)
        {
            var ruloProcess = await ruloProcessRepository.GetWhere(o => !o.IsDeleted && o.RuloID == ruloId);
            var definationResult = await definationProcessRepository.GetWhere(x => !x.IsDeleted);

            var result = (from rp in ruloProcess.ToList()
                          join dp in definationResult.ToList() on rp.DefinationProcessID equals dp.DefinationProcessID
                          where rp.LastUpdateDate != null && !rp.IsDeleted
                          orderby rp.LastUpdateDate descending
                          select dp.Name
                          ).FirstOrDefault();

            return result ?? string.Empty;
        }

    }
}
