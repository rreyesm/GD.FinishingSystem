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
    public class RuloManager : AbstractRuloService
    {
        private IAsyncRepository<Rulo> repository = null;
        private IAsyncRepository<RuloProcess> ruloProcessRepository = null;
        public RuloManager(DbContext context)
        {
            this.repository = new GenericRepository<Rulo>(context);
            this.ruloProcessRepository = new GenericRepository<RuloProcess>(context);
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
            return result;
        }

        public override async Task<RuloProcess> GetRuloProcessFromRuloProcessID(int RuloProcessID)
        {
            var result = await ruloProcessRepository.GetByPrimaryKey(RuloProcessID);
            return result;
        }

        public override async Task SetTestResult(int RuloID, int TestResultID, int setter)
        {
            var result = await repository.GetByPrimaryKey(RuloID);
            result.TestResultID = TestResultID;
            result.TestResultAuthorizer = setter;
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
    }
}
