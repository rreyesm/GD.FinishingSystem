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
    public class DefinationProcessManager : AbstractDefinationProcessService
    {
        private IAsyncRepository<DefinationProcess> repository = null;
        public DefinationProcessManager(DbContext context)
        {
            this.repository = new GenericRepository<DefinationProcess>(context);
        }
        public override async Task Add(DefinationProcess DefinationProcessInformation, int adderRef)
        {
            await repository.Add(DefinationProcessInformation, adderRef);
        }

        public override async Task Delete(DefinationProcess DefinationProcessInformation, int deleterRef)
        {
            await repository.Remove(DefinationProcessInformation.DefinationProcessID, deleterRef);
        }

        public override async Task<DefinationProcess> GetDefinationProcessFromDefinationProcessID(int DefinationProcessID)
        {
            var result = await repository.GetByPrimaryKey(DefinationProcessID);
            return result;
        }

        public override async Task<IEnumerable<DefinationProcess>> GetDefinationProcessList()
        {
            var result = await repository.GetWhere(o => !o.IsDeleted);
            return result;
        }

        public override async Task<IEnumerable<DefinationProcess>> GetDefinationProcessListFromBetweenDate(DateTime begin, DateTime end)
        {
            var result = await repository.GetWhere(o => !o.IsDeleted && (o.CreatedDate <= end && o.CreatedDate >= begin) || (o.CreatedDate <= begin && o.CreatedDate >= end));
            return result;
        }

        public override async Task Update(DefinationProcess DefinationProcessInformation, int updaterRef)
        {
            await repository.Update(DefinationProcessInformation, updaterRef);
        }
    }
}
