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
    public class FloorManager : AbstractFloorService
    {
        private IAsyncRepository<Floor> repository = null;
        public FloorManager(DbContext context)
        {
            this.repository = new GenericRepository<Floor>(context);
        }
        public override async Task Add(Floor floorInformation)
        {
            await repository.Add(floorInformation, 0);
        }

        public override async Task Delete(Floor floorInformation)
        {
            await repository.Remove(floorInformation.FloorID, 0);
        }

        public override async Task<Floor> GetRuloFromRuloID(int floorID)
        {
           var result = await repository.GetByPrimaryKey(floorID);

            return result;
        }

        public override async Task<IEnumerable<Floor>> GetRuloList()
        {
            var result = await repository.GetWhere(x=>x.FloorID != 0);

            return result;
        }

        public override async Task Update(Floor floorInformation)
        {
            await repository.Update(floorInformation, 0);
        }
    }
}
