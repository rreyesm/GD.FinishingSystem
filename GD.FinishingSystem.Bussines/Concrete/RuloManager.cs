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
        private IAsyncRepository<Rulo> repository = null;
        private IAsyncRepository<RuloProcess> ruloProcessRepository = null;
        private IAsyncRepository<DefinationProcess> definationProcess = null;
        public RuloManager(DbContext context)
        {
            this.repository = new GenericRepository<Rulo>(context);
            this.ruloProcessRepository = new GenericRepository<RuloProcess>(context);
            this.definationProcess = new GenericRepository<DefinationProcess>(context);
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

        public override async Task<IEnumerable<Rulo>> GetRuloList()
        {
            var result = await repository.GetWhere(o => !o.IsDeleted);
            return result;
        }

        public override async Task<IEnumerable<Rulo>> GetRuloListFromBetweenDate(DateTime begin, DateTime end)
        {
            var result = await repository.GetWhere(o => !o.IsDeleted && (o.CreatedDate <= end && o.CreatedDate >= begin) || (o.CreatedDate <= begin && o.CreatedDate >= end));
            return result;
        }

        public override async Task<IEnumerable<RuloProcess>> GetRuloProcessesFromRuloID(int RuloID)
        {
            var result = await ruloProcessRepository.GetWhere(o => !o.IsDeleted && o.RuloID == RuloID);

            var definationResult = await definationProcess.GetAll();
            foreach (var item in result)
                item.DefinationProcess = definationResult.Where(x => x.DefinationProcessID == item.DefinationProcessID).FirstOrDefault();

            return result;
        }

        public override async Task<IEnumerable<RuloProcess>> GetRuloProcessListFromBetweenDate(DateTime begin, DateTime end)
        {
            var result = await ruloProcessRepository.GetWhere(o => !o.IsDeleted && (o.CreatedDate <= end && o.CreatedDate >= begin) || (o.CreatedDate <= begin && o.CreatedDate >= end));

            var definationResult = await definationProcess.GetAll();
            foreach (var item in result)
                item.DefinationProcess = definationResult.Where(x => x.DefinationProcessID == item.DefinationProcessID).FirstOrDefault();

            return result;
        }

        public override async Task<RuloProcess> GetRuloProcessFromRuloProcessID(int RuloProcessID)
        {
            var result = await ruloProcessRepository.GetByPrimaryKey(RuloProcessID);

            var definationResult = await definationProcess.GetByPrimaryKey(result.DefinationProcessID);
            result.DefinationProcess = definationResult;

            return result;
        }

        public override async Task SetTestResult(int RuloID, int TestResultID, int? authorizer, int setter)
        {
            var result = await repository.GetByPrimaryKey(RuloID);
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

        public override async Task<IEnumerable<Rulo>> GetRuloListFromFilters(VMRuloFilters ruloFilters)
        {
            var query = repository.GetQueryable(x => !x.IsDeleted && (x.CreatedDate <= ruloFilters.dtEnd && x.CreatedDate >= ruloFilters.dtBegin) || (x.CreatedDate <= ruloFilters.dtBegin && x.CreatedDate >= ruloFilters.dtEnd));

            if (ruloFilters.numLote != 0)
                query = query.Where(x => x.Lote.Contains(ruloFilters.numLote.ToString()));
            if (ruloFilters.numBeam != 0)
                query = query.Where(x => x.Beam == ruloFilters.numBeam);
            if (ruloFilters.numLoom != 0)
                query = query.Where(x => x.Loom == ruloFilters.numLoom);
            if (ruloFilters.numPiece != 0)
                query = query.Where(x => x.Piece == ruloFilters.numPiece);
            if (!string.IsNullOrWhiteSpace(ruloFilters.txtStyle))
                query = query.Where(x => x.Style.Contains(ruloFilters.txtStyle));

            var result = await query.ToListAsync();

            var sql = query.ToQueryString();

            return result;
        }
    }
}
