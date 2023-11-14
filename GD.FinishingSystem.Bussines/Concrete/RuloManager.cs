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
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
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
        private IAsyncRepository<VMRulo> ruloReportRepository = null;
        private IAsyncRepository<TblCustomReport> customReportRepository = null;
        private IAsyncRepository<VMRuloBatch> ruloBatchRepository = null;
        private IAsyncRepository<OriginCategory> originRepository = null;
        private IAsyncRepository<WarehouseCategory> warehouseRepository = null;
        private DbContext Context = null;

        public RuloManager(DbContext context)
        {
            this.repository = new GenericRepository<Rulo>(context);
            this.ruloProcessRepository = new GenericRepository<RuloProcess>(context);
            this.definationProcessRepository = new GenericRepository<DefinationProcess>(context);
            this.testResultRepository = new GenericRepository<TestResult>(context);
            this.testCategoryRepository = new GenericRepository<TestCategory>(context);
            this.userRepository = new GenericRepository<User>(context);
            this.sampleRepository = new GenericRepository<Sample>(context);
            this.ruloReportRepository = new GenericRepository<VMRulo>(context);
            this.customReportRepository = new GenericRepository<TblCustomReport>(context);
            this.ruloBatchRepository = new GenericRepository<VMRuloBatch>(context);
            this.originRepository = new GenericRepository<OriginCategory>(context);
            this.warehouseRepository = new GenericRepository<WarehouseCategory>(context);
            this.Context = context;
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

            //OriginType originType = OriginType.PP00;
            //string sOriginType = string.Empty;
            //if (Enum.TryParse(rulo.OriginID.ToString(), true, out originType))
            //    sOriginType = originType.ToString(); //.SplitCamelCase();

            var origins = await originRepository.GetWhere(x => !x.IsDeleted);
            var origin = origins.Where(x=> x.OriginCategoryID == rulo.OriginID).FirstOrDefault();
            var mainOrigin = origins.Where(x => x.OriginCategoryID == rulo.MainOriginID).FirstOrDefault();

            var warehouses = await warehouseRepository.GetWhere(x => !x.IsDeleted && x.WarehouseCategoryID == rulo.WarehouseCategoryID);
            var warehouse = warehouses.FirstOrDefault();

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
                IsToyotaValue = rulo.IsToyota,
                IsToyota = rulo.IsToyota ? "Yes" : "No",
                Style = rulo.Style,
                StyleName = rulo.StyleName,
                Width = rulo.Width,
                WeavingLength = rulo.WeavingLength,
                EntranceLength = rulo.EntranceLength,
                ExitLength = rulo.ExitLength,
                Shift = rulo.Shift,
                FolioNumber = rulo.FolioNumber,
                RuloObservations = rulo.Observations,
                SentDate = rulo.SentDate,
                //Sender = senderUser,
                //SentAuthorizer = sentAuthorizer,
                SentAuthorizerID = rulo.SentAuthorizerID,
                IsWaitingAnswerFromTestValue = rulo.IsWaitingAnswerFromTest,
                TestResultID = rulo.TestResultID,
                CanContinueValue = testResults.FirstOrDefault()?.CanContinue ?? false,
                CanContinue = (testResults.FirstOrDefault()?.CanContinue ?? false) == true ? "YES" : "NO",
                TestCategoryID = testCategory?.TestCategoryID ?? 0,
                TestCategoryCode = testCategory?.TestCode ?? string.Empty,
                MainOriginCode = mainOrigin?.OriginCode,
                OriginCode = origin?.OriginCode, //sOriginType,
                TestResultAuthorizer = testResultAuthorizerUser?.UserName ?? string.Empty,
                WarehouseCode = warehouse?.WarehouseCode,
            };

            return vwRulo;
        }

        public override async Task<IEnumerable<VMRulo>> GetRuloList()
        {
            var rulos = await repository.GetWhere(o => !o.IsDeleted);
            var testResults = await testResultRepository.GetWhere(x => !x.IsDeleted);
            var testCategories = await testCategoryRepository.GetWhere(x => !x.IsDeleted && x.TestCategoryID != 0);

            //var originList = VMOriginType.ToList();

            var originList = await originRepository.GetWhere(x => !x.IsDeleted);
            var warehouses = await warehouseRepository.GetWhere(x => !x.IsDeleted);

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
                          join o in originList on r.OriginID equals o.OriginCategoryID
                          into ljO
                          from subO in ljO.DefaultIfEmpty()
                          join mainO in originList on r.OriginID equals mainO.OriginCategoryID
                          into ljmainO
                          from submainO in ljmainO.DefaultIfEmpty()

                          join wh in warehouses.ToList() on r.WarehouseCategoryID equals wh.WarehouseCategoryID
                          into ljWH
                          from subWH in ljWH.DefaultIfEmpty()
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
                              WeavingLength = r.WeavingLength,
                              EntranceLength = r.EntranceLength,
                              ExitLength = r.ExitLength,
                              Shift = r.Shift,
                              FolioNumber = r.FolioNumber,
                              RuloObservations = r.Observations,
                              SentDate = r.SentDate,
                              //Sender = subuSender,
                              ////SentAuthorizer = subuSentAutho,
                              SentAuthorizerID = r.SentAuthorizerID,
                              IsWaitingAnswerFromTestValue = r.IsWaitingAnswerFromTest,
                              TestResultID = r.TestResultID,
                              CanContinueValue = subTR?.CanContinue ?? false,
                              CanContinue = (subTR?.CanContinue ?? false) == true ? "YES" : "NO",
                              TestCategoryID = subTC?.TestCategoryID ?? 0,
                              TestCategoryCode = subTC?.TestCode ?? string.Empty,
                              MainOriginCode = submainO?.OriginCode ?? string.Empty,
                              OriginCode = subO?.OriginCode,
                              TestResultAuthorizer = subuResultAutho?.UserName ?? string.Empty,
                              WarehouseCode = subWH?.WarehouseCode
                          }).ToList();

            return result;
        }

        public override async Task<IEnumerable<VMRulo>> GetRuloListFromBetweenDate(DateTime begin, DateTime end)
        {
            var rulos = await repository.GetWhere(o => !o.IsDeleted && ((o.CreatedDate <= end && o.CreatedDate >= begin) || (o.CreatedDate <= begin && o.CreatedDate >= end)));
            var testResults = await testResultRepository.GetWhere(x => !x.IsDeleted);
            var testCategories = await testCategoryRepository.GetWhere(x => x.TestCategoryID != 0);

            //var originList = VMOriginType.ToList();

            var originList = await originRepository.GetWhere(x => !x.IsDeleted);
            var warehouses = await warehouseRepository.GetWhere(x => !x.IsDeleted);

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
                          join o in originList.ToList() on r.OriginID equals o.OriginCategoryID
                          into ljO
                          from subO in ljO.DefaultIfEmpty()
                          join mainO in originList on r.OriginID equals mainO.OriginCategoryID
                          into ljmainO
                          from submainO in ljmainO.DefaultIfEmpty()

                          join wh in warehouses.ToList() on r.WarehouseCategoryID equals wh.WarehouseCategoryID
                          into ljWH
                          from subWH in ljWH.DefaultIfEmpty()
                          select new VMRulo
                          {
                              RuloID = r.RuloID,
                              Lote = r.Lote,
                              Beam = r.Beam,
                              BeamStop = r.BeamStop,
                              Loom = r.Loom,
                              IsToyotaValue = r.IsToyota,
                              IsToyota = r.IsToyota ? "Yes" : "No",
                              Style = r.Style,
                              StyleName = r.StyleName,
                              Width = r.Width,
                              WeavingLength = r.WeavingLength,
                              EntranceLength = r.EntranceLength,
                              ExitLength = r.ExitLength,
                              Shift = r.Shift,
                              FolioNumber = r.FolioNumber,
                              RuloObservations = r.Observations,
                              SentDate = r.SentDate,
                              ////Sender = subuSender,
                              ////SentAuthorizer = subuSentAutho,
                              SentAuthorizerID = r.SentAuthorizerID,
                              IsWaitingAnswerFromTestValue = r.IsWaitingAnswerFromTest,
                              TestResultID = r.TestResultID,
                              CanContinueValue = subTR?.CanContinue ?? false,
                              TestCategoryID = subTC?.TestCategoryID ?? 0,
                              TestCategoryCode = subTC?.TestCode ?? string.Empty,
                              MainOriginCode = submainO?.OriginCode ?? string.Empty,
                              OriginCode = subO?.OriginCode,
                              TestResultAuthorizer = subuResultAutho?.UserName ?? string.Empty,
                              WarehouseCode = subWH?.WarehouseCode
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

        private object[] GetParameters(VMRuloFilters ruloFilters, string numLote, int? numBeam, int? numLoom, int? numPiece, int? folioNumber, string txtStyle, int? numDefinitionProcess, int? numTestCategory, bool withBatches, int? numRuloID, bool isAccountDate, bool isCount, int? pag, int? tammPag)
        {
            return new object[] {
                ruloFilters.dtBegin.ToString("yyyy-MM-dd HH:mm:ss"),
                ruloFilters.dtEnd.ToString("yyyy-MM-dd HH:mm:ss"),
                numLote,
                numBeam,
                numLoom,
                numPiece,
                folioNumber,
                txtStyle,
                numDefinitionProcess,
                numTestCategory,
                withBatches,
                numRuloID,
                isAccountDate,
                isCount,
                pag,
                tammPag
                };
        }

        public override async Task<int> GetRuloTotalRecords(VMRuloFilters ruloFilters)
        {
            //TotalResult result = null;

            //Order in stored procedure
            string numLote = ruloFilters.numLote != 0 ? ruloFilters.numLote.ToString() : null;
            int? numBeam = ruloFilters.numBeam != 0 ? (int?)ruloFilters.numBeam : null;
            int? numLoom = ruloFilters.numLoom != 0 ? (int?)ruloFilters.numLoom : null;
            int? numPiece = ruloFilters.numPiece != 0 ? (int?)ruloFilters.numPiece : null;
            int? folioNumber = ruloFilters.FolioNumber != 0 ? (int?)ruloFilters.FolioNumber : null;
            string txtStyle = !string.IsNullOrWhiteSpace(ruloFilters.txtStyle) ? ruloFilters.txtStyle : null;
            int? numDefinitionProcess = ruloFilters.numDefinitionProcess != 0 ? (int?)ruloFilters.numDefinitionProcess : null;
            int? numTestCategory = ruloFilters.numTestCategory != 0 ? (int?)ruloFilters.numTestCategory : null;
            bool withBatches = false; //Pasamos por dafult el valor de false o 0 ya que sólo requerimos contar los regitros sin información de Guven;
            int? numRuloID = ruloFilters.numRuloID != 0 ? (int?)ruloFilters.numRuloID : null;
            bool isAccountDate = ruloFilters.IsAccountingDate;
            bool isCount = true; //Se pasa como true, ya que se va a obetener el total de registros
            int? pag = 0; //Si se va a obtener todos los registros pasar estos valores como 0
            int? tammPag = 0;

            object[] parameters = GetParameters(ruloFilters, numLote, numBeam, numLoom, numPiece, folioNumber, txtStyle, numDefinitionProcess, numTestCategory, withBatches, numRuloID, isAccountDate, isCount, pag, tammPag);

            var result = Context.Set<TotalResult>().FromSqlRaw("exec stpRuloReport @p0,@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9,@p10,@p11,@p12,@p13,@p14,@p15", parameters)
                                         .AsEnumerable().FirstOrDefault();

            return result.TotalRecords;

        }

        public override async Task<IEnumerable<VMRulo>> GetRuloListFromFilters(VMRuloFilters ruloFilters, int currentPaindex, int pageSize)
        {
            IEnumerable<VMRulo> result = null;

            //Order in stored procedure
            string numLote = ruloFilters.numLote != 0 ? ruloFilters.numLote.ToString() : null;
            int? numBeam = ruloFilters.numBeam != 0 ? (int?)ruloFilters.numBeam : null;
            int? numLoom = ruloFilters.numLoom != 0 ? (int?)ruloFilters.numLoom : null;
            int? numPiece = ruloFilters.numPiece != 0 ? (int?)ruloFilters.numPiece : null;
            int? folioNumber = ruloFilters.FolioNumber != 0 ? (int?)ruloFilters.FolioNumber : null;
            string txtStyle = !string.IsNullOrWhiteSpace(ruloFilters.txtStyle) ? ruloFilters.txtStyle : null;
            int? numDefinitionProcess = ruloFilters.numDefinitionProcess != 0 ? (int?)ruloFilters.numDefinitionProcess : null;
            int? numTestCategory = ruloFilters.numTestCategory != 0 ? (int?)ruloFilters.numTestCategory : null;
            bool withBatches = currentPaindex > 0 || pageSize > 0 ? true : ruloFilters.withBatches; //Pasamos este valor por default en True o 1 ya que es para obtener los registros por pagina y debe venir con toda la información de Guven
            int? numRuloID = ruloFilters.numRuloID != 0 ? (int?)ruloFilters.numRuloID : null;
            bool isAccountDate = ruloFilters.IsAccountingDate;
            bool isCount = false; //Se pasa como false ya que aquí nunca se va a usar el count para contar el total de registros
            int? pag = currentPaindex; //Si se va a obtener todos los registros pasar estos valores como 0
            int? tammPag = pageSize;

            object[] parameters = GetParameters(ruloFilters, numLote, numBeam, numLoom, numPiece, folioNumber, txtStyle, numDefinitionProcess, numTestCategory, withBatches, numRuloID, isAccountDate, isCount, pag, tammPag);

            result = await ruloReportRepository.GetWithRawSql("stpRuloReport @p0,@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9,@p10,@p11,@p12,@p13,@p14,@p15", parameters);
            return result;
        }


        //public override async Task<IEnumerable<VMRulo>> GetRuloListFromFilters(VMRuloFilters ruloFilters)
        //{
        //    IQueryable<Rulo> query = null;

        //    if (ruloFilters.IsAccountingDate)
        //        query = repository.GetQueryable(x => !x.IsDeleted && ((x.LastUpdateDate <= ruloFilters.dtEnd && x.LastUpdateDate >= ruloFilters.dtBegin) || (x.LastUpdateDate <= ruloFilters.dtBegin && x.LastUpdateDate >= ruloFilters.dtEnd)));
        //    else
        //        query = repository.GetQueryable(x => !x.IsDeleted && ((x.CreatedDate <= ruloFilters.dtEnd && x.CreatedDate >= ruloFilters.dtBegin) || (x.CreatedDate <= ruloFilters.dtBegin && x.CreatedDate >= ruloFilters.dtEnd)));

        //    IQueryable<Rulo> rulos = null;
        //    if (ruloFilters.numRuloID > 0)
        //    {
        //        rulos = (from r in query
        //                 where
        //                  r.RuloID == ruloFilters.numRuloID
        //                 select r);
        //    }
        //    else
        //    {
        //        rulos = (
        //        from r in query
        //        where
        //        (!(ruloFilters.numLote > 0) || r.Lote == ruloFilters.numLote.ToString()) &&
        //        (!(ruloFilters.numBeam > 0) || r.Beam == ruloFilters.numBeam) &&
        //        (!(ruloFilters.numLoom > 0) || r.Loom == ruloFilters.numLoom) &&
        //        (!(ruloFilters.numPiece > 0) || r.PieceCount == ruloFilters.numPiece) &&
        //        (!(ruloFilters.FolioNumber > 0) || r.FolioNumber == ruloFilters.FolioNumber) &&
        //         ((string.IsNullOrWhiteSpace(ruloFilters.txtStyle)) || r.Style.Contains(ruloFilters.txtStyle)
        //         )
        //        select r);
        //    }

        //    var testResults = await testResultRepository.GetWhere(x => !x.IsDeleted);
        //    var testCategories = await testCategoryRepository.GetWhere(x => x.TestCategoryID != 0);

        //    //Add machine by rulo
        //    var definationsProcess = await definationProcessRepository.GetWhereWithNoTrack(x => !x.IsDeleted);

        //    var ruloProcessGroup = await ruloProcessRepository.GetWhereWithNoTrack(x => !x.IsDeleted && ((x.BeginningDate <= ruloFilters.dtEnd && x.BeginningDate >= ruloFilters.dtBegin) || (x.BeginningDate <= ruloFilters.dtBegin && x.BeginningDate >= ruloFilters.dtEnd)));
        //    List<RuloProcess> ruloProcessesList = new List<RuloProcess>();
        //    ruloProcessesList = ruloProcessGroup.Where(x => !x.IsDeleted).GroupBy(x => x.RuloID, (key, g) => g.OrderByDescending(e => e.RuloProcessID).FirstOrDefault()).ToList();

        //    var resulRPAndDP = (from rp in ruloProcessesList
        //                        join dp in definationsProcess.ToList() on rp.DefinationProcessID equals dp.DefinationProcessID
        //                        select new
        //                        {
        //                            rp.RuloID,
        //                            rp.RuloProcessID,
        //                            dp.DefinationProcessID,
        //                            dp.ProcessCode,
        //                            dp.Name
        //                        }).ToList();
        //    //////////////////////////

        //    //Add Origin
        //    var originList = VMOriginType.ToList();

        //    var result = (from r in rulos.ToList()
        //                  join tr in testResults.ToList() on r.TestResultID equals tr.TestResultID
        //                  into ljTR
        //                  from subTR in ljTR.DefaultIfEmpty()
        //                  join tc in testCategories.ToList() on subTR?.TestCategoryID equals tc.TestCategoryID
        //                  into ljTC
        //                  from subTC in ljTC.DefaultIfEmpty()

        //                  join rp in resulRPAndDP.ToList() on r.RuloID equals rp.RuloID
        //                  into ljRP
        //                  from subRP in ljRP.DefaultIfEmpty()

        //                  join o in originList on r.OriginID equals o.Value
        //                  into ljO
        //                  from subO in ljO.DefaultIfEmpty()
        //                  select new VMRulo
        //                  {
        //                      RuloID = r.RuloID,
        //                      Lote = r.Lote,
        //                      Beam = r.Beam,
        //                      BeamStop = r.BeamStop,
        //                      Loom = r.Loom,
        //                      IsToyotaValue = r.IsToyota,
        //                      PieceCount = r.PieceCount,
        //                      Style = r.Style,
        //                      StyleName = r.StyleName,
        //                      Width = r.Width,
        //                      WeavingLength = r.WeavingLength,
        //                      EntranceLength = r.EntranceLength,
        //                      ExitLength = r.ExitLength,
        //                      ExitLengthMinusSamples = 0,
        //                      Shrinkage = r.Shrinkage,
        //                      Shift = r.Shift,
        //                      FolioNumber = r.FolioNumber,
        //                      RuloObservations = r.Observations,
        //                      SentDate = r.SentDate,
        //                      //Sender = r.Sender,
        //                      //SentAuthorizer = r.SentAuthorizer,
        //                      SentAuthorizerID = r.SentAuthorizerID,
        //                      IsWaitingAnswerFromTestValue = r.IsWaitingAnswerFromTest,
        //                      TestResultID = r.TestResultID,
        //                      CanContinueValue = subTR?.CanContinue ?? false,
        //                      DateTestResult = subTR?.CreatedDate,
        //                      InspectionLength = 0,
        //                      InspectionCuttingLength = 0,
        //                      BatchNumbers = null,
        //                      TestCategoryID = subTC?.TestCategoryID ?? 0,
        //                      TestCategoryCode = subTC?.TestCode ?? string.Empty,
        //                      Machine = subRP?.Name,
        //                      OriginID = subO?.Text
        //                  }).ToList();

        //    if (ruloFilters.numDefinitionProcess != 0)
        //    {
        //        //var ruloProcess = ruloProcessRepository.GetQueryable(x => !x.IsDeleted && ((x.BeginningDate <= ruloFilters.dtEnd && x.BeginningDate >= ruloFilters.dtBegin) || (x.BeginningDate <= ruloFilters.dtBegin && x.BeginningDate >= ruloFilters.dtEnd)) && x.EndDate == null && x.DefinationProcessID == ruloFilters.numDefinitionProcess).ToList();

        //        //result = (from r in result
        //        //          join rp in ruloProcess on r.RuloID equals rp.RuloID
        //        //          where rp.DefinationProcessID == ruloFilters.numDefinitionProcess
        //        //          select r).ToList();

        //        result = (from r in result
        //                  join rp in resulRPAndDP on r.RuloID equals rp.RuloID
        //                  where rp.DefinationProcessID == ruloFilters.numDefinitionProcess
        //                  select r).ToList();
        //    }

        //    if (ruloFilters.numTestCategory != 0)
        //        result = result.Where(x => x.TestCategoryID == ruloFilters.numTestCategory).ToList();

        //    var sql = query.ToQueryString();

        //    return result.OrderByDescending(x => x.RuloID);
        //}

        //2023-02-16
        public override async Task<IEnumerable<VMRuloBatch>> GetGuvenInformation(IEnumerable<int> ruloIDs)
        {
            IEnumerable<VMRuloBatch> ruloBatch = null;
            if (ruloIDs != null && ruloIDs.Count() > 0)
                ruloBatch = await ruloBatchRepository.GetWithRawSql("stpGetBatchesFromGuven @p0", new[] { string.Join(",", ruloIDs) });
            else
                ruloBatch = new List<VMRuloBatch>();

            return ruloBatch;
        }

        public override decimal GetSumSamples(int ruloID)
        {
            var sumSamples = (from r in repository.GetQueryable()
                              join rp in ruloProcessRepository.GetQueryable() on r.RuloID equals rp.RuloID
                              join s in sampleRepository.GetQueryable() on rp.RuloProcessID equals s.RuloProcessID
                              where r.RuloID == ruloID
                              select s.Meter).Sum(x => x);

            return sumSamples;
        }

        public override async Task<IEnumerable<VMRulo>> GetRuloReportListFromFilters(VMRuloFilters ruloFilters)
        {
            IEnumerable<VMRulo> result = null;

            //Order in stored procedure
            string numLote = ruloFilters.numLote != 0 ? ruloFilters.numLote.ToString() : null;
            int? numBeam = ruloFilters.numBeam != 0 ? (int?)ruloFilters.numBeam : null;
            int? numLoom = ruloFilters.numLoom != 0 ? (int?)ruloFilters.numLoom : null;
            int? numPiece = ruloFilters.numPiece != 0 ? (int?)ruloFilters.numPiece : null;
            int? folioNumber = ruloFilters.FolioNumber != 0 ? (int?)ruloFilters.FolioNumber : null;
            string txtStyle = !string.IsNullOrWhiteSpace(ruloFilters.txtStyle) ? ruloFilters.txtStyle : null;
            int? numDefinitionProcess = ruloFilters.numDefinitionProcess != 0 ? (int?)ruloFilters.numDefinitionProcess : null;
            int? numTestCategory = ruloFilters.numTestCategory != 0 ? (int?)ruloFilters.numTestCategory : null;
            bool withBatches = ruloFilters.withBatches;
            int? numRuloID = ruloFilters.numRuloID;
            bool isAccountDate = ruloFilters.IsAccountingDate;
            bool isCount = false;
            int? pag = 0; //Si se va a obtener todos los registros pasar estos valores como 0
            int? tammPag = 0;

            object[] parameters = GetParameters(ruloFilters, numLote, numBeam, numLoom, numPiece, folioNumber, txtStyle, numDefinitionProcess, numTestCategory, withBatches, numRuloID, isAccountDate, isCount, pag, tammPag);

            int? time = Context.Database.GetCommandTimeout();
            Context.Database.SetCommandTimeout(0);
            result = await ruloReportRepository.GetWithRawSql("stpRuloReport @p0,@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9,@p10,@p11,@p12,@p13,@p14,@p15", parameters);
            Context.Database.SetCommandTimeout(time);
            return result;
        }

        public override async Task<IEnumerable<WarehouseStock>> GetWarehouseStock(VMRuloFilters ruloFilters)
        {
            try
            {
                object[] parameters = new object[] {
                ruloFilters.dtBegin.ToString("yyyy-MM-ddTHH:mm:ss"),
                ruloFilters.dtEnd.ToString("yyyy-MM-ddTHH:mm:ss"),
                };

                var result = Context.Set<WarehouseStock>().FromSqlRaw("stpGetWarehouseStock @p0, @p1", parameters);

                return result.ToList();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        //public override async Task<IEnumerable<VMRuloReport>> GetRuloReportListFromFilters2(VMRuloFilters ruloFilters)
        //{
        //    var query = repository.GetQueryable(x => !x.IsDeleted && ((x.CreatedDate <= ruloFilters.dtEnd && x.CreatedDate >= ruloFilters.dtBegin) || (x.CreatedDate <= ruloFilters.dtBegin && x.CreatedDate >= ruloFilters.dtEnd)));

        //    var rulos = (
        //        from r in query
        //        where
        //        (!(ruloFilters.numLote > 0) || r.Lote == ruloFilters.numLote.ToString()) &&
        //        (!(ruloFilters.numBeam > 0) || r.Beam == ruloFilters.numBeam) &&
        //        (!(ruloFilters.numLoom > 0) || r.Loom == ruloFilters.numLoom) &&
        //        (!(ruloFilters.numPiece > 0) || r.PieceCount == ruloFilters.numPiece) &&
        //        (!(ruloFilters.FolioNumber > 0) || r.FolioNumber == ruloFilters.FolioNumber) &&
        //         ((string.IsNullOrWhiteSpace(ruloFilters.txtStyle)) || r.Style.Contains(ruloFilters.txtStyle))

        //        select r

        //        ).AsNoTracking().ToList();

        //    var testResults = await testResultRepository.GetWhere(x => !x.IsDeleted);
        //    var testCategories = await testCategoryRepository.GetWhere(x => x.TestCategoryID != 0);
        //    var userList = await userRepository.GetWhere(x => !x.IsDeleted);
        //    var originList = VMOriginType.ToList();
        //    var definationsProcess = await definationProcessRepository.GetWhereWithNoTrack(x => !x.IsDeleted);

        //    var ruloProcessGroup = await ruloProcessRepository.GetWhereWithNoTrack(x => !x.IsDeleted && ((x.BeginningDate <= ruloFilters.dtEnd && x.BeginningDate >= ruloFilters.dtBegin) || (x.BeginningDate <= ruloFilters.dtBegin && x.BeginningDate >= ruloFilters.dtEnd)));
        //    IEnumerable<IGrouping<int, RuloProcess>> ruloProcessesListGroup = ruloProcessGroup.GroupBy(x => x.RuloID).ToList();
        //    List<RuloProcess> ruloProcessesList = new List<RuloProcess>();

        //    foreach (var key in ruloProcessesListGroup)
        //    {
        //        ruloProcessesList.Add(key.Where(x => !x.IsDeleted).MaxBy(x => x.RuloProcessID));
        //    }

        //    Get value rama
        //    List<RuloProcess> ruloProcessRamaList = new List<RuloProcess>();
        //    foreach (var key in ruloProcessesListGroup)
        //    {
        //        var temp = key.Where(x => x.DefinationProcessID == 6 || x.DefinationProcessID == 10).FirstOrDefault();
        //        if (temp != null)
        //            ruloProcessRamaList.Add(temp);
        //    }

        //    var resulRPAndDP = (from rp in ruloProcessesList
        //                        join dp in definationsProcess.ToList() on rp.DefinationProcessID equals dp.DefinationProcessID
        //                        select new
        //                        {
        //                            rp.RuloID,
        //                            rp.RuloProcessID,
        //                            dp.DefinationProcessID,
        //                            dp.ProcessCode,
        //                            dp.Name
        //                        }).ToList();


        //    var result = (from r in rulos
        //                  join rp in resulRPAndDP.ToList() on r.RuloID equals rp.RuloID
        //                  into ljRP
        //                  from subRP in ljRP.DefaultIfEmpty()

        //                  join rprama in ruloProcessRamaList on r.RuloID equals rprama.RuloID
        //                  into ljRPRAMA
        //                  from subRPRAMA in ljRPRAMA.DefaultIfEmpty()

        //                  join tr in testResults.ToList() on r.TestResultID equals tr.TestResultID
        //                  into ljTR
        //                  from subTR in ljTR.DefaultIfEmpty()
        //                  join tc in testCategories.ToList() on subTR?.TestCategoryID equals tc.TestCategoryID
        //                  into ljTC
        //                  from subTC in ljTC.DefaultIfEmpty()
        //                  join uResultAutho in userList on r.TestResultAuthorizer equals uResultAutho.UserID
        //                  into ljuResultAutho
        //                  from subuResultAutho in ljuResultAutho.DefaultIfEmpty()
        //                  join uCreator in userList on r.CreatorID equals uCreator.UserID
        //                  into ljuCreator
        //                  from subuCreator in ljuCreator.DefaultIfEmpty()
        //                  join o in originList on r.OriginID equals o.Value
        //                  into ljO
        //                  from subO in ljO.DefaultIfEmpty()
        //                  select new VMRuloReport
        //                  {
        //                      RuloID = r.RuloID,
        //                      Lote = r.Lote,
        //                      Beam = r.Beam,
        //                      BeamStop = r.BeamStop,
        //                      Loom = r.Loom,
        //                      IsToyota = r.IsToyota ? "Yes" : "No",
        //                      PieceCount = r.PieceCount,
        //                      Style = r.Style,
        //                      StyleName = r.StyleName,
        //                      Width = r.Width,
        //                      WeavingLength = r.WeavingLength,
        //                      EntranceLength = r.EntranceLength,
        //                      ExitLengthRama = subRPRAMA?.FinishMeter ?? 0,
        //                      ExitLength = r.ExitLength,
        //                      Shift = r.Shift,
        //                      FolioNumber = r.FolioNumber,
        //                      RuloObservations = r.Observations,
        //                      SentDate = r.SentDate,
        //                      Sender = r.Sender?.ToString(),
        //                      SentAuthorizer = r.SentAuthorizer?.ToString(),
        //                      IsWaitingAnswerFromTest = r.IsWaitingAnswerFromTest ? "Yes" : "No",
        //                      CanContinue = subTR?.CanContinue == true ? "Yes" : "No",
        //                      TestResultObservations = subTR?.Details,
        //                      TestCategoryID = subTC?.TestCategoryID ?? 0,
        //                      TestCategoryCode = subTC?.TestCode ?? string.Empty,
        //                      DateTestResult = subTR?.CreatedDate,
        //                      BatchNumbers = null,
        //                      Origin = subO?.Text,
        //                      TestResultAuthorizer = subuResultAutho?.UserName ?? string.Empty,
        //                      Creator = subuCreator?.UserName ?? string.Empty,
        //                      CreatedDate = r.CreatedDate,
        //                      Machine = subRP?.Name
        //                  }).ToList();

        //    if (ruloFilters.numDefinitionProcess != 0)
        //    {
        //        var ruloProcess = ruloProcessRepository.GetQueryable(x => !x.IsDeleted && ((x.BeginningDate <= ruloFilters.dtEnd && x.BeginningDate >= ruloFilters.dtBegin) || (x.BeginningDate <= ruloFilters.dtBegin && x.BeginningDate >= ruloFilters.dtEnd)) && x.EndDate == null && x.DefinationProcessID == ruloFilters.numDefinitionProcess).ToList();

        //        result = (from r in result
        //                  join rp in ruloProcess on r.RuloID equals rp.RuloID
        //                  where rp.DefinationProcessID == ruloFilters.numDefinitionProcess
        //                  select r).ToList();
        //    }

        //    if (ruloFilters.numTestCategory != 0)
        //        result = result.Where(x => x.TestCategoryID == ruloFilters.numTestCategory).ToList();

        //    var sql = query.ToQueryString();

        //    2023 - 02 - 16: This code was comented because the process is very slow when it is getting data from Guven database
        //    var ruloBatch = await GetGuvenInformation(result.Select(x => x.RuloID));

        //    result.ForEach(x =>
        //    {
        //        x.BatchNumbers = string.Join(",", ruloBatch.Where(y => y.RuloID == x.RuloID).Select(y => y.BatchNumbers));
        //    });

        //    return result;
        //}

        public override async Task DeleteRuloProcessFromRuloProcessID(int RuloProcessID, int deleterRef)
        {
            await ruloProcessRepository.Remove(RuloProcessID, deleterRef);
        }

        public async override Task<Rulo> GetRuloFromFolio(int folioNumber)
        {
            return await repository.FirstOrDefault(x => !x.IsDeleted && x.FolioNumber == folioNumber);
        }

        public async override Task<IEnumerable<VMRulo>> GetAllVMRuloReportList(string query, params object[] parameters)
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

        public async override Task<IEnumerable<TblCustomReport>> GetFinishedFabricReport(VMReportFilter reportFilter)
        {
            string query = string.Empty;

            //Order in stored procedure
            var txtStyle = string.IsNullOrWhiteSpace(reportFilter.txtStyle) ? reportFilter.txtStyle : null;
            var numLote = reportFilter.numLote != 0 ? (int?)reportFilter.numLote : null;
            var numBeam = reportFilter.numBeam != 0 ? (int?)reportFilter.numBeam : null;
            var numLoom = reportFilter.numLoom != 0 ? (int?)reportFilter.numLoom : null;
            var shift = reportFilter.shift != 0 ? (int?)reportFilter.shift : null;

            object[] parameters = null;
            switch (reportFilter.typeReport)
            {
                case 1:
                    //Este es el reporte que indicó Alfredo que se modificara
                    query = "spFinishedFabricReport @p0,@p1,@p2,@p3,@p4,@p5,@p6"; //Antrior: spGetProcessesCompletedReport

                    parameters = new object[] {
                    reportFilter.dtBegin.ToString("yyyy-MM-dd HH:mm:ss"),
                    reportFilter.dtEnd.ToString("yyyy-MM-dd HH:mm:ss"),
                    txtStyle,
                    numLote,
                    numBeam,
                    numLoom,
                    shift
                    };

                    break;
                case 2:

                    break;
                default:
                    break;
            }

            var ruloReportList = await customReportRepository.GetWithRawSql(query, parameters);

            return ruloReportList;
        }

        public async override Task<string> GetMachineByRuloId(int ruloId)
        {
            var ruloProcess = await ruloProcessRepository.GetWhere(o => !o.IsDeleted && o.RuloID == ruloId);
            var definationResult = await definationProcessRepository.GetWhere(x => !x.IsDeleted);

            var result = (from rp in ruloProcess.ToList()
                          join dp in definationResult.ToList() on rp.DefinationProcessID equals dp.DefinationProcessID
                          where !rp.IsDeleted
                          orderby rp.LastUpdateDate descending
                          select dp.Name
                          ).FirstOrDefault();

            return result ?? string.Empty;
        }

    }
}
